
using System;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Dialog;

using Foundation;
using UIKit;
using EventKit;
using donow.PCL.Model;
using donow.PCL;
using Xamarin;

namespace donow.iOS
{
	public partial class CalenderHomeDVC : DialogViewController
	{		

		protected RootElement calendarListRoot = new RootElement ("Calendar Events and Reminders");
		// screens
		protected CalenderEventVC calendarListScreen;
		protected EventListDVC eventListScreen;
		// event controller delegate
		protected CreateEventEditViewDelegate eventControllerDelegate;

		public override void ViewWillAppear (bool animated)
		{
			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
		}

		public CalenderHomeDVC () : base (UITableViewStyle.Grouped, null)
		{
			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("Navigation_Back_Icon.png"),UIBarButtonItemStyle.Plain, (sender,args) => {
					this.NavigationController.PopViewController(true);
				})
				, true);
			
			// build out our table using MT.D
			Root = calendarListRoot;
			// add our calendar lists items
			Root.Add (new Section ("Calendar Lists") {
				new StyledStringElement ("Calendar",
					() => {
						LaunchCalendarListScreen (EKEntityType.Event);
					})
				{ Accessory = UITableViewCellAccessory.DisclosureIndicator },
				new StyledStringElement ("Reminders",
					() => {
						LaunchCalendarListScreen (EKEntityType.Reminder);
					})
				{ Accessory = UITableViewCellAccessory.DisclosureIndicator }
			});
			// events
			Root.Add (new Section ("Events") {
				new StyledStringElement ("Add New Event",
					() => {
						RequestAccess (EKEntityType.Event, () => {
							LaunchCreateNewEvent ();
						});
					})//,
//				new StyledStringElement ("Modify Event",
//					() => {
//						RequestAccess (EKEntityType.Event, () => {
//							LaunchModifyEvent ();
//						});
//					})//,
//				new StyledStringElement ("Save and Retrieve Event",
//					() => {
//						RequestAccess (EKEntityType.Event, () => {
//							SaveAndRetrieveEvent ();
//						});
//					}),
//				new StyledStringElement ("Get Events via Query",
//					() => {
//						RequestAccess (EKEntityType.Event, () => {
//							GetEventsViaQuery ();
//						});
//					})
			});
			// reminders
			Root.Add (new Section ("Reminders") {
				new StyledStringElement ("Create a Reminder",
					() => {
						RequestAccess (EKEntityType.Reminder, () => {
							CreateReminder ();
						});
					}),
				new StyledStringElement ("Get Reminders via Query",
					() => {
						RequestAccess (EKEntityType.Reminder, () => {
							GetRemindersViaQuery ();
						});
					})
			});
		}

		protected void LaunchCalendarListScreen (EKEntityType calendarStore)
		{
			calendarListScreen = new CalenderEventVC(calendarStore);
			NavigationController.PushViewController (calendarListScreen, true);
			//PresentViewController(calendarListScreen, true,null);
		}

		/// <summary>
		/// A convenience method that requests access to the appropriate calendar and
		/// shows an alert if access is not granted, otherwise executes the completion
		/// method.
		/// </summary>
		protected void RequestAccess (EKEntityType type, Action completion)
		{
			AppDelegate.EventStore.RequestAccess (type,
				(bool granted, NSError e) => {
					InvokeOnMainThread (() => {
						if (granted)
							completion.Invoke ();
						else
							new UIAlertView ("Access Denied", "User Denied Access to Calendars/Reminders", null, "ok", null).Show ();
					});
				});
		}

		/// <summary>
		/// Launchs the create new event controller.
		/// </summary>
		protected void LaunchCreateNewEvent ()
		{
			// create a new EKEventEditViewController. This controller is built in an allows
			// the user to create a new, or edit an existing event.
			EventKitUI.EKEventEditViewController eventController =
				new EventKitUI.EKEventEditViewController ();

			// set the controller's event store - it needs to know where/how to save the event
			eventController.EventStore = AppDelegate.EventStore;

			// wire up a delegate to handle events from the controller
			eventControllerDelegate = new CreateEventEditViewDelegate (eventController);
			eventController.EditViewDelegate = eventControllerDelegate;

			// show the event controller
			PresentViewController (eventController, true, null);
			//NavigationController.PushViewController (calendarListScreen, true);

		}

		/// <summary>
		/// Launchs the create new event controller.
		/// </summary>
		protected void LaunchModifyEvent ()
		{
			// first we need to create an event it so we have one that we know exists
			// in a real world scenario, we'd likely either a) be modifying an event that
			// we found via a query, or 2) we'd do like this, in which we'd automatically
			// populate the event data, like for say a dr. appt. reminder, or something
			EKEvent newEvent = EKEvent.FromStore (AppDelegate.EventStore);
			// set the alarm for 10 minutes from now
			newEvent.AddAlarm (EKAlarm.FromDate ((NSDate)DateTime.Now.AddMinutes (10)));
			// make the event start 20 minutes from now and last 30 minutes
			newEvent.StartDate = (NSDate)DateTime.Now.AddMinutes (20);
			newEvent.EndDate = (NSDate)DateTime.Now.AddMinutes (50);
			newEvent.Title = "Get outside and do some exercise!";
			newEvent.Notes = "This is your motivational event to go and do 30 minutes of exercise. Super important. Do this.";

			// create a new EKEventEditViewController. This controller is built in an allows
			// the user to create a new, or edit an existing event.
			EventKitUI.EKEventEditViewController eventController =
				new EventKitUI.EKEventEditViewController ();

			// set the controller's event store - it needs to know where/how to save the event
			eventController.EventStore = AppDelegate.EventStore;
			eventController.Event = newEvent;

			// wire up a delegate to handle events from the controller
			eventControllerDelegate = new CreateEventEditViewDelegate (eventController);
			eventController.EditViewDelegate = eventControllerDelegate;

			// show the event controller
			PresentViewController (eventController, true, null);
		}

		// our delegate for the create new event controller.
		protected class CreateEventEditViewDelegate : EventKitUI.EKEventEditViewDelegate
		{
			// we need to keep a reference to the controller so we can dismiss it
			protected EventKitUI.EKEventEditViewController eventController;

			public CreateEventEditViewDelegate (EventKitUI.EKEventEditViewController eventController)
			{
				// save our controller reference
				this.eventController = eventController;

			}

			void AddEvent(EKEvent calendarEvent)
			{
				UserMeetings userMeetings = new UserMeetings ();
				if (!AppDelegate.IsFromRR) {
					userMeetings.Id = 0;
					userMeetings.LeadId = AppDelegate.CurrentLead.LEAD_ID;
					userMeetings.UserId = AppDelegate.UserDetails.UserId;
					userMeetings.Subject = calendarEvent.Title;
					userMeetings.StartDate = DateTime.SpecifyKind(DateTime.Parse(calendarEvent.StartDate.ToString()),DateTimeKind.Local).ToString();
					userMeetings.EndDate = DateTime.SpecifyKind(DateTime.Parse(calendarEvent.EndDate.ToString()),DateTimeKind.Local).ToString();
					userMeetings.CustomerName = AppDelegate.CurrentLead.LEAD_NAME;
					userMeetings.City = AppDelegate.CurrentLead.CITY;
					userMeetings.State = AppDelegate.CurrentLead.STATE;
					userMeetings.Status = "";
				} else {
					userMeetings.Id = 0;
					userMeetings.LeadId = (int) AppDelegate.CurrentRR.LeadID;
					userMeetings.UserId = AppDelegate.CurrentRR.SellerUserID;
					userMeetings.Subject = calendarEvent.Title;
					userMeetings.StartDate = DateTime.SpecifyKind(DateTime.Parse(calendarEvent.StartDate.ToString()),DateTimeKind.Local).ToString();
					userMeetings.EndDate = DateTime.SpecifyKind(DateTime.Parse(calendarEvent.EndDate.ToString()),DateTimeKind.Local).ToString();
					userMeetings.CustomerName = AppDelegate.CurrentRR.Prospect;
					userMeetings.City = AppDelegate.CurrentRR.City;
					userMeetings.State = AppDelegate.CurrentRR.State;
					userMeetings.Status = "";
					
				}
				AppDelegate.leadsBL.SaveMeetingEvent (userMeetings);
				//Xamarin Insights tracking
				Insights.Track ("SaveMeetingEvent", new Dictionary <string,string> {
					{ "LeadId", userMeetings.LeadId.ToString () },
					{ "UserId", userMeetings.UserId.ToString () },
					{ "Subject", userMeetings.Subject },
					{ "CustomerName", userMeetings.CustomerName }
				});
//				var notification = new UILocalNotification();
//
//				notification.AlertBody = AppDelegate.CurrentLead.LEAD_ID.ToString();
//				// set the fire date (the date time in which it will fire)
//				notification.FireDate = NSDate.FromTimeIntervalSinceNow(100);
//				// configure the alert
////				notification.AlertAction = "View Alert";
////				notification.AlertBody = "Your one minute alert has fired!";
//
//				// modify the badge
//				notification.ApplicationIconBadgeNumber = 1;
//
//				// set the sound to be the default sound
//				notification.SoundName = UILocalNotification.DefaultSoundName;
//
//				// schedule it
//				UIApplication.SharedApplication.ScheduleLocalNotification(notification);

			}


			// completed is called when a user eith
			public override void Completed (EventKitUI.EKEventEditViewController controller, EventKitUI.EKEventEditViewAction action)
			{
				AddEvent(controller.Event);
				eventController.DismissViewController (true, null);

				// action tells you what the user did in the dialog, so you can optionally
				// do things based on what their action was. additionally, you can get the
				// Event from the controller.Event property, so for instance, you could
				// modify the event and then resave if you'd like.
				switch (action) {

				case EventKitUI.EKEventEditViewAction.Canceled:
					break;
				case EventKitUI.EKEventEditViewAction.Deleted:
					break;
				case EventKitUI.EKEventEditViewAction.Saved:
					// if you wanted to modify the event you could do so here, and then
					// save:
					//AppDelegate.EventStore.SaveEvent ( controller.Event, )
					break;
				}
			}
		}

		/// <summary>
		/// This method illustrates how to save and retrieve events programmatically.
		/// </summary>
		protected void SaveAndRetrieveEvent ()
		{
			EKEvent newEvent = EKEvent.FromStore (AppDelegate.EventStore);
			// set the alarm for 5 minutes from now
			newEvent.AddAlarm (EKAlarm.FromDate ((NSDate)DateTime.Now.AddMinutes (5)));
			// make the event start 10 minutes from now and last 30 minutes
			newEvent.StartDate = (NSDate)DateTime.Now.AddMinutes (10);
			newEvent.EndDate = (NSDate)DateTime.Now.AddMinutes (40);
			newEvent.Title = "Appt. to do something Awesome!";
			newEvent.Notes = "Find a boulder, climb it. Find a river, swim it. Find an ocean, dive it.";
			newEvent.Calendar = AppDelegate.EventStore.DefaultCalendarForNewEvents;

			// save the event
			NSError e;
			AppDelegate.EventStore.SaveEvent (newEvent, EKSpan.ThisEvent, out e);
			if (e != null) {
				new UIAlertView ("Err Saving Event", e.ToString (), null, "ok", null).Show ();
				return;
			} else {
				new UIAlertView ("Event Saved", "Event ID: " + newEvent.EventIdentifier, null, "ok", null).Show ();
				Console.WriteLine ("Event Saved, ID: " + newEvent.EventIdentifier);
			}

			// to retrieve the event you can call
			EKEvent mySavedEvent = AppDelegate.EventStore.EventFromIdentifier (newEvent.EventIdentifier);
			Console.WriteLine ("Retrieved Saved Event: " + mySavedEvent.Title);

			// to delete, note that once you remove the event, the reference will be null, so
			// if you try to access it you'll get a null reference error.
			AppDelegate.EventStore.RemoveEvent (mySavedEvent, EKSpan.ThisEvent, true, out e);
			Console.WriteLine ("Event Deleted.");

		}

		/// <summary>
		/// This method retrieves all the events for the past week via a query and displays them
		/// on the EventList Screen.
		/// </summary>
		protected void GetEventsViaQuery ()
		{
			// create our NSPredicate which we'll use for the query
			var startDate = (NSDate)DateTime.Now.AddDays (-7);
			var endDate = (NSDate)DateTime.Now;
			// the third parameter is calendars we want to look in, to use all calendars, we pass null
			NSPredicate query = AppDelegate.EventStore.PredicateForEvents (startDate, endDate, null);

			// execute the query
			EKCalendarItem[] events = AppDelegate.EventStore.EventsMatching (query);

			// create a new event list screen with these events and show it
			eventListScreen = new EventListDVC (events, EKEntityType.Event);
			//NavigationController.PushViewController (eventListScreen, true);
			PresentViewController(eventListScreen, true,null);
		}

		/// <summary>
		/// Creates and saves a reminder to the default reminder calendar
		/// </summary>
		protected void CreateReminder ()
		{
			// create a reminder using the EKReminder.Create method
			EKReminder reminder = EKReminder.Create (AppDelegate.EventStore);
			reminder.Title = "Do something awesome!";
			reminder.Calendar = AppDelegate.EventStore.DefaultCalendarForNewReminders;

			// save the reminder
			NSError e;
			AppDelegate.EventStore.SaveReminder (reminder, true, out e);
			// if there was an error, show it
			if (e != null) {
				new UIAlertView ("err saving reminder", e.ToString (), null, "ok", null).Show ();
				return;
			} else {
				new UIAlertView ("reminder saved", "ID: " + reminder.CalendarItemIdentifier, null, "ok", null).Show ();

				// to retrieve the reminder you can call GetCalendarItem
				EKCalendarItem myReminder = AppDelegate.EventStore.GetCalendarItem (reminder.CalendarItemIdentifier);
				Console.WriteLine ("Retrieved Saved Reminder: " + myReminder.Title);

				// to delete, note that once you remove the event, the reference will be null, so
				// if you try to access it you'll get a null reference error.
				AppDelegate.EventStore.RemoveReminder (myReminder as EKReminder, true, out e);
				Console.WriteLine ("Reminder Deleted.");
			}
		}

		/// <summary>
		/// This method retrieves all the events for the past week via a query and displays them
		/// on the EventList Screen.
		/// </summary>
		protected void GetRemindersViaQuery ()
		{
			// create our NSPredicate which we'll use for the query
			NSPredicate query = AppDelegate.EventStore.PredicateForReminders (null);

			// execute the query
			AppDelegate.EventStore.FetchReminders (
				query, ( EKReminder[] items) => {
					// since this is happening in a completion callback, we have to update
					// on the main thread
					InvokeOnMainThread (() => {
						// create a new event list screen with these events and show it
						eventListScreen = new EventListDVC (items, EKEntityType.Reminder);
						//NavigationController.PushViewController (eventListScreen, true);
						PresentViewController(eventListScreen, true,null);
					});
				});
		}
	}
}

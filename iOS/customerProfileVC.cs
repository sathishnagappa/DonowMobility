using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UIKit;
using donow.PCL.Model;
using donow.PCL;
using CoreGraphics;
using donow.Services;
using MessageUI;
using System.Linq;
using Xamarin;
using System.Threading.Tasks;
using donow.Util;
using EventKit;


namespace donow.iOS

{
	partial class customerProfileVC : UIViewController
	{
		public customerProfileVC (IntPtr handle) : base (handle)
		{
		}
		protected CreateEventEditViewDelegate eventControllerDelegate;
		LoadingOverlay loadingOverlay;
		public Customer customer;
		CustomerDetails customerDetails;
		public bool TableSeeAllClicked = false;

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			LoadScreenData ();
			ButtonDealMakerUsed.Hidden = true;
			ImageMore.Hidden = false;
			//customerDetails.dealMaker != null 
			if (customerDetails.dealMaker != null) {
				LabelDealMakerUsed.Text = customerDetails.dealMaker.BrokerName;
				ButtonDealMakerUsed.Hidden = false;
				ImageMore.Hidden = false;
			}
			else
				LabelDealMakerUsed.Text = "NA";

			List<BingResult> bingResult = AppDelegate.customerBL.GetBingResult (customerDetails.Company + " News");
			TableViewLatestNews.Source = new CustomerIndustryTableSource(bingResult, this);
		    
			await LoadCustomerAndMeetingInfo ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			TableViewEmails.Source = null;
			TableViewDealHistory.Source = null;
			TableViewMeetings.Source = null;
			TableViewPreviousMeetings.Source = null;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{				
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			this.Title = "Customer Profile";

			loadingOverlay = new LoadingOverlay(this.View.Bounds);
			this.View.Add(loadingOverlay);

//			LoadScreenData ();
//			ButtonDealMakerUsed.Hidden = true;
//			//customerDetails.dealMaker != null 
//			if (customerDetails.dealMaker != null) {
//				LabelDealMakerUsed.Text = customerDetails.dealMaker.BrokerName;
//				ButtonDealMakerUsed.Hidden = false;
//			}
			//DealMakersImage1.Hidden = customerDetails.dealMaker != null ? false : true;
			//Add code to navigate to dealmaker details 
			ButtonDealMakerUsed.TouchUpInside += (object sender, EventArgs e) => {
				MyDealMakerDetailVC dealmakerDetailObject = this.Storyboard.InstantiateViewController ("MyDealMakerDetailVC") as MyDealMakerDetailVC;
				if (dealmakerDetailObject != null) { 
					dealmakerDetailObject.brokerObj = customerDetails.dealMaker;
					this.NavigationController.PushViewController (dealmakerDetailObject, true);
				}
			};
			
			ButtonPhone.TouchUpInside += (object sender, EventArgs e) => {
				var phone = string.IsNullOrEmpty(customerDetails.Phone.Trim()) == true ? "0" : customerDetails.Phone;
				var url = new NSUrl ("tel://" + phone);
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				};
				CustomerInteraction customerinteract = new CustomerInteraction();
				customerinteract.CustomerName =  customerDetails.Name;
				customerinteract.UserId = AppDelegate.UserDetails.UserId;
				customerinteract.Type = "Phone";
				customerinteract.DateNTime = DateTime.Now.ToString();
				customerinteract.LeadID = customerDetails.LeadId;
				AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
				//Xamarin Insights tracking
				Insights.Track("SaveCutomerInteraction", new Dictionary <string,string>{
					{"UserId", customerinteract.UserId.ToString()},
					{"CustomerName", customerinteract.CustomerName}
				});
			};

			ButtonMail.TouchUpInside += (object sender, EventArgs e) => {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{customerDetails.Email});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("", false);
					this.PresentViewController (mailController, true, null);
					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						switch(args.Result)
						{
								case MFMailComposeResult.Cancelled: 							
									break;
								case MFMailComposeResult.Saved: 
									break;
								case MFMailComposeResult.Sent: 	
									CustomerInteraction customerinteract = new CustomerInteraction();
									customerinteract.CustomerName =  customerDetails.Name;
									customerinteract.UserId = AppDelegate.UserDetails.UserId;
									customerinteract.Type = "Email";
									customerinteract.DateNTime = DateTime.Now.ToString();
									customerinteract.LeadID = customerDetails.LeadId;
									AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);				

									// Xamarin Insights tracking
									Insights.Track("Save CutomerInteraction", new Dictionary <string,string>{
										{"UserId", customerinteract.UserId.ToString()},
										{"CustomerName", customerinteract.CustomerName},
										{"Type", "Email"}
									});
									break;
								case MFMailComposeResult.Failed: 
									break;
						}
						args.Controller.DismissViewController (true, null);
					};


				}
			};

			ButtonCalendarEvent.TouchUpInside += (object sender, EventArgs e) => {
//				CalenderHomeDVC calendarHomeDV = new CalenderHomeDVC ();
//				this.NavigationController.PushViewController(calendarHomeDV, true);
				LaunchCreateNewEvent();
			};
			//await LoadCustomerAndMeetingInfo ();
		}

		async Task LoadCustomerAndMeetingInfo () {

//			List<BingResult> bingResult = AppDelegate.customerBL.GetBingResult (customerDetails.Name + " News");
//			TableViewLatestNews.Source = new CustomerIndustryTableSource(bingResult, this);

			string[] customerNameArray = customerDetails.Company.Split ();
			//string searchText = customerNameArray [0].Length == 1 ? customerNameArray [1] : customerNameArray [0];
			string searchText = customerNameArray.Count() == 1 ? customerNameArray [1] : (customerNameArray [0] + " " + customerNameArray [1]);
			//List<TwitterStream>  twitterStream =  await TwitterUtil.Search (searchText.ToLower());
			List<TwitterStream>  twitterStream =  await TwitterUtil.Search (searchText.ToLower());
			List<TwitterStream> twitterStreamwithKeyword = new List<TwitterStream>();
			if(twitterStream.Count > 0)
//				twitterStreamwithKeyword =	twitterStream.Where(X => X.text.Contains("Business") || X.text.Contains("Sales") || X.text.Contains("Opportunities")
//					|| X.text.Contains("Organization") || X.text.Contains("Launch") || X.text.Contains("Money") || X.text.Contains("Tools") || X.text.Contains("Competition")
//					|| X.text.Contains("Interest") || X.text.Contains("Industry") || X.text.Contains("Learning")).ToList();	
				  twitterStreamwithKeyword = twitterStream;	

			TableViewLatestCustomerInfo.Source = new CustomerInfoTableSource(twitterStreamwithKeyword,this);
			TableViewLatestCustomerInfo.ReloadData ();
			loadingOverlay.Hide();
		}

		static UIImage FromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data); 
		}

		void LoadScreenData()
		{
			customerDetails = AppDelegate.customerBL.GetCustomersDetails(customer.Name,AppDelegate.UserDetails.UserId,customer.Lead_Source);
			//customerDetails = AppDelegate.customerBL.GetCustomersDetails(customer.Name,AppDelegate.UserDetails.UserId);

			List<UserMeetings> listMeeting = customerDetails.UserMeetingList;
			List<UserMeetings> UCommingMeetinglist = new List<UserMeetings>();
			List<UserMeetings> PreviousMeetingsList = new List<UserMeetings>(); 

			foreach(var item in listMeeting)
			{
				if (DateTime.Compare (DateTime.Parse (item.EndDate), DateTime.Now) > 0) {
					UCommingMeetinglist.Add (item);
				} else {
					PreviousMeetingsList.Add (item);
				}
			}	

			ScrollViewCustomerProfile.ContentSize = new CGSize (375.0f, 2255.0f);

			if(customerDetails.customerInteractionList  != null && customerDetails.customerInteractionList.Count !=0)
				TableViewEmails.Source = new TableSourceInteractionWithCustomer (customerDetails.customerInteractionList , this);
			if(customerDetails.dealHistoryList != null && customerDetails.dealHistoryList.Count !=0)
				TableViewDealHistory.Source = new TableSourceDealHistory(customerDetails.dealHistoryList); 
			if(UCommingMeetinglist.Count != 0)
				TableViewMeetings.Source = new TableSourceupComingMeetings (UCommingMeetinglist,this);
			if(PreviousMeetingsList.Count !=0)
				TableViewPreviousMeetings.Source = new TableSourcePreviousMeetings (PreviousMeetingsList,this);

			LabelCompanyName.Text = customerDetails.Company;
			LabelCustomerName.Text = customerDetails.Name;
			LabelCityAndState.Text = customerDetails.City + ", " + customerDetails.State;
			LabelScore.Text = customerDetails.LeadSource == 2 ? (customerDetails.LeadScore == 0 ? "NA" : customerDetails.LeadScore.ToString()) : customerDetails.LeadScore.ToString(); 
			LabelSource.Text = customerDetails.LeadSource == 2 ? "SFDC" : "DoNow" ;
			if (!string.IsNullOrEmpty (customerDetails.LeadTitle))
				CustomerTitle.Text = "(" + customerDetails.LeadTitle + ")";
			else
				CustomerTitle.Text = "";

			ScrollViewCompanyInfo.ContentSize = new CGSize (this.View.Bounds.Size.Width, 600);

			LabelStreet.Text = EvaluateString (customerDetails.ADDRESS, customerDetails.COUNTY);
			LabelCityState.Text = EvaluateString (customerDetails.City, customerDetails.State);
			LabelZipCodeCountry.Text = EvaluateString (customerDetails.ZIPCODE, customerDetails.COUNTRY);
			LabelPhone.Text = "Tel: " + customerDetails.Phone; 

			LabelIndustry.Text = customerDetails.CompanyInfo;
			LabelFinancials.Text = "Revenue : " + evaluateAmount(customerDetails.REVENUE);
			LabelFiscalYear.Text = customerDetails.FISCALYE;
			LabelLOB.Text = customerDetails.BusinessNeeds;
			LabelNetIncome.Text = customerDetails.NETINCOME;
			LabelEmployees.Text = customerDetails.EMPLOYEES;
			LabelMarketValue.Text = evaluateAmount(customerDetails.MARKETVALUE);
			LabelYearFounded.Text = customerDetails.YEARFOUNDED;
			LabelIndustryRiskScore.Text = customerDetails.INDUSTRYRISK;
			LabelWebsite.Text = customerDetails.WebAddress;

			AppDelegate.CurrentLead = new Leads () { LEAD_ID = customerDetails.LeadId, LEAD_NAME = customerDetails.Name, 
				CITY = customerDetails.City, STATE = customerDetails.State , SFDCLEAD_ID = customerDetails.SFDCLEAD_ID
			};
		}

		string EvaluateString (string firstString, string secondString) {

			if (!string.IsNullOrEmpty(firstString) && !string.IsNullOrEmpty(secondString))
				return (firstString + ", " + secondString);
			else
				return (firstString + secondString);
		}

		string evaluateAmount (string firstString){
			if (string.IsNullOrEmpty(firstString) || firstString == "NA")
				return (firstString);
			else
				return ("$" + firstString + " M");
		}

		protected void LaunchCreateNewEvent ()
		{
			// create a new EKEventEditViewController. This controller is built in an allows
			// the user to create a new, or edit an existing event.
			AppDelegate.EventStore.RequestAccess (EKEntityType.Event, (bool granted, NSError e) => {

				EventKitUI.EKEventEditViewController eventController =
					new EventKitUI.EKEventEditViewController ();
				InvokeOnMainThread (() => { 
					//					EKEvent newEvent = EKEvent.FromStore (AppDelegate.EventStore);
					//					newEvent.Title = "Get outside and do some exercise!";
					//					newEvent.Notes = "This is your motivational event to go and do 30 minutes of exercise. Super important. Do this.";
					//					newEvent.Location = "Seattle,WA";
					// set the controller's event store - it needs to know where/how to save the event
					eventController.EventStore = AppDelegate.EventStore;
					//					eventController.Event = newEvent;
					// wire up a delegate to handle events from the controller
					eventControllerDelegate = new CreateEventEditViewDelegate (eventController);
					eventController.EditViewDelegate = eventControllerDelegate;

					// show the event controller
					PresentViewController (eventController, true, null);
				});
				//NavigationController.PushViewController (calendarListScreen, true);
			});

		}

		public class CreateEventEditViewDelegate : EventKitUI.EKEventEditViewDelegate
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
				userMeetings.Comments = "";
				userMeetings.SFDCLead_ID = AppDelegate.CurrentLead.SFDCLEAD_ID;

				AppDelegate.leadsBL.SaveMeetingEvent (userMeetings);
				AppDelegate.UserDetails.MeetingCount = AppDelegate.UserDetails.MeetingCount + 1;
				//Xamarin Insights tracking
				Insights.Track ("SaveMeetingEvent", new Dictionary <string,string> {
					{ "LeadId", userMeetings.LeadId.ToString () },
					{ "UserId", userMeetings.UserId.ToString () },
					{ "Subject", userMeetings.Subject },
					{ "CustomerName", userMeetings.CustomerName }
				});
			}

			// completed is called when a user eith
			public override void Completed (EventKitUI.EKEventEditViewController controller, EventKitUI.EKEventEditViewAction action)
			{				
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
					AddEvent(controller.Event);
					break;
				}
			}
		}

//		public class TableSourceBtwnYouNCustomer : UITableViewSource {
//
//			string CellIdentifier = "BtwnYouNCustomerTableCell";
////			List<Leads> TableItems;
//			customerProfileVC owner;
//
//			public TableSourceBtwnYouNCustomer (/*List<Leads> items, customerProfileVC owner*/)
//			{
////				TableItems = items;
//				this.owner = owner;
//			}
//
//			public override nint RowsInSection (UITableView tableview, nint section)
//			{
////				if(owner.TableSeeAllClicked == false) {
//					return 2;
////				} else {
////					return TableItems.Count;
////				}
//			}
//
//			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
//			{				
//				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellBtwnYouNCustomer;
//
//				if (cell == null) {
//					cell = new CustomerViewTableCellBtwnYouNCustomer (CellIdentifier);
//				}
//
////				Leads leadsObj = TableItems[indexPath.Row];
//				cell.UpdateCell();
//				return cell;
//			}
//
//			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
//			{
//
////				tableView.DeselectRow (indexPath, true);
////				LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
////				if (leadDetailVC != null) {
////					leadDetailVC.leadObj = TableItems[indexPath.Row];
////					//owner.View.AddSubview (leadDetailVC.View);
////					owner.NavigationController.PushViewController(leadDetailVC,true);
////				}
//			}
//
//			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
//			{
//				return 100.0f;
//			}
//		}

		public class TableSourceInteractionWithCustomer : UITableViewSource {

			string CellIdentifier = "InteractionWithCustomer";
			List<CustomerInteraction> TableItems;
			//customerProfileVC owner;

			public TableSourceInteractionWithCustomer (List<CustomerInteraction> items, customerProfileVC owner)
			{
				TableItems = items;
				//this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellinteractionWithCustomerCell;

				if (cell == null) {
					cell = new CustomerViewTableCellinteractionWithCustomerCell (CellIdentifier);
				}

//				Leads leadsObj = TableItems[indexPath.Row];
				cell.UpdateCell(TableItems[indexPath.Row]);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);

			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 80.0f;
			}
		}

		public class TableSourceupComingMeetings : UITableViewSource {
			string CellIdentifier = "upComingMeetingsCell";
			List<UserMeetings> TableItems;
			customerProfileVC owner;
				
			public TableSourceupComingMeetings (List<UserMeetings> items, customerProfileVC owner)
			{
				TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;

			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellUpcomingMeetings;

				if (cell == null) {
					cell = new CustomerViewTableCellUpcomingMeetings (CellIdentifier);
				}

				cell.UpdateCell(TableItems[indexPath.Row]);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				MyMeetingsVC myMeetingsObj = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				if (myMeetingsObj != null) {
					myMeetingsObj.meetingObj = TableItems[indexPath.Row];
					owner.NavigationController.PushViewController(myMeetingsObj,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 90.0f;
			}
		}

		public class TableSourcePreviousMeetings : UITableViewSource {
			string CellIdentifier = "PreviousMeetingsCell";
			List<UserMeetings> TableItems;
			customerProfileVC owner;

			public TableSourcePreviousMeetings (List<UserMeetings> items, customerProfileVC owner)
			{
				TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;

			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewPreviousMeetings;

				if (cell == null) {
					cell = new CustomerViewPreviousMeetings (CellIdentifier);
				}

				cell.UpdateCell(TableItems[indexPath.Row]);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				MyMeetingsVC myMeetingsObj = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				if (myMeetingsObj != null) {
					myMeetingsObj.meetingObj = TableItems[indexPath.Row];
					owner.NavigationController.PushViewController(myMeetingsObj,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 90.0f;
			}
		}

		public class TableSourceDealHistory : UITableViewSource {

			string CellIdentifier = "LeadsTableCell";
			List<DealHistroy> TableItems;

			public TableSourceDealHistory (List<DealHistroy> items)
			{
				TableItems = items;
				//this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewDealHistory;

				if (cell == null) {
					cell = new CustomerViewDealHistory (CellIdentifier);
				}


				cell.UpdateCell(TableItems[indexPath.Row]);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);

			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 110.0f;
			}
		}

		public class CustomerInfoTableSource : UITableViewSource {

			List<TwitterStream> TableItems;
			string CellIdentifier = "TableCellCusomerInfo";
			customerProfileVC owner;

			public CustomerInfoTableSource (List<TwitterStream> twitterstream,customerProfileVC customerProfileObj)
			{
				TableItems = twitterstream;
				this.owner = customerProfileObj;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				TwitterStream item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				//cell.ImageView.Frame = new CGRect (25, 5, 33, 33);
				cell.ImageView.Frame = new CGRect (25, 15, 40, 35);
				cell.ImageView.Image = UIImage.FromBundle("twitter_icon.png");
				cell.TextLabel.Text = item.text;
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{		

				tableView.DeselectRow (indexPath, true);
				if (!string.IsNullOrEmpty (TableItems [indexPath.Row].url)) {
					BingSearchVC bingSearchVC = owner.Storyboard.InstantiateViewController ("BingSearchVC") as BingSearchVC;
					if (bingSearchVC != null) {					
						bingSearchVC.webURL = TableItems [indexPath.Row].url;
						owner.NavigationController.PushViewController (bingSearchVC, true);
					}
				} else { 
					UIAlertView alert = new UIAlertView ("", "No link available for this feed.", null, "Ok", null);
					alert.Show ();
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}

		}

		public class CustomerIndustryTableSource : UITableViewSource {

			List<BingResult> TableItems;
			string CellIdentifier = "TableCell";

			customerProfileVC owner;

			public CustomerIndustryTableSource (List<BingResult> meetingList, customerProfileVC owner)
			{
				TableItems = meetingList;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return 5;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				BingResult item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }
				cell.ImageView.Image = UIImage.FromBundle("bing_icon.png");
				cell.ImageView.Frame = new CGRect (25, 15, 40, 35);
				cell.TextLabel.Text = item.Title;
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				BingSearchVC bingSearchVC = owner.Storyboard.InstantiateViewController ("BingSearchVC") as BingSearchVC;
				if (bingSearchVC != null) {
					bingSearchVC.webURL = TableItems [indexPath.Row].Url;
					owner.NavigationController.PushViewController (bingSearchVC, true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 50.0f;
			}
		}
	}
}

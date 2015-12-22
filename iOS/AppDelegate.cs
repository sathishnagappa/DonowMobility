using Foundation;
using UIKit;
//using Facebook.CoreKit;
using donow.PCL.Model;
using EventKit;
using System.Collections.Generic;
using donow.PCL;

namespace donow.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
//		string appId = "814782795310317";
//		string appName = "DoNow App";
		public static UserDetails UserDetails;
		public static Profile UserProfile;
		public static EKEventStore eventStore;
		public static bool IsCalendarClicked;
		public static bool IsLeadAccepted;
		public static List<CalenderEvent> CalendarList;
		//public static bool IsNewUser;
		public static Leads CurrentLead;
		public static bool IsFromProspect;
		public static bool IsProspectVisited;
		public static List<UserMeetings> userMeetings;
		public static Leads UpdateLead;
		public static LeadsBL leadsBL;
		public static string accessToken;
		public static CustomerBL customerBL;
		public static bool IsUpdateLeadDone;
		public static bool IsTabIndex;
		public static ReferralRequestBL referralRequestBL;
		public static UserBL userBL;
		public static bool IsUserSeller;
		public static BrokerBL brokerBL;
		public static EKEventStore EventStore
		{
			get { return eventStore; }
		}
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method
//			Profile.EnableUpdatesOnAccessTokenChange (true);
//			Settings.AppID = appId;
//			Settings.DisplayName = appName;
			UserDetails = new UserDetails();
			UserProfile = new Profile ();
			leadsBL = new LeadsBL ();
			eventStore = new EKEventStore ( );
			CalendarList = new List<CalenderEvent> ();
			customerBL = new CustomerBL ();
			IsUpdateLeadDone = false;
			referralRequestBL = new ReferralRequestBL ();
			userBL = new UserBL ();
			brokerBL = new BrokerBL ();

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			//return ApplicationDelegate.SharedInstance.FinishedLaunching (application, launchOptions);
			return true;
		}


		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

		//public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		//{
			 //We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
		//	return ApplicationDelegate.SharedInstance.OpenUrl (application, url, sourceApplication, annotation);
		//}


	}
}



using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using System.Collections.Generic;
using CoreGraphics;
using donow.PCL;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using donow.Util;

namespace donow.iOS
{
	partial class MyMeetingsVC : UIViewController
	{
		public UserMeetings meetingObj;
		//public Customer customer;
		public MyMeetingsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{			
			base.ViewWillAppear (animated);
			LoadMeetingData ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Meeting Info";

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;

			LabelNotes.Layer.BorderWidth = 1.0f;

			LabelNotes.ShouldBeginEditing = delegate {
				ScrollViewMeeting.SetContentOffset ( new CGPoint(0,350),true);
				ScrollViewMeeting.ContentSize = new CGSize (375.0f, 1000.0f);
				return true;	
			};

			LabelNotes.Text = meetingObj.Comments;
			LabelNotes.ShouldChangeText = (text, range, replacementString) =>
			{				
				if (replacementString.Equals("\n"))
				{
					LabelNotes.EndEditing(true);
//					ScrollViewMeeting.SetContentOffset ( new CGPoint(0,0),true);
					ScrollViewMeeting.ContentSize = new CGSize (375.0f, 900.0f);
					return false;
				}
				else
				{
					return true;
				}
			};

			ButtonSaveNotes.TouchUpInside += (object sender, EventArgs e) => {
				UserMeetings usermeeting = new UserMeetings();
				usermeeting.Id = meetingObj.Id;
				usermeeting.Status= meetingObj.Status;
				usermeeting.Comments = LabelNotes.Text;
				AppDelegate.userBL.UpdateMeetingList(usermeeting);

				var alert = new UIAlertView ("Notes","Notes saved successfully !!", null, "Ok", null);
				alert.Show ();
			};

//			UserMeetings usermeeting = new UserMeetings();
//			usermeeting.Id = meetingObj.Id;
//			usermeeting.Status="Done";
//			usermeeting.Comments = meetingObj.Comments;
//			AppDelegate.userBL.UpdateMeetingList(usermeeting);

			//LoadMeetingData ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		void LoadMeetingData()
		{
			LabelMeetingHeader.Text = "Meeting W/" + meetingObj.CustomerName;
			LabelDateAndTime.Text = DateTime.Parse(meetingObj.StartDate).ToString("MMM. dd, yyyy  hh:mm tt");
			LabelCityState.Text = meetingObj.City + ", " + meetingObj.State;
			//if (string.IsNullOrEmpty (LabelNotes.Text)) {
			//LabelNotesDate.Text = DateTime.Parse (meetingObj.EndDate).ToString ();
			//LabelNotes.Text = 
			//}

			ScrollViewMeeting.ContentSize = new CGSize (375.0f, 900f);
			TextViewTalkingPoints.Text = "What is the dream solution if pricing was not a problem?\n\n" + 
				"How do you want to maintain this solution long term?\n\n" +
				"What else are you willing to look into?\n\n" +
				"Would you mind telling me about your current situation?\n\n" +
				"Who do you currently use for this service?\n\n" +
				"What’s working for you?\n\n" +
				"What’s not working?\n\n" +
				"What are your main frustrations?\n\n" +
				"What would you like to change about things?\n\n" +
				"Ask your prospect if they’d like to know more about how you could answer their needs?\n\n" +
				"Does this sound like something that can solve your problems/make you feel better/address your issues?\n\n" +
				"I feel really good about this, I know this is going to work well for us. What’s the best way to get things underway?\n\n" +
				"I could email you an order when I get back to the office or I could just get it from you now. What works best for you?";							
		}

		static UIImage FromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data);
		}
	}
}
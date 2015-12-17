using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.Drawing;
using MessageUI;
using donow.PCL;
using System.Collections.Generic;

namespace donow.iOS
{
	partial class ScrollReq : UIViewController
	{
		public bool Accepted=false;
		public List<ReferralRequest> refferalRequests;
		public ScrollReq (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Referral Request";
			ScrollViewRR.ContentSize=new CGSize (400f, 1300);

			ButtonAccepRR.TouchUpInside += (object sender, EventArgs e) => {
				//BrokerBL brokerbl = new BrokerBL();
				//brokerbl.UpdateBrokerStatus(brokerObj.BrokerID,"Acceptance Pending");

				PassView.Hidden=true;
				MakeView.Hidden=false;
			};
			ButtonReferLater.TouchUpInside += (object sender, EventArgs e) => {
//				AlertView.Hidden=true;
//				AlertSubView.Hidden=true;	

				AlertViewLater.Hidden=false;
				AlertSubViewLater.Hidden=false;

			};
			ButtonLaterView.TouchUpInside += (object sender, EventArgs e) => {

				LandingRefferalRequestVC landingRefferalRequestVC = this.Storyboard.InstantiateViewController("landingRefferalRequestVC") as LandingRefferalRequestVC;
				if (landingRefferalRequestVC != null)
				{
					//landingRefferalRequestVC.CaseID = GetCurrentCaseID();
					this.NavigationController.PushViewController(landingRefferalRequestVC, true);
				}  
			};

			ButtonPassRR.TouchUpInside += (object sender, EventArgs e) => {

//
				LandingRefferalRequestVC landingRefferalRequestVC = this.Storyboard.InstantiateViewController("landingRefferalRequestVC") as LandingRefferalRequestVC;
				if (landingRefferalRequestVC != null)
			{
					//landingRefferalRequestVC.CaseID = GetCurrentCaseID();
					this.NavigationController.PushViewController(landingRefferalRequestVC, true);
			}  
//
//				LandingRefferalRequestVC landingRefferalRequestVC = this.Storyboard.InstantiateViewController ("MyDealMakerDetailVC") as LandingRefferalRequestVC;
//				if (landingRefferalRequestVC != null) {
//					//					dealmakerDetailObject.leadObj = TableItems [indexPath.Row];
//					this.NavigationController.PushViewController (landingRefferalRequestVC, true);
//				}
////				this.NavigationController.PopViewController(false);
////				this.NavigationController.PopViewController(false);
////				this.NavigationController.PushViewController
//
			};
			ButtonMakeReferal.TouchUpInside += (object sender, EventArgs e) => {
				AlertView.Hidden=false;
				AlertSubView.Hidden=false;

			};

			ButtonAlertViewDisable.TouchUpInside += (object sender, EventArgs e) => {

				AlertViewLater.Hidden=true;
				AlertSubViewLater.Hidden=true;

			};
			DisableReqMeetScroll.TouchUpInside += (object sender, EventArgs e) => {
			
				AlertViewRequestMeeting.Hidden=true;
				AlertSubViewRequestMeeting.Hidden=true;
			};

			ButtonScroll_.TouchUpInside += (object sender, EventArgs e) => {
		
				AlertView.Hidden=true;
				AlertSubView.Hidden=true;		
			};

			ButtonRequestMeeting.TouchUpInside += (object sender, EventArgs e) => {

				CalenderHomeDVC calendarHomeDV = new CalenderHomeDVC ();
				// if (calendarHomeDV != null)
				this.NavigationController.PushViewController(calendarHomeDV, true);

			};
			ButtonEmailAcceptView.TouchUpInside += (object sender, EventArgs e) =>  {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{"john@doe.com"});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("Hello <Insert Name>,\n\nMy name is [My Name] and I head up business development efforts with [My Company]. \n\nI am taking an educated stab here and based on your profile, you appear to be an appropriate person to connect with.\n\nI’d like to speak with someone from [Company] who is responsible for [handling something that's relevant to my product]\n\nIf that’s you, are you open to a fifteen minute call on _________ [time and date] to discuss ways the [Company Name] platform can specifically help your business? If not you, can you please put me in touch with the right person?\n\nI appreciate the help!\n\nBest,\n\n[Insert Name]", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						Console.WriteLine (args.Result.ToString ());
						args.Controller.DismissViewController (true, null);
						AlertView.Hidden=true;
						AlertSubView.Hidden=true;

						AlertViewRequestMeeting.Hidden=false;
						AlertSubViewRequestMeeting.Hidden=false;

					};

					this.PresentViewController (mailController, true, null);

					//					Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
				}

			};

		}
	}
}

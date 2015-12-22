using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;
using MessageUI;
using System.Linq;

namespace donow.iOS
{
	partial class prospectDetailsVC : UIViewController
	{
		public Leads localLeads;
		List<Broker> brokerList;

		public prospectDetailsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			
			base.ViewWillAppear (animated);
			AppDelegate.IsProspectVisited = true;
			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
		}

		public override void ViewDidLoad ()
		{
			AppDelegate.IsCalendarClicked = false;
			base.ViewDidLoad ();

			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Title = "Leads";
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;


			AppDelegate.CurrentLead = localLeads;
			LabelProspectName.Text = localLeads.LEAD_NAME;
			LabelProspectCompanyName.Text = localLeads.COMPANY_NAME;
			LabelProspectCityandState.Text = localLeads.CITY + ", " + localLeads.STATE;
			LabelLeadScore.Text = localLeads.LEAD_SCORE.ToString();
			LabelLeadSource.Text = localLeads.LEAD_SOURCE == 2 ? "SFDC" : "DoNow";

			BrokerBL brokerBL = new BrokerBL ();
			brokerList = brokerBL.GetBrokerForProspect (localLeads.LEAD_ID).OrderByDescending(X => X.BrokerScore).ToList();

			showBrokerImage (brokerList.Count);

			if (localLeads.LEAD_STATUS.Equals(1)) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Close Sale Highlight.png");
			} else if (localLeads.LEAD_STATUS.Equals(2)) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Proposal Highlight.png");
			} else if (localLeads.LEAD_STATUS.Equals(3)) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Follow Up Highlight.png");
			} else {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Acquire Lead Highlight.png");
			}

			ButtonPhoneProspect.TouchUpInside += (object sender, EventArgs e) => {
				var url = new NSUrl ("tel://" + localLeads.PHONE);
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				};
				CustomerInteraction customerinteract = new CustomerInteraction();
				customerinteract.CustomerName =  localLeads.LEAD_NAME;
				customerinteract.UserId = AppDelegate.UserDetails.UserId;
				customerinteract.Type = "Phone";
				customerinteract.DateNTime = DateTime.Now.ToString();
				AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
			};



			ButtonMailProspect.TouchUpInside += (object sender, EventArgs e) => {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{localLeads.EMAILID});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("Hello <Insert Name>,\n\nMy name is [My Name] and I head up business development efforts with [My Company]. \n\nI am taking an educated stab here and based on your profile, you appear to be an appropriate person to connect with.\n\nI’d like to speak with someone from [Company] who is responsible for [handling something that's relevant to my product]\n\nIf that’s you, are you open to a fifteen minute call on _________ [time and date] to discuss ways the [Company Name] platform can specifically help your business? If not you, can you please put me in touch with the right person?\n\nI appreciate the help!\n\nBest,\n\n[Insert Name]", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						CustomerInteraction customerinteract = new CustomerInteraction();
						customerinteract.CustomerName =  localLeads.LEAD_NAME;
						customerinteract.UserId = AppDelegate.UserDetails.UserId;
						customerinteract.Type = "Email";
						customerinteract.DateNTime = DateTime.Now.ToString();
						AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
						args.Controller.DismissViewController (true, null);
					};

					this.PresentViewController (mailController, true, null);
				}
			};

			ButtonSeeAllBrokers.TouchUpInside += (object sender, EventArgs e) => 
			{
				MyDealMakerVC myDealMaker = this.Storyboard.InstantiateViewController ("MyDealMakerVC") as MyDealMakerVC;

				if(myDealMaker!=null)
				{				
					AppDelegate.IsFromProspect = true;
					this.NavigationController.PushViewController(myDealMaker,true);					
				}
			};
		}

		void showBrokerImage (int count) {
			switch (count) {
			case 0:
				ImageFirstBroker.Hidden = true;
				LabelScoreFirstBroker.Hidden = true;
				ImageSecondBroker.Hidden = true;
				LabelScoreSecondBroker.Hidden = true;
				ImageThirdBroker.Hidden = true;
				LabelScoreThirdBroker.Hidden = true;
				ButtonSeeAllBrokers.Hidden = true;
				break;
			case 1:
				ButtonSeeAllBrokers.Hidden = false;
				LabelScoreFirstBroker.Text = "Score: " + brokerList [0].BrokerScore;
				ImageSecondBroker.Hidden = true; LabelScoreSecondBroker.Hidden = true;
				ImageThirdBroker.Hidden = true; LabelScoreThirdBroker.Hidden = true;
				break;
			case 2:
				ButtonSeeAllBrokers.Hidden = false;
				LabelScoreFirstBroker.Text = "Score: " + brokerList [0].BrokerScore;
				LabelScoreSecondBroker.Text = "Score: " + brokerList [1].BrokerScore;
				ImageThirdBroker.Hidden = true; LabelScoreThirdBroker.Hidden = true;
				break;
			default:
				ButtonSeeAllBrokers.Hidden = false;
				LabelScoreFirstBroker.Hidden = false;
				LabelScoreSecondBroker.Hidden = false;
				LabelScoreThirdBroker.Hidden = false;
				LabelScoreFirstBroker.Text = "Score: " + brokerList[0].BrokerScore;
				LabelScoreSecondBroker.Text = "Score: " + brokerList [1].BrokerScore;
				LabelScoreThirdBroker.Text = "Score: " + brokerList [2].BrokerScore;
				break;
			}
		}
	}
}


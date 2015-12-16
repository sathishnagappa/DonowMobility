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
//		    this.NavigationController.NavigationItem.SetLeftBarButtonItem( new UIBarButtonItem(UIImage.FromFile("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
//				LandingLeadsVC landingLeadsVC = this.Storyboard.InstantiateViewController ("LandingLeadsVC") as LandingLeadsVC;
//				if (landingLeadsVC != null) {
//					this.NavigationController.PopToViewController(landingLeadsVC, true);
//				} 
//			}), true);


		}
		public override void ViewWillDisappear (bool animated)
		{
//			LeadDetailVC landingLeadsVC = this.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
//			List<UIViewController> listvc = this.NavigationController.ViewControllers.ToList();
//			foreach (var item in listvc) {
//
//				if(item == landingLeadsVC)
//				{
//					item.NavigationController.RemoveFromParentViewController ();
//				}
//			}
		}


		public override void ViewDidLoad ()
		{
			AppDelegate.IsCalendarClicked = false;
			base.ViewDidLoad ();

			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Title = "Leads";
			btn.Image = UIImage.FromFile("See All Icon.png");
			btn.Clicked += (sender , e)=>{
				LandingLeadsVC leadsPage = this.Storyboard.InstantiateViewController ("LandingLeadsVC") as LandingLeadsVC;
				this.NavigationController.PushViewController(leadsPage,true);
			};
			NavigationItem.LeftBarButtonItem = btn;


			AppDelegate.CurrentLead = localLeads;
			LabelProspectName.Text = localLeads.LEAD_NAME;
			LabelProspectCompanyName.Text = localLeads.COMPANY_NAME;
			LabelProspectCityandState.Text = localLeads.CITY + ", " + localLeads.STATE;
			LabelLeadScore.Text = localLeads.LEAD_SCORE.ToString();
			LabelLeadSource.Text = localLeads.LEAD_SOURCE == 1? "SFDC" : "DoNow";

			BrokerBL brokerBL = new BrokerBL ();
			brokerList = brokerBL.GetBrokerForProspect (localLeads.LEAD_ID);

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
						Console.WriteLine (args.Result.ToString ());
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


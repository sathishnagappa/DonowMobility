using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;
using donow.PCL;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;

namespace donow.iOS
{
	partial class LandingRefferalRequestVC : UIViewController
	{
		//List<ReferralRequest> rrList;
		public LandingRefferalRequestVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

			//ReferralRequestBL rrBL = new ReferralRequestBL ();
			//rrList = rrBL.GetReferralRequest (AppDelegate.UserDetails.UserId);
//			this.NavigationController.TabBarItem.BadgeValue = .ToString ()

		}

	

	public override void ViewDidLoad ()
		{

		    this.NavigationItem.Title = "My Deals";
			this.NavigationController.TabBarItem.Title = "My Deals";
			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);

			Broker brokerprofile =  AppDelegate.brokerBL.GetBrokerFromID(AppDelegate.UserDetails.UserId).FirstOrDefault();

			if (brokerprofile != null) {
				LabelBrokerScore.Text = brokerprofile.BrokerScore;
				LabelTotalEarnings.Text = brokerprofile.BrokerTotalEarning;
			}
			LabelTitle.Text = AppDelegate.UserDetails.Title;
			LabelUserName.Text = AppDelegate.UserDetails.FullName;
			LabelIndustry.Text = AppDelegate.UserDetails.Industry;
			LabelCompany.Text = AppDelegate.UserDetails.Company;
			LabelCityState.Text = AppDelegate.UserDetails.City + "," + AppDelegate.UserDetails.State;

			ReferralRequestScrollView.ContentSize =  new SizeF (375,752);
			ReferralRequestDetails referralRequestVC = this.Storyboard.InstantiateViewController ("ReferralRequestDetails") as ReferralRequestDetails;
			List<ReferralRequest> rrdetails = new List<ReferralRequest> ();
			rrdetails = AppDelegate.referralRequestBL.GetReferralRequest (AppDelegate.UserDetails.UserId);



			List<ReferralRequest> newrequest = (from item in rrdetails
			                                    where item.Status == 1
												select item).ToList();			
			List<ReferralRequest> acceptedRequest = (from item in rrdetails
													where item.Status == 2
													select item).ToList();
			List<ReferralRequest> passedRequest = (from item in rrdetails
													where item.Status == 3
													select item).ToList();
			List<ReferralRequest> completedRequest = (from item in rrdetails
													 where item.Status == 4
													 select item).ToList();

			if (newrequest.Count == 0) {
				CompletedRequestView.Frame = new CGRect (0,530,375,155);
				PassedRequestView.Frame = new CGRect (0,375,375,155);
				AcceptedRequestView.Frame = new CGRect (0,220,375,155);
				NewRequestView.Hidden = true;
//				ReferralRequestScrollView.ContentSize = new CGSize (414,736);
			} else {	
				this.NavigationController.TabBarItem.BadgeValue = newrequest.Count.ToString ();
				NewRequestView.Hidden = false;
				TextNewSellerName.Text = newrequest [0].SellerName;
				TextNewIndustry.Text = newrequest [0].Industry;
			}
			
			if (acceptedRequest.Count == 0) {
				CompletedRequestView.Frame = new CGRect (0,530,375,155);
				PassedRequestView.Frame = new CGRect (0,375,375,155);
				AcceptedRequestView.Hidden = true;
				//ReferralRequestScrollView.ContentSize = new CGSize (414,736);
			} else {				
				AcceptedRequestView.Hidden = false;
				TextAcceptedSellerName.Text = acceptedRequest [0].SellerName;
				TextAcceptedIndustry.Text = acceptedRequest [0].Industry;
			}

			if (passedRequest.Count == 0) {
				CompletedRequestView.Frame =  new CGRect (0,530,375,155);
				PassedRequestView.Hidden = true;
				//ReferralRequestScrollView.ContentSize = new CGSize (414,736);
			} else {
				PassedRequestView.Hidden = false;
				TextPassedSellerName.Text = passedRequest [0].SellerName;
				TextPassedIndustry.Text = passedRequest [0].Industry;
			}

			if (completedRequest.Count == 0) {
				CompletedRequestView.Hidden = true;
				//ReferralRequestScrollView.ContentSize = new CGSize (414,736);
			} else {
				CompletedRequestView.Hidden = false;
				TextCompletedSellerName.Text = completedRequest [0].SellerName;
				TextCompletedIndusty.Text = completedRequest [0].Industry;
			}

			if ((newrequest.Count == 0) && (acceptedRequest.Count == 0) && (passedRequest.Count == 0) && (completedRequest.Count == 0)) {
				LabelNoReferralRequest.Hidden = false;
			}

			ButtonNewRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "New";
				AppDelegate.CurrentRRList = newrequest;
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};
			ButtonAcceptRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "Accepted";
				AppDelegate.CurrentRRList = acceptedRequest;
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};
			ButtonPassedRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "Passed";
				AppDelegate.CurrentRRList = passedRequest;
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};
			ButtonCompletedRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "Completed";
				AppDelegate.CurrentRRList = completedRequest;
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};

		}
	}
}

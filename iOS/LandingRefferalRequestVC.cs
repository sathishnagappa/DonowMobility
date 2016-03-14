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
		Broker brokerprofile;
		ReferralRequestDetails referralRequestVC;
		List<ReferralRequest> newrequest, acceptedRequest, passedRequest, completedRequest, rrdetails;

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

			this.NavigationItem.SetHidesBackButton (true, false);

			//ReferralRequestBL rrBL = new ReferralRequestBL ();
			//rrList = rrBL.GetReferralRequest (AppDelegate.UserDetails.UserId);
//			this.NavigationController.TabBarItem.BadgeValue = .ToString ()

			brokerprofile =  AppDelegate.brokerBL.GetBrokerFromID(AppDelegate.UserDetails.UserId).FirstOrDefault();
			loadView ();		
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			brokerprofile = null;
			rrdetails = null;
		}

		void loadView () 
		{

			if (brokerprofile != null) {
				LabelBrokerScore.Text = brokerprofile.BrokerScore;
				LabelTotalEarnings.Text = brokerprofile.BrokerTotalEarning;
			}

			LabelTitle.Text = AppDelegate.UserDetails.Title;
			LabelUserName.Text = AppDelegate.UserDetails.FullName;
			LabelIndustry.Text = AppDelegate.UserDetails.Industry;
			LabelCompany.Text = AppDelegate.UserDetails.Company;
			LabelCityState.Text = AppDelegate.UserDetails.City + "," + AppDelegate.UserDetails.State;

			//			ReferralRequestScrollView.ContentSize =  new SizeF (375,752);
			referralRequestVC = this.Storyboard.InstantiateViewController ("ReferralRequestDetails") as ReferralRequestDetails;
			rrdetails = new List<ReferralRequest> ();
			rrdetails = AppDelegate.referralRequestBL.GetReferralRequest (AppDelegate.UserDetails.UserId);

			newrequest = (from item in rrdetails
				where item.Status == 1
				select item).ToList();			
			acceptedRequest = (from item in rrdetails
				where item.Status == 2
				select item).ToList();
			passedRequest = (from item in rrdetails
				where item.Status == 3
				select item).ToList();
			completedRequest = (from item in rrdetails
				where item.Status == 4
				select item).ToList();

//			if (newrequest.Count == 0) {
//				CompletedRequestView.Frame = new CGRect (0,347,this.View.Bounds.Size.Width,70);
//				PassedRequestView.Frame = new CGRect (0,277,this.View.Bounds.Size.Width,70);
//				AcceptedRequestView.Frame = new CGRect (0,207,this.View.Bounds.Size.Width,70);
//				NewRequestView.Hidden = true;
//				//				ReferralRequestScrollView.ContentSize = new CGSize (414,736);
//			} else {	
//				this.NavigationController.TabBarItem.BadgeValue = newrequest.Count.ToString ();
//				NewRequestView.Hidden = false;
//				//				TextNewSellerName.Text = newrequest [0].SellerName;
//				//				TextNewIndustry.Text = newrequest [0].Industry;
//			}
//
//			if (acceptedRequest.Count == 0) {
//				CompletedRequestView.Frame = new CGRect (0,347,this.View.Bounds.Size.Width,70);
//				PassedRequestView.Frame = new CGRect (0,277,this.View.Bounds.Size.Width,70);
//				AcceptedRequestView.Hidden = true;
//				//ReferralRequestScrollView.ContentSize = new CGSize (414,736);
//			} else {				
//				AcceptedRequestView.Hidden = false;
//				//				TextAcceptedSellerName.Text = acceptedRequest [0].SellerName;
//				//				TextAcceptedIndustry.Text = acceptedRequest [0].Industry;
//			}
//
//			if (passedRequest.Count == 0) {
//				CompletedRequestView.Frame =  new CGRect (0,347,this.View.Bounds.Size.Width,70);
//				PassedRequestView.Hidden = true;
//				//ReferralRequestScrollView.ContentSize = new CGSize (414,736);
//			} else {
//				PassedRequestView.Hidden = false;
//				//				TextPassedSellerName.Text = passedRequest [0].SellerName;
//				//				TextPassedIndustry.Text = passedRequest [0].Industry;
//			}
//
//			if (completedRequest.Count == 0) {
//				CompletedRequestView.Hidden = true;
//				//ReferralRequestScrollView.ContentSize = new CGSize (414,736);
//			} else {
//				CompletedRequestView.Hidden = false;
//				//				TextCompletedSellerName.Text = completedRequest [0].SellerName;
//				//				TextCompletedIndusty.Text = completedRequest [0].Industry;
//			}
			this.NavigationController.TabBarItem.BadgeValue = newrequest.Count.ToString ();
			if (newrequest.Count == 0) {
				NewRequestView.Hidden = true;
				NewRequestView.Frame = new CGRect (0,0,this.View.Bounds.Size.Width, 0);
			} else {	
				NewRequestView.Frame = new CGRect (0,0,this.View.Bounds.Size.Width, 70);
				NewRequestView.Hidden = false;
			}
				

			if (acceptedRequest.Count == 0) {
				AcceptedRequestView.Hidden = true;
				AcceptedRequestView.Frame= new CGRect (0,NewRequestView.Frame.Y + NewRequestView.Bounds.Size.Height,this.View.Bounds.Size.Width,0);
			} else {				
				AcceptedRequestView.Frame = new CGRect (0,NewRequestView.Frame.Y + NewRequestView.Bounds.Size.Height,this.View.Bounds.Size.Width,70);
				AcceptedRequestView.Hidden = false;
			}
				
			if (passedRequest.Count == 0) {
				PassedRequestView.Hidden = true;
				PassedRequestView.Frame= new CGRect (0,AcceptedRequestView.Frame.Y + AcceptedRequestView.Bounds.Size.Height,this.View.Bounds.Size.Width, 0);
			} else {
				PassedRequestView.Frame = new CGRect (0,AcceptedRequestView.Frame.Y + AcceptedRequestView.Bounds.Size.Height,this.View.Bounds.Size.Width,70);
				PassedRequestView.Hidden = false;
			}
				
			if (completedRequest.Count == 0) {
				CompletedRequestView.Hidden = true;
				CompletedRequestView.Frame = new CGRect (0,PassedRequestView.Frame.Y + PassedRequestView.Bounds.Size.Height,this.View.Bounds.Size.Width, 0);
			} else {
				CompletedRequestView.Frame = new CGRect (0,PassedRequestView.Frame.Y + PassedRequestView.Bounds.Size.Height,this.View.Bounds.Size.Width,70);
				CompletedRequestView.Hidden = false;
			}

			if ((newrequest.Count == 0) && (acceptedRequest.Count == 0) && (passedRequest.Count == 0) && (completedRequest.Count == 0)) {
				LabelNoReferralRequest.Hidden = false;
			}
		}

		public override void ViewDidLoad ()
		{
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

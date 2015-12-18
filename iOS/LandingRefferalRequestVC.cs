using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;
using donow.PCL;
using System.Collections.Generic;

namespace donow.iOS
{
	partial class LandingRefferalRequestVC : UIViewController
	{
		List<ReferralRequest> rrList;
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

			ReferralRequestBL rrBL = new ReferralRequestBL ();
			rrList = rrBL.GetReferralRequest (AppDelegate.UserDetails.UserId);
//			this.NavigationController.TabBarItem.BadgeValue = .ToString ()
		}


		public override void ViewDidLoad ()
		{
			this.Title = "Referral Request";

			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);

			ReferralRequestScrollView.ContentSize =  new SizeF (0f, 900f);
			ReferralRequestDetails referralRequestVC = this.Storyboard.InstantiateViewController ("ReferralRequestDetails") as ReferralRequestDetails;



			ButtonNewRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "New";
				referralRequestVC.referralRequests = rrList;
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};
			ButtonAcceptRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "Accepted";
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};
			ButtonPassedRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "Passed";
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};
			ButtonCompletedRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "Completed";
				if (referralRequestVC != null) {
					this.NavigationController.PushViewController (referralRequestVC, true);
				}
			};

		}
	}
}

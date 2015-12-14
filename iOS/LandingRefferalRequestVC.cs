using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;

namespace donow.iOS
{
	partial class LandingRefferalRequestVC : UIViewController
	{
		public LandingRefferalRequestVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.ParentViewController.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
		}


		public override void ViewDidLoad ()
		{
			this.Title = "Referral Request";
			ReferralRequestScrollView.ContentSize =  new SizeF (0f, 900f);
			ReferralRequestDetails referralRequestVC = this.Storyboard.InstantiateViewController ("ReferralRequestDetails") as ReferralRequestDetails;

			ButtonNewRequest.TouchUpInside += (object sender, EventArgs e) => {
				referralRequestVC.referralRequestType = "New";
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

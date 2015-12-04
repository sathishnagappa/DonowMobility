using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class LandingCustomerStreamVC : UIViewController
	{
		public LandingCustomerStreamVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (false, false);
		}

		public override void ViewDidLoad ()
		{
//			this.Title = "Referral Request";
		}
	}
}

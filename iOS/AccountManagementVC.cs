using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;

namespace donow.iOS
{
	partial class AccountManagementVC : UIViewController
	{
		UserBL userBL;
		public AccountManagementVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (157, 50, 49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

		}

		public override void ViewDidLoad ()
		{
			AppDelegate.IsNewUser = true;
			ButtonFinish.TouchUpInside += (object sender, EventArgs e) => {					
				userBL = new UserBL();
				//AppDelegate.UserDetails.UserId = 7;
				AppDelegate.UserDetails.UserId = userBL.CreateUser(AppDelegate.UserDetails);
				WelcomeVC welcomeVC = this.Storyboard.InstantiateViewController ("WelcomeVC") as WelcomeVC;
				if (welcomeVC != null) {
					this.NavigationController.PushViewController (welcomeVC, true);
				}
			};
		}
	}
}

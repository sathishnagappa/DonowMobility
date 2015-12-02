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

		public override void ViewDidLoad ()
		{
			ButtonFinish.TouchUpInside += (object sender, EventArgs e) => {					
				userBL = new UserBL();
				AppDelegate.UserDetails.UserId = 5;
				userBL.CreateUser(AppDelegate.UserDetails);
				WelcomeVC welcomeVC = this.Storyboard.InstantiateViewController ("WelcomeVC") as WelcomeVC;
				if (welcomeVC != null) {
					this.NavigationController.PushViewController (welcomeVC, true);
				}
			};
		}
	}
}

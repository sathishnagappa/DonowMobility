using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class AccountManagementVC : UIViewController
	{
		public AccountManagementVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			ButtonFinish.TouchUpInside += (object sender, EventArgs e) => {	
				WelcomeVC welcomeVC = this.Storyboard.InstantiateViewController ("WelcomeVC") as WelcomeVC;
				if (welcomeVC != null) {
					this.NavigationController.PushViewController (welcomeVC, true);
				}
			};
		}
	}
}

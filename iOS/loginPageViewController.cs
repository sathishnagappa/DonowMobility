using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;
using CoreGraphics;

namespace donow.iOS
{
	partial class loginPageViewController : UIViewController
	{
		public loginPageViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.NavigationBar.BarTintColor = UIColor.DarkGray;
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

			TextBoxUserName.ShouldReturn = delegate {
				TextBoxUserName.ResignFirstResponder ();
				return true;
			};

			TextBoxPassword.ShouldReturn = delegate {
				TextBoxPassword.ResignFirstResponder ();
				return true;
			};
				
			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

			ButtonLogin.TouchUpInside += (object sender, EventArgs e) => {
				//Call to Authencation service 
				if (ValidateCredentials ()) {
					LandingTabBarVC landingVC = this.Storyboard.InstantiateViewController ("LandingTabBarVC") as LandingTabBarVC;
					if (landingVC != null) {
						this.NavigationController.PushViewController (landingVC, true);
					}
				} 
			};

			ButtonSignUp.TouchUpInside += (object sender, EventArgs e) => {
				signUpLoginDetailsVC signUpVC = this.Storyboard.InstantiateViewController ("signUpLoginDetailsVC") as signUpLoginDetailsVC;
				if (signUpVC != null) {
					this.NavigationController.PushViewController (signUpVC, true);
				}
			};

		}

		bool ValidateCredentials()
		{
			if (TextBoxUserName.Text != null && TextBoxPassword.Text != null) {
				if (AppDelegate.UserDetails.UserName.ToLower () == TextBoxUserName.Text.ToLower () && TextBoxPassword.Text == Crypto.Decrypt (AppDelegate.UserDetails.Password)) {
					return true;
				}
				else {
					UIAlertView alert = new UIAlertView () { 
						Title = "Invalid Credentials", 
						Message = "User Name or Password doesn't match"
					};
					alert.AddButton ("OK");
					alert.Show ();
					return false;
				}
			} else {
				UIAlertView alert = new UIAlertView () { 
					Title = "Error", 
					Message = "User Name or Password cannot be blank"
				};
				alert.AddButton ("OK");
				alert.Show ();
			}
			return false;
		}
	}
}

using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;
using CoreGraphics;
using Auth0.SDK;
using donow.PCL.Model;

namespace donow.iOS
{
	partial class loginPageViewController : UIViewController
	{
		public loginPageViewController (IntPtr handle) : base (handle)
		{
		}

		public  override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

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

			ButtonLogin.TouchUpInside +=  (object sender, EventArgs e) => {
				if (ValidateCredentials ()) {
					LandingTabBarVC landingVC = this.Storyboard.InstantiateViewController ("LandingTabBarVC") as LandingTabBarVC;
					if (landingVC != null) {
						this.PresentViewController(landingVC, true, null);
					}
				} 
			};

			ButtonLinkedInLogin.TouchUpInside += async (object sender, EventArgs e) => {
				
				var auth0 = new Auth0Client(
					"donow.auth0.com",
					"1ghdA3NFkpT9V7ibOuIKp8QK3oF49RId");

				var user = await auth0.LoginAsync(this,"linkedin");

				AppDelegate.UserProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<Profile>(user.Profile.ToString());

				if(AppDelegate.UserProfile.email_verified == true)
				{
					signUpOtherDetailsVC signUpVC = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
					if (signUpVC != null) {
						this.PresentViewController(signUpVC, true, null);
					}
				}
			};

			ButtonSignUp.TouchUpInside += (object sender, EventArgs e) => {
				signUpLoginDetailsVC signUpVC = this.Storyboard.InstantiateViewController ("signUpLoginDetailsVC") as signUpLoginDetailsVC;
				if (signUpVC != null) {
					this.PresentViewController(signUpVC, true , null);
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

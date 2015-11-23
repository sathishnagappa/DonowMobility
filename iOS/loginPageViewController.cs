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

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (true, false);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
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

			LabelUserName.Layer.CornerRadius = 5.0f;
			LabelUserName.Layer.BorderWidth = 1.0f;
			LabelUserName.Layer.BorderColor = UIColor.DarkGray.CGColor;

			LabelPassword.Layer.CornerRadius = 5.0f;
			LabelPassword.Layer.BorderWidth = 1.0f;
			LabelPassword.Layer.BorderColor = UIColor.DarkGray.CGColor;

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

			ButtonLogin.TouchUpInside +=  (object sender, EventArgs e) => {
				if (ValidateCredentials ()) {
					// Call to Get user details and validate credentials
					LandingTabBarVC landingVC = this.Storyboard.InstantiateViewController ("LandingTabBarVC") as LandingTabBarVC;
					if (landingVC != null) {
						this.NavigationController.PushViewController(landingVC, true);
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
						this.NavigationController.PushViewController(signUpVC, true);
					}
				}
			};

			ButtonSignUp.TouchUpInside += (object sender, EventArgs e) => {
				signUpLoginDetailsVC signUpVC = this.Storyboard.InstantiateViewController ("signUpLoginDetailsVC") as signUpLoginDetailsVC;
				if (signUpVC != null) {
					this.NavigationController.PushViewController(signUpVC, true);
				}
			};

			ButtonForgotPassword.TouchUpInside += (object sender, EventArgs e) => {			
				ForgotPasswordVC forgotPasswordVC = this.Storyboard.InstantiateViewController ("ForgotPasswordVC") as ForgotPasswordVC;
				if (forgotPasswordVC != null) {
					this.NavigationController.PushViewController(forgotPasswordVC, true);
				}
			};

		}

		bool ValidateCredentials()
		{
			UIAlertView alert = null;
			if (TextBoxUserName.Text != null && TextBoxPassword.Text != null) {
				if (AppDelegate.UserDetails.UserName.ToLower () == TextBoxUserName.Text.ToLower () && TextBoxPassword.Text == Crypto.Decrypt (AppDelegate.UserDetails.Password)) {
					return true;
				}
				else {
					alert = new UIAlertView () { 
						Title = "Invalid Credentials", 
						Message = "User Name or Password doesn't match"
					};
					alert.AddButton ("OK");
					alert.Show ();
					return false;
				}
			} else {
				alert = new UIAlertView () { 
					Title = "Error", 
					Message = "User Name or Password cannot be blank"
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
		}
	}
}

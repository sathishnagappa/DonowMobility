using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;
using CoreGraphics;
using Auth0.SDK;
using donow.PCL.Model;
//using Salesforce;
using System.Linq;
using System.Collections.Generic;
//using Salesforce;
using System.Net;
using donow.Services;
using donow.PCL;

namespace donow.iOS
{
	partial class loginPageViewController : UIViewController
	{
		LoadingOverlay loadingOverlay;
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
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
			this.NavigationController.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIImage.FromBundle("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
				this.NavigationController.PopViewController(true);
			}), true);
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
			//AppDelegate.UserDetails.UserName = "sathish";
			//AppDelegate.UserDetails.Password = Crypto.Encrypt("sathish");
			ButtonLogin.TouchUpInside +=  (object sender, EventArgs e) => {

//				RestService rs = new RestService();
//				string content = rs.SFDCAuthentication();
//				await rs.UpdateSFDCData(content);

	
				if (ValidateCredentials ()) {
					var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
					if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
						bounds.Size = new CGSize (bounds.Size.Height, bounds.Size.Width);
					}
					loadingOverlay = new LoadingOverlay (bounds);
					View.Add (loadingOverlay);
					// Call to Get user details and validate credentials
					LandingTabBarVC landingVC = this.Storyboard.InstantiateViewController ("LandingTabBarVC") as LandingTabBarVC;
					if (landingVC != null) {
						this.NavigationController.PushViewController(landingVC, true);
					}
					loadingOverlay.Hide ();
				}
			};

			ButtonLinkedInLogin.TouchUpInside += async (object sender, EventArgs e) => {
				
				var auth0 = new Auth0Client(
					"donow.auth0.com",
					"1ghdA3NFkpT9V7ibOuIKp8QK3oF49RId");				


//				Auth0User user = null;
//				try
//				{
				 var user = await auth0.LoginAsync(this,"linkedin");
//				}
//				catch(Exception ex)
//				{
//					this.DismissViewController(true,null);
//				}

				if(user != null)
				{
					
				AppDelegate.UserProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<Profile>(user.Profile.ToString());
				
				if(AppDelegate.UserProfile.email_verified == true)
				{
					signUpOtherDetailsVC signUpVC = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
					if (signUpVC != null) {
						this.NavigationController.PushViewController(signUpVC, true);
					}
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
			UserBL userBL = new UserBL ();
			AppDelegate.UserDetails = userBL.GetUserDetails (TextBoxUserName.Text);
			if (!string.IsNullOrEmpty(TextBoxUserName.Text)  && !string.IsNullOrEmpty(TextBoxPassword.Text) ) {
				if (!string.IsNullOrEmpty(AppDelegate.UserDetails.Name) && 
					AppDelegate.UserDetails.Password != null && AppDelegate.UserDetails.Name.ToLower () == TextBoxUserName.Text.ToLower () && TextBoxPassword.Text == Crypto.Decrypt (AppDelegate.UserDetails.Password)) {
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

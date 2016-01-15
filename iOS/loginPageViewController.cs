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
using Xamarin;
using System.Threading.Tasks;
//using BigTed;

namespace donow.iOS
{
	partial class loginPageViewController : UIViewController
	{
		//LoadingOverlay loadingOverlay;
		public loginPageViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if(this.NavigationController != null)			
				this.NavigationController.SetNavigationBarHidden (true, false);					

		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			if (NavigationController != null) {
				this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (157, 50, 49);
				this.NavigationController.NavigationBar.TintColor = UIColor.White;
				this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
			}
//			this.NavigationController.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIImage.FromBundle("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
//				this.NavigationController.PopViewController(true);
//			}), true);
		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.SetHidesBackButton (true, false);	
			this.NavigationItem.SetLeftBarButtonItem(null, true);

			SetUILayOut ();

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

			AppDelegate.IsFromSignUp = false;
			ButtonLogin.TouchUpInside +=  async (object sender, EventArgs e) => {				
				if ( ValidateCredentials ()) {
					// Call to Get user details and validate credentials
					LandingTabBarVC landingVC = this.Storyboard.InstantiateViewController ("LandingTabBarVC") as LandingTabBarVC;
					if (landingVC != null) {						
						this.NavigationController.PushViewController(landingVC, true);
					}
				}
			};

			ButtonLinkedInLogin.TouchUpInside += async (object sender, EventArgs e) => {

				var auth0 = new Auth0Client("donow.auth0.com","1ghdA3NFkpT9V7ibOuIKp8QK3oF49RId");
				
				Auth0User user = null;
				try
				{
					user = await auth0.LoginAsync(this,"linkedin");	

				}
				catch (AggregateException ex)
				{
				//this.SetResultText(e.Flatten().Message);
				}
				catch (Exception ex)
				{
				//this.SetResultText(e.Message);
				}

				if(user != null)
				{
					LoadingOverlay loadingOverlay;
					AppDelegate.UserProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<Profile>(user.Profile.ToString());

					if(AppDelegate.UserProfile.email_verified == true)
					{

						AppDelegate.UserDetails =  AppDelegate.userBL.GetUserFromEmail(AppDelegate.UserProfile.email);

						if(AppDelegate.UserDetails != null)
						{
							LandingTabBarVC landingVC = this.Storyboard.InstantiateViewController ("LandingTabBarVC") as LandingTabBarVC;
							if (landingVC != null) {
								this.NavigationController.PushViewController(landingVC, true);
							}
						}
						else
						{
							signUpOtherDetailsVC signUpVC = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
							if (signUpVC != null) {
//								signUpVC.NavigationItem.SetHidesBackButton (true, false);	
//								signUpVC.NavigationItem.SetLeftBarButtonItem(null, true);

						
								signUpVC.NavigationItem.SetHidesBackButton (true, false);

								this.NavigationController.PushViewController(signUpVC, true);

							}
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

		void SetUILayOut()
		{
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
			
		}

		bool ValidateCredentials()
		{

			
			UIAlertView alert = null;
			if (!string.IsNullOrEmpty(TextBoxUserName.Text)  && !string.IsNullOrEmpty(TextBoxPassword.Text) ) {
				AppDelegate.UserDetails =  AppDelegate.userBL.GetUserDetails (TextBoxUserName.Text);
				if (AppDelegate.UserDetails != null && !string.IsNullOrEmpty(AppDelegate.UserDetails.Name) && 
					AppDelegate.UserDetails.Password != null && AppDelegate.UserDetails.Name.ToLower () == TextBoxUserName.Text.ToLower () && TextBoxPassword.Text == Crypto.Decrypt (AppDelegate.UserDetails.Password)) {
					#region This code block is used for Insight tracking
					var userInfos = new Dictionary<string,string> {
						{ Insights.Traits.Email, AppDelegate.UserDetails.Email },
						{ Insights.Traits.Name, AppDelegate.UserDetails.FullName},
						{ "Title",AppDelegate.UserDetails.Title},
						{ "Company",AppDelegate.UserDetails.Company},
						{ "Industry",AppDelegate.UserDetails.Industry},
						{ Insights.Traits.Address,AppDelegate.UserDetails.OfficeAddress},
						{ Insights.Traits.Phone,AppDelegate.UserDetails.Phone}
					};
					#endregion
					Insights.Identify(AppDelegate.UserDetails.UserId.ToString(), userInfos);

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

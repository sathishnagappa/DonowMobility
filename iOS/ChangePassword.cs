using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;
using Xamarin;
using System.Collections.Generic;
using CoreGraphics;

namespace donow.iOS
{
	partial class ChangePassword : UIViewController
	{
		public ChangePassword (IntPtr handle) : base (handle)
		{
		}
		//public bool isFromSignUp;
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
//			if (!isFromSignUp)
//				this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
//			//this.NavigationController.SetNavigationBarHidden (false, false);
		}
		public  override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.Title = "Change Password";

			ButtonChangePassword.Layer.CornerRadius = 5.0f;
			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
//				AccountManagementVC loginPage = this.Storyboard.InstantiateViewController ("AccountManagementVC") as AccountManagementVC;
//				this.NavigationController.PushViewController(loginPage,true);
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;
			TextBoxUserName.Text = AppDelegate.UserDetails.Name;

			ButtonChangePassword.TouchUpInside += (object sender, EventArgs e) =>  {
				
				if(Validation())
				{						
					AppDelegate.UserDetails.Password = Crypto.Encrypt(TextBoxNewPassword.Text); 
					AppDelegate.userBL.UpdateUserDetails(AppDelegate.UserDetails);
					//Xamarin Insights tracking
					Insights.Track("Change Password", new Dictionary <string,string>{
						{"UserId", AppDelegate.UserDetails.UserId.ToString()}
					});
					//SendMail(AppDelegate.UserDetails.Email, newPassword);
					UIAlertView alert = new UIAlertView () { 
						Title = "", 
						Message = "Password Changed Successfully."
					};
					alert.AddButton ("OK");
					alert.Show ();
				}
			};

			TextBoxShouldReturn ();
			TextBoxConfirmPassword.ShouldBeginEditing = delegate {
				ScrollViewChangePswd.SetContentOffset ( new CGPoint(0,50),true);
				return true;	
			};
		}

		void TextBoxShouldReturn () {
			
			TextBoxOldPassword.ShouldReturn = delegate {
				TextBoxOldPassword.ResignFirstResponder ();
				return true;
			};
			TextBoxNewPassword.ShouldReturn = delegate {
				TextBoxNewPassword.ResignFirstResponder ();
				return true;
			};
			TextBoxConfirmPassword.ShouldReturn = delegate {
				TextBoxConfirmPassword.ResignFirstResponder ();
				ScrollViewChangePswd.SetContentOffset ( new CGPoint(0,0),true);
				return true;
			};
		}

		bool Validation()
		{
			UIAlertView alert = null;
			if (string.IsNullOrEmpty(TextBoxOldPassword.Text)) {
				alert = new UIAlertView () { 
					Title = "Old Password is blank", 
					Message = "Please enter the Old Password."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty(TextBoxNewPassword.Text)) {
				alert = new UIAlertView () { 
					Title = "New Password is blank", 
					Message = "Please enter the New Password."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty(TextBoxNewPassword.Text)) {
				alert = new UIAlertView () { 
					Title = "Confirm Password is blank", 
					Message = "Please enter the Confirm Password."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (TextBoxNewPassword.Text != TextBoxConfirmPassword.Text) {				
				alert = new UIAlertView () { 
					Title = "Password Mismatch", 
					Message = "Confirm password doesn't match with the new password."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (AppDelegate.UserDetails != null && !string.IsNullOrEmpty(AppDelegate.UserDetails.Name) && 
				AppDelegate.UserDetails.Password != null && TextBoxOldPassword.Text == Crypto.Decrypt(AppDelegate.UserDetails.Password)) {
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
			return true;		
		}
	}
}

using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;

namespace donow.iOS
{
	partial class ForgotPasswordVC : UIViewController
	{
		public ForgotPasswordVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (false, false);
		}
		public  override void ViewDidLoad ()
		{
			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				loginPageViewController loginPage = this.Storyboard.InstantiateViewController ("loginPageViewController") as loginPageViewController;
				this.NavigationController.PushViewController(loginPage,true);
			};
			NavigationItem.LeftBarButtonItem = btn;

			ButtonChange.TouchUpInside += (object sender, EventArgs e) =>  {
			
				if(TextBoxPassword.Text == TextBoxConfirmPassword.Text) {
					// Call to save new password 
					AppDelegate.UserDetails.Name = TextBoxEmailID.Text;
					AppDelegate.UserDetails.Password = Crypto.Encrypt(TextBoxPassword.Text.ToLower());
					this.NavigationController.PopViewController(true);
				}
				else
				{
					UIAlertView alert = new UIAlertView () { 
						Title = "Password Mismatch", 
						Message = "Confirm password doesn't match with the password."
					};
					alert.AddButton ("OK");
					alert.Show ();
				}
			};
		}
	}
}

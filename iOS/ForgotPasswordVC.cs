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

		public  override void ViewDidLoad ()
		{

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

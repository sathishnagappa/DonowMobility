using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;

namespace donow.iOS
{
	partial class signUpLoginDetailsVC : UIViewController
	{
		public signUpLoginDetailsVC (IntPtr handle) : base (handle)
		{
			
		}

		public override void ViewDidLoad ()
		{
			NextBtn.Layer.CornerRadius = 5.0f;

			TextBoxUserName.ShouldReturn = delegate {
				TextBoxUserName.ResignFirstResponder ();
				return true;
			};
			TextBoxPassword.ShouldReturn = delegate {
				TextBoxPassword.ResignFirstResponder ();
				return true;
			};

			NextBtn.TouchUpInside += (object sender, EventArgs e) => {

				if(TextBoxPassword.Text == TextBoxVerifyPassword.Text) {

					AppDelegate.UserDetails.UserName = TextBoxUserName.Text;
					AppDelegate.UserDetails.Password = Crypto.Encrypt(TextBoxPassword.Text.ToLower());

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

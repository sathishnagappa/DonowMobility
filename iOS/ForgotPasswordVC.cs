using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;
using System.Text.RegularExpressions;

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


				if(Validation())
				{
					AppDelegate.userBL.GetUserFromEmail(TextBoxPassword.Text);

				}
			};




		}

		bool Validation()
		{
			UIAlertView alert;
			if (string.IsNullOrEmpty (TextBoxPassword.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please enter Email ID."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (!Regex.IsMatch (TextBoxPassword.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
				RegexOptions.IgnoreCase)) 
			{
				 alert = new UIAlertView () { 
					Title = "Invalid Email ID", 
					Message = "Please enter valid Email ID"
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
		
			return true;
		}
	}
}

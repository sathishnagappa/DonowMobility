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

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (false, false);
		}
		public  override void ViewDidLoad ()
		{
			this.Title = "Forgot Password";

			ButtonChange.Layer.CornerRadius = 5.0f;
			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				loginPageViewController loginPage = this.Storyboard.InstantiateViewController ("loginPageViewController") as loginPageViewController;
				this.NavigationController.PushViewController(loginPage,true);
			};
			NavigationItem.LeftBarButtonItem = btn;

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

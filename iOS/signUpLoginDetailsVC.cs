using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;
using donow.PCL.Model;
using System.Collections.Generic;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	partial class signUpLoginDetailsVC : UIViewController
	{
		//LoadingOverlay loadingOverlay;
		public signUpLoginDetailsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			this.Title = "Sign Up";
//			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
//			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
//				bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
//			}
//			loadingOverlay = new LoadingOverlay (bounds);
//			View.Add (loadingOverlay);

			//List<Leads> leads = new  List<Leads>();
			//LeadsBL leadsbl = new LeadsBL ();
			//leads = leadsbl.GetAllLeads ();
			//loadingOverlay.Hide ();

			//Leads lead = new Leads();
			//lead = leadsbl.GetLeadDetails (1);
			NextBtn.Layer.CornerRadius = 5.0f;

			TextBoxUserName.ShouldReturn = delegate {
				TextBoxUserName.ResignFirstResponder ();
				return true;
			};
			TextBoxPassword.ShouldReturn = delegate {
				TextBoxPassword.ResignFirstResponder ();
				return true;
			};

			TextBoxVerifyPassword.ShouldReturn = delegate {
				TextBoxVerifyPassword.ResignFirstResponder ();
				return true;
			};

			NextBtn.TouchUpInside += (object sender, EventArgs e) => {
				if (Validation ()) {
					
						AppDelegate.UserDetails.UserName = TextBoxUserName.Text;
						AppDelegate.UserDetails.Password = Crypto.Encrypt (TextBoxPassword.Text.ToLower ());

						signUpOtherDetailsVC signUpVC = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
						if (signUpVC != null) {
							this.NavigationController.PushViewController (signUpVC, true);
					}

				}
			};	

		}

		private bool Validation()
		{
			UIAlertView alert = null;
			if (string.IsNullOrEmpty(TextBoxUserName.Text)) {
				alert = new UIAlertView () { 
					Title = "User Name is blank", 
					Message = "Please enter the User Name."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty(TextBoxPassword.Text)) {
				alert = new UIAlertView () { 
					Title = "Password is blank", 
					Message = "Please enter the Password."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (TextBoxPassword.Text != TextBoxVerifyPassword.Text) {				
				alert = new UIAlertView () { 
					Title = "Password Mismatch", 
					Message = "Confirm password doesn't match with the password."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			return true;
		}
	}
}

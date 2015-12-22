using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.Util;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Xamarin;
using System.Collections.Generic;

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
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			ButtonChange.TouchUpInside += (object sender, EventArgs e) =>  {


				if(Validation())
				{
					AppDelegate.UserDetails =  AppDelegate.userBL.GetUserFromEmail(TextBoxEmailID.Text);
					string newPassword = RandomString();
					AppDelegate.UserDetails.Password = Crypto.Encrypt(newPassword); 
					AppDelegate.userBL.UpdateUserDetails(AppDelegate.UserDetails);
					SendMail(AppDelegate.UserDetails.Email, newPassword);
					//Xamarin Insights tracking
					Insights.Track("Forgot Password", new Dictionary <string,string>{
						{"UserId", AppDelegate.UserDetails.UserId.ToString()},
						{Insights.Traits.Email, AppDelegate.UserDetails.Email}
					});
				}
			};
		}

		void SendMail(string email, string newPassword)
		{
			UIAlertView alert;
			try
			{
				MailMessage mail=new MailMessage();
				SmtpClient SmtpServer=new SmtpClient("smtp.gmail.com");
				mail.From=new MailAddress("vaibhav22barchhiha@gmail.com");
				mail.To.Add(new MailAddress("sathish.nagappa@brillio.com"));
				mail.Subject = "New Password";
				mail.Body = "Here is your New password " + newPassword;
				SmtpServer.Port = 587;
				SmtpServer.Credentials=new System.Net.NetworkCredential("vaibhav22barchhiha","mastercard22_");
				SmtpServer.EnableSsl=true;
				ServicePointManager.ServerCertificateValidationCallback=delegate(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
					return true;
				};
				SmtpServer.Send(mail);

				alert = new UIAlertView () {
					Title = "", 
					Message = "Mail Sent."
				};
				alert.AddButton ("OK");
				alert.Show ();

			}
			catch(Exception ex) 
			{
				Insights.Report(ex);
				alert = new UIAlertView () {
					Title = "Email Error", 
					Message = "Coundn't send email."
				};
				alert.AddButton ("OK");
				alert.Show ();
			}
		}

		public static string RandomString()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, 8)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		bool Validation()
		{
			UIAlertView alert;
			if (string.IsNullOrEmpty (TextBoxEmailID.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please enter Email ID."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (!Regex.IsMatch (TextBoxEmailID.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
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

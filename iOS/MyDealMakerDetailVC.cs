// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using donow.PCL;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace donow.iOS
{
	public partial class MyDealMakerDetailVC : UIViewController
	{
		public Broker brokerObj;
		public MyDealMakerDetailVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ButtonSendRequest.Enabled = true;
			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				MyDealMakerVC dealMaker = this.Storyboard.InstantiateViewController ("MyDealMakerVC") as MyDealMakerVC;
				this.NavigationController.PushViewController(dealMaker,true);
				//this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;
			this.Title = "Deal Maker";
			ViewBackgroundTransparent.Hidden = true;
			ViewSendRequestView.Hidden = true;
			if (!AppDelegate.IsFromProspect) {
				ButtonSendRequest.Hidden = true;
				ButtonCancel.Hidden = true;
			}

			ViewSendRequestView.Layer.CornerRadius = 10.0f;
			ButtonOkSendRequestView.Layer.CornerRadius = 5.0f;
			LabelNameDealMaker.Text = brokerObj.City + " Deal Maker";
			LabelBrokerJobTitle.Text = brokerObj.BrokerTitle;

			ButtonSendRequest.TouchUpInside += (object sender, EventArgs e) =>  {
				
				AppDelegate.brokerBL.UpdateBrokerStatus(brokerObj.BrokerID, 2,brokerObj.LeadID);
				ReferralRequest rrnew = new ReferralRequest ();
				rrnew.ID = 0;
				rrnew.SellerName = AppDelegate.UserDetails.FullName;
				rrnew.City = AppDelegate.UserDetails.City;
				rrnew.State = AppDelegate.UserDetails.State;
				rrnew.Industry = AppDelegate.UserDetails.Industry;
				rrnew.Prospect = AppDelegate.CurrentLead.LEAD_NAME;
				rrnew.BusinessNeeds = AppDelegate.CurrentLead.BUSINESS_NEED;
				rrnew.BrokerID = brokerObj.BrokerID;
				rrnew.BrokerUserID = brokerObj.BrokerUserID;
				rrnew.Status = 1;
				rrnew.CreatedOn = DateTime.Now.ToString();
				rrnew.SellerUserID = AppDelegate.UserDetails.UserId;
				rrnew.CompanyInfo = AppDelegate.UserDetails.Company;
				rrnew.CompanyName = AppDelegate.UserDetails.Company;
				rrnew.LeadEmailID = AppDelegate.CurrentLead.EMAILID;
				AppDelegate.referralRequestBL.SaveReferralRequest(rrnew);

				MailMessage mail=new MailMessage();
				SmtpClient SmtpServer=new SmtpClient("outlook.office365.com");
				mail.From=new MailAddress("support@donowx.com");
				mail.To.Add(new MailAddress("sathish.nagappa@brillio.com"));
				mail.Subject = "Need Referral for " + AppDelegate.CurrentLead.LEAD_NAME;
				mail.Body = "Here is an opportunity for you refer and earn. Please download the donow app and join the donow network.";
				SmtpServer.Port = 587;
				SmtpServer.Credentials=new System.Net.NetworkCredential("support@donowx.com","dnsupport$9");
				SmtpServer.EnableSsl=true;
				ServicePointManager.ServerCertificateValidationCallback=delegate(object sender1, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
					return true;
				};
				SmtpServer.SendAsync(mail,null);

				ViewBackgroundTransparent.Hidden = false;
				ViewSendRequestView.Hidden = false;
				ButtonSendRequest.Enabled = false;
			};
			ButtonOkSendRequestView.TouchUpInside += (object sender, EventArgs e) => {
				ViewBackgroundTransparent.Hidden = true;
				ViewSendRequestView.Hidden = true;
			};

			LabelBrokerScore.Text = "Broker Score \n" + brokerObj.BrokerScore;
			LabelBrokerFee.Text = "Broker Fee \n" + brokerObj.BrokerFee;
			LabelTotalEarnings.Text = "# of Deals made \n" + "0"; //brokerObj.BrokerTotalEarning;
			LabelCompanyInfoDescription.Text = brokerObj.Industry;
			labelConnectionToLead.Text = brokerObj.ConnectionLead;

			ButtonSendRequest.Layer.CornerRadius = 8.0f;
			ButtonOkSendRequestView.Layer.CornerRadius = 8.0f;

			ButtonCancel.Layer.BorderWidth = 2.0f;
			ButtonCancel.Layer.BorderColor = UIColor.FromRGB (45, 125, 177).CGColor;
			ButtonCancel.Layer.CornerRadius = 8.0f;

			ButtonCancel.TouchUpInside += (object sender, EventArgs e) =>  {
				this.NavigationController.PopViewController(true);
			};
//			ScrollViewDealMakerDetails.ContentSize = new CGSize (375, 633.0f);
		}
	}
}

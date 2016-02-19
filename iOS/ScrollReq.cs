using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.Drawing;
using MessageUI;
using donow.PCL;
using System.Collections.Generic;
using Xamarin;

namespace donow.iOS
{
	partial class ScrollReq : UIViewController
	{
		public bool Accepted=false;
		public ReferralRequest refferalRequests;
		public string referralRequestType;
		public ScrollReq (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Requestor Profile";
			ScrollViewRR.ContentSize=new CGSize (375.0f, 1200.0f);

			ButtonReferLater.Layer.BorderWidth = 2.0f;
			ButtonReferLater.Layer.BorderColor = UIColor.FromRGB (44, 145, 188).CGColor;

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				//this.NavigationController.PopViewController(false);
				ReferralRequestDetails referralRequestDetails = this.Storyboard.InstantiateViewController("ReferralRequestDetails") as ReferralRequestDetails;
				if(referralRequestDetails != null)
					referralRequestDetails.referralRequestType = referralRequestType;
					this.NavigationController.PushViewController(referralRequestDetails,true);
			};
			NavigationItem.LeftBarButtonItem = btn;

			AlertSubView.Layer.CornerRadius = 15.0f;
			AlertSubView.Layer.MasksToBounds = true;

			AlertSubViewRequestMeeting.Layer.CornerRadius = 15.0f;
			AlertSubViewRequestMeeting.Layer.MasksToBounds = true;

			AlertSubViewLater.Layer.CornerRadius = 15.0f;
			AlertSubViewLater.Layer.MasksToBounds = true;


			ButtonEmailAcceptView.Layer.CornerRadius = 5.0f;
			ButtonLaterView.Layer.CornerRadius = 5.0f;
			DisableReqMeetScroll.Layer.CornerRadius = 5.0f;
			ButtonRequestMeeting.Layer.CornerRadius = 5.0f;

			ButtonReferLater.Layer.BorderWidth = 2.0f;
			ButtonReferLater.Layer.BorderColor = UIColor.FromRGB (50, 135, 172).CGColor;


			LabelNameSeller.Text = refferalRequests.SellerName;
			LabelTitleSeller.Text = refferalRequests.SellerTitle;
			LabelIndustrySeller.Text = refferalRequests.SellerCompany;
			LabelCityState.Text = EvaluateString (refferalRequests.SellerCity,refferalRequests.SellerState);

			LabelComapnyNameSeller.Text = refferalRequests.SellerIndustry;
			LabelStreetSeller.Text = refferalRequests.SellerOfficeAddress;
			LabelCityStateSeller.Text = EvaluateString (refferalRequests.City,refferalRequests.State);
			LabelZipCodeCountrySeller.Text = EvaluateString (refferalRequests.SellerZipCode,"United State");
			LabelPhoneSeller.Text = "Tel: " + refferalRequests.SellerPhone;
			LabelLOBSeller.Text = refferalRequests.SellerLOB;

			LabelNameProspect.Text = refferalRequests.Prospect;
			LabelTitleProspect.Text = refferalRequests.LEAD_TITLE;
			LabelIndustryProspect.Text = refferalRequests.COMPANY_NAME;
			LabelCityStateProspectProfile.Text = EvaluateString (refferalRequests.LEAD_CITY,refferalRequests.LEAD_STATE);

			LabelStreetProspect.Text = EvaluateString (refferalRequests.LEAD_COMP_ADDRESS,refferalRequests.County);
			LabelCityStateProspect.Text = EvaluateString (refferalRequests.LEAD_CITY,refferalRequests.LEAD_STATE);
			LabelZipCountryProspect.Text = EvaluateString (refferalRequests.LEAD_COMP_ZIPCODE,refferalRequests.LEAD_COMP_COUNTRY);
			LabelPhoneProspect.Text = "Tel: " + refferalRequests.LEAD_PHONE;

			LabelFinancialsProspect.Text = "Revenue : "+ evaluateAmount(refferalRequests.Revenue);
			LabelFiscalYearProspect.Text = refferalRequests.FiscalYE;
			LabelLOBProspect.Text = refferalRequests.LEAD_LOB;
			LabelNetIncomeProspect.Text = refferalRequests.NetIncome;
			LabelEmployeesProspect.Text = refferalRequests.Employees;
			LabelMarketValueProspect.Text = evaluateAmount (refferalRequests.MarketValue);
			LabelMYearFoundedProspect.Text = refferalRequests.YearFounded;
			LabelIndustryRiskProspect.Text = refferalRequests.IndustryRiskScore;
			LabelWebsiteProspect.Text = refferalRequests.WebAddress;

			if (refferalRequests.Status == 2) {
				PassView.Hidden = true;
				MakeView.Hidden = false;
			} else if (refferalRequests.Status == 4) {
				PassView.Hidden = true;
				MakeView.Hidden = true;
			} else if (refferalRequests.Status == 1)  {
				LabelNameSeller.Hidden = true;			
			}

			ButtonAccepRR.TouchUpInside += (object sender, EventArgs e) => {
			
				AppDelegate.brokerBL.UpdateBrokerStatus(refferalRequests.BrokerID,4,refferalRequests.LeadID);
				LabelNameSeller.Hidden = false;
				//Xamarin Insights tracking
				Insights.Track("Update BrokerStatus", new Dictionary <string,string>{
					{"BrokerID", refferalRequests.BrokerID.ToString()},
					{"Status", "4"},
					{"LeadID", refferalRequests.LeadID.ToString()}
				});
				AppDelegate.referralRequestBL.UpdateReferralRequest(refferalRequests.ID,2);
				//Xamarin Insights tracking
				Insights.Track("Update ReferralRequest", new Dictionary <string,string>{
					{"ReferralReqId", refferalRequests.ID.ToString()},
					{"Status", "2"}
				});

				PassView.Hidden = true;
				MakeView.Hidden = false;
				AppDelegate.CurrentRRList.Remove(refferalRequests);
			};
				
			ButtonReferLater.TouchUpInside += (object sender, EventArgs e) => {

				AlertViewLater.Hidden=false;
				AlertSubViewLater.Hidden=false;

			};
			ButtonLaterView.TouchUpInside += (object sender, EventArgs e) => {

				LandingRefferalRequestVC landingRefferalRequestVC = this.Storyboard.InstantiateViewController("landingRefferalRequestVC") as LandingRefferalRequestVC;
				if (landingRefferalRequestVC != null)
				{
					//landingRefferalRequestVC.CaseID = GetCurrentCaseID();
					this.NavigationController.PushViewController(landingRefferalRequestVC, true);
				}  
			};

			ButtonPassRR.TouchUpInside += (object sender, EventArgs e) => {

				AppDelegate.referralRequestBL.UpdateReferralRequest(refferalRequests.ID,3);
				AppDelegate.brokerBL.UpdateBrokerStatus(refferalRequests.BrokerID,3,refferalRequests.LeadID);
				LandingRefferalRequestVC landingRefferalRequestVC = this.Storyboard.InstantiateViewController("landingRefferalRequestVC") as LandingRefferalRequestVC;
				if (landingRefferalRequestVC != null)
			{
					//landingRefferalRequestVC.CaseID = GetCurrentCaseID();
					this.NavigationController.PushViewController(landingRefferalRequestVC, true);
			}  


			};
			ButtonMakeReferal.TouchUpInside += (object sender, EventArgs e) => {
				AlertView.Hidden=false;
				AlertSubView.Hidden=false;
			};

			ButtonAlertViewDisable.TouchUpInside += (object sender, EventArgs e) => {
				AlertViewLater.Hidden=true;
				AlertSubViewLater.Hidden=true;
			};

			DisableReqMeetScroll.TouchUpInside += (object sender, EventArgs e) => {
			
				AlertViewRequestMeeting.Hidden=true;
				AlertSubViewRequestMeeting.Hidden=true;
			};

			ButtonScroll_.TouchUpInside += (object sender, EventArgs e) => {
		
				AlertView.Hidden=true;
				AlertSubView.Hidden=true;		
			};

			ButtonRequestMeeting.TouchUpInside += (object sender, EventArgs e) => {

				CalenderHomeDVC calendarHomeDV = new CalenderHomeDVC ();
				AppDelegate.IsFromRR = true;
				AppDelegate.CurrentRR = refferalRequests;
				if(AppDelegate.CurrentRRList.Contains(refferalRequests))
					AppDelegate.CurrentRRList.Remove(refferalRequests);
				this.NavigationController.PushViewController(calendarHomeDV, true);

			};
			ButtonEmailAcceptView.TouchUpInside += (object sender, EventArgs e) =>  {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{refferalRequests.LeadEmailID});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						args.Controller.DismissViewController (true, null);
						AlertView.Hidden=true;
						AlertSubView.Hidden=true;
						//PagingSendMail.BackgroundColor=UIColor.Red;
						AppDelegate.referralRequestBL.UpdateReferralRequest(refferalRequests.BrokerUserID,4);
						//Xamarin Insights tracking
						Insights.Track("Update ReferralRequest", new Dictionary <string,string>{
							{"BrokerUserID", refferalRequests.BrokerUserID.ToString()},
							{"Status", "4"}
						});
						CustomerInteraction customerinteract = new CustomerInteraction();
						customerinteract.CustomerName =  refferalRequests.Prospect;
						customerinteract.UserId = AppDelegate.UserDetails.UserId;
						customerinteract.Type = "Email";
						customerinteract.DateNTime = DateTime.Now.ToString();
						customerinteract.LeadID = (int)refferalRequests.LeadID;
						AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
						//Xamarin Insights tracking
						Insights.Track("Update ReferralRequest", new Dictionary <string,string>{
							{"UserId", customerinteract.UserId.ToString()},
							{"CustomerName", customerinteract.CustomerName},
							{"Type","Email"}
						});
						AlertViewRequestMeeting.Hidden=false;
						AlertSubViewRequestMeeting.Hidden=false;
						if(AppDelegate.CurrentRRList.Contains(refferalRequests))
							AppDelegate.CurrentRRList.Remove(refferalRequests);
						

					};

					this.PresentViewController (mailController, true, null);

					//					Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
				}

			};

		}

		string EvaluateString (string firstString, string secondString) {

			if (!string.IsNullOrEmpty(firstString) && !string.IsNullOrEmpty(secondString))
				return (firstString + ", " + secondString);
			else
				return (firstString + secondString);
		}

		string evaluateAmount (string firstString){
			if (string.IsNullOrEmpty(firstString) || firstString == "NA")
				return (firstString);
			else
				return ("$" + firstString + " M");
		}
	}
}

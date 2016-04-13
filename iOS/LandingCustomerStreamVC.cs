using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;
using donow.Util;
using CoreGraphics;
using System.Linq;

namespace donow.iOS
{
	partial class LandingCustomerStreamVC : UIViewController
	{
		public bool flag = false;

		Dashboard dashboardObj;
		public LandingCustomerStreamVC (IntPtr handle) : base (handle)
		{
		}

		public LandingCustomerStreamVC () {
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
//			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

			//bingResult = AppDelegate.customerBL.GetBingNewsResult(AppDelegate.UserDetails.Industry + " + Customers");

			//TableViewCustomerStream.Source= new CustomerIndustryTableSource(bingResult.OrderByDescending(X => X.Date).ToList(), this);
			dashboardObj = AppDelegate.userBL.GetDashBoardDetails (AppDelegate.UserDetails.UserId);
			string nextMeeting = string.IsNullOrEmpty (dashboardObj.next_meeting) == true ? "" : DateTime.Parse (dashboardObj.next_meeting).ToString ("MMM. dd, yyyy  hh:mm tt");
			string title = string.IsNullOrEmpty (dashboardObj.CustomerName) == true ? "NA" : "Meeting With " + dashboardObj.CustomerName;
			TextViewNextMeeting.Text = title + "\n" + nextMeeting;
			LabelTotalCustomers.Text = dashboardObj.total_customers.ToString();

//			TextViewCRM.Text = "Total Leads : " + dashboardObj.crm_total_leads.ToString() + "\n"
//				+ "Leads with DealMakers : " + dashboardObj.crm_leads_with_dealmakers.ToString() + "\n"
//				+ "Leads without DealMakers : " + dashboardObj.crm_leads_without_dealmakers.ToString();
//
//			TextViewDonow.Text = "Total Requested : " + dashboardObj.dn_total_leads.ToString() + "\n"
//				+ "Total Accepted : " + dashboardObj.dn_total_leads_accepted.ToString() + "\n"
//				+ "Leads with DealMakers : " + dashboardObj.dn_leads_with_dealmakers.ToString() + "\n"
//				+ "Leads without DealMakers : " + dashboardObj.dn_leads_without_dealmakers.ToString();

			LabelCRMNew.Text = dashboardObj.CRMNew.ToString();
			LabelCRMWorking.Text = dashboardObj.CRMWorking.ToString();
			LabelCRMProposalNegotiation.Text = dashboardObj.CRMProposal.ToString();
			LabelCRMConnectionMade.Text = dashboardObj.CRMConnectionMade.ToString();
			LabelCRMClosedWon.Text = dashboardObj.CRMClosedWon.ToString();

			LabelDonowNew.Text = dashboardObj.DoNowNew.ToString();
			LabelDonowWorking.Text = dashboardObj.DoNowWorking.ToString();
			LabelDonowProposalNegotiation.Text = dashboardObj.DoNowProposal.ToString();
			LabelDonowConnectionMade.Text = dashboardObj.DoNowConnectionMade.ToString();
			LabelDonowClosedWon.Text = dashboardObj.DoNowClosedWon.ToString();

			LabelTotalEarnings.Text = dashboardObj.total_earning + "\n";
			TextViewDealMakers.Text = "Total Deal Requests: " + dashboardObj.total_lead_requests.ToString() + "\n"
				+ "Deals Accepted: " + dashboardObj.total_accepted.ToString() + "\n"
				+ "Deals Referred: " + dashboardObj.total_referred.ToString();

			if (string.IsNullOrEmpty (dashboardObj.next_meeting)) {
				ButtonNextMeeting.Hidden = true;
				ImageShowMore.Hidden = true;
			}
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			dashboardObj = null;
		}

		protected override void Dispose (bool disposing)
		{
//			if (TableViewCustomerStream.Source != null)
//				TableViewCustomerStream.Source.Dispose ();
			base.Dispose (disposing);
		}


		public override void ViewDidLoad ()
		{
			this.Title = "My Dashboard";

			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);

			ScrollViewDashboard.ContentSize = new CGSize (375, 1000);

			//Dashboard dashboardObj = AppDelegate.userBL.GetDashBoardDetails (AppDelegate.UserDetails.UserId);

			ButtonUpdateProfile.Layer.BorderWidth = 2.0f;
			ButtonUpdateProfile.Layer.BorderColor = UIColor.FromRGB (45, 125, 177).CGColor;
			ButtonUpdateProfile.Layer.CornerRadius = 3.0f;

			ButtonUpdateProfile.TouchUpInside += (object sender, EventArgs e) => {
				signUpOtherDetailsVC userInfo = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
				if(userInfo != null)
					userInfo.isFromDashBoard = true;
					this.NavigationController.PresentViewController (userInfo, true, null);		
			};

			ButtonGoToLeads.Layer.CornerRadius = 3.0f;
			ButtonGoToLeads.TouchUpInside += (object sender, EventArgs e) => {
				this.TabBarController.SelectedIndex = 2;
			};
			ButtonGoToDeals.Layer.CornerRadius = 3.0f;
			ButtonGoToDeals.TouchUpInside += (object sender, EventArgs e) => {
				this.TabBarController.SelectedIndex = 3;
			};
			LabelCustomerName.Text = AppDelegate.UserDetails.FullName;
			LabelTitle.Text = AppDelegate.UserDetails.Title;
			LabelCompanyName.Text = AppDelegate.UserDetails.Company;
			LabelCityState.Text = AppDelegate.UserDetails.City + ", " + AppDelegate.UserDetails.State;

			ButtonNextMeeting.TouchUpInside += (object sender, EventArgs e) => {
				MyMeetingsVC myMeetingsObj = this.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				if (myMeetingsObj != null) {
					myMeetingsObj.meetingObj = new UserMeetings();
					myMeetingsObj.meetingObj.CustomerName = dashboardObj.CustomerName;
					myMeetingsObj.meetingObj.StartDate = dashboardObj.next_meeting;
					myMeetingsObj.meetingObj.City = dashboardObj.City;
					myMeetingsObj.meetingObj.State = dashboardObj.State;
					myMeetingsObj.meetingObj.Id = dashboardObj.MeetingID;
					myMeetingsObj.meetingObj.Comments = dashboardObj.Comments;
					this.NavigationController.PushViewController(myMeetingsObj,true);
				}
			};
		}
	}
}

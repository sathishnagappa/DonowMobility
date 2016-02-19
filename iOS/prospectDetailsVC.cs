using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;
using MessageUI;
using System.Linq;
using Xamarin;
using CoreGraphics;

namespace donow.iOS
{
	partial class prospectDetailsVC : UIViewController
	{
		public Leads localLeads;
		//List<Broker> brokerList;
		Prospect prospectDetails;

		public prospectDetailsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{			
			base.ViewWillAppear (animated);
			//AppDelegate.IsProspectVisited = true;
			//UpdateSalesStage();
			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			LoadData ();
		}

		public override void ViewDidLoad ()
		{
			AppDelegate.IsCalendarClicked = false;
			base.ViewDidLoad ();

			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Title = "Leads";
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				LandingLeadsVC landingleads = this.Storyboard.InstantiateViewController("LandingLeadsVC") as LandingLeadsVC;
				if(landingleads != null)
					this.NavigationController.PushViewController(landingleads,true);
			};
			NavigationItem.LeftBarButtonItem = btn;
//			if(localLeads.LEAD_TYPE == "Y")
//				this.NavigationItem.Title = "Lead Prospect";
//			else 
				this.NavigationItem.Title = "Lead Details";
			
//			LoadData ();

			ButtonPhoneProspect.TouchUpInside += (object sender, EventArgs e) => {
				var phone = string.IsNullOrEmpty(localLeads.PHONE.Trim()) == true ? "0" : localLeads.PHONE;
				var url = new NSUrl ("tel://" + phone);
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				};
				CustomerInteraction customerinteract = new CustomerInteraction();
				customerinteract.CustomerName =  localLeads.LEAD_NAME;
				customerinteract.UserId = AppDelegate.UserDetails.UserId;
				customerinteract.Type = "Phone";
				customerinteract.DateNTime = DateTime.Now.ToString();
				customerinteract.LeadID = localLeads.LEAD_ID;
				AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
			    //if(localLeads.LEAD_SOURCE ==2)
				//{
					InteractionLeadUpdateVC interactionLeadUpdateVC = this.Storyboard.InstantiateViewController ("InteractionLeadUpdateVC") as InteractionLeadUpdateVC;
					if (interactionLeadUpdateVC != null) {
						interactionLeadUpdateVC.leadObj = localLeads;
						this.PresentViewController (interactionLeadUpdateVC, true, null);
					}
				//}
				//Xamarin Insights tracking
				Insights.Track("Save CutomerInteraction", new Dictionary <string,string>{
					{"UserId", customerinteract.UserId.ToString()},
					{"CustomerName", customerinteract.CustomerName},
					{"Type", "Phone"}
				});
			};



			ButtonMailProspect.TouchUpInside += (object sender, EventArgs e) => {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{localLeads.EMAILID});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("Hello <Insert Name>,\n\nMy name is [My Name] and I head up business development efforts with [My Company]. \n\nI am taking an educated stab here and based on your profile, you appear to be an appropriate person to connect with.\n\nI’d like to speak with someone from [Company] who is responsible for [handling something that's relevant to my product]\n\nIf that’s you, are you open to a fifteen minute call on _________ [time and date] to discuss ways the [Company Name] platform can specifically help your business? If not you, can you please put me in touch with the right person?\n\nI appreciate the help!\n\nBest,\n\n[Insert Name]", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {						
						CustomerInteraction customerinteract = new CustomerInteraction();
						customerinteract.CustomerName =  localLeads.LEAD_NAME;
						customerinteract.UserId = AppDelegate.UserDetails.UserId;
						customerinteract.Type = "Email";
						customerinteract.DateNTime = DateTime.Now.ToString();
						customerinteract.LeadID = localLeads.LEAD_ID;
						AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
						args.Controller.DismissViewController (true, null);

						//Xamarin Insights tracking
						Insights.Track("Save CutomerInteraction", new Dictionary <string,string>{
							{"UserId", customerinteract.UserId.ToString()},
							{"CustomerName", customerinteract.CustomerName},
							{"Type", "Email"}
						});
					};

					this.PresentViewController (mailController, true, null);
				}
			};

			ButtonUpdateProspect.TouchUpInside += (object sender, EventArgs e) => {
				InteractionLeadUpdateVC interactionLeadUpdateVC = this.Storyboard.InstantiateViewController ("InteractionLeadUpdateVC") as InteractionLeadUpdateVC;
				if (interactionLeadUpdateVC != null) {
					interactionLeadUpdateVC.leadObj = localLeads;
					this.PresentViewController (interactionLeadUpdateVC, true, null);
				}
			};

			ButtonSeeAllBrokers.TouchUpInside += (object sender, EventArgs e) => 
			{
				callDealMakerList();
			};

			ButtonFirstDealMaker.TouchUpInside += (object sender, EventArgs e) =>  {
				callDealMakerList();
			};

			ButtonSecondDealMaker.TouchUpInside += (object sender, EventArgs e) =>  {
				callDealMakerList();
			};
			ButtonThirdDealMaker.TouchUpInside += (object sender, EventArgs e) =>  {
				callDealMakerList();
			};

			ButtonCalendarEvent.TouchUpInside += (object sender, EventArgs e) => {
				CalenderHomeDVC calendarHomeDV = new CalenderHomeDVC ();
				this.NavigationController.PushViewController(calendarHomeDV, true);
			};
		}

		void callDealMakerList () {
			MyDealMakerVC myDealMaker = this.Storyboard.InstantiateViewController ("MyDealMakerVC") as MyDealMakerVC;

			if (myDealMaker != null) {				
				AppDelegate.IsFromProspect = true;
				this.NavigationController.PushViewController (myDealMaker, true);					
			}
		}

		protected override void Dispose (bool disposing)
		{
			if (TableViewEmails.Source != null)
				TableViewEmails.Source.Dispose ();
			if (TableViewMeetings.Source != null)
				TableViewMeetings.Source.Dispose ();
			if (TableViewPreviousMeetings.Source != null)
				TableViewPreviousMeetings.Source.Dispose ();
			base.Dispose (disposing);
		}

		void LoadData()
		{
			ScrollViewProspectDetails.ContentSize = new CGSize (375.0f, 1801.0f);

			prospectDetails = AppDelegate.leadsBL.GetProspectDetails(localLeads.LEAD_ID,AppDelegate.UserDetails.UserId);

			List<UserMeetings> listMeeting = prospectDetails.UserMeetingList;
			List<UserMeetings> UCommingMeetinglist = new List<UserMeetings>();
			List<UserMeetings> PreviousMeetingsList = new List<UserMeetings>(); 

			if(listMeeting != null)
			{
				foreach (var item in listMeeting) {
					if (DateTime.Compare (DateTime.Parse (item.EndDate), DateTime.Now) > 0) {
						UCommingMeetinglist.Add (item);
					} else {
						PreviousMeetingsList.Add (item);
					}
				}
			}

			if(prospectDetails.customerInteractionList  != null && prospectDetails.customerInteractionList.Count !=0)
				TableViewEmails.Source = new TableSourceInteractionWithCustomer (prospectDetails.customerInteractionList , this);
			if(UCommingMeetinglist.Count != 0)
				TableViewMeetings.Source = new TableSourceupComingMeetings (UCommingMeetinglist,this);
			if(PreviousMeetingsList.Count !=0)
				TableViewPreviousMeetings.Source = new TableSourcePreviousMeetings (PreviousMeetingsList,this);

			AppDelegate.CurrentLead = localLeads;
			LabelProspectName.Text = prospectDetails.LEAD_NAME;
			LabelProspectCompanyName.Text = prospectDetails.COMPANY_NAME;
			string coma = (string.IsNullOrEmpty (prospectDetails.CITY) || string.IsNullOrEmpty (prospectDetails.STATE)) ? "" : ", ";

			LabelProspectCityandState.Text = prospectDetails.CITY + coma + prospectDetails.STATE;
			LabelLeadScore.Text = prospectDetails.LEAD_SCORE.ToString();
			LabelLeadSource.Text = prospectDetails.LEAD_SOURCE == 2 ? "SFDC" : "DoNow";
			LabelCustomerVsProspect.Text = prospectDetails.LEAD_TYPE == "Y" ? "Existing Customer" : "New Prospect" ;
			//brokerList = AppDelegate.brokerBL.GetBrokerForProspect (localLeads.LEAD_ID).OrderByDescending(X => X.BrokerScore).ToList();
			if (!string.IsNullOrEmpty (prospectDetails.LEAD_TITLE))
				ProspectTitle.Text = "("+prospectDetails.LEAD_TITLE+")";
			else
				ProspectTitle.Text = "";
			
			showBrokerImage (prospectDetails.brokerList.Count);

			LabelStreetCustomerInfo.Text = EvaluateString (prospectDetails.ADDRESS,prospectDetails.COUNTY);
			LabelCityStateCustomerInfo.Text = EvaluateString (prospectDetails.CITY,prospectDetails.STATE);
			LabelZipCodeCountryCustomerInfo.Text = EvaluateString (prospectDetails.ZIPCODE,prospectDetails.COUNTRY);
			LabelMobileNumberCustomerInfo.Text = "Tel: " + prospectDetails.PHONE;

			LabelIndustry.Text = prospectDetails.INDUSTRY_INFO;
			LabelFinancials.Text = "Revenue : "+ evaluateAmount(prospectDetails.REVENUE);
			LabelFiscalYear.Text = prospectDetails.FISCALYE;
			LabelLOBCustomerInfo.Text = prospectDetails.BUSINESS_NEED;
			LabelNetIncome.Text = prospectDetails.NETINCOME;
			LabelTotalEmployees.Text = prospectDetails.EMPLOYEES;
			LabelMarketValueCustomerInfo.Text = evaluateAmount (prospectDetails.MARKETVALUE);
			LabelYearFoundedCustomerInfo.Text = prospectDetails.YEARFOUNDED;
			LabelIndustryRiskScoreCustomerInfo.Text = prospectDetails.INDUSTRYRISK;

//			LabelWebsite.Text = "<html><body><a href=" + prospectDetails.WebAddress + ">" + prospectDetails.WebAddress + "</a></body></html>";
			LabelWebsite.Text = prospectDetails.WebAddress;
			CustomerInfoScrollView.ContentSize = new CGSize (this.View.Bounds.Size.Width, 600);
			AppDelegate.CurrentLead = new Leads () { LEAD_ID = prospectDetails.LEAD_ID, LEAD_NAME = prospectDetails.LEAD_NAME, 
				CITY = prospectDetails.CITY, STATE = prospectDetails.STATE
			};
			UpdateSalesStage ();
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

		void UpdateSalesStage()
		{
			if (prospectDetails.LEAD_STATUS.Equals("(4) Close Sale")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Close Sale Highlight.png");
			} else if (prospectDetails.LEAD_STATUS.Equals("(2) Proposal")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Proposal Highlight.png");
			} else if (prospectDetails.LEAD_STATUS.Equals("(3) Follow Up")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Follow Up Highlight.png");
			} else {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Acquire Lead Highlight.png");
			}
		}

		void showBrokerImage (int count) {
			switch (count) {
			case 0:
				ButtonFirstDealMaker.Hidden = true;
				LabelScoreFirstBroker.Hidden = true;
				ButtonSecondDealMaker.Hidden = true;
				LabelScoreSecondBroker.Hidden = true;
				ButtonThirdDealMaker.Hidden = true;
				LabelScoreThirdBroker.Hidden = true;
				ButtonSeeAllBrokers.Hidden = true;
				break;
			case 1:
				ButtonSeeAllBrokers.Hidden = false;
				LabelScoreFirstBroker.Text = "Score: " + prospectDetails.brokerList[0].BrokerScore;
				ButtonSecondDealMaker.Hidden = true; LabelScoreSecondBroker.Hidden = true;
				ButtonThirdDealMaker.Hidden = true; LabelScoreThirdBroker.Hidden = true;
				break;
			case 2:
				ButtonSeeAllBrokers.Hidden = false;
				LabelScoreFirstBroker.Text = "Score: " + prospectDetails.brokerList[0].BrokerScore;
				LabelScoreSecondBroker.Text = "Score: " + prospectDetails.brokerList[1].BrokerScore;
				ButtonThirdDealMaker.Hidden = true; LabelScoreThirdBroker.Hidden = true;
				break;
			default:
				ButtonSeeAllBrokers.Hidden = false;
				LabelScoreFirstBroker.Hidden = false;
				LabelScoreSecondBroker.Hidden = false;
				LabelScoreThirdBroker.Hidden = false;
				LabelScoreFirstBroker.Text = "Score: " + prospectDetails.brokerList[0].BrokerScore;
				LabelScoreSecondBroker.Text = "Score: " + prospectDetails.brokerList[1].BrokerScore;
				LabelScoreThirdBroker.Text = "Score: " + prospectDetails.brokerList[2].BrokerScore;
				break;
			}
		}

	public class TableSourceInteractionWithCustomer : UITableViewSource {

		string CellIdentifier = "InteractionWithCustomer";
		List<CustomerInteraction> TableItems;
		//customerProfileVC owner;

		public TableSourceInteractionWithCustomer (List<CustomerInteraction> items, prospectDetailsVC owner)
		{
			TableItems = items;
			//this.owner = owner;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return TableItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{				
			var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellinteractionWithCustomerCell;

			if (cell == null) {
				cell = new CustomerViewTableCellinteractionWithCustomerCell (CellIdentifier);
			}

			//				Leads leadsObj = TableItems[indexPath.Row];
			cell.UpdateCell(TableItems[indexPath.Row]);
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);

		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}
	}
	public class TableSourceupComingMeetings : UITableViewSource {
		string CellIdentifier = "upComingMeetingsCell";
		List<UserMeetings> TableItems;
		prospectDetailsVC owner;

		public TableSourceupComingMeetings (List<UserMeetings> items, prospectDetailsVC owner)
		{
			TableItems = items;
			this.owner = owner;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return TableItems.Count;

		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{                
			var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellUpcomingMeetings;

			if (cell == null) {
				cell = new CustomerViewTableCellUpcomingMeetings (CellIdentifier);
			}

			cell.UpdateCell(TableItems[indexPath.Row]);
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{

			tableView.DeselectRow (indexPath, true);
			MyMeetingsVC myMeetingsObj = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
			if (myMeetingsObj != null) {
				myMeetingsObj.meetingObj = TableItems[indexPath.Row];
//					myMeetingsObj.customer = new Customer();
//					myMeetingsObj.customer.Company = owner.localLeads.COMPANY_NAME;
//					myMeetingsObj.customer.LeadId = owner.localLeads.LEAD_ID;
//					myMeetingsObj.customer.Name = owner.localLeads.LEAD_NAME;
				owner.NavigationController.PushViewController(myMeetingsObj,true);
			}
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 90.0f;
		}
	}

	public class TableSourcePreviousMeetings : UITableViewSource {
		string CellIdentifier = "PreviousMeetingsCell";
		List<UserMeetings> TableItems;
		prospectDetailsVC owner;

		public TableSourcePreviousMeetings (List<UserMeetings> items, prospectDetailsVC owner)
		{
			TableItems = items;
			this.owner = owner;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return TableItems.Count;

		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{                
			var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewPreviousMeetings;

			if (cell == null) {
				cell = new CustomerViewPreviousMeetings (CellIdentifier);
			}

			cell.UpdateCell(TableItems[indexPath.Row]);
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{

			tableView.DeselectRow (indexPath, true);
			MyMeetingsVC myMeetingsObj = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
			if (myMeetingsObj != null) {
				myMeetingsObj.meetingObj = TableItems[indexPath.Row];
//					myMeetingsObj.customer = new Customer();
//					myMeetingsObj.customer.Company = owner.localLeads.COMPANY_NAME;
//					myMeetingsObj.customer.LeadId = owner.localLeads.LEAD_ID;
//					myMeetingsObj.customer.Name = owner.localLeads.LEAD_NAME;
				owner.NavigationController.PushViewController(myMeetingsObj,true);
			}
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 90.0f;
		}
	}
	}
}


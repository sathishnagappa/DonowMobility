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
using EventKit;
using System.Threading.Tasks;

namespace donow.iOS
{
	partial class prospectDetailsVC : UIViewController
	{
		public Leads localLeads;
		//List<Broker> brokerList;
		Prospect prospectDetails;
		protected CreateEventEditViewDelegate eventControllerDelegate;
		public List<UserMeetings> listMeeting;
		public List<UserMeetings> UCommingMeetinglist;
		public List<UserMeetings> PreviousMeetingsList;

		public prospectDetailsVC (IntPtr handle) : base (handle)
		{
		}

		public override async void ViewWillAppear (bool animated)
		{			
			base.ViewWillAppear (animated);
			//AppDelegate.IsProspectVisited = true;
			//UpdateSalesStage();
			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);

			LoadData ();
			await LoadCustomerAndMeetingInfo ();
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
			
			ScrollViewProspectDetails.ContentSize = new CGSize (375.0f, 2300.0f);

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
					this.PresentViewController (mailController, true, null);
					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						switch(args.Result)
						{
						case MFMailComposeResult.Cancelled: 							
							break;
						case MFMailComposeResult.Saved: 
							break;
						case MFMailComposeResult.Sent: 	
											CustomerInteraction customerinteract = new CustomerInteraction();
											customerinteract.CustomerName =  localLeads.LEAD_NAME;
											customerinteract.UserId = AppDelegate.UserDetails.UserId;
											customerinteract.Type = "Email";
											customerinteract.DateNTime = DateTime.Now.ToString();
											customerinteract.LeadID = localLeads.LEAD_ID;
											AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);				

											// Xamarin Insights tracking
											Insights.Track("Save CutomerInteraction", new Dictionary <string,string>{
												{"UserId", customerinteract.UserId.ToString()},
												{"CustomerName", customerinteract.CustomerName},
												{"Type", "Email"}
											});
							break;
						case MFMailComposeResult.Failed: 
							break;
						}
						args.Controller.DismissViewController (true, null);
					};
				}
			};

			ButtonUpdateProspect.TouchUpInside += (object sender, EventArgs e) => {
				InteractionLeadUpdateVC interactionLeadUpdateVC = this.Storyboard.InstantiateViewController ("InteractionLeadUpdateVC") as InteractionLeadUpdateVC;
				if (interactionLeadUpdateVC != null) {
					interactionLeadUpdateVC.leadObj = localLeads;
					this.PresentViewController (interactionLeadUpdateVC, true, null);
				}
			};

			ButtonNumberOfDealmakers.TouchUpInside += (object sender, EventArgs e) =>  {
				callDealMakerList();
			};

			ButtonCalendarEvent.TouchUpInside += (object sender, EventArgs e) => {
				//CalenderHomeDVC calendarHomeDV = new CalenderHomeDVC ();
				//this.NavigationController.PushViewController(calendarHomeDV, true);
					LaunchCreateNewEvent();
			
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

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			TableViewEmails.Source = null;
			TableViewMeetings.Source = null;
			TableViewPreviousMeetings.Source = null;
			listMeeting = null;
			UCommingMeetinglist = null;
			PreviousMeetingsList = null;
		}

		void LoadData()
		{			
			prospectDetails = AppDelegate.leadsBL.GetProspectDetails(localLeads.LEAD_ID,AppDelegate.UserDetails.UserId,localLeads.LEAD_SOURCE);
			//prospectDetails = AppDelegate.leadsBL.GetProspectDetails(localLeads.LEAD_ID,AppDelegate.UserDetails.UserId);

			List<BingResult> bingResult = AppDelegate.customerBL.GetBingResult (prospectDetails.COMPANY_NAME + " News");
			TableViewLatestNews.Source = new CustomerIndustryTableSource(bingResult, this);

			listMeeting = prospectDetails.UserMeetingList;
			UCommingMeetinglist = new List<UserMeetings>();
			PreviousMeetingsList = new List<UserMeetings>(); 

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
			if (prospectDetails.LEAD_SOURCE == 2) {
				LabelLeadScore.Text = prospectDetails.LEAD_SCORE == 0 ? "NA" : prospectDetails.LEAD_SCORE.ToString(); 
				LabelLeadSource.Text = "SFDC";
				ButtonPassProspect.Hidden = true;
				ButtonUpdateProspect.Frame = new CGRect (25, 2245, this.View.Bounds.Size.Width - 50, 40);
			} else {
				LabelLeadScore.Text = prospectDetails.LEAD_SCORE.ToString();
				LabelLeadSource.Text = "dealtrio";
				ButtonPassProspect.Hidden =  false;
			}
			LabelProspectCityandState.Text = prospectDetails.CITY + coma + prospectDetails.STATE;
			LabelCustomerVsProspect.Text = prospectDetails.LEAD_TYPE == "Y" ? "Existing Customer" : "New Prospect" ;
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
				CITY = prospectDetails.CITY, STATE = prospectDetails.STATE, SFDCLEAD_ID = prospectDetails.SFDCLEAD_ID
			};

			UpdateSalesStage ();
		}

		async Task LoadCustomerAndMeetingInfo () {

			//			List<BingResult> bingResult = AppDelegate.customerBL.GetBingResult (customerDetails.Name + " News");
			//			TableViewLatestNews.Source = new CustomerIndustryTableSource(bingResult, this);

			string[] customerNameArray = prospectDetails.COMPANY_NAME.Split ();
			//string searchText = customerNameArray [0].Length == 1 ? customerNameArray [1] : customerNameArray [0];
			string searchText = customerNameArray [0].Length == 1 ? customerNameArray [1] : (customerNameArray [0] + " " + customerNameArray [1]);
			//string searchText = customerNameArray.Count() == 1 ? customerNameArray [0] : (customerNameArray [0] + customerNameArray [1]);
			//List<TwitterStream>  twitterStream =  await TwitterUtil.Search (searchText.ToLower());
			List<TwitterStream>  twitterStream =  await TwitterUtil.Search (searchText.ToLower());
			List<TwitterStream> twitterStreamwithKeyword = new List<TwitterStream>();
			if(twitterStream.Count > 0)
				//				twitterStreamwithKeyword =	twitterStream.Where(X => X.text.Contains("Business") || X.text.Contains("Sales") || X.text.Contains("Opportunities")
				//					|| X.text.Contains("Organization") || X.text.Contains("Launch") || X.text.Contains("Money") || X.text.Contains("Tools") || X.text.Contains("Competition")
				//					|| X.text.Contains("Interest") || X.text.Contains("Industry") || X.text.Contains("Learning")).ToList();	
				twitterStreamwithKeyword = twitterStream;	

			TableViewLatestCustomerInfo.Source = new CustomerInfoTableSource(twitterStreamwithKeyword,this);
			TableViewLatestCustomerInfo.ReloadData ();
			//loadingOverlay.Hide();
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
			if (prospectDetails.LEAD_STATUS.Equals ("Closed Won")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_ClosedWon.png");
			} else if (prospectDetails.LEAD_STATUS.Equals ("Proposal Negotiation")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_ProposalNegotiation.png");
			}else if (prospectDetails.LEAD_STATUS.Equals("Connection Made")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_ConnectionMade.png");
			} else if (prospectDetails.LEAD_STATUS.Equals("Working")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Working.png");
			} else {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_New.png");
			}
		}

		void showBrokerImage (int count) {
			if (count > 0) {
				ButtonNumberOfDealmakers.Hidden = false;
				ImageShowMore.Hidden = false;
			} else {
				ButtonNumberOfDealmakers.Hidden = true;
				ImageShowMore.Hidden = true;
			}
			LabelNumberOfDealMaker.Text = "No. of Dealmakers Found: " + count.ToString ();
		}

		protected void LaunchCreateNewEvent ()
		{
			// create a new EKEventEditViewController. This controller is built in an allows
			// the user to create a new, or edit an existing event.
			AppDelegate.EventStore.RequestAccess (EKEntityType.Event, (bool granted, NSError e) => {

				EventKitUI.EKEventEditViewController eventController =
					new EventKitUI.EKEventEditViewController ();
				InvokeOnMainThread (() => { 
//					EKEvent newEvent = EKEvent.FromStore (AppDelegate.EventStore);
//					newEvent.Title = "Get outside and do some exercise!";
//					newEvent.Notes = "This is your motivational event to go and do 30 minutes of exercise. Super important. Do this.";
//					newEvent.Location = "Seattle,WA";
				// set the controller's event store - it needs to know where/how to save the event
				eventController.EventStore = AppDelegate.EventStore;
//					eventController.Event = newEvent;
				// wire up a delegate to handle events from the controller
				eventControllerDelegate = new CreateEventEditViewDelegate (eventController);
				eventController.EditViewDelegate = eventControllerDelegate;

				// show the event controller
				PresentViewController (eventController, true, null);
				});
				//NavigationController.PushViewController (calendarListScreen, true);
			});

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

	public class CustomerIndustryTableSource : UITableViewSource {

		List<BingResult> TableItems;
		string CellIdentifier = "TableCell";

		prospectDetailsVC owner;

		public CustomerIndustryTableSource (List<BingResult> meetingList, prospectDetailsVC owner)
		{
			TableItems = meetingList;
			this.owner = owner;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return 5;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			BingResult item = TableItems[indexPath.Row];

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }
			cell.ImageView.Image = UIImage.FromBundle("bing_icon.png");
			cell.ImageView.Frame = new CGRect (25, 15, 40, 35);
			cell.TextLabel.Text = item.Title;
			cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			cell.TextLabel.Lines = 0;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
			BingSearchVC bingSearchVC = owner.Storyboard.InstantiateViewController ("BingSearchVC") as BingSearchVC;
			if (bingSearchVC != null) {
				bingSearchVC.webURL = TableItems [indexPath.Row].Url;
				owner.NavigationController.PushViewController (bingSearchVC, true);
			}
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 50.0f;
		}
	}
	public class CustomerInfoTableSource : UITableViewSource {

		List<TwitterStream> TableItems;
		string CellIdentifier = "TableCellCusomerInfo";
		prospectDetailsVC owner;

		public CustomerInfoTableSource (List<TwitterStream> twitterstream, prospectDetailsVC customerProfileObj)
		{
			TableItems = twitterstream;
			this.owner = customerProfileObj;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return TableItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			TwitterStream item = TableItems[indexPath.Row];

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

			//cell.ImageView.Frame = new CGRect (25, 5, 33, 33);
			cell.ImageView.Frame = new CGRect (25, 15, 40, 35);
			cell.ImageView.Image = UIImage.FromBundle("twitter_icon.png");
			cell.TextLabel.Text = item.text;
			cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			cell.TextLabel.Lines = 0;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{		

			tableView.DeselectRow (indexPath, true);
			if (!string.IsNullOrEmpty (TableItems [indexPath.Row].url)) {
				BingSearchVC bingSearchVC = owner.Storyboard.InstantiateViewController ("BingSearchVC") as BingSearchVC;
				if (bingSearchVC != null) {					
					bingSearchVC.webURL = TableItems [indexPath.Row].url;
					owner.NavigationController.PushViewController (bingSearchVC, true);
				}
			} else { 
				UIAlertView alert = new UIAlertView ("", "No link available for this feed.", null, "Ok", null);
				alert.Show ();
			}
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 100.0f;
		}

	}
}

	public class CreateEventEditViewDelegate : EventKitUI.EKEventEditViewDelegate
	{
		// we need to keep a reference to the controller so we can dismiss it
		protected EventKitUI.EKEventEditViewController eventController;

		public CreateEventEditViewDelegate (EventKitUI.EKEventEditViewController eventController)
		{
			// save our controller reference
			this.eventController = eventController;
		}

		void AddEvent(EKEvent calendarEvent)
		{
			UserMeetings userMeetings = new UserMeetings ();
				userMeetings.Id = 0;
				userMeetings.LeadId = AppDelegate.CurrentLead.LEAD_ID;
				userMeetings.UserId = AppDelegate.UserDetails.UserId;
				userMeetings.Subject = calendarEvent.Title;
				userMeetings.StartDate = DateTime.SpecifyKind(DateTime.Parse(calendarEvent.StartDate.ToString()),DateTimeKind.Local).ToString();
				userMeetings.EndDate = DateTime.SpecifyKind(DateTime.Parse(calendarEvent.EndDate.ToString()),DateTimeKind.Local).ToString();
				userMeetings.CustomerName = AppDelegate.CurrentLead.LEAD_NAME;
				userMeetings.City = AppDelegate.CurrentLead.CITY;
				userMeetings.State = AppDelegate.CurrentLead.STATE;
				userMeetings.Status = "";
				userMeetings.Comments = "";
				userMeetings.SFDCLead_ID = AppDelegate.CurrentLead.SFDCLEAD_ID;
	    
			AppDelegate.leadsBL.SaveMeetingEvent (userMeetings);
			AppDelegate.UserDetails.MeetingCount = AppDelegate.UserDetails.MeetingCount + 1;
			//Xamarin Insights tracking
			Insights.Track ("SaveMeetingEvent", new Dictionary <string,string> {
				{ "LeadId", userMeetings.LeadId.ToString () },
				{ "UserId", userMeetings.UserId.ToString () },
				{ "Subject", userMeetings.Subject },
				{ "CustomerName", userMeetings.CustomerName }
			});
		}

		// completed is called when a user eith
		public override void Completed (EventKitUI.EKEventEditViewController controller, EventKitUI.EKEventEditViewAction action)
		{				
			eventController.DismissViewController (true, null);

			// action tells you what the user did in the dialog, so you can optionally
			// do things based on what their action was. additionally, you can get the
			// Event from the controller.Event property, so for instance, you could
			// modify the event and then resave if you'd like.
			switch (action) {

			case EventKitUI.EKEventEditViewAction.Canceled:
				break;
			case EventKitUI.EKEventEditViewAction.Deleted:
				break;
			case EventKitUI.EKEventEditViewAction.Saved:
				// if you wanted to modify the event you could do so here, and then
				// save:
				//AppDelegate.EventStore.SaveEvent ( controller.Event, )
				AddEvent(controller.Event);
				break;
			}
		}
	}

}
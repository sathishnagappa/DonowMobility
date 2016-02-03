using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UIKit;
using donow.PCL.Model;
using donow.PCL;
using CoreGraphics;
using donow.Services;
using MessageUI;
using System.Linq;
using Xamarin;
using System.Threading.Tasks;
using donow.Util;


namespace donow.iOS

{
	partial class customerProfileVC : UIViewController
	{
		public customerProfileVC (IntPtr handle) : base (handle)
		{
		}
		LoadingOverlay loadingOverlay;
		public Customer customer;
		CustomerDetails customerDetails;
		public bool TableSeeAllClicked = false;

		public override void ViewWillDisappear (bool animated)
		{			
			base.ViewWillDisappear (animated);
			//this.Dispose ();

		}

		protected override void Dispose (bool disposing)
		{
			if (TableViewEmails.Source != null) {
				TableViewEmails.Source.Dispose ();
				TableViewEmails.Source = null;
			}
			if (TableViewDealHistory.Source != null) {
				TableViewDealHistory.Source.Dispose ();
				TableViewDealHistory.Source = null;
			}
			if (TableViewMeetings.Source != null) {
				TableViewMeetings.Source.Dispose ();
				TableViewMeetings.Source = null;
			}
			if (TableViewPreviousMeetings.Source != null) {
				TableViewPreviousMeetings.Source.Dispose ();
				TableViewPreviousMeetings.Source = null;
			}
			base.Dispose (disposing);
		}

//		public override void ViewDidUnload ()
//		{
//			if (TableViewEmails.Source != null)
//				TableViewEmails.Source.Dispose ();
//			if (TableViewDealHistory.Source != null)
//				TableViewDealHistory.Source.Dispose ();
//			if (TableViewMeetings.Source != null)
//				TableViewMeetings.Source.Dispose ();
//			if (TableViewPreviousMeetings.Source != null)
//				TableViewPreviousMeetings.Source.Dispose ();
//			base.ViewDidUnload ();
//		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{				
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			this.Title = "Customer Profile";

			loadingOverlay = new LoadingOverlay(this.View.Bounds);
			this.View.Add(loadingOverlay);

			LoadScreenData ();

			//int brokerWorkingWith = AppDelegate.brokerBL.GetBrokerForProspect (customer.LeadId).Where(X => X.Status ==4).ToList().Count;
			 
			//int  brokerWorkingWith =  AppDelegate.brokerBL.GetBrokerForStatus(customer.LeadId,4).Count;

			DealMakersImage1.Hidden = customerDetails.broker != null ? false : true;			
			LabelIndustry.Text = customerDetails.CompanyInfo;
			LabelLineOfBusiness.Text = customerDetails.BusinessNeeds;
//			LabelCustomerVsProspect.Text = customerDetails == "Y" ? "Existing Customer" : "New Prospect" ;

//			ButtonSeeAllPreviousMeetings.TouchUpInside += (object sender, EventArgs e) =>  {
//
//				TableSeeAllClicked = true;
//				TableViewMeetings.ReloadData ();
//			};
			
			
			ButtonPhone.TouchUpInside += (object sender, EventArgs e) => {
				var url = new NSUrl ("tel://" + customerDetails.Phone);
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				};
				CustomerInteraction customerinteract = new CustomerInteraction();
				customerinteract.CustomerName =  customerDetails.Name;
				customerinteract.UserId = AppDelegate.UserDetails.UserId;
				customerinteract.Type = "Phone";
				customerinteract.DateNTime = DateTime.Now.ToString();
				customerinteract.LeadID = customerDetails.LeadId;
				AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
				//Xamarin Insights tracking
				Insights.Track("SaveCutomerInteraction", new Dictionary <string,string>{
					{"UserId", customerinteract.UserId.ToString()},
					{"CustomerName", customerinteract.CustomerName}
				});
			};

			ButtonMail.TouchUpInside += (object sender, EventArgs e) => {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{customerDetails.Email});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						CustomerInteraction customerinteract = new CustomerInteraction();
						customerinteract.CustomerName =  customerDetails.Name;
						customerinteract.UserId = AppDelegate.UserDetails.UserId;
						customerinteract.Type = "Email";
						customerinteract.DateNTime = DateTime.Now.ToString();
						customerinteract.LeadID = customerDetails.LeadId;
						AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
						args.Controller.DismissViewController (true, null);
					};

					this.PresentViewController (mailController, true, null);
				}
			};

			await LoadCustomerAndMeetingInfo ();
		}

		async Task LoadCustomerAndMeetingInfo () {

			List<BingResult>  bingResult =  AppDelegate.customerBL.GetBingResult (customerDetails.Name + " News");
			TableViewLatestNews.Source = new CustomerIndustryTableSource(bingResult, this);

			string[] customerNameArray = customerDetails.Company.Split ();
			string searchText = customerNameArray [0].Length == 1 ? customerNameArray [1] : customerNameArray [0];
			List<TwitterStream>  twitterStream =  await TwitterUtil.Search (searchText.ToLower());
			List<TwitterStream> twitterStreamwithKeyword = new List<TwitterStream>();
			if(twitterStream.Count > 0)
				twitterStreamwithKeyword =	twitterStream.Where(X => X.text.Contains("Business") || X.text.Contains("Sales") || X.text.Contains("Opportunities")
					|| X.text.Contains("Organization") || X.text.Contains("Launch") || X.text.Contains("Money") || X.text.Contains("Tools") || X.text.Contains("Competition")
					|| X.text.Contains("Interest") || X.text.Contains("Industry") || X.text.Contains("Learning")).ToList();		

			TableViewLatestCustomerInfo.Source = new CustomerInfoTableSource(twitterStreamwithKeyword);
			TableViewLatestCustomerInfo.ReloadData ();
			loadingOverlay.Hide();
		}

		static UIImage FromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data); 
		}

		void LoadScreenData()
		{
			customerDetails = AppDelegate.customerBL.GetCustomersDetails(customer.LeadId,AppDelegate.UserDetails.UserId);

			List<UserMeetings> listMeeting = customerDetails.UserMeetingList;
			List<UserMeetings> UCommingMeetinglist = new List<UserMeetings>();
			List<UserMeetings> PreviousMeetingsList = new List<UserMeetings>(); 

			foreach(var item in listMeeting)
			{
				if (DateTime.Compare (DateTime.Parse (item.EndDate), DateTime.Now) > 0) {
					UCommingMeetinglist.Add (item);
				} else {
					PreviousMeetingsList.Add (item);
				}
			}	

			ScrollViewCustomerProfile.ContentSize = new CGSize (375.0f, 2600.0f);

			if(customerDetails.customerInteractionList  != null && customerDetails.customerInteractionList.Count !=0)
				TableViewEmails.Source = new TableSourceInteractionWithCustomer (customerDetails.customerInteractionList , this);
			if(customerDetails.dealHistoryList != null && customerDetails.dealHistoryList.Count !=0)
				TableViewDealHistory.Source = new TableSourceDealHistory(customerDetails.dealHistoryList); 
			if(UCommingMeetinglist.Count != 0)
				TableViewMeetings.Source = new TableSourceupComingMeetings (UCommingMeetinglist,this);
			if(PreviousMeetingsList.Count !=0)
				TableViewPreviousMeetings.Source = new TableSourcePreviousMeetings (PreviousMeetingsList,this);

			LabelCompanyName.Text = customerDetails.Company;
			LabelCustomerName.Text = customerDetails.Name;
			LabelCityAndState.Text = customerDetails.City + ", " + customerDetails.State;
			LabelScore.Text = customerDetails.LeadScore.ToString();
			LabelSource.Text = customerDetails.LeadSource == 2 ? "SFDC" : "DoNow" ;

		}

//		public class TableSourceBtwnYouNCustomer : UITableViewSource {
//
//			string CellIdentifier = "BtwnYouNCustomerTableCell";
////			List<Leads> TableItems;
//			customerProfileVC owner;
//
//			public TableSourceBtwnYouNCustomer (/*List<Leads> items, customerProfileVC owner*/)
//			{
////				TableItems = items;
//				this.owner = owner;
//			}
//
//			public override nint RowsInSection (UITableView tableview, nint section)
//			{
////				if(owner.TableSeeAllClicked == false) {
//					return 2;
////				} else {
////					return TableItems.Count;
////				}
//			}
//
//			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
//			{				
//				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellBtwnYouNCustomer;
//
//				if (cell == null) {
//					cell = new CustomerViewTableCellBtwnYouNCustomer (CellIdentifier);
//				}
//
////				Leads leadsObj = TableItems[indexPath.Row];
//				cell.UpdateCell();
//				return cell;
//			}
//
//			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
//			{
//
////				tableView.DeselectRow (indexPath, true);
////				LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
////				if (leadDetailVC != null) {
////					leadDetailVC.leadObj = TableItems[indexPath.Row];
////					//owner.View.AddSubview (leadDetailVC.View);
////					owner.NavigationController.PushViewController(leadDetailVC,true);
////				}
//			}
//
//			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
//			{
//				return 100.0f;
//			}
//		}

		public class TableSourceInteractionWithCustomer : UITableViewSource {

			string CellIdentifier = "InteractionWithCustomer";
			List<CustomerInteraction> TableItems;
			//customerProfileVC owner;

			public TableSourceInteractionWithCustomer (List<CustomerInteraction> items, customerProfileVC owner)
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
			customerProfileVC owner;
				
			public TableSourceupComingMeetings (List<UserMeetings> items, customerProfileVC owner)
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
					myMeetingsObj.customer = owner.customer;
					owner.NavigationController.PushViewController(myMeetingsObj,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}
		}

		public class TableSourcePreviousMeetings : UITableViewSource {
			string CellIdentifier = "PreviousMeetingsCell";
			List<UserMeetings> TableItems;
			customerProfileVC owner;

			public TableSourcePreviousMeetings (List<UserMeetings> items, customerProfileVC owner)
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
					myMeetingsObj.customer = owner.customer;
					owner.NavigationController.PushViewController(myMeetingsObj,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}
		}

		public class TableSourceDealHistory : UITableViewSource {

			string CellIdentifier = "LeadsTableCell";
			List<DealHistroy> TableItems;

			public TableSourceDealHistory (List<DealHistroy> items)
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
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewDealHistory;

				if (cell == null) {
					cell = new CustomerViewDealHistory (CellIdentifier);
				}


				cell.UpdateCell(TableItems[indexPath.Row]);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);

			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 110.0f;
			}
		}

		public class CustomerInfoTableSource : UITableViewSource {

			List<TwitterStream> TableItems;
			string CellIdentifier = "TableCellCusomerInfo";

			public CustomerInfoTableSource (List<TwitterStream> twitterstream)
			{
				TableItems = twitterstream;
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
				cell.ImageView.Image = FromUrl(item.profile_image_url);
				cell.TextLabel.Text = item.text;
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{			

				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}

		}

		public class CustomerIndustryTableSource : UITableViewSource {

			List<BingResult> TableItems;
			string CellIdentifier = "TableCell";

			customerProfileVC owner;

			public CustomerIndustryTableSource (List<BingResult> meetingList, customerProfileVC owner)
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
				cell.ImageView.Frame = new CGRect (25, 15, 40, 35);
				cell.ImageView.Image = UIImage.FromBundle("Article 1 Thumb.png");
				cell.TextLabel.Text = item.Title;
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				BingSearchVC bingSearchVC = owner.Storyboard.InstantiateViewController ("BingSearchVC") as BingSearchVC;
				if (bingSearchVC != null) {
					bingSearchVC.webURL = TableItems [indexPath.Row].Url;
					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController (bingSearchVC, true);
				}
				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 105.0f;
			}
		}
	}
}

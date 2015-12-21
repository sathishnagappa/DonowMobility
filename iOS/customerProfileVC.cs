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

namespace donow.iOS

{
	partial class customerProfileVC : UIViewController
	{
		public customerProfileVC (IntPtr handle) : base (handle)
		{
		}

		public Customer customer;
		public bool TableSeeAllClicked = false;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				//LandingCustomerVC customerPage = this.Storyboard.InstantiateViewController ("landingCustomerVC") as LandingCustomerVC;
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;

			this.Title = "Customer";

			LoadScreenData ();

			int brokerWorkingWith = AppDelegate.brokerBL.GetBrokerForProspect (customer.LeadId).Where(X => X.Status ==4).ToList().Count;

			DealMakersImage1.Hidden = brokerWorkingWith > 0 ? true : false;			
			LabelIndustry.Text = customer.CompanyInfo;
			LabelLineOfBusiness.Text = customer.BusinessNeeds;

			ButtonSeeAllPreviousMeetings.TouchUpInside += (object sender, EventArgs e) =>  {

				TableSeeAllClicked = true;
				TableViewMeetings.ReloadData ();
			};
			
			
			ButtonPhone.TouchUpInside += (object sender, EventArgs e) => {
				var url = new NSUrl ("tel://" + customer.Phone);
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				};
				CustomerInteraction customerinteract = new CustomerInteraction();
				customerinteract.CustomerName =  customer.Name;
				customerinteract.UserId = AppDelegate.UserDetails.UserId;
				customerinteract.Type = "Phone";
				customerinteract.DateNTime = DateTime.Now.ToString();
				AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
			};

			ButtonMail.TouchUpInside += (object sender, EventArgs e) => {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{customer.Email});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("Hello <Insert Name>,\n\nMy name is [My Name] and I head up business development efforts with [My Company]. \n\nI am taking an educated stab here and based on your profile, you appear to be an appropriate person to connect with.\n\nI’d like to speak with someone from [Company] who is responsible for [handling something that's relevant to my product]\n\nIf that’s you, are you open to a fifteen minute call on _________ [time and date] to discuss ways the [Company Name] platform can specifically help your business? If not you, can you please put me in touch with the right person?\n\nI appreciate the help!\n\nBest,\n\n[Insert Name]", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						CustomerInteraction customerinteract = new CustomerInteraction();
						customerinteract.CustomerName =  customer.Name;
						customerinteract.UserId = AppDelegate.UserDetails.UserId;
						customerinteract.Type = "Email";
						customerinteract.DateNTime = DateTime.Now.ToString();
						AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
						Console.WriteLine (args.Result.ToString ());
						args.Controller.DismissViewController (true, null);
					};

					this.PresentViewController (mailController, true, null);
				}
			};
		}

		void LoadScreenData()
		{

			List<CustomerInteraction> customerInteractionList = AppDelegate.customerBL.GetCustomerInteraction (customer.Name,AppDelegate.UserDetails.UserId);


			List<UserMeetings> listMeeting = new List<UserMeetings> ();
			listMeeting = AppDelegate.userBL.GetMeetings(customer.Name);
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

			List<DealHistroy> listDealHistory = new List<DealHistroy> ();
			listDealHistory = AppDelegate.customerBL.GetDealHistroy (customer.LeadId, AppDelegate.UserDetails.UserId);

			ScrollViewCustomerProfile.ContentSize = new CGSize (414.0f, 2074.0f);

			if(customerInteractionList.Count !=0)
				TableViewEmails.Source = new TableSourceInteractionWithCustomer (customerInteractionList, this);
			if(listDealHistory.Count !=0)
				TableViewDealHistory.Source = new TableSourceDealHistory(listDealHistory,this);
			if(UCommingMeetinglist.Count != 0)
				TableViewMeetings.Source = new TableSourceupComingMeetings (UCommingMeetinglist,this);
			if(PreviousMeetingsList.Count !=0)
				TableViewPreviousMeetings.Source = new TableSourcePreviousMeetings (PreviousMeetingsList,this);

			LabelCompanyName.Text = customer.Company;
			LabelCustomerName.Text = customer.Name;
			LabelCityAndState.Text = customer.City + ", " + customer.State;
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
			customerProfileVC owner;

			public TableSourceInteractionWithCustomer (List<CustomerInteraction> items, customerProfileVC owner)
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
				return 100.0f;
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
					owner.NavigationController.PushViewController(myMeetingsObj,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
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
					owner.NavigationController.PushViewController(myMeetingsObj,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
			}
		}

		public class TableSourceDealHistory : UITableViewSource {

			string CellIdentifier = "LeadsTableCell";
			List<DealHistroy> TableItems;
			customerProfileVC owner;

			public TableSourceDealHistory (List<DealHistroy> items, customerProfileVC owner)
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
				return 150.0f;
			}
		}
	}
}

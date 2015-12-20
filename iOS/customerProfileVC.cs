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

			//Leads = AppDelegate.leadsBL.GetLeadsDetails (customer.CustomerId);

			CustomerBL customerbl = new CustomerBL ();
			List<CustomerInteraction> customerInteractionList = customerbl.GetCustomerInteraction (customer.Name,AppDelegate.UserDetails.UserId);


			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				LandingCustomerVC customerPage = this.Storyboard.InstantiateViewController ("landingCustomerVC") as LandingCustomerVC;
				this.NavigationController.PushViewController(customerPage,true);
			};
			NavigationItem.LeftBarButtonItem = btn;

			this.Title = "Customer";
			UserBL userbl = new UserBL ();
			List<UserMeetings> listMeeting = new List<UserMeetings> ();
			listMeeting =  userbl.GetMeetings(customer.Name);

			List<UserMeetings> UpComingMeetings = (from item in listMeeting
					where DateTime.Compare (DateTime.Parse(item.EndDate), DateTime.Now) >= 0
				select item).ToList();

			List<UserMeetings> PreviousMeetings = (from item in listMeeting
					where DateTime.Compare (DateTime.Parse(item.EndDate), DateTime.Now) < 0
				select item).ToList();

			List<DealHistroy> listDealHistory = new List<DealHistroy> ();
			listDealHistory = customerbl.GetDealHistroy (customer.Name, AppDelegate.UserDetails.UserId);

			ScrollViewCustomerProfile.ContentSize = new CGSize (414.0f, 2074.0f);

			TableViewEmails.Source = new TableSourceInteractionWithCustomer (customerInteractionList, this);
			//TableViewDealHistory.Source = new TableSourceDealHistory (listDealHistory,this);
			TableViewMeetings.Source = new TableSourceupComingMeetings (UpComingMeetings,this);
			TableViewPreviousMeetings.Source = new TableSourcePreviousMeetings (PreviousMeetings,this);

			LabelCompanyName.Text = customer.Company;
			LabelCustomerName.Text = customer.Name;
			LabelCityAndState.Text = customer.City + ", " + customer.State;

			ButtonSeeAllLeadsTable.TouchUpInside += (object sender, EventArgs e) =>  {

				TableSeeAllClicked = true;
				TableViewNewLeads.ReloadData ();

			};
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
			List<UserMeetings> items;
			customerProfileVC owner;
				
			public TableSourceupComingMeetings (List<UserMeetings> items, customerProfileVC owner)
			{
				items = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return items.Count;

			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellUpcomingMeetings;

				if (cell == null) {
					cell = new CustomerViewTableCellUpcomingMeetings (CellIdentifier);
				}

				cell.UpdateCell(items[indexPath.Row]);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				MyMeetingsVC myMeetingsObj = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				if (myMeetingsObj != null) {
					myMeetingsObj.meetingObj = items[indexPath.Row];
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
			List<UserMeetings> items;
			customerProfileVC owner;

			public TableSourcePreviousMeetings (List<UserMeetings> items, customerProfileVC owner)
			{
				items = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return items.Count;

			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellUpcomingMeetings;

				if (cell == null) {
					cell = new CustomerViewTableCellUpcomingMeetings (CellIdentifier);
				}

				cell.UpdateCell(items[indexPath.Row]);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				MyMeetingsVC myMeetingsObj = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				if (myMeetingsObj != null) {
					myMeetingsObj.meetingObj = items[indexPath.Row];
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
				var cell = tableView.DequeueReusableCell (CellIdentifier) as Customer360TableCell;

				if (cell == null) {
					cell = new Customer360TableCell (CellIdentifier);
				}


				//cell.UpdateCell(leadsObj);
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

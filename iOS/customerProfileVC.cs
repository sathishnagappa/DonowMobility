using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UIKit;
using donow.PCL.Model;
using donow.PCL;
using CoreGraphics;
using donow.Services;

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

//			List <Leads> ListLeads = new List <Leads> ();

			LeadsBL leadsVC = new LeadsBL ();
			List <Leads> ListLeads = leadsVC.GetAllLeads(AppDelegate.UserDetails.UserId);



//			LeadsBL leadsVC = new LeadsBL ();
//			ListLeads = leadsVC.GetAllLeads(AppDelegate.UserDetails.UserId);


//			RestService restservice = new RestService ();
			//restservice.GetBingResult ("DoNow Market Trends");

			this.Title = "Customer";
//			UserBL userbl = new UserBL ();
//			List<UserMeetings> listMeeting = new List<UserMeetings> ();
//			listMeeting =  userbl.GetMeetings(customer.Name);

			//List<UserMeetings>
			ScrollViewCustomerProfile.ContentSize = new CGSize (414.0f, 2074.0f);

			//TableViewNewLeads.Source = new TableSource (ListLeads, this);
			TableViewMeetings.Source = new TableSourceupComingMeetings (this);
			TableViewEmails.Source = new TableSourceInteractionWithCustomer ();
			TableViewDealHistory.Source = new TableSourceBtwnYouNCustomer ();
			TableViewPreviousMeetings.Source = new TableSourceupComingMeetings (this);

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
		}

		public class TableSourceBtwnYouNCustomer : UITableViewSource {

			string CellIdentifier = "BtwnYouNCustomerTableCell";
//			List<Leads> TableItems;
			customerProfileVC owner;

			public TableSourceBtwnYouNCustomer (/*List<Leads> items, customerProfileVC owner*/)
			{
//				TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
//				if(owner.TableSeeAllClicked == false) {
					return 2;
//				} else {
//					return TableItems.Count;
//				}
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellBtwnYouNCustomer;

				if (cell == null) {
					cell = new CustomerViewTableCellBtwnYouNCustomer (CellIdentifier);
				}

//				Leads leadsObj = TableItems[indexPath.Row];
				cell.UpdateCell();
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

//				tableView.DeselectRow (indexPath, true);
//				LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
//				if (leadDetailVC != null) {
//					leadDetailVC.leadObj = TableItems[indexPath.Row];
//					//owner.View.AddSubview (leadDetailVC.View);
//					owner.NavigationController.PushViewController(leadDetailVC,true);
//				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}
		}

		public class TableSourceInteractionWithCustomer : UITableViewSource {

			string CellIdentifier = "InteractionWithCustomer";
//			List<Leads> TableItems;
//			customerProfileVC owner;

			public TableSourceInteractionWithCustomer (/*List<Leads> items, customerProfileVC owner*/)
			{
//				TableItems = items;
//				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return 2;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellinteractionWithCustomerCell;

				if (cell == null) {
					cell = new CustomerViewTableCellinteractionWithCustomerCell (CellIdentifier);
				}

//				Leads leadsObj = TableItems[indexPath.Row];
				cell.UpdateCell();
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

//				tableView.DeselectRow (indexPath, true);
//				LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
//				if (leadDetailVC != null) {
//					leadDetailVC.leadObj = TableItems[indexPath.Row];
//					//owner.View.AddSubview (leadDetailVC.View);
//					owner.NavigationController.PushViewController(leadDetailVC,true);
//				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}
		}

		public class TableSourceupComingMeetings : UITableViewSource {
			string CellIdentifier = "upComingMeetingsCell";
//			List<UserMeetings> items;
			customerProfileVC owner;
				
			public TableSourceupComingMeetings (/*List<UserMeetings> items,*/ customerProfileVC owner)
			{
//				items = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
//				return items.Count;
				return 1;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{				
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellUpcomingMeetings;

				if (cell == null) {
					cell = new CustomerViewTableCellUpcomingMeetings (CellIdentifier);
				}

//				UserMeetings leadsObj = items[indexPath.Row];
				cell.UpdateCell();
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				MyMeetingsVC myMeetingsObj = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				if (myMeetingsObj != null) {
//					myMeetingsObj.leadObj = items[indexPath.Row];
					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController(myMeetingsObj,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
			}
		}

		public class TableSource : UITableViewSource {

			string CellIdentifier = "LeadsTableCell";
			List<Leads> TableItems;
			customerProfileVC owner;

			public TableSource (List<Leads> items, customerProfileVC owner)
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

				Leads leadsObj = TableItems[indexPath.Row];
				cell.UpdateCell(leadsObj);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
				if (leadDetailVC != null) {
					leadDetailVC.leadObj = TableItems[indexPath.Row];
					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController(leadDetailVC,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
			}
		}
	}
}

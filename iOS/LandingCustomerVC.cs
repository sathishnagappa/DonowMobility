using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using CoreGraphics;

namespace donow.iOS
{
	partial class LandingCustomerVC : UIViewController
	{
		public LandingCustomerVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			//			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
			//			this.NavigationController.NavigationItem.SetLeftBarButtonItem( new UIBarButtonItem(UIImage.FromFile("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
			//				this.NavigationController.PopViewController(true);
			//			}), true);
		}

		public override void ViewDidLoad ()
		{
			CustomerBL customerBL = new CustomerBL ();
			List<Customer> cusotmerList =  customerBL.GetAllCustomers ();

			TableViewCustomerList.Source = new TableSource (cusotmerList, this);
			this.Title = "Customers";
		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			List<Customer> TableItems;
			LandingCustomerVC owner;

			public List<String> headerArray = new List <String>
			{
				"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O",
				"P","Q", "R", "S", "T", "U", "V","W", "X", "Y", "Z"
			};

			public TableSource (List<Customer> items, LandingCustomerVC owner)
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
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellLeads;
				Customer customerObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new CustomerViewTableCellLeads(CellIdentifier);
				}

				cell.UpdateCell(customerObj);
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				customerProfileVC customerDetailVC = owner.Storyboard.InstantiateViewController ("customerProfileVC") as customerProfileVC;
				if (customerDetailVC != null) {
					customerDetailVC.customer = TableItems[indexPath.Row];
					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController(customerDetailVC,true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}

//			public virtual UITableViewHeaderFooterView GetHeaderView (nint section) {
//
//				UIView headerView = new UIView ();
//				headerView.Frame = new CGRect (0, 0, 414, 40);
//
//				UILabel headerLabel = new UILabel ();
//				headerLabel.Frame = new CGRect (0, 0, 414, 40);
//
//				headerLabel.BackgroundColor = UIColor.LightGray;
//				headerView.AddSubview (headerLabel);
//
//				headerLabel.Text = headerArray[(int)section];
//
////				return headerView;
//			}
//
////			public virtual nint NumberOfRowsInSection (nint section) {
////
////
////			}
//
//			public virtual nint NumberOfSections () {
//				return 26;
//			}
		}
	}
}

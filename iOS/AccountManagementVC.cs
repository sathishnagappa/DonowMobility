using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using CoreGraphics;
using System.Collections.Generic;

namespace donow.iOS
{
	partial class AccountManagementVC : UIViewController
	{
		UserBL userBL;
		public AccountManagementVC (IntPtr handle) : base (handle)
		{
		}
		public bool isFromSignUp;
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (!isFromSignUp)
			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			
			this.NavigationController.SetNavigationBarHidden (false, false);
 			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (157, 50, 49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

		}

		public override void ViewDidLoad ()
		{
			AppDelegate.IsNewUser = true;
			TableViewCustomerStreamActivity.Hidden = true;

			ScrollViewAccountManager.ContentSize = new CGSize (414.0f, 1200.0f);

			ButtonInfoIndustryDropDown.Layer.BorderWidth = 1.0f;
			ButtonInfoCompanyDropDown.Layer.BorderWidth = 1.0f;
			ButtonInfoCustomersDropDown.Layer.BorderWidth = 1.0f;

			IndustryBL industry = new IndustryBL ();
			List<String> industryList = industry.GetIndustry ();

			ButtonInfoIndustryDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewCustomerStreamActivity.Hidden = false;
				TableViewCustomerStreamActivity.Frame = new CGRect (242,100,123,20);
				TableViewCustomerStreamActivity.Source = new IndustryTableSource (industryList, this);
				TableViewCustomerStreamActivity.ReloadData();
			};

			CustomerBL customer = new CustomerBL ();
			List<Customer> CompanyList = customer.GetAllCustomers();

			ButtonInfoCompanyDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewCustomerStreamActivity.Hidden = false;
				TableViewCustomerStreamActivity.Frame = new CGRect (242,151,123,20);
				TableViewCustomerStreamActivity.Source = new companyTableSource (CompanyList, this);
				TableViewCustomerStreamActivity.ReloadData();
			};
				
			ButtonInfoCustomersDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewCustomerStreamActivity.Hidden = false;
				TableViewCustomerStreamActivity.Frame = new CGRect (242,201,123,20);
				TableViewCustomerStreamActivity.Source = new CustomerTableSource (CompanyList, this);
				TableViewCustomerStreamActivity.ReloadData();
			};

			ButtonFinish.TouchUpInside += (object sender, EventArgs e) => {					
				userBL = new UserBL();
				//AppDelegate.UserDetails.UserId = 7;
				AppDelegate.UserDetails.UserId = userBL.CreateUser(AppDelegate.UserDetails);
				WelcomeVC welcomeVC = this.Storyboard.InstantiateViewController ("WelcomeVC") as WelcomeVC;
				if (welcomeVC != null) {
					this.NavigationController.PushViewController (welcomeVC, true);
				}
			};
		}

		public void UpdateControls (string Parameter, string TableType)
		{
			if (TableType == "Industry") {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoIndustryDropDown.SetTitle (Parameter, UIControlState.Normal);
			} else if (TableType == "Company") {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoCompanyDropDown.SetTitle(Parameter,UIControlState.Normal);
			} else {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoCustomersDropDown.SetTitle(Parameter,UIControlState.Normal);
			}
		}

		public class companyTableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			List<Customer> TableItems;
			AccountManagementVC owner;

			public companyTableSource (List<Customer> items, AccountManagementVC owner)
			{
				this.TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				string item = TableItems[indexPath.Row].Company;

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				cell.TextLabel.Text = item;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				owner.UpdateControls(TableItems[indexPath.Row].Company,"Company");
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 25.0f;
			}
		}

		public class IndustryTableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			IList<string> TableItems;
			AccountManagementVC owner;

			public IndustryTableSource (IList<string> items, AccountManagementVC owner)
			{
				this.TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				var item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				cell.TextLabel.Text = item;
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				owner.UpdateControls(TableItems[indexPath.Row],"Industry");
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 25.0f;
			}
		}

		public class CustomerTableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			IList<Customer> TableItems;
			AccountManagementVC owner;
		
			public CustomerTableSource (IList<Customer> items, AccountManagementVC owner)
			{
				this.TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				var item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				cell.TextLabel.Text = item.Name;
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				owner.UpdateControls(TableItems[indexPath.Row].Name,"Customer");
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 25.0f;
			}
		}

	}
}

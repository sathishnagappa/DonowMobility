using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using CoreGraphics;
using System.Collections.Generic;
using Xamarin;

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
			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
//				if (isFromSignUp) {
//					signUpOtherDetailsVC signUpOtherDetailsPage = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
//					this.NavigationController.PushViewController(signUpOtherDetailsPage,true);
//				}
//				else {
//					HambergerMenuVC hambergerVC = this.Storyboard.InstantiateViewController("HambergerMenuVC") as HambergerMenuVC;
//					this.NavigationController.PushViewController(hambergerVC,true);
//				}
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			AppDelegate.IsNewUser = true;
			TableViewCustomerStreamActivity.Hidden = true;

			ScrollViewAccountManager.ContentSize = new CGSize (414.0f, 1200.0f);

			ButtonInfoIndustryDropDown.Layer.BorderWidth = 2.0f;
			ButtonInfoIndustryDropDown.SetTitle ("Tech", UIControlState.Normal);
			ButtonInfoIndustryDropDown.Layer.BorderColor = UIColor.LightGray.CGColor; 
			ButtonInfoCompanyDropDown.Layer.BorderWidth = 2.0f;
			ButtonInfoCompanyDropDown.SetTitle ("Tech", UIControlState.Normal);
			ButtonInfoCompanyDropDown.Layer.BorderColor = UIColor.LightGray.CGColor; 
			ButtonInfoCustomersDropDown.Layer.BorderWidth = 2.0f;
			ButtonInfoCustomersDropDown.SetTitle ("Tech", UIControlState.Normal);
			ButtonInfoCustomersDropDown.Layer.BorderColor = UIColor.LightGray.CGColor; 

			TableViewCustomerStreamActivity.Layer.BorderWidth = 2.0f;
			TableViewCustomerStreamActivity.Layer.BorderColor = UIColor.LightGray.CGColor; 

			IndustryBL industry = new IndustryBL ();
			List<String> industryList = industry.GetIndustry ();

			ButtonInfoIndustryDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewCustomerStreamActivity.Hidden = false;
				TableViewCustomerStreamActivity.Frame = new CGRect (200,355,168,128);
				TableViewCustomerStreamActivity.Source = new IndustryTableSource (industryList, this);
				TableViewCustomerStreamActivity.ReloadData();
			};

			CustomerBL customer = new CustomerBL ();
			List<Customer> CompanyList = customer.GetAllCustomers();

			ButtonInfoCompanyDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewCustomerStreamActivity.Hidden = false;
				TableViewCustomerStreamActivity.Frame = new CGRect (200,410,168,128);
				TableViewCustomerStreamActivity.Source = new companyTableSource (CompanyList, this);
				TableViewCustomerStreamActivity.ReloadData();
			};
				
			ButtonInfoCustomersDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewCustomerStreamActivity.Hidden = false;
				TableViewCustomerStreamActivity.Frame = new CGRect (200,455,168,128);
				TableViewCustomerStreamActivity.Source = new CustomerTableSource (CompanyList, this);
				TableViewCustomerStreamActivity.ReloadData();
			};

			ButtonUserInfo.TouchUpInside += (object sender, EventArgs e) =>  {
				if (isFromSignUp) {
					this.NavigationController.PopViewController(false);
				} else {
					signUpOtherDetailsVC userInfo = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
					if(userInfo != null)
						this.NavigationController.PushViewController (userInfo, true);					
				}
			};

			ButtonFinish.TouchUpInside += (object sender, EventArgs e) => {
				userBL = new UserBL();

				AppDelegate.UserDetails.IsNewLeadNotificationRequired = SwitchNewLeads.Selected;
				AppDelegate.UserDetails.IsBusinessUpdatesRequired = SwitchBusinessUpdates.Selected;
				AppDelegate.UserDetails.IsCustomerFollowUpRequired = SwitchFollowUp.Selected;
				AppDelegate.UserDetails.IsReferralRequestRequired = SwitchReferralRequests.Selected;
				AppDelegate.UserDetails.IsMeetingRemindersRequired = SwitchMeetingReminders.Selected;
				 
				//AppDelegate.UserDetails.UserId = 7;
				if(AppDelegate.UserDetails.UserId == 0)
				{
					AppDelegate.UserDetails.UserId = userBL.CreateUser(AppDelegate.UserDetails);
					//Xamarin Insight tracking code
					Insights.Track("CreateUser", new Dictionary <string,string>{
						{"UserId", AppDelegate.UserDetails.UserId.ToString()}
					});
				}
				else
				{
					userBL.UpdateUserDetails(AppDelegate.UserDetails);
					//Xamarin Insight tracking code
					Insights.Track("UpdateUserDetails", new Dictionary <string,string>{
						{"UserId", AppDelegate.UserDetails.UserId.ToString()}
					});
				}

				if(isFromSignUp) {
					WelcomeVC welcomeVC = this.Storyboard.InstantiateViewController ("WelcomeVC") as WelcomeVC;
					if (welcomeVC != null) {
						this.NavigationController.PushViewController (welcomeVC, true);
					}
				}else
					this.NavigationController.PopViewController(false);
			};
		}

		public void UpdateControls (string Parameter, string TableType)
		{
			if (TableType == "Industry") {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoIndustryDropDown.SetTitle (Parameter, UIControlState.Normal);
				AppDelegate.UserDetails.PreferredIndustry = Parameter;
			} else if (TableType == "Company") {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoCompanyDropDown.SetTitle(Parameter,UIControlState.Normal);
				AppDelegate.UserDetails.PreferredCompany = Parameter;
			} else {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoCustomersDropDown.SetTitle(Parameter,UIControlState.Normal);
				AppDelegate.UserDetails.PreferredCustomers = Parameter;
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
				cell.TextLabel.TextColor = UIColor.DarkGray;
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
				cell.TextLabel.TextColor = UIColor.DarkGray;

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
				cell.TextLabel.TextColor = UIColor.DarkGray;

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

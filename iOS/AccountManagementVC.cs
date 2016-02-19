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
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			//AppDelegate.IsNewUser = true;
			TableViewCustomerStreamActivity.Hidden = true;

			ScrollViewAccountManager.ContentSize = new CGSize (375.0f, 950.0f);

			industryLabel.Layer.BorderColor=UIColor.LightGray.CGColor;
			industryLabel.Layer.BorderWidth = 2.0f;

			CompanyLabel.Layer.BorderColor=UIColor.LightGray.CGColor;
			CompanyLabel.Layer.BorderWidth = 2.0f;

			CustomerLabel.Layer.BorderColor=UIColor.LightGray.CGColor;
			CustomerLabel.Layer.BorderWidth = 2.0f;

//			ButtonInfoIndustryDropDown.Layer.BorderWidth = 2.0f;
			if(string.IsNullOrEmpty(AppDelegate.UserDetails.PreferredIndustry)) 
				ButtonInfoIndustryDropDown.SetTitle (" Select", UIControlState.Normal);
			else
				ButtonInfoIndustryDropDown.SetTitle (AppDelegate.UserDetails.PreferredIndustry, UIControlState.Normal);
			
			ButtonInfoIndustryDropDown.Layer.BorderColor = UIColor.LightGray.CGColor; 
//			ButtonInfoCompanyDropDown.Layer.BorderWidth = 2.0f;
			if(string.IsNullOrEmpty(AppDelegate.UserDetails.PreferredCompany)) 
				ButtonInfoCompanyDropDown.SetTitle (" Select", UIControlState.Normal);
			else
				ButtonInfoCompanyDropDown.SetTitle (AppDelegate.UserDetails.PreferredCompany, UIControlState.Normal);
			
			ButtonInfoCompanyDropDown.Layer.BorderColor = UIColor.LightGray.CGColor; 
//			ButtonInfoCustomersDropDown.Layer.BorderWidth = 2.0f;
			if(string.IsNullOrEmpty(AppDelegate.UserDetails.PreferredCustomers)) 
				ButtonInfoCustomersDropDown.SetTitle (" Select", UIControlState.Normal);
			else
				ButtonInfoCustomersDropDown.SetTitle (AppDelegate.UserDetails.PreferredCustomers, UIControlState.Normal);
			
			ButtonInfoCustomersDropDown.Layer.BorderColor = UIColor.LightGray.CGColor; 

			if (AppDelegate.UserDetails.UserId != 0) {

//				SwitchNewLeads.On = AppDelegate.UserDetails.IsNewLeadNotificationRequired;
//				SwitchBusinessUpdates.On = AppDelegate.UserDetails.IsBusinessUpdatesRequired;
//				SwitchFollowUp.On = AppDelegate.UserDetails.IsCustomerFollowUpRequired;
//				SwitchReferralRequests.On = AppDelegate.UserDetails.IsReferralRequestRequired;
//				SwitchMeetingReminders.On = AppDelegate.UserDetails.IsMeetingRemindersRequired;
			}

			TableViewCustomerStreamActivity.Layer.BorderWidth = 2.0f;
			TableViewCustomerStreamActivity.Layer.BorderColor = UIColor.LightGray.CGColor; 

			List<String> industryList = AppDelegate.industryBL.GetIndustry ();

			ButtonInfoIndustryDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewCustomerStreamActivity.Hidden = false;
				TableViewCustomerStreamActivity.Frame = new CGRect (200,355,168,128);
				TableViewCustomerStreamActivity.Source = new IndustryTableSource (industryList, this);
				TableViewCustomerStreamActivity.ReloadData();
			};

			List<Customer> CompanyList = null;
			if (AppDelegate.UserDetails.UserId != 0)
				//CompanyList = AppDelegate.customerBL.GetCustomersMaster ();
				CompanyList = new List<Customer>();
			else
				CompanyList = new List<Customer> ();
		
				

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
						userInfo.isFromDashBoard = true;
					this.NavigationController.PresentViewController (userInfo, true, null);					
				}
			};

			buttonChangePassword.TouchUpInside += (object sender, EventArgs e) =>  {
			   ChangePassword changePassword = this.Storyboard.InstantiateViewController ("ChangePassword") as ChangePassword;
					if(changePassword != null)
						this.NavigationController.PushViewController (changePassword, true);			
			};
//			SwitchNewLeads.ValueChanged+= (object sender, EventArgs e) => {
//				AppDelegate.UserDetails.IsNewLeadNotificationRequired = SwitchNewLeads.On;
//			};
//			SwitchBusinessUpdates.ValueChanged+= (object sender, EventArgs e) => {
//				AppDelegate.UserDetails.IsBusinessUpdatesRequired = SwitchBusinessUpdates.On;
//			};
//			SwitchFollowUp.ValueChanged+= (object sender, EventArgs e) => {
//				AppDelegate.UserDetails.IsCustomerFollowUpRequired = SwitchFollowUp.On;
//			};
//			SwitchReferralRequests.ValueChanged+= (object sender, EventArgs e) => {
//				AppDelegate.UserDetails.IsReferralRequestRequired = SwitchReferralRequests.On;
//			};
//			SwitchMeetingReminders.ValueChanged+= (object sender, EventArgs e) => {
//				AppDelegate.UserDetails.IsMeetingRemindersRequired = SwitchMeetingReminders.On;
//			};

			ButtonFinish.TouchUpInside += (object sender, EventArgs e) => {

//				AppDelegate.UserDetails.IsNewLeadNotificationRequired = SwitchNewLeads.On;
//				AppDelegate.UserDetails.IsBusinessUpdatesRequired = SwitchBusinessUpdates.On;
//				AppDelegate.UserDetails.IsCustomerFollowUpRequired = SwitchFollowUp.On;
//				AppDelegate.UserDetails.IsReferralRequestRequired = SwitchReferralRequests.On;
//				AppDelegate.UserDetails.IsMeetingRemindersRequired = SwitchMeetingReminders.On;
				 
				//AppDelegate.UserDetails.UserId = 7;
				if(AppDelegate.UserDetails.UserId == 0)
				{
					AppDelegate.UserDetails.UserId = AppDelegate.userBL.CreateUser(AppDelegate.UserDetails);
					//Xamarin Insight tracking code
					Insights.Track("CreateUser", new Dictionary <string,string>{
						{"UserId", AppDelegate.UserDetails.UserId.ToString()}
					});
				}
				else
				{
					AppDelegate.userBL.UpdateUserDetails(AppDelegate.UserDetails);
					//Xamarin Insight tracking code
					Insights.Track("UpdateUserDetails", new Dictionary <string,string>{
						{"UserId", AppDelegate.UserDetails.UserId.ToString()}
					});
				}

				if(isFromSignUp && AppDelegate.UserDetails.UserId >= 0) {
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
				ButtonInfoIndustryDropDown.SetTitle (" "+Parameter, UIControlState.Normal);
				AppDelegate.UserDetails.PreferredIndustry = Parameter;
			} else if (TableType == "Company") {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoCompanyDropDown.SetTitle(" "+Parameter,UIControlState.Normal);
				AppDelegate.UserDetails.PreferredCompany = Parameter;
			} else {
				TableViewCustomerStreamActivity.Hidden = true;
				ButtonInfoCustomersDropDown.SetTitle(" "+Parameter,UIControlState.Normal);
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

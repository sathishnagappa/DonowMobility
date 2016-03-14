using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace donow.iOS
{
	partial class HambergerMenuVC  : UIViewController
	{
		public HambergerMenuVC  (IntPtr handle) : base (handle)
		{
		}

		IList<string> imageIcons = new List<string>
		{
			"Account Mgmt Icon.png","help.png", "LogOutIcon.png"
		};

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
//			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.Title = "More";
			var table = new UITableView(View.Bounds); // defaults to Plain style
			string[] tableItems = new string[] {"Account Management", "Help","Log Out"};
			table.Source = new TableSource(tableItems, imageIcons, this);
			View.Add (table);
		}

		public class TableSource : UITableViewSource {

			string[] TableItems;
			string CellIdentifier = "TableCell";
			IList<string> imageIcons;
			HambergerMenuVC owner;

			public TableSource (string[] items, IList<string> imageIcons, HambergerMenuVC owner)
			{
				TableItems = items;
				this.owner = owner;
				this.imageIcons = imageIcons;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Length;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				string item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				cell.TextLabel.Text = item;
				cell.ImageView.Image = UIImage.FromBundle(imageIcons[indexPath.Row]); 
				//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);

				switch (TableItems [indexPath.Row]) {
				//				
				case "Account Management":  
					AccountManagementVC accountManagementVC = owner.Storyboard.InstantiateViewController ("AccountManagementVC") as AccountManagementVC;
					if (accountManagementVC != null) {
//						owner.NavigationController.SetNavigationBarHidden (true, false);
						owner.NavigationController.PushViewController (accountManagementVC, true);
						//owner.PresentViewController(accountManagementVC,);
					}
					break;

				case "Help": 
					InfoPage infoVC = owner.Storyboard.InstantiateViewController ("InfoPage") as InfoPage;
					if (infoVC != null) {
						owner.NavigationController.PushViewController (infoVC, true);
					}
					break;

				case "Log Out":
					loginPageViewController login = owner.Storyboard.InstantiateViewController ("loginPageViewController") as loginPageViewController;
					if (login != null) {
						login.HidesBottomBarWhenPushed = true;
						owner.NavigationController.PushViewController (login, true);
					}
					break;

				default:
					UIAlertController okAlertController = UIAlertController.Create ("Row Touched", TableItems [indexPath.Row], UIAlertControllerStyle.Alert);
					okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
					owner.PresentViewController (okAlertController, true, null);
					break;
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 80.0f;
			}

		}
	}
}

using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace donow.iOS
{
	partial class HambergerMenuVC  : UITableViewController
	{
		public HambergerMenuVC  (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var table = new UITableView(View.Bounds); // defaults to Plain style
			string[] tableItems = new string[] {"Notifications","My Profile","My Meetings","Deal Makers","Account Management",};
			table.Source = new TableSource(tableItems, this);
			Add (table);
		}

		public class TableSource : UITableViewSource {

			string[] TableItems;
			string CellIdentifier = "TableCell";
			HambergerMenuVC owner;

			public TableSource (string[] items, HambergerMenuVC owner)
			{
				TableItems = items;
				this.owner = owner;
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
				cell.ImageView.Image = UIImage.FromBundle("settingsImage.png"); 
				//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				switch (TableItems [indexPath.Row]) {
				case "Notifications": 
					NotificationsVC notificationVC = owner.Storyboard.InstantiateViewController ("NotificationsVC") as NotificationsVC;
					if (notificationVC != null) {
						owner.NavigationController.PushViewController (notificationVC, true);
					}
					break;
				case "My Profile": 
					MyProfileVC myProfileVC = owner.Storyboard.InstantiateViewController ("MyProfileVC") as MyProfileVC;
					if (myProfileVC != null) {
						owner.NavigationController.PushViewController (myProfileVC, true);
					}
					break;
				case "My Meetings": 
					MyMeetingsVC myMeetingVC = owner.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
					if (myMeetingVC != null) {
						owner.NavigationController.PushViewController (myMeetingVC, true);
					}
					break;
				case "Deal Makers": 
					DealMakersVC dealMakersVC = owner.Storyboard.InstantiateViewController ("DealMakersVC") as DealMakersVC;
					if (dealMakersVC != null) {
						owner.NavigationController.PushViewController (dealMakersVC, true);
					}
					break;
				case "Account Management": 
					AccountManagementVC accountManagementVC = owner.Storyboard.InstantiateViewController ("AccountManagementVC") as AccountManagementVC;
					if (accountManagementVC != null) {
						owner.NavigationController.PushViewController (accountManagementVC, true);
					}
					break;
				default:
					UIAlertController okAlertController = UIAlertController.Create ("Row Touched", TableItems [indexPath.Row], UIAlertControllerStyle.Alert);
					okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
					owner.PresentViewController (okAlertController, true, null);
					break;
				}

//
				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60.0f;
			}

		}
	}
}

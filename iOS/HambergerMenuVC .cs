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
			"My Profile Icon.png","My Deal Makers Icon.png","Account Mgmt Icon.png","Info Page Icon.png","Info Page Icon.png","Info Page Icon.png"
		};

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.SetNavigationBarHidden (true, false);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "More";
			var table = new UITableView(View.Bounds); // defaults to Plain style
			string[] tableItems = new string[] {"My Profile","My Deal Makers","Account Management", "Info Page","Intial FeedBack","F2F Feedback" };
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
				switch (TableItems [indexPath.Row]) {
				case "My Profile": 
					MyProfileVC myProfileVC = owner.Storyboard.InstantiateViewController ("MyProfileVC") as MyProfileVC;
					if (myProfileVC != null) {
						owner.NavigationController.PushViewController (myProfileVC, true);
					}
					break;
				case "My Deal Makers": 
					DealMakerVC dealMakerVC = owner.Storyboard.InstantiateViewController ("DealMakerVC") as DealMakerVC;
					if (dealMakerVC != null) {
						owner.NavigationController.PushViewController (dealMakerVC, true);
					}
					break;
				case "Account Management": 
					AccountManagementVC accountManagementVC = owner.Storyboard.InstantiateViewController ("AccountManagementVC") as AccountManagementVC;
					if (accountManagementVC != null) {
						owner.NavigationController.PushViewController (accountManagementVC, true);
					}
					break;
				case "Info Page": 
					InfoPage infoVC = owner.Storyboard.InstantiateViewController ("InfoPage") as InfoPage;
					if (infoVC != null) {
						owner.NavigationController.PushViewController (infoVC, true);
					}
//					break;
				case "Intial FeedBack": 
					InteractionLeadUpdateVC IntialFeeback = owner.Storyboard.InstantiateViewController ("InteractionLeadUpdateVC") as InteractionLeadUpdateVC;
					if (IntialFeeback != null) {
						owner.NavigationController.PushViewController (IntialFeeback, true);
										}
					break;
				case "F2F Feedback": 
					LeadUpdateVC f2fFeedback = owner.Storyboard.InstantiateViewController ("LeadUpdateVC") as LeadUpdateVC;
					if (f2fFeedback != null) {
						owner.NavigationController.PushViewController (f2fFeedback, true);
										}
					break;
				default:
					UIAlertController okAlertController = UIAlertController.Create ("Row Touched", TableItems [indexPath.Row], UIAlertControllerStyle.Alert);
					okAlertController.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
					owner.PresentViewController (okAlertController, true, null);
					break;
				}

				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 80.0f;
			}

		}
	}
}

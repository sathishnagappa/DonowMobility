using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UIKit;
using donow.PCL.Model;
using donow.PCL;

namespace donow.iOS
{
	partial class customerProfileVC : UIViewController
	{
		public customerProfileVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			List <Leads> ListLeads = new List <Leads> ();
			LeadsBL leadsVC = new LeadsBL ();
			ListLeads = leadsVC.GetAllLeads ();

			TableViewNewLeads.Source = new TableSource (ListLeads, this, "LeadsTable");
		}

		public class TableSource : UITableViewSource {

			string CellIdentifier = "LeadsTableCell";
			List<Leads> TableItems;
			customerProfileVC owner;
			string tableType = string.Empty;

			public TableSource (List<Leads> items, customerProfileVC owner, string TableType)
			{
				TableItems = items;
				this.owner = owner;
				tableType = TableType;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }
				if (TableItems [indexPath.Row].IsNew == true) {
					cell.ImageView.Image = UIImage.FromFile ("");
				}
				if (cell.TextLabel.Tag == 10) {
					cell.TextLabel.Text = "Vaibhav";
				}

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

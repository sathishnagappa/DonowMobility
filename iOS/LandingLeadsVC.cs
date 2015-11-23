using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class LandingLeadsVC : UIViewController
	{
		public LandingLeadsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			var table = new UITableView(View.Bounds); // defaults to Plain style

			TableViewLeads.Source = new TableSource();
		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
//			LandingLeadsVC owner;

			public TableSource ()
			{
//				TableItems = items;
//				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
//				return TableItems.Length;
				return 3;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as LeadsTableCell;

				if (cell == null) {
					cell = new LeadsTableCell(CellIdentifier);
				}

//				cell.UpdateCell("name"
//					, UIImage.FromFile ("Images/ic_facebook.png") );
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				

				tableView.DeselectRow (indexPath, true);
			}
	
			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60.0f;
			}
		}
	}
}

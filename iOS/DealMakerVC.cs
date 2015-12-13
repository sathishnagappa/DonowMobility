using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using donow.PCL;
using System.Collections.Generic;
using donow.Util;
using CoreGraphics;

namespace donow.iOS
{
	partial class DealMakerVC : UIViewController
	{
		LoadingOverlay loadingOverlay;
		public DealMakerVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableViewDealMaker.Source = new TableSource (this);

		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			//			List<Leads> TableItems;
			DealMakerVC owner;

			public TableSource (DealMakerVC owner)
			{
				//				TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				//				return TableItems.Count;
				return 1;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				DealMakerTableViewCell cell = tableView.DequeueReusableCell (CellIdentifier) as DealMakerTableViewCell;
				//				Leads leadObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new DealMakerTableViewCell(CellIdentifier);
				}

				cell.UpdateCell();
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				DealMakerDetailVC dealmakerDetailObject = owner.Storyboard.InstantiateViewController ("DealMakerDetailVC") as DealMakerDetailVC;
				if (dealmakerDetailObject != null) {
					//					dealmakerDetailObject.leadObj = TableItems [indexPath.Row];
					owner.NavigationController.PushViewController (dealmakerDetailObject, true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 120.0f;
			}
		}
	}
}

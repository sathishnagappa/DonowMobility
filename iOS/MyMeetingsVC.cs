using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using System.Collections.Generic;

namespace donow.iOS
{
	partial class MyMeetingsVC : UIViewController
	{
		public MyMeetingsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "My Meetings";
			var table = new UITableView(View.Bounds); // defaults to Plain style
			var meetingList = AppDelegate.CalendarList;
			table.Source = new TableSource(meetingList, this);
			Add (table);
		}

		public class TableSource : UITableViewSource {

			List<CalenderEvent> TableItems;
			string CellIdentifier = "TableCell";

			MyMeetingsVC owner;

			public TableSource (List<CalenderEvent> meetingList, MyMeetingsVC owner)
			{
				TableItems = meetingList;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				CalenderEvent item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				cell.TextLabel.Text = item.Subject;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{			

				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 80.0f;
			}

		}
	}
}

using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using System.Collections.Generic;
using CoreGraphics;

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
			ScrollViewMeeting.ContentSize = new CGSize (414f, 1350f);
			//var meetingList = AppDelegate.CalendarList[0];
			string[] TakingPoints =  {"* What is the dream solution if \n pricing was not a problem?", "* How do you want to maintain \n this solution long term?",
				"* What else are you willing to \n look into?"};
			string[] LastestCustomerInfo =  {"* What is the dream solution if \n pricing was not a problem?", "* How do you want to maintain \n this solution long term?"};
			string[] LatestIndustryInfo =  {"* What is the dream solution if \n pricing was not a problem?", "* How do you want to maintain \n this solution long term?",
				"* What else are you willing to \n look into?"};
			TalkingPointTable.Source = new TableSource(TakingPoints, this);
			LatestCustomerInfoTable.Source = new CustomerInfoTableSource(LastestCustomerInfo, this);
			LatestIndustryNewsTable.Source = new CustomerIndustryTableSource(LatestIndustryInfo, this);
		}

		public class TableSource : UITableViewSource {

			string[] TableItems;
			string CellIdentifier = "TableCell";

			MyMeetingsVC owner;

			public TableSource (string[] meetingList, MyMeetingsVC owner)
			{
				TableItems = meetingList;
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
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{			

				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 70.0f;
			}

		}

		public class CustomerInfoTableSource : UITableViewSource {

			string[] TableItems;
			string CellIdentifier = "TableCell";

			MyMeetingsVC owner;

			public CustomerInfoTableSource (string[] meetingList, MyMeetingsVC owner)
			{
				TableItems = meetingList;
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
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{			

				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}

		}

		public class CustomerIndustryTableSource : UITableViewSource {

			string[] TableItems;
			string CellIdentifier = "TableCell";

			MyMeetingsVC owner;

			public CustomerIndustryTableSource (string[] meetingList, MyMeetingsVC owner)
			{
				TableItems = meetingList;
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
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{			

				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 105.0f;
			}

		}
	}
}

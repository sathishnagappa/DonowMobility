using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using System.Collections.Generic;
using CoreGraphics;
using donow.PCL;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using donow.Util;

namespace donow.iOS
{
	partial class MyMeetingsVC : UIViewController
	{
		public UserMeetings meetingObj;
		//public Customer customer;
		public MyMeetingsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{			
			base.ViewWillAppear (animated);
			LoadMeetingData ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Meeting Info";

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;

			LabelNotes.Layer.BorderWidth = 1.0f;

			LabelNotes.ShouldBeginEditing = delegate {
				ScrollViewMeeting.SetContentOffset ( new CGPoint(0,350),true);
				return true;	
			};

			LabelNotes.ShouldChangeText = (text, range, replacementString) =>
			{
				if (replacementString.Equals("\n"))
				{
					LabelNotes.EndEditing(true);
					ScrollViewMeeting.SetContentOffset ( new CGPoint(0,-64),true);
					return false;
				}
				else
				{
					return true;
				}
			};

			//LoadMeetingData ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			TalkingPointTable.Source = null;
			base.ViewWillDisappear (animated);
		}

		void LoadMeetingData()
		{
			LabelMeetingHeader.Text = "Meeting W/" + meetingObj.CustomerName;
			LabelDateAndTime.Text = DateTime.Parse(meetingObj.StartDate).ToString("MMM. dd, yyyy  hh:mm tt");
			LabelCityState.Text = meetingObj.City + ", " + meetingObj.State;
			//if (string.IsNullOrEmpty (LabelNotes.Text)) {
			//LabelNotesDate.Text = DateTime.Parse (meetingObj.EndDate).ToString ();
			//LabelNotes.Text = 
			//}

			ScrollViewMeeting.ContentSize = new CGSize (375.0f, 824f);
			string[] TalkingPoints = {
				"What is the dream solution if pricing was not a problem?",
				"How do you want to maintain this solution long term?",
				"What else are you willing to look into?",
				"Would you mind telling me about your current situation?",
				"Who do you currently use for this service?",
				"What’s working for you?",
				"What’s not working?",
				"What are your main frustrations?",
				"What would you like to change about things?",
				"Ask your prospect if they’d like to know more about how you could answer their needs?"
			};

			string[] NewTalkingPoints = {"Does this sound like something that can solve your problems/make you feel better/address your issues?",
				"I feel really good about this, I know this is going to work well for us. What’s the best way to get things underway?",
				"I could email you an order when I get back to the office or I could just get it from you now. What works best for you?"};

			TalkingPointTable.Source = new TableSource(TalkingPoints,NewTalkingPoints);
				
		}

		static UIImage FromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data);
		}

		public class TableSource : UITableViewSource {

			string[] TableItems, NewTableIttems;

			public TableSource (string[] talkingPoint, string[] newTalkingPoint)
			{
				TableItems = talkingPoint;
				NewTableIttems = newTalkingPoint;
			}

			public override nint NumberOfSections (UITableView tableView)
			{				
				return 2;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				if (section == 0)
					return (TableItems.Length);
				if (section == 1) 
					return (NewTableIttems.Length);

				return 0;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				string CellIdentifier = string.Empty;
				string oldCellIdentifier = "TableCell";
				string NewCellIdentifier = "NewTableCell";
				string item = string.Empty;

				if (indexPath.Section == 0) {
					CellIdentifier = oldCellIdentifier;
					item = TableItems [indexPath.Row];

				} else if (indexPath.Section == 1) {
					CellIdentifier = NewCellIdentifier;
					item = NewTableIttems [indexPath.Row];
				}

				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);

				//---- if there are no cells to reuse, create a new one
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);
				}

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
				if (indexPath.Section == 0)
					return 50.0f;
				else if (indexPath.Section == 1)
					return 80.0f;

				return 0;
			}

		}


	}
}

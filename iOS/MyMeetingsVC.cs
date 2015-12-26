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
		public Customer customer;
		LoadingOverlay loadingOverlay;
		public MyMeetingsVC (IntPtr handle) : base (handle)
		{
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Meeting Info";

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;


			LabelMeetingHeader.Text = "Meeting W/" + meetingObj.CustomerName;
			LabelDateAndTime.Text = DateTime.Parse(meetingObj.StartDate).ToString("MMM. dd, yyyy  hh:mm tt");
			LabelCityState.Text = meetingObj.City + ", " + meetingObj.State;
			//if (string.IsNullOrEmpty (LabelNotes.Text)) {
				//LabelNotesDate.Text = DateTime.Parse (meetingObj.EndDate).ToString ();
				//LabelNotes.Text = 
			//}


			ScrollViewMeeting.ContentSize = new CGSize (414f, 1350f);
			await LoadMeetingData ();

		}

		public override void ViewWillDisappear (bool animated)
		{
			TalkingPointTable.Source = null;
			LatestCustomerInfoTable.Source = null;
			LatestIndustryNewsTable.Source = null;
			base.ViewWillDisappear (animated);
		}

		async Task LoadMeetingData()
		{
			//var meetingList = AppDelegate.CalendarList[0];
			string[] TakingPoints = {
				"●  What is the dream solution if pricing was not a problem?",
				"●  How do you want to maintain this solution long term?",
				"●  What else are you willing to look into?",
				"●  Would you mind telling me about your current situation?",
				"●  Who do you currently use for this service?",
				"●  What’s working for you?",
				"●  What’s not working?",
				"●  What are your main frustrations?",
				"●  What would you like to change about things?"
			};

			string[] NewTakingPoints = {"●  Ask your prospect if they’d like to know more about how you could answer their needs?",
				"●  Does this sound like something that can solve your problems/make you feel better/address your issues?",
				"●  I feel really good about this, I know this is going to work well for us. What’s the best way to get things underway?",
				"●  I could email you an order when I get back to the office or I could just get it from you now. What works best for you?"};

			List<BingResult>  bingResult = AppDelegate.customerBL.GetBingResult (AppDelegate.UserDetails.Company + " + Products");
			TalkingPointTable.Source = new TableSource(TakingPoints,NewTakingPoints);
			LatestIndustryNewsTable.Source = new CustomerIndustryTableSource(bingResult, this);

//			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
//			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
//				bounds.Size = new CGSize (bounds.Size.Height, bounds.Size.Width);
//			}
//			loadingOverlay = new LoadingOverlay (bounds);
//			View.Add (loadingOverlay);

			string[] customerNameArray = customer.Company.Split ();
			string searchText = customerNameArray [0].Length == 1 ? customerNameArray [1] : customerNameArray [0];
			List<TwitterStream>  twitterStream =  await TwitterUtil.Search (searchText.ToLower());
			List<TwitterStream> twitterStreamwithKeyword = new List<TwitterStream>();
			if(twitterStream.Count > 0)
				twitterStreamwithKeyword =	twitterStream.Where(X => X.text.Contains("Business") || X.text.Contains("Sales") || X.text.Contains("Opportunities")
					|| X.text.Contains("Organization") || X.text.Contains("Launch") || X.text.Contains("Money") || X.text.Contains("Tools") || X.text.Contains("Competition")
					|| X.text.Contains("Interest") || X.text.Contains("Industry") || X.text.Contains("Learning")).ToList();		
			
			LatestCustomerInfoTable.Source = new CustomerInfoTableSource(twitterStreamwithKeyword);
			TalkingPointTable.RowHeight = UITableView.AutomaticDimension;
			//TalkingPointTable.EstimatedRowHeight = 100; 
			LatestCustomerInfoTable.ReloadData ();
			//loadingOverlay.Hide ();
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
				//throw new System.NotImplementedException ();
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
					return 100.0f;

				return 0;
			}

		}

		public class CustomerInfoTableSource : UITableViewSource {

			List<TwitterStream> TableItems;
			string CellIdentifier = "TableCellCusomerInfo";

			public CustomerInfoTableSource (List<TwitterStream> twitterstream)
			{
				TableItems = twitterstream;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				TwitterStream item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				//cell.ImageView.Frame = new CGRect (25, 5, 33, 33);
				cell.ImageView.Image = FromUrl(item.profile_image_url);
				cell.TextLabel.Text = item.text;
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

			List<BingResult> TableItems;
			string CellIdentifier = "TableCell";

			MyMeetingsVC owner;

			public CustomerIndustryTableSource (List<BingResult> meetingList, MyMeetingsVC owner)
			{
				TableItems = meetingList;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return 5;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				BingResult item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				cell.ImageView.Image = UIImage.FromBundle("Article 1 Thumb.png");
				cell.TextLabel.Text = item.Title;
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				BingSearchVC bingSearchVC = owner.Storyboard.InstantiateViewController ("BingSearchVC") as BingSearchVC;
				if (bingSearchVC != null) {
					bingSearchVC.webURL = TableItems [indexPath.Row].Url;
					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController (bingSearchVC, true);
				}
				tableView.DeselectRow (indexPath, true);
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 105.0f;
			}

		}
	}
}

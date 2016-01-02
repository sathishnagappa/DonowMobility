using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;

namespace donow.iOS
{
	partial class ReferralRequestDetails : UIViewController
	{
		public string referralRequestType = string.Empty;
		//public List<ReferralRequest> referralRequests;
		public ReferralRequestDetails (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			SetImageAndTitle ();

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (157, 50, 49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Deal Maker";

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
//				this.NavigationController.PopViewController(false);
				LandingRefferalRequestVC landingRefferalRequestVC = this.Storyboard.InstantiateViewController("landingRefferalRequestVC") as LandingRefferalRequestVC;
				if (landingRefferalRequestVC != null)
				{
					this.NavigationController.PushViewController(landingRefferalRequestVC, true);
				} 
			};
			NavigationItem.LeftBarButtonItem = btn;

			// ************ Search Button to be added *****************//

			UIBarButtonItem rightBtn = new UIBarButtonItem ();
			rightBtn.Image = UIImage.FromFile("Magnifying Glass_small.png");
			rightBtn.Clicked += (sender, e) => {
			};
			NavigationItem.RightBarButtonItem = rightBtn;

			// ************ Search Button to be added *****************//

			SetImageAndTitle ();

		}

		void SetImageAndTitle()
		{
			//switch (referralRequestType) {

			if (referralRequestType == "New") {
				ImageRR.Image = UIImage.FromBundle ("New Referral Request Icon.png");
				LabelRRTitle.Text = "New Referral Requests";
			} else if (referralRequestType == "Accepted") {

				ImageRR.Image = UIImage.FromBundle ("Referral Action Pending Icon.png");
				LabelRRTitle.Text = "Accepted Requests (Action Pending)";
			} else if (referralRequestType == "Passed") {

				ImageRR.Image = UIImage.FromBundle ("Passed Requests Icon.png");
				LabelRRTitle.Text = "Passed Requests";
			} else if (referralRequestType == "Completed") {
			
				ImageRR.Image = UIImage.FromBundle ("Completed Requests Icon.png");
				LabelRRTitle.Text = "Completed Requests";
			}
				
			TableViewRR.Source = new TableSource (AppDelegate.CurrentRRList, this);			
			//}
			
		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			ReferralRequestDetails owner;
			List<ReferralRequest> TableItems;

			public TableSource (List<ReferralRequest> items, ReferralRequestDetails owner)
			{
				TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as ReferralRequestTableCell;
				ReferralRequest rrObj = TableItems[indexPath.Row];

				if (cell == null)
				{ cell = new ReferralRequestTableCell (CellIdentifier); }

				cell.UpdateCell (rrObj);
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);

				if (TableItems [indexPath.Row].Status != 3) {
					ScrollReq ScrollReqProf = owner.Storyboard.InstantiateViewController ("ScrollReqProf") as ScrollReq;
					if (ScrollReqProf != null) {
						ScrollReqProf.refferalRequests = TableItems [indexPath.Row];
						ScrollReqProf.referralRequestType = owner.referralRequestType;
						owner.NavigationController.PushViewController (ScrollReqProf, true);
					}
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 200.0f;
			}
		}
	}
}

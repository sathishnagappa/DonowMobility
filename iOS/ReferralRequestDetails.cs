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
		public List<ReferralRequest> referralRequests;
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
				

						ReferralRequest rr = new ReferralRequest ();
						rr.ReferralRequestID = 1;
						rr.UserName = "Susan";
						rr.City = "Seattle";
						rr.State = "WA";
						rr.Industry = "Technology";
						rr.Prospect = "Scott Anders";
						rr.BusinessNeeds = "Sales Tracking SoftWare";
						List<ReferralRequest> rrdetails = new List<ReferralRequest> ();
						rrdetails.Add (rr);
						TableViewRR.Source = new TableSource (rrdetails, this);			
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

				ScrollReq ScrollReqProf = owner.Storyboard.InstantiateViewController ("ScrollReqProf") as ScrollReq;
				if (ScrollReqProf != null) {
					owner.NavigationController.PushViewController(ScrollReqProf,true);
				}
				//LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
//				if (leadDetailVC != null) {
//					leadDetailVC.leadObj = TableItems[indexPath.Row];
//					//owner.View.AddSubview (leadDetailVC.View);
//					owner.NavigationController.PushViewController(leadDetailVC,true);
//				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 200.0f;
			}
		}
	}
}

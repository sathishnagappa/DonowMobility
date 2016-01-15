using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using donow.PCL.Model;
using donow.PCL;
using System.Linq;
using CoreGraphics;

namespace donow.iOS
{
	partial class MyProfileVC : UIViewController
	{
		public bool LeadExpanded, ReferralExpanded;

		public MyProfileVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (157, 50, 49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Seller Profile";
			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(false);
			};	
			NavigationItem.LeftBarButtonItem = btn;
			LabelName.Text = AppDelegate.UserDetails.FullName;
			LabelCompanyName.Text = AppDelegate.UserDetails.Company;
			LabelCityAndState.Text = AppDelegate.UserDetails.City + "," + AppDelegate.UserDetails.State;

			LabelSellerScore.Text = "Comming Soon";


			List<LeadMaster> tableItems = AppDelegate.leadsBL.GetAllLeads(AppDelegate.UserDetails.UserId);
			if (tableItems.Count > 0) {
				LabelLeadsReceived.Text = tableItems.Where (x => (x.USER_LEAD_STATUS != 3 || x.USER_LEAD_STATUS != 5)).ToList ().Count.ToString ();
				TableViewActiveLeads.Source = new ActiveLeadsTableSource (tableItems, this);
			}
			else
				LabelLeadsReceived.Text = "0";

	
			List<ReferralRequest> referralRequestList = AppDelegate.referralRequestBL.GetReferralRequest (AppDelegate.UserDetails.UserId);
			if (referralRequestList.Count > 0) {
				referralRequestList = referralRequestList.Where (x => x.Status == 1).ToList ();
				TableViewReferralRequests.Source = new ReferralRequestsTableSource (referralRequestList, this);
			}

		
			ScrollViewMyProfile.ContentSize = new CGSize (375.0f, 830.0f);
			TableViewActiveLeads.ScrollEnabled = false;
			TableViewReferralRequests.ScrollEnabled = false;

			ButtonLeadExpansion.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewActiveLeads.ScrollEnabled = true;
				LeadExpanded = true;
//				TableViewActiveLeads.Frame = new CGRect(35, 68, 325, (tableItems.Count * 65.0f));
//				ScrollViewMyProfile.ContentSize = new CGSize (414.0f, );
			};

			ButtonRefrralExpension.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewReferralRequests.ScrollEnabled = true;
				ReferralExpanded = true;
//				TableViewActiveLeads.Frame = new CGRect(35, 70, 325, (tableItems.Count * 65.0f));
			};
		}

		void LoadData()
		{
			
		}

		public class ActiveLeadsTableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			List<LeadMaster> TableItems;
//			MyProfileVC owner;

			public ActiveLeadsTableSource (List<LeadMaster> items, MyProfileVC myProfileVC)
			{
				TableItems = items;
//				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as ActiveLeadsMyProfileCell;
				LeadMaster leadObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new ActiveLeadsMyProfileCell(CellIdentifier);
				}

				cell.UpdateCell(leadObj);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
			}
		}

		public class ReferralRequestsTableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			List<ReferralRequest> TableItems;
//			MyProfileVC owner;

			public ReferralRequestsTableSource (List<ReferralRequest> items, MyProfileVC myProfileVC)
			{
				TableItems = items;
//				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{				
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as ReferralRequestsMadeMyProfileCell;
				ReferralRequest leadObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new ReferralRequestsMadeMyProfileCell(CellIdentifier);
				}

				cell.UpdateCell(leadObj);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
			}
		}
	}
}

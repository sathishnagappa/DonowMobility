using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using donow.PCL;
using System.Collections.Generic;
using donow.Util;
using CoreGraphics;
using System.Linq;

namespace donow.iOS
{
	partial class LandingLeadsVC : UIViewController
	{
		LoadingOverlay loadingOverlay;
		List<Leads> leads;
		public LandingLeadsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			//this.ParentViewController.NavigationItem.Title = "Leads";
//			if(this.NavigationController.NavigationItem.BackBarButtonItem != null)
//			this.NavigationController.NavigationItem.BackBarButtonItem.Enabled = false;
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
//			this.NavikeygationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
//			this.NavigationController.NavigationItem.SetLeftBarButtonItem( new UIBarButtonItem(UIImage.FromFile("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
//				this.NavigationController;
//			}), true);
//			List<Leads> newleads = GetLeads();
//			this.TabBarItem.BadgeValue = newleads.Count.ToString();
//			TableViewLeads.Source = new TableSource (newleads, this);



			AppDelegate.IsProspectVisited = false; 
			List<Leads> leads = GetLeads ();
			if (leads.Count > 0) {
				this.TabBarItem.BadgeValue = leads.Count.ToString ();
				TableViewLeads.Source = new TableSource (leads, this);
			} else {
				//AlertView.Hidden = false;
				UIAlertView alert = new UIAlertView () { 
					Title = "", 
					Message = "We are gathering your leads for you! \n Check Back Shortly."
				};
				alert.AddButton ("OK");
				alert.Show ();
			}
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

//			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
//			this.NavigationController.SetNavigationBarHidden (false, false);
//			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
//			this.NavigationController.NavigationBar.TintColor = UIColor.White;
//			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
//			this.NavigationController.NavigationItem.SetLeftBarButtonItem( new UIBarButtonItem(UIImage.FromFile("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
//				this.NavigationController.PopViewController(true);
//			}), true);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.Title = "Leads";
			this.NavigationItem.SetHidesBackButton (true, false);
//			this.NavigationItem.SetLeftBarButtonItem(null, true);


			//List<UserMeetings> userMeeetings 
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new CGSize (bounds.Size.Height, bounds.Size.Width);
			}
			loadingOverlay = new LoadingOverlay (bounds);
			View.Add (loadingOverlay);


			ButtonRequestNewLead.TouchUpInside += (object sender, EventArgs e) => {
				View.Add (loadingOverlay);
			if (leads.Count > 0) {
				leads = GetLeads();
				this.TabBarItem.BadgeValue = leads.Count.ToString();
				} else {
					//AlertView.Hidden = false;
					UIAlertView alert = new UIAlertView () { 
						Title = "", 
						Message = "We are gathering your leads for you! \n Check Back Shortly."
					};
					alert.AddButton ("OK");
					alert.Show ();
				}
				TableViewLeads.Source = new TableSource (leads, this);
				loadingOverlay.Hide ();
			};

			loadingOverlay.Hide ();
		}

		List<Leads> GetLeads()
		{
			List<Leads> leads = new  List<Leads> ();
			LeadsBL leadsbl = new LeadsBL ();
			leads = leadsbl.GetAllLeads (AppDelegate.UserDetails.UserId);
			return leads;
		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			List<Leads> TableItems;
			LandingLeadsVC owner;

			public TableSource (List<Leads> items, LandingLeadsVC owner)
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
				var cell = tableView.DequeueReusableCell (CellIdentifier) as LeadTableCell;
				Leads leadObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new LeadTableCell(CellIdentifier);
				}
					
				cell.UpdateCell(leadObj);
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				
				tableView.DeselectRow (indexPath, true);
				AppDelegate.CurrentLead = TableItems [indexPath.Row];
				if (TableItems [indexPath.Row].STATUS == "NEW") 
				{
					LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
					if (leadDetailVC != null) {
						leadDetailVC.leadObj = TableItems [indexPath.Row];
						//owner.View.AddSubview (leadDetailVC.View);
						owner.NavigationController.PushViewController (leadDetailVC, true);
					}
				} else if (TableItems [indexPath.Row].STATUS == "Accepted") {
					prospectDetailsVC prospectVC = owner.Storyboard.InstantiateViewController ("dummyViewController") as prospectDetailsVC;
					if (prospectVC != null) {
						prospectVC.localLeads = TableItems [indexPath.Row];
						owner.NavigationController.PushViewController (prospectVC, true);
					}
					
				}

			}
	
			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
			}
		}
	}
}

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
	partial class LandingLeadsVC : UIViewController
	{
		LoadingOverlay loadingOverlay;
		public LandingLeadsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

            this.ParentViewController.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.SetNavigationBarHidden (true, false);
//			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
//			this.NavigationController.NavigationBar.TintColor = UIColor.White;
//			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
//			this.NavigationController.NavigationItem.SetLeftBarButtonItem( new UIBarButtonItem(UIImage.FromFile("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
//				this.NavigationController.PopViewController(true);
//			}), true);
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
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new CGSize (bounds.Size.Height, bounds.Size.Width);
			}
			loadingOverlay = new LoadingOverlay (bounds);
			View.Add (loadingOverlay);

			List<Leads> leads = GetLeads (false);
			this.TabBarItem.BadgeValue = leads.Count.ToString();
			TableViewLeads.Source = new TableSource (leads, this);

			ButtonRequestNewLead.TouchUpInside += (object sender, EventArgs e) => {
				View.Add (loadingOverlay);
				List<Leads> newleads = GetLeads(true);
				this.TabBarItem.BadgeValue = newleads.Count.ToString();
				TableViewLeads.Source = new TableSource (newleads, this);
				loadingOverlay.Hide ();
			};

			loadingOverlay.Hide ();
		}

		List<Leads> GetLeads(bool isNewLeadRequest)
		{
			List<Leads> leads = new  List<Leads> ();
			LeadsBL leadsbl = new LeadsBL ();
			leads = leadsbl.GetAllLeads ();
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
				LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
				if (leadDetailVC != null) {
					leadDetailVC.leadObj = TableItems[indexPath.Row];
					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController(leadDetailVC,true);
				}
			}
	
			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
			}
		}
	}
}

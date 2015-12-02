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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			var table = new UITableView(View.Bounds); // defaults to Plain style
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new CGSize (bounds.Size.Height, bounds.Size.Width);
			}
			loadingOverlay = new LoadingOverlay (bounds);
			View.Add (loadingOverlay);

			List<Leads> leads = new  List<Leads> ();
			LeadsBL leadsbl = new LeadsBL ();
			leads = leadsbl.GetAllLeads ();
			this.TabBarItem.BadgeValue = leads.Count.ToString();
			loadingOverlay.Hide ();

			ButtonRequestNewLead.TouchUpInside += (object sender, EventArgs e) => {
				View.Add (loadingOverlay);
				List<Leads> newleads = new  List<Leads> ();
//				LeadsBL newleadsbl = new LeadsBL ();
				newleads = leadsbl.GetAllLeads ();
				loadingOverlay.Hide ();
				this.TabBarItem.BadgeValue = newleads.Count.ToString();
				TableViewLeads.Source = new TableSource (newleads, this);
			};

			TableViewLeads.Source = new TableSource (leads, this);
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
					owner.View.AddSubview (leadDetailVC.View);
//				UIWindow window = new UIWindow(UIScreen.MainScreen.Bounds);
//
//				UINavigationController NavigationController = new UINavigationController ();
//					leadDetailVC = NavigationController.TopViewController as LeadDetailVC;
////				leadDetailVC.leadObj = TableItems[indexPath.Row];
//				window.RootViewController = leadDetailVC;
//					window.MakeKeyAndVisible ();
				}
			}
	
			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
			}
		}
	}
}

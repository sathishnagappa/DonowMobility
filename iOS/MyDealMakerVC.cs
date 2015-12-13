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
	partial class MyDealMakerVC : UIViewController
	{
		LoadingOverlay loadingOverlay;
		public MyDealMakerVC (IntPtr handle) : base (handle)
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

			BrokerBL brokerBL = new BrokerBL ();
			List<Broker> brokerList = brokerBL.GetAllBrokers (AppDelegate.UserDetails.Industry,AppDelegate.UserDetails.LineOfBusiness);

			TableViewDealMaker.Source = new TableSource (brokerList,this);

		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			List<Broker> TableItems;
			MyDealMakerVC owner;

			public TableSource (List<Broker> brokerList,MyDealMakerVC owner)
			{
				TableItems = brokerList;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				DealMakerTableViewCell cell = tableView.DequeueReusableCell (CellIdentifier) as DealMakerTableViewCell;
				Broker brokerobj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new DealMakerTableViewCell(CellIdentifier);
				}

				cell.UpdateCell(brokerobj);
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				MyDealMakerDetailVC dealmakerDetailObject = owner.Storyboard.InstantiateViewController ("MyDealMakerDetailVC") as MyDealMakerDetailVC;
				if (dealmakerDetailObject != null) {
					//					dealmakerDetailObject.leadObj = TableItems [indexPath.Row];
					owner.NavigationController.PushViewController (dealmakerDetailObject, true);
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 120.0f;
			}
		}
	}
}

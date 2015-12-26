using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;
using donow.Util;
using CoreGraphics;
using System.Linq;

namespace donow.iOS
{
	partial class LandingCustomerStreamVC : UIViewController
	{
		public LandingCustomerStreamVC (IntPtr handle) : base (handle)
		{
		}

		public LandingCustomerStreamVC () {
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

			List<BingResult>  bingResult = AppDelegate.customerBL.GetBingNewsResult(AppDelegate.UserDetails.Industry + " + Customers");

			TableViewCustomerStream.Source= new CustomerIndustryTableSource(bingResult.OrderByDescending(X => X.Date).ToList(), this);
		}

		public override void ViewWillDisappear (bool animated)
		{
			TableViewCustomerStream.Source = null;
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidLoad ()
		{
			this.Title = "Customer Stream";

			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);

		}

		public class CustomerIndustryTableSource : UITableViewSource {

			List<BingResult> TableItems;
			string CellIdentifier = "TableCell";

			LandingCustomerStreamVC owner;

			public CustomerIndustryTableSource (List<BingResult> meetingList, LandingCustomerStreamVC owner)
			{
				TableItems = meetingList;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				BingResult item = TableItems[indexPath.Row];

				//---- if there are no cells to reuse, create a new one
				if (cell == null)
				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

				cell.ImageView.Image = UIImage.FromBundle("Article 1 Thumb.png");
				cell.TextLabel.Text = item.Title; //+ " - " + getDate(item.Date);
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 0;

				return cell;
			}

//			int getDate(DateTime? date)
//			{
//				var hours = Math.Round((DateTime.Now - DateTime.Parse (date.ToString())).TotalHours);
//				return hours;
//			}

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

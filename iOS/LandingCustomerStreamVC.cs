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
		public bool flag = false;
		public UITableView searchTableView;
		public List<BingResult> bingResult;

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

			bingResult = AppDelegate.customerBL.GetBingNewsResult(AppDelegate.UserDetails.Industry + " + Customers");

			TableViewCustomerStream.Source= new CustomerIndustryTableSource(bingResult.OrderByDescending(X => X.Date).ToList(), this);
		}

		public override void ViewWillDisappear (bool animated)
		{
			TableViewCustomerStream.Source = null;
			base.ViewWillDisappear (animated);
			if (searchTableView == null) {
				TableViewCustomerStream.ReloadData ();
			}
		}

		public override void ViewDidLoad ()
		{
			this.Title = "Customer Stream";

			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);

			// ************ Search Button to be added *****************//

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Magnifying Glass_small.png");
			btn.Clicked += (sender, e) => {
				if (flag==true) {
					flag=false;
					SearchBarCustomerStream.Hidden=true;
					TableViewCustomerStream.Frame =new CGRect (0, 0, this.View.Bounds.Size.Width, 604);
					SearchBarCustomerStream.ResignFirstResponder ();
				}
				else
				{
					flag=true;
					SearchBarCustomerStream.Hidden=false;
					TableViewCustomerStream.Frame =new CGRect (0, 44, this.View.Bounds.Size.Width, 560);
				}
			};
			NavigationItem.RightBarButtonItem = btn;

			SearchBarCustomerStream.Delegate = new SearchDelegate (this, searchTableView);

			// ************ Search Button to be added *****************//
		}

		public class SearchDelegate : UISearchBarDelegate {
			LandingCustomerStreamVC owner;
			//static bool isSearchStarted;
			UITableView _localSearchTableView;

			public SearchDelegate (LandingCustomerStreamVC owner, UITableView _searchTableView)
			{
				_localSearchTableView=_searchTableView;
				this.owner=owner;
			}

			[Foundation.Export("searchBarShouldBeginEditing:")]
			public override Boolean ShouldBeginEditing (UISearchBar searchBar)
			{
				//				owner.isSearchStarted = true;
				return true;
			}

			[Foundation.Export("searchBarShouldEndEditing:")]
			public override Boolean ShouldEndEditing (UISearchBar searchBar)
			{
				//_localSearchTableView.RemoveFromSuperview ();
				return true;
			}

			[Foundation.Export("searchBar:textDidChange:")]
			public override void TextChanged (UISearchBar searchBar, String searchText)
			{
				List<BingResult> PerformSearch = owner.bingResult.Where (x => x.Title.ToLower().Contains (searchBar.Text.ToLower())).ToList ();

				if (searchBar.Text.Length > 0) {
					if (_localSearchTableView == null) {
						_localSearchTableView = new UITableView ();                    
						_localSearchTableView.Frame = new CGRect (0, 44, owner.View.Bounds.Size.Width, 623);
						owner.View.AddSubview (_localSearchTableView);
					}
					_localSearchTableView.Hidden = false;
					_localSearchTableView.Source = new CustomerIndustryTableSource (PerformSearch, owner);
					_localSearchTableView.ReloadData ();

				} else {
					if (_localSearchTableView != null)
						_localSearchTableView.Hidden = true;

					if (owner.SearchBarCustomerStream.Hidden == true) {
						owner.TableViewCustomerStream.Frame =new CGRect (0, 0, owner.View.Bounds.Size.Width, 667);
					}
					else if(owner.SearchBarCustomerStream.Hidden == false)
					{
						owner.TableViewCustomerStream.Frame =new CGRect (0, 44, owner.View.Bounds.Size.Width, 623);
					}
				}
			}
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

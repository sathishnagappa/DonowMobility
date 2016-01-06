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
	partial class MyDealMakerVC : UIViewController
	{		
		public bool flag = false;
		public UITableView searchTableView;
		public List<Broker> brokerList;

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

			if (!AppDelegate.IsFromProspect) {				
				brokerList = AppDelegate.brokerBL.GetAllBrokers (AppDelegate.UserDetails.Industry, AppDelegate.UserDetails.LineOfBusiness).OrderByDescending (X => X.BrokerScore).ToList ();
			}
			else
				brokerList = AppDelegate.brokerBL.GetBrokerForProspect (AppDelegate.CurrentLead.LEAD_ID).OrderByDescending(X => X.BrokerScore).ToList();


			TableViewDealMaker.Source = new TableSource (brokerList,this);

		}

		public override void ViewWillDisappear (bool animated)
		{
			TableViewDealMaker.Source = null;
			base.ViewWillDisappear (animated);
			if (searchTableView == null) {

				TableViewDealMaker.ReloadData ();
			}
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
			//	HambergerMenuVC hambergerVC = this.Storyboard.InstantiateViewController("HambergerMenuVC") as HambergerMenuVC;
				this.NavigationController.PopViewController(true);
			};

			NavigationItem.LeftBarButtonItem = btn;
			this.Title = "Deal Makers";

			// ************ Search Button to be added *****************//

			UIBarButtonItem rightBtn = new UIBarButtonItem ();
			rightBtn.Image = UIImage.FromFile("Magnifying Glass_small.png");
			rightBtn.Clicked += (sender, e) => {
				if (flag==true) {
					flag=false;
					searchBarDealMaker.Hidden=true;
					TableViewDealMaker.Frame =new CGRect (0, 0, this.View.Bounds.Size.Width, 667);
					searchBarDealMaker.ResignFirstResponder();
				}
				else
				{
					flag=true;
					searchBarDealMaker.Hidden=false;
					TableViewDealMaker.Frame =new CGRect (0, 44, this.View.Bounds.Size.Width, 623);
				}
			};
			NavigationItem.RightBarButtonItem = rightBtn;

			searchBarDealMaker.Delegate = new SearchDelegate (this, searchTableView);

			// ************ Search Button to be added *****************//
		}

		public class SearchDelegate : UISearchBarDelegate {
			MyDealMakerVC owner;
			//static bool isSearchStarted;
			UITableView _localSearchTableView;

			public SearchDelegate (MyDealMakerVC owner, UITableView _searchTableView)
			{
				_localSearchTableView=_searchTableView;
				this.owner=owner;
			}

			[Foundation.Export("searchBarShouldBeginEditing:")]
			public virtual Boolean ShouldBeginEditing (UISearchBar searchBar)
			{
//				owner.isSearchStarted = true;
				return true;
			}

			[Foundation.Export("searchBarShouldEndEditing:")]
			public virtual Boolean ShouldEndEditing (UISearchBar searchBar)
			{
				//_localSearchTableView.RemoveFromSuperview ();
				return true;
			}

			[Foundation.Export("searchBar:textDidChange:")]
			public virtual void TextChanged (UISearchBar searchBar, String searchText)
			{
				List<Broker> PerformSearch =owner.brokerList.Where (x => x.City.ToLower().StartsWith (searchBar.Text.ToLower())).ToList ();

				if (searchBar.Text.Length > 0) {
					if (_localSearchTableView == null) {
						_localSearchTableView = new UITableView ();                    
						_localSearchTableView.Frame = new CGRect (0, 44, owner.View.Bounds.Size.Width, 623);
						//                        _localSearchTableView.BackgroundColor = UIColor.Red;

						owner.View.AddSubview (_localSearchTableView);
					}
					_localSearchTableView.Hidden = false;
					_localSearchTableView.Source = new TableSource (PerformSearch, owner);
					_localSearchTableView.ReloadData ();

				} else {
					if (_localSearchTableView != null)
						_localSearchTableView.Hidden = true;

					owner.searchBarDealMaker.ResignFirstResponder ();

					if (owner.searchBarDealMaker.Hidden == true) {
						owner.TableViewDealMaker.Frame =new CGRect (0, 0, owner.View.Bounds.Size.Width, 667);
					}
					else if(owner.searchBarDealMaker.Hidden == false)
					{
						owner.TableViewDealMaker.Frame =new CGRect (0, 44, owner.View.Bounds.Size.Width, 623);
					}
				}
			}
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
				tableView.DeselectRow (indexPath, true);

				if (TableItems [indexPath.Row].Status == 1 || TableItems [indexPath.Row].Status == 2) {
					tableView.DeselectRow (indexPath, true);
					MyDealMakerDetailVC dealmakerDetailObject = owner.Storyboard.InstantiateViewController ("MyDealMakerDetailVC") as MyDealMakerDetailVC;
					if (dealmakerDetailObject != null) {
						dealmakerDetailObject.brokerObj = TableItems [indexPath.Row];
						owner.NavigationController.PushViewController (dealmakerDetailObject, true);
					}
				}
				else if (TableItems [indexPath.Row].Status == 4 || TableItems [indexPath.Row].Status == 5) 
				{
					DealMakerAcceptedReferralRequestlVC dealmakerDetailObject = owner.Storyboard.InstantiateViewController ("DealMakerAcceptedReferralRequestlVC") as DealMakerAcceptedReferralRequestlVC;
					if (dealmakerDetailObject != null) {
					 dealmakerDetailObject.brokerObj = TableItems [indexPath.Row];
						owner.NavigationController.PushViewController (dealmakerDetailObject, true);
					}
				}
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 120.0f;
			}
		}
	}
}

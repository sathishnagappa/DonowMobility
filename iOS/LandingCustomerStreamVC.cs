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

			//bingResult = AppDelegate.customerBL.GetBingNewsResult(AppDelegate.UserDetails.Industry + " + Customers");

			//TableViewCustomerStream.Source= new CustomerIndustryTableSource(bingResult.OrderByDescending(X => X.Date).ToList(), this);
		}

		public override void ViewWillDisappear (bool animated)
		{

			base.ViewWillDisappear (animated);
			//this.Dispose ();
//			if (searchTableView == null) {
//				TableViewCustomerStream.ReloadData ();
//			}
		}

		protected override void Dispose (bool disposing)
		{
//			if (TableViewCustomerStream.Source != null)
//				TableViewCustomerStream.Source.Dispose ();
			base.Dispose (disposing);
		}


		public override void ViewDidLoad ()
		{
			this.Title = "My Dashboard";

			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);

			ScrollViewDashboard.ContentSize = new CGSize (375, 870);

			Dashboard dashboardObj = AppDelegate.userBL.GetDashBoardDetails (AppDelegate.UserDetails.UserId);

			ButtonUpdateProfile.Layer.BorderWidth = 2.0f;
			ButtonUpdateProfile.Layer.BorderColor = UIColor.FromRGB (45, 125, 177).CGColor;
			ButtonUpdateProfile.Layer.CornerRadius = 3.0f;

			ButtonUpdateProfile.TouchUpInside += (object sender, EventArgs e) => {
				signUpOtherDetailsVC userInfo = this.Storyboard.InstantiateViewController ("signUpOtherDetailsVC") as signUpOtherDetailsVC;
				if(userInfo != null)
					userInfo.isFromDashBoard = true;
					this.NavigationController.PresentViewController (userInfo, true, null);		
			};

			ButtonGoToLeads.Layer.CornerRadius = 3.0f;
			ButtonGoToLeads.TouchUpInside += (object sender, EventArgs e) => {
				this.TabBarController.SelectedIndex = 2;
			};
			ButtonGoToDeals.Layer.CornerRadius = 3.0f;
			ButtonGoToDeals.TouchUpInside += (object sender, EventArgs e) => {
				this.TabBarController.SelectedIndex = 3;
			};
			LabelCustomerName.Text = AppDelegate.UserDetails.FullName;
			LabelTitle.Text = AppDelegate.UserDetails.Title;
			LabelCompanyName.Text = AppDelegate.UserDetails.Company;
			LabelCityState.Text = AppDelegate.UserDetails.City + ", " + AppDelegate.UserDetails.State;
			string nextMeeting = string.IsNullOrEmpty (dashboardObj.next_meeting) == true ? "" : DateTime.Parse (dashboardObj.next_meeting).ToString ("MMM. dd, yyyy  hh:mm tt");
			string title = string.IsNullOrEmpty (dashboardObj.CustomerName) == true ? "NA" : "Meeting W/" + dashboardObj.CustomerName;
			TextViewNextMeeting.Text = title + "\n" + nextMeeting;
			LabelTotalCustomers.Text = "Total Customers : " + dashboardObj.total_customers.ToString();
//			TextViewCRM.Font = UIFont.FromName ("Arial", 16f);
	
			TextViewCRM.Text = "Total Leads : " + dashboardObj.crm_total_leads.ToString() + "\n"
				+ "Leads with DealMakers : " + dashboardObj.crm_leads_with_dealmakers.ToString() + "\n"
				+ "Leads without DealMakers : " + dashboardObj.crm_leads_without_dealmakers.ToString();

			TextViewDonow.Text = "Total Requested : " + dashboardObj.dn_total_leads.ToString() + "\n"
				+ "Total Accepted : " + dashboardObj.dn_total_leads_accepted.ToString() + "\n"
				+ "Leads with DealMakers : " + dashboardObj.dn_leads_with_dealmakers.ToString() + "\n"
				+ "Leads without DealMakers : " + dashboardObj.dn_leads_without_dealmakers.ToString();

			LabelTotalEarnings.Text = "Total Earnings : " + dashboardObj.total_earning + "\n";
			TextViewDealMakers.Text = "Total Deal Requests : " + dashboardObj.total_lead_requests.ToString() + "\n"
				+ "Deals Accepted : " + dashboardObj.total_accepted.ToString() + "\n"
				+ "Deals Referred : " + dashboardObj.total_referred.ToString();

			if (string.IsNullOrEmpty (dashboardObj.next_meeting)) {
				ButtonNextMeeting.Hidden = true;
				ImageShowMore.Hidden = true;
			}

			ButtonNextMeeting.TouchUpInside += (object sender, EventArgs e) => {
				MyMeetingsVC myMeetingsObj = this.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				if (myMeetingsObj != null) {
					myMeetingsObj.meetingObj = new UserMeetings();
					myMeetingsObj.meetingObj.CustomerName = dashboardObj.CustomerName;
					myMeetingsObj.meetingObj.StartDate = dashboardObj.next_meeting;
					myMeetingsObj.meetingObj.City = dashboardObj.City;
					myMeetingsObj.meetingObj.State = dashboardObj.State;
					this.NavigationController.PushViewController(myMeetingsObj,true);
				}
			};
		}

//		public class SearchDelegate : UISearchBarDelegate {
//			LandingCustomerStreamVC owner;
//			//static bool isSearchStarted;
//			UITableView _localSearchTableView;
//
//			public SearchDelegate (LandingCustomerStreamVC owner, UITableView _searchTableView)
//			{
//				_localSearchTableView=_searchTableView;
//				this.owner=owner;
//			}
//
//			[Foundation.Export("searchBarShouldBeginEditing:")]
//			public override Boolean ShouldBeginEditing (UISearchBar searchBar)
//			{
//				//				owner.isSearchStarted = true;
//				return true;
//			}
//
//			[Foundation.Export("searchBarShouldEndEditing:")]
//			public override Boolean ShouldEndEditing (UISearchBar searchBar)
//			{
//				//_localSearchTableView.RemoveFromSuperview ();
//				return true;
//			}
//
//			[Foundation.Export("searchBar:textDidChange:")]
//			public override void TextChanged (UISearchBar searchBar, String searchText)
//			{
//				List<BingResult> PerformSearch = owner.bingResult.Where (x => x.Title.ToLower().Contains (searchBar.Text.ToLower())).ToList ();
//
//				if (searchBar.Text.Length > 0) {
//					if (_localSearchTableView == null) {
//						_localSearchTableView = new UITableView ();                    
//						_localSearchTableView.Frame = new CGRect (0, 44, owner.View.Bounds.Size.Width, 623);
//						owner.View.AddSubview (_localSearchTableView);
//					}
//					_localSearchTableView.Hidden = false;
//					_localSearchTableView.Source = new CustomerIndustryTableSource (PerformSearch, owner);
//					_localSearchTableView.ReloadData ();
//
//				} else {
//					owner.SearchBarCustomerStream.ResignFirstResponder ();
//
//					if (_localSearchTableView != null)
//						_localSearchTableView.Hidden = true;
//
//					if (owner.SearchBarCustomerStream.Hidden == true) {
//						owner.TableViewCustomerStream.Frame =new CGRect (0, 0, owner.View.Bounds.Size.Width, 667);
//					}
//					else if(owner.SearchBarCustomerStream.Hidden == false)
//					{
//						owner.TableViewCustomerStream.Frame =new CGRect (0, 44, owner.View.Bounds.Size.Width, 623);
//					}
//				}
//			}
//		}
//
//		public class CustomerIndustryTableSource : UITableViewSource {
//
//			List<BingResult> TableItems;
//			string CellIdentifier = "TableCell";
//
//			LandingCustomerStreamVC owner;
//
//			public CustomerIndustryTableSource (List<BingResult> meetingList, LandingCustomerStreamVC owner)
//			{
//				TableItems = meetingList;
//				this.owner = owner;
//			}
//
//			public override nint RowsInSection (UITableView tableview, nint section)
//			{
//				return TableItems.Count;
//			}
//
//			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
//			{
//				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
//				BingResult item = TableItems[indexPath.Row];
//
//				//---- if there are no cells to reuse, create a new one
//				if (cell == null)
//				{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }
//
//				cell.ImageView.Image = UIImage.FromBundle("Article 1 Thumb.png");
//				cell.TextLabel.Text = item.Title; //+ " - " + getDate(item.Date);
//				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
//				cell.TextLabel.Lines = 0;
//
//				return cell;
//			}
//
////			int getDate(DateTime? date)
////			{
////				var hours = Math.Round((DateTime.Now - DateTime.Parse (date.ToString())).TotalHours);
////				return hours;
////			}
//
//			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
//			{
//				BingSearchVC bingSearchVC = owner.Storyboard.InstantiateViewController ("BingSearchVC") as BingSearchVC;
//				if (bingSearchVC != null) {
//					bingSearchVC.webURL = TableItems [indexPath.Row].Url;
//					//owner.View.AddSubview (leadDetailVC.View);
//					owner.NavigationController.PushViewController (bingSearchVC, true);
//				}
//				tableView.DeselectRow (indexPath, true);
//			}
//
//			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
//			{
//				return 105.0f;
//			}
//		}
	}
}

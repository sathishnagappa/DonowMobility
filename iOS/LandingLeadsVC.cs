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
using System.Threading;
using System.Threading.Tasks;

namespace donow.iOS
{
	partial class LandingLeadsVC : UIViewController
	{
		public static List<LeadMaster> leads;
		Leads leadDetails;
		public bool flag = false;
		public string txtSearched = string.Empty;
		public UITableView searchTableView;

		public LandingLeadsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.Title = "Lead";
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

			if(AppDelegate.UserDetails.LeadCount > 0)
				leads = GetLeads ();

			if (leads != null && leads.Count > 0) {
				this.NavigationController.TabBarItem.BadgeValue = leads.Where (x => x.USER_LEAD_STATUS == 1).ToList ().Count.ToString();
				TableViewLeads.Source = new TableSource (leads, this);
			} else if(AppDelegate.IsFromSignUp) {
				AlertView.Hidden = false;
				LabelAlertView.Hidden = false;
			}

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			if(AppDelegate.UserDetails.MeetingCount > 0)
				GetLeadUpdatePage ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			TableViewLeads.Source = null;
			if (searchTableView == null) {
				TableViewLeads.ReloadData ();
			}
			leads = null;
		}

		protected override void Dispose (bool disposing)
		{
			if (TableViewLeads.Source != null)
				TableViewLeads.Source.Dispose ();
			base.Dispose (disposing);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationItem.Title = "Leads";
			this.NavigationItem.SetHidesBackButton (true, false);


			// ************ Search Button to be added *****************//

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Magnifying Glass_small.png");
			btn.Clicked += (sender, e) => {
				if (flag==true) {
					flag=false;
					searchBarLeads.Hidden=true;
					topView.Frame = new CGRect (0,0,topView.Frame.Size.Width,124);
					this.TableViewLeads.Frame =new CGRect (0, 124, TableViewLeads.Frame.Size.Width, 480);
					searchBarLeads.ResignFirstResponder();
				}
				else
				{
					flag=true;
					searchBarLeads.Hidden=false;				
					topView.Frame = new CGRect (0, 44, topView.Frame.Size.Width, 124);
					this.TableViewLeads.Frame =new CGRect (0, 168, TableViewLeads.Frame.Size.Width, 436);
				}
			};
			NavigationItem.RightBarButtonItem = btn;

			// ************ Search Button to be added *****************//

			searchBarLeads.Delegate = new SearchDelegate (this, searchTableView);

			LabelAlertView.Layer.CornerRadius = 5.0f;
			ButtonOk.Layer.CornerRadius = 5.0f;

			ButtonOk.TouchUpInside += (object sender, EventArgs e) =>  {
				AlertView.Hidden = true;
				LabelAlertView.Hidden = true;
			};

			ButtonRequestNewLead.TouchUpInside +=  (object sender, EventArgs e) => {

				leads =  GetNewLeads();
				if (leads.Count > 0) {
					AppDelegate.UserDetails.LeadCount = AppDelegate.UserDetails.LeadCount + 1;
					this.NavigationController.TabBarItem.BadgeValue = leads.Where (x => x.USER_LEAD_STATUS == 1).ToList ().Count.ToString();
				TableViewLeads.Source = new TableSource (leads, this);
				TableViewLeads.ReloadData ();
				} else {
					AlertView.Hidden = false;
					LabelAlertView.Hidden = false;
				}

			};


		}

		public class SearchDelegate : UISearchBarDelegate {
			LandingLeadsVC owner;
			//static bool isSearchStarted;
			UITableView _localSearchTableView;

			public SearchDelegate (LandingLeadsVC owner, UITableView _searchTableView)
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
				List<LeadMaster> PerformSearch = LandingLeadsVC.leads.Where (x => x.COMPANY_NAME.ToLower().StartsWith (searchBar.Text.ToLower())).ToList ();

				if (searchBar.Text.Length > 0) {
					if (_localSearchTableView == null) {
						_localSearchTableView = new UITableView ();                    
						_localSearchTableView.Frame = new CGRect (0, 168, owner.View.Bounds.Size.Width, 480);
						//                        _localSearchTableView.BackgroundColor = UIColor.Red;

						owner.View.AddSubview (_localSearchTableView);
					}
					_localSearchTableView.Hidden = false;
					_localSearchTableView.Source = new TableSource (PerformSearch, owner);
					_localSearchTableView.ReloadData ();

				} else {
					   owner.searchBarLeads.ResignFirstResponder ();

					if (_localSearchTableView != null)
						_localSearchTableView.Hidden = true;
					
					if (owner.searchBarLeads.Hidden == true) {
						owner.topView.Frame = new CGRect (0,0,owner.topView.Frame.Size.Width,124);
						owner.TableViewLeads.Frame =new CGRect (0, 124, owner.TableViewLeads.Frame.Size.Width, 480);
					}
					else if(owner.searchBarLeads.Hidden == false)
					{
						owner.topView.Frame = new CGRect (0, 44, owner.topView.Frame.Size.Width, 124);
						owner.TableViewLeads.Frame =new CGRect (0, 168, owner.TableViewLeads.Frame.Size.Width, 436);
					}
				}
			}
		}

		List<LeadMaster> GetLeads()
		{
			leads =  AppDelegate.leadsBL.GetAllLeads (AppDelegate.UserDetails.UserId);
			return leads;
		}

		List<LeadMaster> GetNewLeads()
		{
			leads =  AppDelegate.leadsBL.GetNewLeads(AppDelegate.UserDetails.UserId);
			return leads;
		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			List<LeadMaster> TableItems;
			LandingLeadsVC owner;

			public TableSource (List<LeadMaster> items, LandingLeadsVC owner)
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
				LeadMaster leadObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new LeadTableCell(CellIdentifier);
				}
					
				cell.UpdateCell(leadObj);
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{				
				tableView.DeselectRow (indexPath, true);
				//AppDelegate.CurrentLead = TableItems [indexPath.Row];
				owner.leadDetails = AppDelegate.leadsBL.GetLeadsDetails (TableItems [indexPath.Row].LEAD_ID,AppDelegate.UserDetails.UserId,TableItems [indexPath.Row].LEAD_SOURCE);
				//owner.leadDetails = AppDelegate.leadsBL.GetLeadsDetails (TableItems [indexPath.Row].LEAD_ID,AppDelegate.UserDetails.UserId);
				AppDelegate.CurrentLead = owner.leadDetails;
				if ((owner.leadDetails.USER_LEAD_STATUS == 1 || owner.leadDetails.USER_LEAD_STATUS == 2) && owner.leadDetails.LEAD_SOURCE == 1) 
				{
					if (owner.leadDetails.STATUS.ToUpper() != "ACCEPTED") {
						LeadDetailVC leadDetailVC = owner.Storyboard.InstantiateViewController ("LeadDetailVC") as LeadDetailVC;
						if (leadDetailVC != null) {
							leadDetailVC.leadObj = owner.leadDetails;
							owner.NavigationController.PushViewController (leadDetailVC, true);
						}
					}
					else
					{
						UIAlertView alert = new UIAlertView () { 
							Title = "Alert", 
							Message = "Lead is Accepted by another Seller."
						};
						alert.AddButton ("OK");
						alert.Show ();
					}

				} else if (owner.leadDetails.USER_LEAD_STATUS != 5) {
					prospectDetailsVC prospectVC = owner.Storyboard.InstantiateViewController ("dummyViewController") as prospectDetailsVC;
					if (prospectVC != null) {
						prospectVC.localLeads = owner.leadDetails;
						owner.NavigationController.PushViewController (prospectVC, true);
					}

				}
			}
	
			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 140.0f;
			}

			public override nfloat EstimatedHeight (UITableView tableView, NSIndexPath indexPath)
			{
				return 140.0f;
			}
		}

		void GetLeadUpdatePage()
		{

			List<UserMeetings> userMeetings = new List<UserMeetings> ();
			userMeetings = AppDelegate.userBL.GetMeetingsByUserName (AppDelegate.UserDetails.UserId);



			if (userMeetings.Count > 0) {
				TimerCallback timerDelegate = new TimerCallback (CheckStatus);
				// Create a timer that waits one second, then invokes every second.
				DateTime meeetingEndDate;
				foreach (var item in userMeetings) {

					meeetingEndDate = DateTime.Parse (item.EndDate);
					TimeSpan span = meeetingEndDate.Subtract(DateTime.Now);

					if (DateTime.Compare (meeetingEndDate, DateTime.Now) > 0 && span.Days < 50) {
						Timer timer = new Timer (timerDelegate, item, span, Timeout.InfiniteTimeSpan);
					}
					if ((DateTime.Compare (DateTime.Parse (item.EndDate), DateTime.Now) <= 0) && item.Status != "Done") {

						LeadUpdateVC leadUpdateVC = this.Storyboard.InstantiateViewController ("LeadUpdateVC") as LeadUpdateVC;
						if (leadUpdateVC != null) {
							leadUpdateVC.meetingObj = item;
							this.PresentViewController (leadUpdateVC, true, null);
						}

					}
				}
			}

		}

		void CheckStatus(Object userMeeting) {
			InvokeOnMainThread(() =>
				{
						LeadUpdateVC leadUpdateVC = this.Storyboard.InstantiateViewController ("LeadUpdateVC") as LeadUpdateVC;
						if (leadUpdateVC != null) {
							leadUpdateVC.meetingObj = (UserMeetings)userMeeting;
							this.PresentViewController (leadUpdateVC,true,null);
						}

				});


			
		}

	}
}

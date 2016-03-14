using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;
using System.Drawing;
using MessageUI;


namespace donow.iOS
{
	partial class LandingCustomerVC : UIViewController
	{
		//private Dictionary<string,List<Customer>> custDictionary;
		public bool isScrolledTop;
		public  bool isSearchStarted;
		//protected int sectionHit;
		private static int cellIndexCount;
		public static List<Customer> CustomerList { get; set; }
		public string txtSearched;
		public UITableView searchTableView;
		public static string tempStr = "";
		//public static string txtEntered;
		public static List<Customer> cusotmerList;

		public static int searchCount=0;
		public bool flag;
		public LandingCustomerVC (IntPtr handle) : base (handle)
		{


		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			//custDictionary = new Dictionary<string, List<Customer>> ();
//			cellIndexCount = 0;
			LoadCustomers ();

			searchBarCustomer.Hidden=true;
			TableViewCustomerList.Frame =new CGRect (0, 0, this.TableViewCustomerList.Frame.Size.Width, this.TableViewCustomerList.Frame.Size.Height);
//			TableViewCustomerList.ScrolledToTop += (object sender, EventArgs e) => 
//			{
//				isScrolledTop = true;
//			};
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			//this.Dispose ();
			TableViewCustomerList.Source = null;
			if (searchTableView == null) {
				TableViewCustomerList.ReloadData ();
			}
		}

		protected override void Dispose (bool disposing)
		{
			if (TableViewCustomerList.Source != null)
				TableViewCustomerList.Source.Dispose ();
			base.Dispose (disposing);
		}


		public override void ViewDidLoad ()
		{

			//LoadCustomers ();
			this.NavigationItem.Title = "Customers";
			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);


			searchBarCustomer.Delegate = new SearchDelegate (this, isSearchStarted,searchTableView);

			searchCount=0;

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Magnifying Glass_small.png");
			btn.Clicked += (sender , e)=>{
//				this.NavigationController.PopViewController(false);


				if (flag==true) {
					flag=false;

					searchBarCustomer.Hidden=true;

					searchBarCustomer.ResignFirstResponder();
					this.TableViewCustomerList.Frame =new CGRect (this.TableViewCustomerList.Frame.X, 0, this.TableViewCustomerList.Frame.Size.Width, this.TableViewCustomerList.Frame.Size.Height);

				}
				else
				{
					flag=true;
					searchBarCustomer.Hidden=false;
					this.TableViewCustomerList.Frame =new CGRect (this.TableViewCustomerList.Frame.X, 45, this.TableViewCustomerList.Frame.Size.Width, this.TableViewCustomerList.Frame.Size.Height);
				}
			};
//			};
			NavigationItem.RightBarButtonItem = btn;
			txtSearched = searchBarCustomer.Text;
		}

		public void changeText (List<Customer> PerformSearch) {
			TableViewCustomerList.Source = new TableSource(PerformSearch,isSearchStarted,this);
			TableViewCustomerList.ReloadData ();
		}

		public class SearchDelegate : UISearchBarDelegate {
			LandingCustomerVC owner;
			//static bool isSearchStarted;
			UITableView _localSearchTableView;

			public SearchDelegate (LandingCustomerVC owner, bool isSearchStarted,UITableView _searchTableView)
			{
				_localSearchTableView=_searchTableView;
				this.owner=owner;
			}

			[Foundation.Export("searchBarShouldBeginEditing:")]
			public override Boolean ShouldBeginEditing (UISearchBar searchBar)
			{
				owner.isSearchStarted = true;
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
				List<Customer> PerformSearch = LandingCustomerVC.CustomerList.Where (x => x.Company.ToLower().StartsWith (searchBar.Text.ToLower())).ToList ();

				if (searchBar.Text.Length > 0) {
					if (_localSearchTableView == null) {
						_localSearchTableView = new UITableView ();                    
						_localSearchTableView.Frame = new CGRect (0, 44, owner.TableViewCustomerList.Frame.Size.Width, owner.TableViewCustomerList.Frame.Size.Height);
						//                        _localSearchTableView.BackgroundColor = UIColor.Red;

						owner.View.AddSubview (_localSearchTableView);
					}
					_localSearchTableView.Hidden = false;
					_localSearchTableView.Source = new SearchTableSource (PerformSearch, owner);
					_localSearchTableView.ReloadData ();

				} else {
					if (_localSearchTableView != null)
						_localSearchTableView.Hidden = true;

					owner.searchBarCustomer.ResignFirstResponder ();

					if (owner.searchBarCustomer.Hidden == true) {
						owner.TableViewCustomerList.Frame =new CGRect (owner.TableViewCustomerList.Frame.X, 0, owner.TableViewCustomerList.Frame.Size.Width, owner.TableViewCustomerList.Frame.Size.Height);
					}
					else if(owner.searchBarCustomer.Hidden == false)
					{
						owner.TableViewCustomerList.Frame =new CGRect (owner.TableViewCustomerList.Frame.X, 45, owner.TableViewCustomerList.Frame.Size.Width, owner.TableViewCustomerList.Frame.Size.Height);
					}
				}
			}
		}


		public class SearchTableSource: UITableViewSource
		{
			List<Customer> _searchList;
			LandingCustomerVC customerVC;
			string CellIdentifier = "TableCell";

			public SearchTableSource (List<Customer> items, LandingCustomerVC owner)
			{
				_searchList = items;
				customerVC = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return _searchList.Count;
			}
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.Hidden = true;
				tableView.DeselectRow (indexPath, true);
				customerProfileVC customerDetailVC = customerVC.Storyboard.InstantiateViewController ("customerProfileVC") as customerProfileVC;
				if (customerDetailVC != null) {
					customerDetailVC.customer = _searchList[indexPath.Row];
					customerVC.NavigationController.PushViewController(customerDetailVC,true);
				}
			}
			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellLeads;
				//Customer customerObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new CustomerViewTableCellLeads (CellIdentifier);
				}

				cell.UpdateCell(_searchList[indexPath.Row]);


				return cell;
			}
			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 80.0f;
			}
		}
		void LoadCustomers()
		{

			cusotmerList =  AppDelegate.customerBL.GetAllCustomers (AppDelegate.UserDetails.UserId);
			CustomerList = cusotmerList;
		   TableViewCustomerList.Source = new TableSource (cusotmerList,isSearchStarted,  this);

		}

		public class TableSource : UITableViewSource {
			//List<Customer> _searchList;

			//string txtSearchedLocal;
			//protected int TappedIndex;
			string[] _arraySectionTitle;
			string CellIdentifier = "TableCell1";

			//bool isSearchStarted;
			Dictionary<string,List<Customer>> TableItemsDictionary,custDictionary;
			LandingCustomerVC owner;

			public List<string> headerArray = new List <string>
			{
				"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O",
				"P","Q", "R", "S", "T", "U", "V","W", "X", "Y", "Z"
			};



			public TableSource (List<Customer> items,bool isSearchStarted1, LandingCustomerVC owner)
			{
				//isSearchStarted=isSearchStarted1;
				custDictionary=new Dictionary<string, List<Customer>>();
				//txtSearchedLocal=txtSearched;
				for (char c = 'A'; c <= 'Z'; c++)
				{
					custDictionary.Add (c.ToString(), items.FindAll (x => x.Name.StartsWith (c.ToString())));
				} 
				//string[] TableItems;
				TableItemsDictionary=new Dictionary<string, List<Customer>>();
				TableItemsDictionary = custDictionary;
				//_searchList = items;

				this.owner = owner;


			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				//int unicode = Convert.ToInt32(section) + 65;
				//char character = (char) unicode;
				//string text = character.ToString();
				string text = _arraySectionTitle [section];
				//                int str=Convert.ToInt32(section);
				//                TappedIndex = Convert.ToInt32 (str);
				//                string indexValue = headerArray [Convert.ToInt32 (section)];
				var sectionRowCount=TableItemsDictionary.Where(x=>x.Key==text).FirstOrDefault().Value.Count();
				return sectionRowCount;
			}

			int tappedCellIndex = 0;
			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellLeads;
				//Customer customerObj = TableItems[indexPath.Row];

				if (cell == null) 
				{
					cell = new CustomerViewTableCellLeads(CellIdentifier);
				}

				if (tappedCellIndex != indexPath.Section)
					cellIndexCount = 0;
				tappedCellIndex = indexPath.Section;
				var result = TableItemsDictionary.Values;
				List<Customer> _list = new List<Customer> ();
				string tappedKey = _arraySectionTitle [indexPath.Section];

				if (TableItemsDictionary.ContainsKey (tappedKey)) {
					_list = TableItemsDictionary.FirstOrDefault (x => x.Key == tappedKey).Value;
					//_list = TableItemsDictionary.FirstOrDefault (x => x.Key.Equals("K"));
				}

				if (_list != null && _list.Any ()) {
//					if (cellIndexCount < _list.Count) {						
						cell.UpdateCell (_list [indexPath.Row]);//check		
//						};
					}
				return cell;
			}

			public override nint NumberOfSections (UITableView tableView)
			{
				var result=TableItemsDictionary.Keys;
				List<string> sectionlist = new List<string> ();
				foreach (var item in TableItemsDictionary.Keys) {
					if (TableItemsDictionary.FirstOrDefault (x => x.Key == item).Value.Count > 0)
						sectionlist.Add (item);
				}
				//var values = TableItemsDictionary.Values.Where (x => x.Count > 0);
				//_arraySectionTitle=result.ToArray ();
				//return _arraySectionTitle.Count();
				_arraySectionTitle=sectionlist.ToArray ();
				return _arraySectionTitle.Count();
			}
			//public override string TitleForHeader (UITableView tableView, nint section)
			//{


//				var result=TableItemsDictionary.Keys;
//				var values = TableItemsDictionary.FirstOrDefault (x => x.Key == _arraySectionTitle[section]).Value;
//				_arraySectionTitle=result.ToArray ();
//
//				//                string str=(section).ToString();
//				//                TappedIndex = Convert.ToInt32 (str);
//
//				return _arraySectionTitle[section];

			//}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				//var values = TableItemsDictionary.FirstOrDefault (x => x.Key == _arraySectionTitle[section]).Value;

				//if (values.Count > 0) {
					UIView headerView = new UIView ();
					headerView.Frame = new CoreGraphics.CGRect (10, 0, 414, 50);
					headerView.BackgroundColor = UIColor.FromRGB (232, 231, 231);

					UILabel headerLabel = new UILabel ();
					headerLabel.Frame = new CoreGraphics.CGRect (36, 0, 20, 50);
					headerLabel.TextColor = UIColor.Black; // Set your color
					headerLabel.Font = UIFont.FromName ("Arial-BoldMT", 25f);
					headerLabel.Text = _arraySectionTitle [section];
					headerLabel.TextAlignment = UITextAlignment.Center;

					headerView.AddSubview (headerLabel);
					return headerView;
				//} else
				//	return null;
			}


			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				string tappedKey = _arraySectionTitle [indexPath.Section];
				customerProfileVC customerDetailVC = owner.Storyboard.InstantiateViewController ("customerProfileVC") as customerProfileVC;
				if (customerDetailVC != null) {
					customerDetailVC.customer = TableItemsDictionary[tappedKey][indexPath.Row];
					owner.NavigationController.PushViewController(customerDetailVC,true);
				}
			}

//			public override string[] SectionIndexTitles (UITableView tableView)
//			{
//
//				//string[] _arrayString = new string[26];
//				return headerArray.ToArray();
//
//			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}
		}
	}
}
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;

namespace donow.iOS
{
	partial class LandingCustomerVC : UIViewController
	{
		private Dictionary<string,List<Customer>> custDictionary;

		protected int sectionHit;

		public LandingCustomerVC (IntPtr handle) : base (handle)
		{
			custDictionary = new Dictionary<string, List<Customer>> ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			//			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
			//			this.NavigationController.NavigationItem.SetLeftBarButtonItem( new UIBarButtonItem(UIImage.FromFile("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
			//				this.NavigationController.PopViewController(true);
			//			}), true);
		}

		public override void ViewDidLoad ()
		{


			//			CustomerBL customerBL = new CustomerBL ();
			//			List<Customer> cusotmerList =  customerBL.GetAllCustomers ();
			//
			//			TableViewCustomerList.Source = new TableSource (cusotmerList.OrderBy(X => X.Name).ToList(), this);
			//			this.Title = "Customers";
			//
			//			this.NavigationItem.SetHidesBackButton (true, false);
			//			this.NavigationItem.SetLeftBarButtonItem(null, true);

			CustomerBL customerBL = new CustomerBL ();
			List<Customer> cusotmerList = customerBL.GetAllCustomers ().OrderBy (x => x.Name).ToList ();
			//            custDictionary = new Dictionary<string, List<Customer>> ();

			for (char c = 'A'; c <= 'Z'; c++)
			{
				custDictionary.Add (c.ToString(), cusotmerList.FindAll (x => x.Name.StartsWith (c.ToString())));
			} 
			//			 _arraySectionTitle=new string[custDictionary.Count];
			//
			//			var result=custDictionary.Keys;
			//			_arraySectionTitle=result.ToArray ();

			TableViewCustomerList.Source = new TableSource (custDictionary, this);
			this.Title = "Customers";

			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.SetLeftBarButtonItem(null, true);
		}

		public class TableSource : UITableViewSource {
			List<Customer> _list;

			protected int TappedIndex;
			string[] _arraySectionTitle;
			string CellIdentifier = "TableCell";
			Dictionary<string,List<Customer>> TableItemsDictionary;
			LandingCustomerVC owner;

			public List<String> headerArray = new List <String>
			{
				"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O",
				"P","Q", "R", "S", "T", "U", "V","W", "X", "Y", "Z"
			};

			public TableSource ( Dictionary<string,List<Customer>> items, LandingCustomerVC owner)
			{
				string[] TableItems;
				TableItemsDictionary=new Dictionary<string, List<Customer>>();
				TableItemsDictionary = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return TableItemsDictionary.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerViewTableCellLeads;
				//Customer customerObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new CustomerViewTableCellLeads(CellIdentifier);
				}
				var result=TableItemsDictionary.Values;
				List<Customer> _list = new List<Customer> ();
				string tappedKey = _arraySectionTitle [TappedIndex];
				if (TableItemsDictionary.ContainsKey (tappedKey)) 
				{
					_list=TableItemsDictionary.FirstOrDefault(x=>x.Key==tappedKey).Value;
					//_list = TableItemsDictionary.FirstOrDefault (x => x.Key.Equals("K"));
				}
//				
				cell.UpdateCell(_list[indexPath.Row]);
				return cell;

			}
		
			public override nint NumberOfSections (UITableView tableView)
			{
				var result=TableItemsDictionary.Keys;
				_arraySectionTitle=result.ToArray ();
				return _arraySectionTitle.Count();
			}
			public override string TitleForHeader (UITableView tableView, nint section)
			{

				//string[] _arraySectionTitle=new string[TableItemsDictionary.Count];

				var result=TableItemsDictionary.Keys;
				_arraySectionTitle=result.ToArray ();
				//				string[] _arrayString = new string[26];
				//
				//				_arrayString=_arr
				//				//table.Source = new TableSource(_arraySectionTitle);
				//				return _arrayString=_arraySectionTitle.ToArray
//				string[] _arrayString = new string[26];
				string str=(section).ToString();
				TappedIndex = Convert.ToInt32 (str);

				return _arraySectionTitle[section];

			}
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				customerProfileVC customerDetailVC = owner.Storyboard.InstantiateViewController ("customerProfileVC") as customerProfileVC;
				if (customerDetailVC != null) {
					//customerDetailVC.customer = TableItems[indexPath.Row];
					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController(customerDetailVC,true);
				}
			}


			public override string[] SectionIndexTitles (UITableView tableView)
			{

				string[] _arrayString = new string[26];
				return _arrayString=headerArray.ToArray();

			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}

			//			public virtual UITableViewHeaderFooterView GetHeaderView (nint section) {
			//
			//				UIView headerView = new UIView ();
			//				headerView.Frame = new CGRect (0, 0, 414, 40);
			//
			//				UILabel headerLabel = new UILabel ();
			//				headerLabel.Frame = new CGRect (0, 0, 414, 40);
			//
			//				headerLabel.BackgroundColor = UIColor.LightGray;
			//				headerView.AddSubview (headerLabel);
			//
			//				headerLabel.Text = headerArray[(int)section];
			//
			////				return headerView;
			//			}
			//
			////			public virtual nint NumberOfRowsInSection (nint section) {
			////
			////
			////			}
			//
			//			public virtual nint NumberOfSections () {
			//				return 26;
			//			}
		}
	}
}

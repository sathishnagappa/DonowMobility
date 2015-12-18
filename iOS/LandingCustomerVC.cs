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
		private static int cellIndexCount;


		public LandingCustomerVC (IntPtr handle) : base (handle)
		{
			custDictionary = new Dictionary<string, List<Customer>> ();
			cellIndexCount = 0;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

		}

		public override void ViewDidLoad ()
		{
		
			CustomerBL customerBL = new CustomerBL ();
			List<Customer> cusotmerList = customerBL.GetAllCustomers ().OrderBy (x => x.Name).ToList ();
			//            custDictionary = new Dictionary<string, List<Customer>> ();

			for (char c = 'A'; c <= 'Z'; c++)
			{
				custDictionary.Add (c.ToString(), cusotmerList.FindAll (x => x.Name.StartsWith (c.ToString())));
			} 

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
			public List<string> headerArray = new List <string>
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
				int unicode = Convert.ToInt32(section) + 65;
				char character = (char) unicode;
				string text = character.ToString();
//				int str=Convert.ToInt32(section);
//				TappedIndex = Convert.ToInt32 (str);
//				string indexValue = headerArray [Convert.ToInt32 (section)];
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
					cell = new CustomerViewTableCellLeads(null);
				}

				if (tappedCellIndex != indexPath.Section)
					cellIndexCount = 0;
				tappedCellIndex = indexPath.Section;
				var result=TableItemsDictionary.Values;
				List<Customer> _list = new List<Customer> ();
				string tappedKey = _arraySectionTitle [indexPath.Section];

				if (TableItemsDictionary.ContainsKey (tappedKey)) 
				{
					_list = TableItemsDictionary.FirstOrDefault (x => x.Key == tappedKey).Value;
					//_list = TableItemsDictionary.FirstOrDefault (x => x.Key.Equals("K"));
				}
//				if (cellIndexCount > _list.Count)
//					return null;
		
				if (_list != null && _list.Any ()) {
					
						if (cellIndexCount <= _list.Count) 
						{
							cell.UpdateCell (_list [cellIndexCount++]);//check
						}

				}
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


				var result=TableItemsDictionary.Keys;
				_arraySectionTitle=result.ToArray ();
		
//				string str=(section).ToString();
//				TappedIndex = Convert.ToInt32 (str);

				return _arraySectionTitle[section];

			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				UIView headerView = new UIView ();
				headerView.Frame=new CoreGraphics.CGRect (10, 0, 414, 53);
				headerView.BackgroundColor = UIColor.FromRGB(232,231,231);

				UILabel headerLabel = new UILabel ();
				headerLabel.Frame = new CoreGraphics.CGRect (36, 10, 150, 30);
				headerLabel.TextColor = UIColor.Black; // Set your color
				headerLabel.Font=UIFont.FromName("Arial-BoldMT",25f);
				headerLabel.Text = _arraySectionTitle[section];

				headerView.AddSubview (headerLabel);
				return headerView;
			}

		
//			public override UIView GetViewForHeader (UITableView tableView, nint section)
//			{
//				
//				UIView headerView = new UIView ();
//				headerView.Frame = new CoreGraphics.CGRect (10, 20, 100, 10);
//				headerView.BackgroundColor = UIColor.LightGray; 
//
//
//				UILabel headerLabel = new UILabel ();
//				headerLabel.Frame = new CoreGraphics.CGRect (10, 20, 10, 10);
//				// Set the frame size you need
//				headerLabel.TextColor = UIColor.White; // Set your color
//
//				headerView.AddSubview (headerLabel);
//				return headerView;
//			}
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

			public override string[] SectionIndexTitles (UITableView tableView)
			{

				string[] _arrayString = new string[26];
				return _arrayString=headerArray.ToArray();

			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 100.0f;
			}


		}
	}
}

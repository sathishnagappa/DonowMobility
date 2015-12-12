using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;
using donow.Util;
using CoreGraphics;

namespace donow.iOS
{
	partial class LandingCustomerStreamVC : UIViewController
	{
		public LandingCustomerStreamVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.SetNavigationBarHidden (true, false);
		}

		public override void ViewDidLoad ()
		{
			this.Title = "Customer Stream";
<<<<<<< HEAD
			CustomerBL customerObj = new CustomerBL ();
			IList<Customer> customerList = customerObj.GetAllCustomers ();
			TableViewCustomerStream.Source= new TableSource(customerList, this);


		}

		public class TableSource : UITableViewSource {
			string CellIdentifier = "TableCell";
			IList<Customer> TableItems;
			LandingCustomerStreamVC owner;

//			public List<String> headerArray = new List <String>
//			{
//				"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O",
//				"P","Q", "R", "S", "T", "U", "V","W", "X", "Y", "Z"
//			};

			public TableSource (IList<Customer> items, LandingCustomerStreamVC owner)
			{
				this.TableItems = items;
				this.owner = owner;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
							return TableItems.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier) as CustomerStreamTableViewCell;
				Customer customerObj = TableItems[indexPath.Row];

				if (cell == null) {
					cell = new CustomerStreamTableViewCell(CellIdentifier);
				}

				cell.UpdateCell(customerObj);
				return cell;

			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

				tableView.DeselectRow (indexPath, true);
				LandingCustomerDetailVC customerStreamDetailVC = owner.Storyboard.InstantiateViewController ("LandingCustomerDetailVC") as LandingCustomerDetailVC;
				if (customerStreamDetailVC != null) {
					//customerStreamDetailVC.customerObj = TableItems[indexPath.Row];
					//Customer customerObj = TableItems[indexPath.Row];

					//owner.View.AddSubview (leadDetailVC.View);
					owner.NavigationController.PushViewController(customerStreamDetailVC,true);
				}


			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 150.0f;
			}
=======
>>>>>>> origin/master
		}

	}
}

using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;

namespace donow.iOS
{
	partial class LandingCustomerVC : UIViewController
	{
		public LandingCustomerVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (false, false);
			//this.NavigationController.SetNavigationBarHidden (true, false);
			//			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			//			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			//			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
			//			this.NavigationController.NavigationItem.SetLeftBarButtonItem( new UIBarButtonItem(UIImage.FromFile("Navigation_Back_Icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
			//				this.NavigationController.PopViewController(true);
			//			}), true);
		}


		public override void ViewDidLoad ()
		{
			CustomerBL customerBL = new CustomerBL ();
			List<Customer> cusotmerList =  customerBL.GetAllCustomers ();
		}
	}
}

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

		public override void ViewDidLoad ()
		{
			CustomerBL customerBL = new CustomerBL ();
			List<Customer> cusotmerList =  customerBL.GetAllCustomers ();
		}
	}
}

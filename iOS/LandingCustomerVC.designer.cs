// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	[Register ("LandingCustomerVC")]
	partial class LandingCustomerVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewCustomerList { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (TableViewCustomerList != null) {
				TableViewCustomerList.Dispose ();
				TableViewCustomerList = null;
			}
		}
	}
}

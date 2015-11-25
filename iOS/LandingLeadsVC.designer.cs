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
	[Register ("LandingLeadsVC")]
	partial class LandingLeadsVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewLeads { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (TableViewLeads != null) {
				TableViewLeads.Dispose ();
				TableViewLeads = null;
			}
		}
	}
}

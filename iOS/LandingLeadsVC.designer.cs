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
		UIView AlertView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonOk { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonRequestNewLead { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView LabelAlertView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewLeads { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AlertView != null) {
				AlertView.Dispose ();
				AlertView = null;
			}
			if (ButtonOk != null) {
				ButtonOk.Dispose ();
				ButtonOk = null;
			}
			if (ButtonRequestNewLead != null) {
				ButtonRequestNewLead.Dispose ();
				ButtonRequestNewLead = null;
			}
			if (LabelAlertView != null) {
				LabelAlertView.Dispose ();
				LabelAlertView = null;
			}
			if (TableViewLeads != null) {
				TableViewLeads.Dispose ();
				TableViewLeads = null;
			}
		}
	}
}

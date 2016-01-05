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
	[Register ("ReferralRequestDetails")]
	partial class ReferralRequestDetails
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ImageRR { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelRRTitle { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISearchBar searchBarReferral { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewRR { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView topView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ImageRR != null) {
				ImageRR.Dispose ();
				ImageRR = null;
			}
			if (LabelRRTitle != null) {
				LabelRRTitle.Dispose ();
				LabelRRTitle = null;
			}
			if (searchBarReferral != null) {
				searchBarReferral.Dispose ();
				searchBarReferral = null;
			}
			if (TableViewRR != null) {
				TableViewRR.Dispose ();
				TableViewRR = null;
			}
			if (topView != null) {
				topView.Dispose ();
				topView = null;
			}
		}
	}
}

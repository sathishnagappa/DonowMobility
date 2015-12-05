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
	[Register ("MyMeetingsVC")]
	partial class MyMeetingsVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView LatestCustomerInfoTable { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView LatestIndustryNewsTable { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewMeeting { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TalkingPointTable { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LatestCustomerInfoTable != null) {
				LatestCustomerInfoTable.Dispose ();
				LatestCustomerInfoTable = null;
			}
			if (LatestIndustryNewsTable != null) {
				LatestIndustryNewsTable.Dispose ();
				LatestIndustryNewsTable = null;
			}
			if (ScrollViewMeeting != null) {
				ScrollViewMeeting.Dispose ();
				ScrollViewMeeting = null;
			}
			if (TalkingPointTable != null) {
				TalkingPointTable.Dispose ();
				TalkingPointTable = null;
			}
		}
	}
}

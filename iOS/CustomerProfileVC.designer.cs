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
	[Register ("customerProfileVC")]
	partial class customerProfileVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonMail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonPhone { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSeeAllEmails { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSeeAllLeadsTable { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSeeAllPreviousMeetings { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView DealMakersImage1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCityAndState { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCompanyName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCustomerName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewCustomerProfile { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewEmails { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewMeetings { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewNewLeads { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewPreviousMeetings { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonMail != null) {
				ButtonMail.Dispose ();
				ButtonMail = null;
			}
			if (ButtonPhone != null) {
				ButtonPhone.Dispose ();
				ButtonPhone = null;
			}
			if (ButtonSeeAllEmails != null) {
				ButtonSeeAllEmails.Dispose ();
				ButtonSeeAllEmails = null;
			}
			if (ButtonSeeAllLeadsTable != null) {
				ButtonSeeAllLeadsTable.Dispose ();
				ButtonSeeAllLeadsTable = null;
			}
			if (ButtonSeeAllPreviousMeetings != null) {
				ButtonSeeAllPreviousMeetings.Dispose ();
				ButtonSeeAllPreviousMeetings = null;
			}
			if (DealMakersImage1 != null) {
				DealMakersImage1.Dispose ();
				DealMakersImage1 = null;
			}
			if (LabelCityAndState != null) {
				LabelCityAndState.Dispose ();
				LabelCityAndState = null;
			}
			if (LabelCompanyName != null) {
				LabelCompanyName.Dispose ();
				LabelCompanyName = null;
			}
			if (LabelCustomerName != null) {
				LabelCustomerName.Dispose ();
				LabelCustomerName = null;
			}
			if (ScrollViewCustomerProfile != null) {
				ScrollViewCustomerProfile.Dispose ();
				ScrollViewCustomerProfile = null;
			}
			if (TableViewEmails != null) {
				TableViewEmails.Dispose ();
				TableViewEmails = null;
			}
			if (TableViewMeetings != null) {
				TableViewMeetings.Dispose ();
				TableViewMeetings = null;
			}
			if (TableViewNewLeads != null) {
				TableViewNewLeads.Dispose ();
				TableViewNewLeads = null;
			}
			if (TableViewPreviousMeetings != null) {
				TableViewPreviousMeetings.Dispose ();
				TableViewPreviousMeetings = null;
			}
		}
	}
}

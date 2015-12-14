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
	[Register ("MyProfileVC")]
	partial class MyProfileVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCityAndState { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCompanyName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelLeadsReceived { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelSellerScore { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewActiveLeads { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewReferralRequests { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LabelCityAndState != null) {
				LabelCityAndState.Dispose ();
				LabelCityAndState = null;
			}
			if (LabelCompanyName != null) {
				LabelCompanyName.Dispose ();
				LabelCompanyName = null;
			}
			if (LabelLeadsReceived != null) {
				LabelLeadsReceived.Dispose ();
				LabelLeadsReceived = null;
			}
			if (LabelName != null) {
				LabelName.Dispose ();
				LabelName = null;
			}
			if (LabelSellerScore != null) {
				LabelSellerScore.Dispose ();
				LabelSellerScore = null;
			}
			if (ViewActiveLeads != null) {
				ViewActiveLeads.Dispose ();
				ViewActiveLeads = null;
			}
			if (ViewReferralRequests != null) {
				ViewReferralRequests.Dispose ();
				ViewReferralRequests = null;
			}
		}
	}
}

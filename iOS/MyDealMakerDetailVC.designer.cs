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
	[Register ("MyDealMakerDetailVC")]
	partial class MyDealMakerDetailVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonCancel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonOkSendRequestView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSendRequest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelBrokerFee { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelBrokerScore { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCompanyInfoDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel labelConnectionToLead { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelNameDealMaker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelTotalEarnings { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewDealMakerDetails { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewBackgroundTransparent { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewSendRequestView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonCancel != null) {
				ButtonCancel.Dispose ();
				ButtonCancel = null;
			}
			if (ButtonOkSendRequestView != null) {
				ButtonOkSendRequestView.Dispose ();
				ButtonOkSendRequestView = null;
			}
			if (ButtonSendRequest != null) {
				ButtonSendRequest.Dispose ();
				ButtonSendRequest = null;
			}
			if (LabelBrokerFee != null) {
				LabelBrokerFee.Dispose ();
				LabelBrokerFee = null;
			}
			if (LabelBrokerScore != null) {
				LabelBrokerScore.Dispose ();
				LabelBrokerScore = null;
			}
			if (LabelCompanyInfoDescription != null) {
				LabelCompanyInfoDescription.Dispose ();
				LabelCompanyInfoDescription = null;
			}
			if (labelConnectionToLead != null) {
				labelConnectionToLead.Dispose ();
				labelConnectionToLead = null;
			}
			if (LabelNameDealMaker != null) {
				LabelNameDealMaker.Dispose ();
				LabelNameDealMaker = null;
			}
			if (LabelTotalEarnings != null) {
				LabelTotalEarnings.Dispose ();
				LabelTotalEarnings = null;
			}
			if (ScrollViewDealMakerDetails != null) {
				ScrollViewDealMakerDetails.Dispose ();
				ScrollViewDealMakerDetails = null;
			}
			if (ViewBackgroundTransparent != null) {
				ViewBackgroundTransparent.Dispose ();
				ViewBackgroundTransparent = null;
			}
			if (ViewSendRequestView != null) {
				ViewSendRequestView.Dispose ();
				ViewSendRequestView = null;
			}
		}
	}
}

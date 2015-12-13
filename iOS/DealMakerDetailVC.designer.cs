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
	[Register ("DealMakerDetailVC")]
	partial class DealMakerDetailVC
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

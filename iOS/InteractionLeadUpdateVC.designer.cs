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
	[Register ("InteractionLeadUpdateVC")]
	partial class InteractionLeadUpdateVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonDisLikeCustomerAcknowledge { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonDislikeInteraction { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonInteractionDislikeReasonDropDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonLikeCustomerAcknowledge { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonLikeInteraction { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewInteractionDislikerReason { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView TextViewComments { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonDisLikeCustomerAcknowledge != null) {
				ButtonDisLikeCustomerAcknowledge.Dispose ();
				ButtonDisLikeCustomerAcknowledge = null;
			}
			if (ButtonDislikeInteraction != null) {
				ButtonDislikeInteraction.Dispose ();
				ButtonDislikeInteraction = null;
			}
			if (ButtonInteractionDislikeReasonDropDown != null) {
				ButtonInteractionDislikeReasonDropDown.Dispose ();
				ButtonInteractionDislikeReasonDropDown = null;
			}
			if (ButtonLikeCustomerAcknowledge != null) {
				ButtonLikeCustomerAcknowledge.Dispose ();
				ButtonLikeCustomerAcknowledge = null;
			}
			if (ButtonLikeInteraction != null) {
				ButtonLikeInteraction.Dispose ();
				ButtonLikeInteraction = null;
			}
			if (TableViewInteractionDislikerReason != null) {
				TableViewInteractionDislikerReason.Dispose ();
				TableViewInteractionDislikerReason = null;
			}
			if (TextViewComments != null) {
				TextViewComments.Dispose ();
				TextViewComments = null;
			}
		}
	}
}

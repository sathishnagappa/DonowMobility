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
		UIButton ButtonSaveNotes { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelCityState { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelDateAndTime { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelMeetingHeader { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView LabelNotes { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelNotesDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewMeeting { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView TextViewTalkingPoints { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonSaveNotes != null) {
				ButtonSaveNotes.Dispose ();
				ButtonSaveNotes = null;
			}
			if (LabelCityState != null) {
				LabelCityState.Dispose ();
				LabelCityState = null;
			}
			if (LabelDateAndTime != null) {
				LabelDateAndTime.Dispose ();
				LabelDateAndTime = null;
			}
			if (LabelMeetingHeader != null) {
				LabelMeetingHeader.Dispose ();
				LabelMeetingHeader = null;
			}
			if (LabelNotes != null) {
				LabelNotes.Dispose ();
				LabelNotes = null;
			}
			if (LabelNotesDate != null) {
				LabelNotesDate.Dispose ();
				LabelNotesDate = null;
			}
			if (ScrollViewMeeting != null) {
				ScrollViewMeeting.Dispose ();
				ScrollViewMeeting = null;
			}
			if (TextViewTalkingPoints != null) {
				TextViewTalkingPoints.Dispose ();
				TextViewTalkingPoints = null;
			}
		}
	}
}

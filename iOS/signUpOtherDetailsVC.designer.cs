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
	[Register ("signUpOtherDetailsVC")]
	partial class signUpOtherDetailsVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonDolater { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonNext { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonState { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewSignUpDetails { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewState { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonDolater != null) {
				ButtonDolater.Dispose ();
				ButtonDolater = null;
			}
			if (ButtonNext != null) {
				ButtonNext.Dispose ();
				ButtonNext = null;
			}
			if (ButtonState != null) {
				ButtonState.Dispose ();
				ButtonState = null;
			}
			if (ScrollViewSignUpDetails != null) {
				ScrollViewSignUpDetails.Dispose ();
				ScrollViewSignUpDetails = null;
			}
			if (TableViewState != null) {
				TableViewState.Dispose ();
				TableViewState = null;
			}
		}
	}
}

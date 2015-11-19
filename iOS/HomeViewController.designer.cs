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
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView homePagePickerView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LabelResponse { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (homePagePickerView != null) {
				homePagePickerView.Dispose ();
				homePagePickerView = null;
			}
			if (LabelResponse != null) {
				LabelResponse.Dispose ();
				LabelResponse = null;
			}
		}
	}
}

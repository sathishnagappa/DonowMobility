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
	[Register ("ForgotPasswordVC")]
	partial class ForgotPasswordVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonChange { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxConfirmPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxEmailID { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxPassword { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonChange != null) {
				ButtonChange.Dispose ();
				ButtonChange = null;
			}
			if (TextBoxConfirmPassword != null) {
				TextBoxConfirmPassword.Dispose ();
				TextBoxConfirmPassword = null;
			}
			if (TextBoxEmailID != null) {
				TextBoxEmailID.Dispose ();
				TextBoxEmailID = null;
			}
			if (TextBoxPassword != null) {
				TextBoxPassword.Dispose ();
				TextBoxPassword = null;
			}
		}
	}
}

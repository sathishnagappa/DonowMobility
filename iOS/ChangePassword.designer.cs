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
	[Register ("ChangePassword")]
	partial class ChangePassword
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonChangePassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewChangePswd { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxConfirmPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxNewPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxOldPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxUserName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonChangePassword != null) {
				ButtonChangePassword.Dispose ();
				ButtonChangePassword = null;
			}
			if (ScrollViewChangePswd != null) {
				ScrollViewChangePswd.Dispose ();
				ScrollViewChangePswd = null;
			}
			if (TextBoxConfirmPassword != null) {
				TextBoxConfirmPassword.Dispose ();
				TextBoxConfirmPassword = null;
			}
			if (TextBoxNewPassword != null) {
				TextBoxNewPassword.Dispose ();
				TextBoxNewPassword = null;
			}
			if (TextBoxOldPassword != null) {
				TextBoxOldPassword.Dispose ();
				TextBoxOldPassword = null;
			}
			if (TextBoxUserName != null) {
				TextBoxUserName.Dispose ();
				TextBoxUserName = null;
			}
		}
	}
}

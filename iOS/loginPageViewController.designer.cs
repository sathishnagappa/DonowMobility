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
	[Register ("loginPageViewController")]
	partial class loginPageViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonLinkedInLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSignUp { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxUserName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonLinkedInLogin != null) {
				ButtonLinkedInLogin.Dispose ();
				ButtonLinkedInLogin = null;
			}
			if (ButtonLogin != null) {
				ButtonLogin.Dispose ();
				ButtonLogin = null;
			}
			if (ButtonSignUp != null) {
				ButtonSignUp.Dispose ();
				ButtonSignUp = null;
			}
			if (TextBoxPassword != null) {
				TextBoxPassword.Dispose ();
				TextBoxPassword = null;
			}
			if (TextBoxUserName != null) {
				TextBoxUserName.Dispose ();
				TextBoxUserName = null;
			}
		}
	}
}

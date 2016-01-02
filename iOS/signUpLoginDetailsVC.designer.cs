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
	[Register ("signUpLoginDetailsVC")]
	partial class signUpLoginDetailsVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton NextBtn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxUserName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxVerifyPassword { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (NextBtn != null) {
				NextBtn.Dispose ();
				NextBtn = null;
			}
			if (TextBoxPassword != null) {
				TextBoxPassword.Dispose ();
				TextBoxPassword = null;
			}
			if (TextBoxUserName != null) {
				TextBoxUserName.Dispose ();
				TextBoxUserName = null;
			}
			if (TextBoxVerifyPassword != null) {
				TextBoxVerifyPassword.Dispose ();
				TextBoxVerifyPassword = null;
			}
		}
	}
}

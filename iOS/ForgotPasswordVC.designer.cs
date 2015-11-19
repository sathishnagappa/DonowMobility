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
		UIButton ButtonSend { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBoxEmailID { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonSend != null) {
				ButtonSend.Dispose ();
				ButtonSend = null;
			}
			if (TextBoxEmailID != null) {
				TextBoxEmailID.Dispose ();
				TextBoxEmailID = null;
			}
		}
	}
}

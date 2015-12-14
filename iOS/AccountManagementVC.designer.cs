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
	[Register ("AccountManagementVC")]
	partial class AccountManagementVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonFinish { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewAccountManager { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonFinish != null) {
				ButtonFinish.Dispose ();
				ButtonFinish = null;
			}
			if (ScrollViewAccountManager != null) {
				ScrollViewAccountManager.Dispose ();
				ScrollViewAccountManager = null;
			}
		}
	}
}

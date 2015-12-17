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
	[Register ("WelcomeVC")]
	partial class WelcomeVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonStartReferring { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonStartSelling { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonStartReferring != null) {
				ButtonStartReferring.Dispose ();
				ButtonStartReferring = null;
			}
			if (ButtonStartSelling != null) {
				ButtonStartSelling.Dispose ();
				ButtonStartSelling = null;
			}
		}
	}
}

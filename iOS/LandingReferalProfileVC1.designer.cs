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
	[Register ("LandingReferalProfileVC1")]
	partial class LandingReferalProfileVC1
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ScrollReferalProfile { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ScrollReqProf { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ScrollReferalProfile != null) {
				ScrollReferalProfile.Dispose ();
				ScrollReferalProfile = null;
			}
			if (ScrollReqProf != null) {
				ScrollReqProf.Dispose ();
				ScrollReqProf = null;
			}
		}
	}
}

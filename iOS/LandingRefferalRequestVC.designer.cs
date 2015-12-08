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
	[Register ("LandingRefferalRequestVC")]
	partial class LandingRefferalRequestVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonAcceptRequest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonCompletedRequest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonNewRequest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonPassedRequest { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonAcceptRequest != null) {
				ButtonAcceptRequest.Dispose ();
				ButtonAcceptRequest = null;
			}
			if (ButtonCompletedRequest != null) {
				ButtonCompletedRequest.Dispose ();
				ButtonCompletedRequest = null;
			}
			if (ButtonNewRequest != null) {
				ButtonNewRequest.Dispose ();
				ButtonNewRequest = null;
			}
			if (ButtonPassedRequest != null) {
				ButtonPassedRequest.Dispose ();
				ButtonPassedRequest = null;
			}
		}
	}
}

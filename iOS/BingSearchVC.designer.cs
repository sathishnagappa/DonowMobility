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
	[Register ("BingSearchVC")]
	partial class BingSearchVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIWebView BingWebView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (BingWebView != null) {
				BingWebView.Dispose ();
				BingWebView = null;
			}
		}
	}
}

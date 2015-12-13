using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class BingSearchVC : UIViewController
	{
		public string webURL { get; set; }

		public BingSearchVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			BingWebView.LoadRequest(new NSUrlRequest(new NSUrl(webURL)));
		}
	}
}

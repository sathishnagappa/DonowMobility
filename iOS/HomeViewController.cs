using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using donow.PCL;

namespace donow.iOS
{
	partial class HomeViewController : UIViewController
	{
		public HomeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
//			var webView = new UIWebView (View.Bounds);
//			View.AddSubview(webView);
//			var url = "https://www.bing.com/news/search?q=ibm+trends&qpvt=ibm+trends&FORM=EWRE"; // NOTE: https secure request
//			webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
//
			UserBL userBL = new UserBL ();
			string response =  userBL.GetUserDetailsString ();
//			LabelResponse.Text = response;
		}
	}
}

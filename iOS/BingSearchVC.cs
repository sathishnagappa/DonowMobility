using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using donow.Util;


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
			LoadingOverlay loadingOverlay;
			base.ViewWillAppear (animated);
			loadingOverlay = new LoadingOverlay(this.BingWebView.Bounds);
			//When the web view starts to load
			this.BingWebView.LoadStarted += (object sender, EventArgs e) => {

				this.BingWebView.Add(loadingOverlay);
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			};
			//BingWebView.LoadRequest(new NSUrlRequest(new NSUrl(webURL)));

			this.BingWebView.LoadRequest (new NSUrlRequest(new NSUrl(webURL)));

			BingWebView.ScalesPageToFit = true;


			//When the web view is finished loading
			this.BingWebView.LoadFinished += (object sender, EventArgs e) => {

				loadingOverlay.Hide();
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			};

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;
		}
	}
}

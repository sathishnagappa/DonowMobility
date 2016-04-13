using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using MessageUI;

namespace donow.iOS
{
	partial class InfoPage : UIViewController
	{
		public InfoPage (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (157, 50, 49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationItem.Title = "Help";

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;
			ScrollViewInfoPage.ContentSize = new CGSize (375.0f, 1065.0f);

			ButtonVideo.TouchUpInside += (object sender, EventArgs e) =>  {
				NSUrl URL = new NSUrl("http://www.youtube.com/watch?v=I0N-4Dtog6E");
				UIApplication.SharedApplication.OpenUrl(URL);

			};
			ButtonWebsite.TouchUpInside += (object sender, EventArgs e) =>  {
				NSUrl URL = new NSUrl("http://www.donowx.com");
				UIApplication.SharedApplication.OpenUrl(URL);
			};
		}

		public class textViewDelegateClass : UITextViewDelegate {

			public textViewDelegateClass ()
			{				
			}

			public override bool ShouldInteractWithUrl (UITextView textView, NSUrl URL, NSRange characterRange)
			{			
				return true;
			}
		}
	}
}

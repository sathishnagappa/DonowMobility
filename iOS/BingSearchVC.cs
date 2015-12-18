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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				MyMeetingsVC meetingPage = this.Storyboard.InstantiateViewController ("MyMeetingsVC") as MyMeetingsVC;
				this.NavigationController.PushViewController(meetingPage,true);
			};
			NavigationItem.LeftBarButtonItem = btn;
		}
	}
}

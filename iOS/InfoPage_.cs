using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

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

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
//				HambergerMenuVC hambergerVC = this.Storyboard.InstantiateViewController("HambergerMenuVC") as HambergerMenuVC;
//				this.NavigationController.PushViewController(hambergerVC,true);
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;
			ScrollViewInfoPage.ContentSize = new CGSize (375.0f, 1227.0f);
		}
	}
}

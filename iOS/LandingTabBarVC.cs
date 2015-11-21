using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class LandingTabBarVC : UITabBarController
	{
		public LandingTabBarVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
		}
	}
}

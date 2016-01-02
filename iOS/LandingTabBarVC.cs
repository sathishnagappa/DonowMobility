using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;
using System.Linq;

namespace donow.iOS
{
	partial class LandingTabBarVC : UITabBarController
	{
		public bool isReferring;

		public LandingTabBarVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
//			this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
//			this.NavigationController.NavigationBar.TintColor = UIColor.White;
//			this.NavigationController.NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.White;
			if (isReferring)
				this.SelectedIndex = 3;
			else
				this.SelectedIndex = 2;		
		}
	}
}

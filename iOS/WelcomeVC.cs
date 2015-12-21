using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class WelcomeVC : UIViewController
	{
		public WelcomeVC (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{				
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			this.Title = @"Get Started";
		
			ButtonStartSelling.TouchUpInside += (object sender, EventArgs e) => {
				LandingTabBarVC landingPage = this.Storyboard.InstantiateViewController("LandingTabBarVC") as LandingTabBarVC;
				landingPage.isReferring = false;
				this.NavigationController.PushViewController(landingPage,false);
			};

			ButtonStartReferring.TouchUpInside += (object sender, EventArgs e) => {
				LandingTabBarVC landingPage = this.Storyboard.InstantiateViewController("LandingTabBarVC") as LandingTabBarVC;
				landingPage.isReferring = true;
				this.NavigationController.PushViewController(landingPage,false);
			};
		}
	}
}

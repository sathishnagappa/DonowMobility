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
			this.Title = @"Get Started";
		}
	}
}

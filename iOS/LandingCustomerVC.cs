using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class LandingCustomerVC : UIViewController
	{
		public LandingCustomerVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			this.Title = "Referral Request";
		}
	}
}

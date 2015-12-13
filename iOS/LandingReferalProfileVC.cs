using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.Drawing;

namespace donow.iOS
{
	public partial class LandingReferalProfileVC : UIViewController
	{
		public LandingReferalProfileVC (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{

			ScrollReferalProfile.ContentSize =  new CGSize (415f, 1100);
		}


	}
}

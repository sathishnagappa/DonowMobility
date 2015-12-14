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
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ScrollViewInfoPage.ContentSize = new CGSize (414.0f, 1362.0f);
		}
	}
}

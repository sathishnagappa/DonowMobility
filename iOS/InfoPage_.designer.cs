// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	[Register ("InfoPage")]
	partial class InfoPage
	{
		[Outlet]
		UIKit.UIButton ButtonWebsite { get; set; }

		[Outlet]
		UIKit.UIView HeaderView { get; set; }

		[Outlet]
		UIKit.UILabel LabelCompany { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollViewInfoPage { get; set; }

		[Outlet]
		UIKit.UITextView TextViewCompanyInfo { get; set; }

		[Outlet]
		UIButton ButtonVideo { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonVideo != null) {
				ButtonVideo.Dispose ();
				ButtonVideo = null;
			}
			if (ButtonWebsite != null) {
				ButtonWebsite.Dispose ();
				ButtonWebsite = null;
			}
			if (ScrollViewInfoPage != null) {
				ScrollViewInfoPage.Dispose ();
				ScrollViewInfoPage = null;
			}
		}
	}
}

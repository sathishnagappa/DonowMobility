// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace donow.iOS
{
	[Register ("signUpLoginDetailsVC")]
	partial class signUpLoginDetailsVC
	{
		[Outlet]
		UIKit.UITextView MyText { get; set; }

		[Outlet]
		UIKit.UIButton NextBtn { get; set; }

		[Outlet]
		UIKit.UITextField TextBoxPassword { get; set; }

		[Outlet]
		UIKit.UITextField TextBoxUserName { get; set; }

		[Outlet]
		UIKit.UITextField TextBoxVerifyPassword { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NextBtn != null) {
				NextBtn.Dispose ();
				NextBtn = null;
			}

			if (TextBoxPassword != null) {
				TextBoxPassword.Dispose ();
				TextBoxPassword = null;
			}

			if (TextBoxUserName != null) {
				TextBoxUserName.Dispose ();
				TextBoxUserName = null;
			}

			if (TextBoxVerifyPassword != null) {
				TextBoxVerifyPassword.Dispose ();
				TextBoxVerifyPassword = null;
			}

			if (MyText != null) {
				MyText.Dispose ();
				MyText = null;
			}
		}
	}
}

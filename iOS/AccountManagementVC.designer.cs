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
	[Register ("AccountManagementVC")]
	partial class AccountManagementVC
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton buttonChangePassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonFinish { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonInfoCompanyDropDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonInfoCustomersDropDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonInfoIndustryDropDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonProfileImageSetter { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonUserInfo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel CompanyLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel CustomerLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel industryLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollViewAccountManager { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableViewCustomerStreamActivity { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (buttonChangePassword != null) {
				buttonChangePassword.Dispose ();
				buttonChangePassword = null;
			}
			if (ButtonFinish != null) {
				ButtonFinish.Dispose ();
				ButtonFinish = null;
			}
			if (ButtonInfoCompanyDropDown != null) {
				ButtonInfoCompanyDropDown.Dispose ();
				ButtonInfoCompanyDropDown = null;
			}
			if (ButtonInfoCustomersDropDown != null) {
				ButtonInfoCustomersDropDown.Dispose ();
				ButtonInfoCustomersDropDown = null;
			}
			if (ButtonInfoIndustryDropDown != null) {
				ButtonInfoIndustryDropDown.Dispose ();
				ButtonInfoIndustryDropDown = null;
			}
			if (ButtonProfileImageSetter != null) {
				ButtonProfileImageSetter.Dispose ();
				ButtonProfileImageSetter = null;
			}
			if (ButtonUserInfo != null) {
				ButtonUserInfo.Dispose ();
				ButtonUserInfo = null;
			}
			if (CompanyLabel != null) {
				CompanyLabel.Dispose ();
				CompanyLabel = null;
			}
			if (CustomerLabel != null) {
				CustomerLabel.Dispose ();
				CustomerLabel = null;
			}
			if (industryLabel != null) {
				industryLabel.Dispose ();
				industryLabel = null;
			}
			if (ScrollViewAccountManager != null) {
				ScrollViewAccountManager.Dispose ();
				ScrollViewAccountManager = null;
			}
			if (TableViewCustomerStreamActivity != null) {
				TableViewCustomerStreamActivity.Dispose ();
				TableViewCustomerStreamActivity = null;
			}
		}
	}
}

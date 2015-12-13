using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;

namespace donow.iOS
{
	partial class prospectDetailsVC : UIViewController
	{
		public Leads localLeads;

		public prospectDetailsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
		}

		public override void ViewDidLoad ()
		{
			AppDelegate.IsCalendarClicked = false;
			base.ViewDidLoad ();

			LabelProspectName.Text = localLeads.LEAD_NAME;
			LabelProspectCompanyName.Text = localLeads.COMPANY_NAME;
			LabelProspectCityandState.Text = localLeads.CITY + ", " + localLeads.STATE;
			LabelLeadScore.Text = localLeads.LEAD_SCORE.ToString();
			LabelLeadSource.Text = localLeads.LEAD_SOURCE == 1? "SFDC" : "DoNow";

			if (localLeads.LEAD_STATUS.Equals(1)) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Close Sale Highlight.png");
			} else if (localLeads.LEAD_STATUS.Equals(2)) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Proposal Highlight.png");
			} else if (localLeads.LEAD_STATUS.Equals(3)) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Follow Up Highlight.png");
			} else {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Acquire Lead Highlight.png");
			}
		}

	}
}

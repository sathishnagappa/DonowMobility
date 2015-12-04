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

			LabelProspectName.Text = localLeads.Name;
			LabelProspectCompanyName.Text = localLeads.Company;
			LabelProspectCityandState.Text = localLeads.City + ", " + localLeads.State;
			LabelLeadScore.Text = localLeads.LeadScore.ToString();
			LabelLeadSource.Text = localLeads.Source;

			if (localLeads.SalesStage.Equals("Closed Sale")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Close Sale Highlight.png");
			} else if (localLeads.SalesStage.Equals("Proposal")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Proposal Highlight.png");
			} else if (localLeads.SalesStage.Equals("Follow Up")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Follow Up Highlight.png");
			} else {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("LifeCycle_Acquire Lead Highlight.png");
			}
		}

	}
}

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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			LabelProspectName.Text = localLeads.Name;
			LabelProspectCompanyName.Text = localLeads.Company;
			LabelProspectCityandState.Text = localLeads.City + ", " + localLeads.State;

			LabelLeadScore.Text = localLeads.LeadScore.ToString();
			LabelLeadSource.Text = localLeads.Source;

			if (localLeads.SalesStage.Equals("Acquire Leads")) {
				ImageBackgroundAcquireLead.Image = UIImage.FromBundle ("");
			} else if (localLeads.SalesStage.Equals("Proposal")) {
				ImageBackgroundProposal.Image = UIImage.FromBundle ("");
			} else if (localLeads.SalesStage.Equals("Follow Up")) {
				ImageBackgroundFollowUp.Image = UIImage.FromBundle ("");
			} else {
				ImageBackgroundCloseSale.Image = UIImage.FromBundle ("");
			}
		}

	}
}

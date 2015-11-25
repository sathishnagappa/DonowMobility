using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;

namespace donow.iOS
{
	partial class LeadDetailVC : UIViewController
	{
		public Leads leadObj;
		public LeadDetailVC (IntPtr handle) : base (handle)
		{
		}

		public  override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			LabelTitleName.Text = leadObj.Name;
			LabelScore.Text = leadObj.Source;
			LabelSourceName.Text = leadObj.Source;
			LabelCompanyInfo.Text = leadObj.CompanyInfo;
			LabelTitleCompany.Text = leadObj.Company;
			LabelLocation.Text = leadObj.City + "," + leadObj.State;
//			LabelBusinessNeeds.Text = leadObj.BusinessNeeds;

		}
	}
}

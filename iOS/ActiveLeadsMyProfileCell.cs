using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;
using donow.PCL;
namespace donow.iOS
{
	public class ActiveLeadsMyProfileCell :UITableViewCell
	{

		UILabel LabelLeadName,LabelCompanyName,LabelHourText;

		public ActiveLeadsMyProfileCell  (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			LabelLeadName = new UILabel () {
				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB(73,73,73),
				Lines = 0,
			};
			ContentView.AddSubviews (new UIView[]{ LabelLeadName });

			LabelCompanyName = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB(73,73,73),
				Lines = 0,
			};
			ContentView.AddSubviews (new UIView[]{ LabelCompanyName });

			LabelHourText = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB(152,152,152),
				Lines = 0,
			};
			ContentView.AddSubviews (new UIView[]{ LabelHourText });

		}

		public void UpdateCell (LeadMaster obj)
		{
			LabelLeadName.Text = obj.LEAD_NAME;
			LabelCompanyName.Text = obj.COMPANY_NAME;
			//LabelHourText.Text = DateTime.Parse (obj.CreatedOn).ToString ("MMM dd");
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			LabelLeadName.Frame = new CGRect (0, 0, 175,30);
			LabelCompanyName.Frame = new CGRect (0, 25, 300, 30);
			LabelHourText.Frame = new CGRect (260,25,150,30);
		}

	}
}


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
				Font = UIFont.FromName ("Arial", 15f),
				TextColor = UIColor.Black,
				Lines = 0,
			};
			ContentView.AddSubviews (new UIView[]{ LabelLeadName });

			LabelCompanyName = new UILabel () {

				Font = UIFont.FromName ("Arial", 15f),
				TextColor = UIColor.Black,
				Lines = 0,
			};
			ContentView.AddSubviews (new UIView[]{ LabelCompanyName });

			LabelHourText = new UILabel () {

				Font = UIFont.FromName ("Arial", 12f),
				TextColor = UIColor.LightGray,
				Lines = 0,
			};
			ContentView.AddSubviews (new UIView[]{ LabelHourText });

		}

		public void UpdateCell (Leads obj)
		{
			LabelLeadName.Text = obj.LEAD_NAME;
			LabelCompanyName.Text = obj.COMPANY_NAME;
			LabelHourText.Text = DateTime.Parse (obj.LEAD_CREATE_TIME).ToString ("yyyy MMMMM dd");
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			LabelLeadName.Frame = new CGRect (14, 0, 175,30);
			LabelCompanyName.Frame = new CGRect (14, 25, 175, 30);
			LabelHourText.Frame = new CGRect (265,25,60,30);
		}

	}
}



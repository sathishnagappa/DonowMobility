using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;

namespace donow.iOS
{
	public class LeadTableCell : UITableViewCell  {
		
		UILabel LabelLeadName, LabelCompanyName, LabelCityAndState, LabelScoreDigit, LabelScore, LabelNewLead;
		UIImageView ImageViewLeadImage;

		public LeadTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelLeadName = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 19f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadName});

			LabelCompanyName = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
//				TextColor = UIColor.FromRGB (35.0f, 31.0f, 32.0f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCompanyName});

			LabelCityAndState = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
//				TextColor = Color 35.0f, 31.0f, 32.0f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCityAndState});

			LabelScore = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
//				TextColor = UIColor.FromRGB (35.0f, 31.0f, 32.0f),
				TextColor = UIColor.Black,
				Text = "Lead Score:",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScore});

			LabelScoreDigit = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
//				TextColor = UIColor.FromRGB (35.0f, 31.0f, 32.0f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScoreDigit});

			LabelNewLead = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 15f),
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.Black,
				TextAlignment = UITextAlignment.Center
			};
			ContentView.AddSubviews(new UIView[] {LabelNewLead});

			ImageViewLeadImage = new UIImageView () {
				Image = UIImage.FromBundle("Salesperson Logo_Small.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});
		}

		public void UpdateCell (Leads lead)
		{
			LabelLeadName.Text = lead.LEAD_NAME;
			LabelCompanyName.Text = lead.COMPANY_NAME;
			LabelCityAndState.Text = lead.CITY + ", " + lead.STATE; 
			LabelScoreDigit.Text = lead.LEAD_SCORE.ToString();
			LabelNewLead.Text = GetStatus(lead.USER_LEAD_STATUS) ;
		}

		string  GetStatus(int Status)
		{
			
			switch (Status) {
			case 1: 
				return "New";
			case 2:
				return "Acceptance Pending";
			case 3:
				return "Assigned";
			case 4:
				return "Accepted";
			case 5:
				return "Passed";
			default:
				return "";
			}

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (10, 35, 50, 50);
			LabelLeadName.Frame = new CGRect (85, 38, 220, 30);
			LabelCompanyName.Frame = new CGRect (85, 63, 200, 25);
			LabelCityAndState.Frame = new CGRect (85, 83, 200, 25);
			LabelScore.Frame = new CGRect (85, 118, 120, 25);
			LabelScoreDigit.Frame = new CGRect (270, 117, 25, 25);
			LabelNewLead.Frame = new CGRect (330, 20, 80, 25);
		}
	}
}


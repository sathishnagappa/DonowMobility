using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;
using donow.PCL;

namespace donow.iOS
{
	public class LeadTableCell : UITableViewCell  {
		
		UILabel LabelLeadName, LabelTitle, LabelCompanyName, LabelCityAndState, LabelScoreDigit, LabelScore, LabelNewLead,LeadIndustry,LeadType;
		UIImageView ImageViewLeadImage;

		public LeadTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelLeadName = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadName});

			LabelTitle = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelTitle});

			LabelCompanyName = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCompanyName});

			LabelCityAndState = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCityAndState});

			LabelScore = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				Text = "Lead Score:",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScore});


			LeadIndustry = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				Text = "Industry:",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LeadIndustry});


			LabelScoreDigit = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScoreDigit});

			LabelNewLead = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 15f),
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.FromRGB (35, 31, 32),
				TextAlignment = UITextAlignment.Right
			};
			ContentView.AddSubviews(new UIView[] {LabelNewLead});

			LeadType = new UILabel () {
				Font = UIFont.FromName("Arial", 19f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LeadType});

			ImageViewLeadImage = new UIImageView () {
				Image = UIImage.FromBundle("Salesperson Logo_Small.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});
		}

		public void UpdateCell (LeadMaster lead)
		{
			LabelLeadName.Text = lead.LEAD_NAME;
			LabelTitle.Text = lead.LEAD_TITLE;
			LabelCompanyName.Text = lead.COMPANY_NAME;
			string coma = (string.IsNullOrEmpty (lead.CITY) || string.IsNullOrEmpty (lead.STATE)) ? "" : ", ";
			LabelCityAndState.Text = lead.CITY + coma + lead.STATE; 
			LabelScoreDigit.Text = lead.LEAD_SCORE.ToString();
			LabelNewLead.Text = GetStatus(lead.USER_LEAD_STATUS) ;
			LabelNewLead.TextAlignment = UITextAlignment.Right;
			LeadIndustry.Text = lead.LeadIndustry;
			LeadType.Text = lead.LEAD_TYPE == "Y" ? "Existing Customer" : "Prospect";

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

			ImageViewLeadImage.Frame = new CGRect (25, 75, 50, 50);

			LabelLeadName.Frame = new CGRect (100, 23, 220, 30);
			LabelTitle.Frame = new CGRect (100, 47, 280, 25);

			LabelCompanyName.Frame = new CGRect (100, 67, 280, 25);
			LeadType.Frame = new CGRect (100, 87, 300,25);

			LabelCityAndState.Frame = new CGRect (100, 107, 280, 25);
			LeadIndustry.Frame = new CGRect (100, 127, 280, 25);

			LabelScore.Frame = new CGRect (100, 147, 120, 25);
			LabelScoreDigit.Frame = new CGRect (270, 147, 25, 25);

			LabelNewLead.Frame = new CGRect (this.Bounds.Size.Width - 170, 10, 150, 25);
		}
	}
}


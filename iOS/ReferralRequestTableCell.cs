using System;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public class ReferralRequestTableCell : UITableViewCell  {

		UILabel LabelLeadName, LabelCompanyName, LabelCityAndState, LabelScoreDigit, LabelScore, LabelNewLead;
		UIImageView ImageViewLeadImage;

		public ReferralRequestTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelLeadName = new UILabel () {
				Font = UIFont.FromName("Arial", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadName});

			LabelCompanyName = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCompanyName});

			LabelCityAndState = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCityAndState});

			LabelScore = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				Text = "Lead Score:",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScore});

			LabelScoreDigit = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScoreDigit});

			LabelNewLead = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				//				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Black,
				Text = "New",
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center
			};
			ContentView.AddSubviews(new UIView[] {LabelNewLead});

			ImageViewLeadImage = new UIImageView () {
				Image = UIImage.FromBundle("Scott Anders_Large.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});
		}

		public void UpdateCell (ReferralRequest rrObj)
		{
//			LabelLeadName.Text = lead.Name;
//			LabelCompanyName.Text = lead.Company;
//			LabelCityAndState.Text = lead.City; 
//			LabelScoreDigit.Text = lead.LeadScore.ToString();
//			if (lead.IsNew == false)
//				LabelNewLead.Hidden = true;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (10, 35, 100, 100);
			LabelLeadName.Frame = new CGRect (140, 38, 180, 30);
			LabelCompanyName.Frame = new CGRect (140, 63, 150, 25);
			LabelCityAndState.Frame = new CGRect (140, 83, 150, 25);
			LabelScore.Frame = new CGRect (140, 118, 120, 25);
			LabelScoreDigit.Frame = new CGRect (270, 117, 25, 25);
			LabelNewLead.Frame = new CGRect (330, 20, 80, 25);
		}
	}
}


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
				Font = UIFont.FromName("Segoe UI", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadName});

			LabelCompanyName = new UILabel () {
				Font = UIFont.FromName("Segoe UI", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCompanyName});

			LabelCityAndState = new UILabel () {
				Font = UIFont.FromName("Segoe UI", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCityAndState});

			LabelScore = new UILabel () {
				Font = UIFont.FromName("Segoe UI", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScore});

			LabelScoreDigit = new UILabel () {
				Font = UIFont.FromName("Segoe UI", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelScoreDigit});

			LabelNewLead = new UILabel () {
				Font = UIFont.FromName("Segoe UI", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelNewLead});

			ImageViewLeadImage = new UIImageView ();

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});
		}

		public void UpdateCell (Leads lead)
		{
			LabelLeadName.Text = lead.Name;
			LabelCompanyName.Text = lead.Company;
			LabelCityAndState.Text = lead.City; 
			LabelScoreDigit.Text = lead.LeadScore.ToString();
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (10, 35, 100, 100);
			LabelLeadName.Frame = new CGRect (140, 38, 180, 20);
			LabelCompanyName.Frame = new CGRect (140, 63, 150, 20);
			LabelCityAndState.Frame = new CGRect (140, 83, 150, 20);
			LabelScore.Frame = new CGRect (140, 118, 120, 20);
			LabelScoreDigit.Frame = new CGRect (270, 117, 20, 20);
			LabelNewLead.Frame = new CGRect (330, 20, 80, 20);
		}
	}
}


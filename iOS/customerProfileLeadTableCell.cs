using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;
using System.Collections.Generic;

namespace donow.iOS
{
	public class customerProfileLeadTableCell : UITableViewCell  {

		UILabel LabelLeadStory, LabelLeadScore, LabelLeadSource, LabelLeadHours;
		UIImageView ImageViewLeadImage;

		public customerProfileLeadTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelLeadStory = new UILabel () {
				Font = UIFont.FromName("Arial", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadStory});

			LabelLeadScore = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadScore});

			LabelLeadSource = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadSource});

			LabelLeadHours = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				Text = "Lead Score:",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLeadHours});

			ImageViewLeadImage = new UIImageView () {
				Image = UIImage.FromBundle("Scott Anders_Large.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});
		}

		public void UpdateCell (String lead)
		{
//			ImageViewLeadImage.Image = UIImage.FromBundle ();
			LabelLeadStory.Text = lead;
//			LabelLeadScore.Text = lead.Company;
//			LabelLeadSource.Text = lead.City; 
//			LabelLeadHours.Text = lead.LeadScore.ToString();
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (35, 23, 60, 55);
			LabelLeadStory.Frame = new CGRect (122,17,273,32);
			LabelLeadScore.Frame = new CGRect (124,58,127,121);
			LabelLeadSource.Frame = new CGRect (264,57,127,21);
			LabelLeadHours.Frame = new CGRect (124, 100, 72, 21);
		}
	}
}


using System;
using UIKit;
using CoreGraphics;
using donow.PCL.Model;
using donow.PCL;

namespace donow.iOS
{
	public class Customer360TableCell : UITableViewCell  {

		UILabel LabelLeadStory, LabelLeadScore, LabelLeadSource, LabelLeadHours;
		UIImageView ImageViewLeadImage;

		public Customer360TableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
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
				Image = UIImage.FromBundle("New Leads Thumb.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});
		}

		public void UpdateCell (Leads leads)
		{
//			ImageViewLeadImage.Image = UIImage.FromBundle ();
			LabelLeadStory.Text = leads.BUSINESS_NEED;
			LabelLeadScore.Text = "Score: " + leads.LEAD_SCORE.ToString();
			LabelLeadSource.Text = "Source:" + leads.LEAD_SOURCE; 
			LabelLeadHours.Text = DateTime.Now.Hour.ToString() + "h";
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (35, 23, 60, 55);
			LabelLeadStory.Frame = new CGRect (122,17,273,32);
			LabelLeadScore.Frame = new CGRect (124,58,127,21);
			LabelLeadSource.Frame = new CGRect (264,57,127,21);
			LabelLeadHours.Frame = new CGRect (124, 100, 72, 21);
		}

	}
}


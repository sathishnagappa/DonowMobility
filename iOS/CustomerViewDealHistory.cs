using System;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public class CustomerViewDealHistory : UITableViewCell
	{
		UILabel LabelSentEmail, LabelDate, LabelTime;
		UIImageView ImageViewEmail;

		public CustomerViewDealHistory (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelSentEmail = new UILabel () {
				Font = UIFont.FromName("Arial", 16f),
				//TextColor = UIColor.FromRGB (127, 51, 0),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelSentEmail});

			LabelDate = new UILabel () {
				Font = UIFont.FromName("Arial", 14f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelDate});

			LabelTime = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				//TextColor = UIColor.FromRGB (127, 51, 0),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelTime});

			ImageViewEmail = new UIImageView () {
				Image = UIImage.FromBundle("Deal History Thumb.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewEmail});
		}

		public void UpdateCell (DealHistroy dealHistory)
		{
			LabelSentEmail.Text = DateTime.Parse(dealHistory.Date).ToString("MMM. dd, yyyy  hh:mm tt");
			LabelDate.Text = "Location:" + dealHistory.City + "," + dealHistory.State;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewEmail.Frame = new CGRect (25, 19, 40, 35);
			LabelSentEmail.Frame = new CGRect (85, 10, 272, 32);
			LabelDate.Frame = new CGRect (85,35,280,21);
//			LabelTime.Frame = new CGRect (264,57,127,21);
		}
	}
}


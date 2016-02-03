using System;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public class CustomerViewDealHistory : UITableViewCell
	{
		UILabel LabelDealHistroyDate, LabelLocation, LabelIndustry,LabelBrokerName;
		UIImageView ImageViewEmail;

		public CustomerViewDealHistory (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelDealHistroyDate = new UILabel () {
				Font = UIFont.FromName("Arial", 16f),
				//TextColor = UIColor.FromRGB (127, 51, 0),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelDealHistroyDate});

			LabelLocation = new UILabel () {
				Font = UIFont.FromName("Arial", 14f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLocation});

			LabelIndustry = new UILabel () {
				Font = UIFont.FromName("Arial", 16f),
				//TextColor = UIColor.FromRGB (127, 51, 0),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelIndustry});

			LabelBrokerName = new UILabel () {
				Font = UIFont.FromName("Arial", 16f),
				//TextColor = UIColor.FromRGB (127, 51, 0),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelBrokerName});

			ImageViewEmail = new UIImageView () {
				Image = UIImage.FromBundle("Deal History Thumb.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewEmail});
		}

		public void UpdateCell (DealHistroy dealHistory)
		{
			LabelDealHistroyDate.Text = DateTime.Parse(dealHistory.Date).ToString("MMM. dd, yyyy  hh:mm tt");
			LabelLocation.Text = "Location:" + dealHistory.City + "," + dealHistory.State;
			LabelIndustry.Text = dealHistory.LeadIndustry;
			LabelBrokerName.Text = "Lead Contact : " + (string.IsNullOrEmpty(dealHistory.BrokerName) ? "N/A" : dealHistory.BrokerName);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewEmail.Frame = new CGRect (25, 19, 40, 35);
			LabelDealHistroyDate.Frame = new CGRect (85, 10, 272, 32);
			LabelIndustry.Frame = new CGRect (85,35,280,21); 
			LabelBrokerName.Frame = new CGRect (85,55,280,21);
			LabelLocation.Frame = new CGRect (85,75,280,21);
		}
	}
}


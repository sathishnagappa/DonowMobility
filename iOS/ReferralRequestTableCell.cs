using System;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public class ReferralRequestTableCell : UITableViewCell  {

		UILabel LabelSellerName, LabelIndustryName, LabelLocation, LabelProspect, LabelBusinessNeeds,LabelNewRequest;
		UIImageView ImageViewLeadImage;
		UIView ViewSeperator;

		public ReferralRequestTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelSellerName = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 22f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelSellerName});

			LabelIndustryName = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelIndustryName});

			LabelLocation = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				Text = "Lead Score:",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelLocation});

			LabelProspect = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),   
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelProspect});

			ViewSeperator = new UIView () {
				BackgroundColor = UIColor.FromRGB (147, 149, 151)
			};

			ContentView.AddSubviews(new UIView[] {ViewSeperator});

			LabelBusinessNeeds = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelBusinessNeeds});

			ImageViewLeadImage = new UIImageView () {
				Image = UIImage.FromBundle("Salesperson Logo_Small.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});

			LabelNewRequest = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				Text = "New",
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelNewRequest});

		}

		public void UpdateCell (ReferralRequest rrObj)
		{

			LabelSellerName.Text = "Seller :" + rrObj.SellerName;
			LabelIndustryName.Text = "Industry:" + rrObj.Industry;
			LabelLocation.Text = "Location:"+ rrObj.City + ", " + rrObj.State;
			LabelProspect.Text = "Prospect:"+ rrObj.Prospect;
			LabelBusinessNeeds.Text =  "Business Needs:" + rrObj.BusinessNeeds;
			if (rrObj.Status == 1)
				LabelNewRequest.Hidden = false;
			else
				LabelNewRequest.Hidden = true;
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (25, 22, 50, 50);
			LabelSellerName.Frame = new CGRect (100, 20, 180, 30);
			LabelIndustryName.Frame = new CGRect (100, 50, 180, 25);
			LabelLocation.Frame = new CGRect (100, 70, 180, 25);
			ViewSeperator.Frame = new CGRect (100, 105, 414,1);

			LabelProspect.Frame = new CGRect (100, 133, 180, 25);
			LabelBusinessNeeds.Frame = new CGRect (100, 153, 250, 25);
			LabelNewRequest.Frame = new CGRect (40, 150, 60, 25);

		}
	}
}
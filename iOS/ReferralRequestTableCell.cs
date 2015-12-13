using System;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public class ReferralRequestTableCell : UITableViewCell  {

		UILabel LabelSellerName, LabelIndustryName, LabelLocation, LabelProspect, LabelBusinessNeeds;
		UIImageView ImageViewLeadImage;
		UIView ViewSeperator;

		public ReferralRequestTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelSellerName = new UILabel () {
				Font = UIFont.FromName("Arial", 22f),
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
				Image = UIImage.FromBundle("Scott Anders_Large.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});
		}

		public void UpdateCell (ReferralRequest rrObj)
		{



//			LabelSellerName.Text = "Seller :" + rrObj.UserName;
//			LabelIndustryName.Text = "Industry:" + rrObj.Industry;
//			LabelLocation.Text = "Location:"+ rrObj.City + ", " + rrObj.State;
//			LabelProspect.Text = "Prospect:"+ rrObj.Prospect;
//			LabelBusinessNeeds.Text =  "Business Needs:" + rrObj.BusinessNeeds;
//			if (lead.IsNew == false)
//				LabelNewLead.Hidden = true;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (10, 20, 100, 100);
			LabelSellerName.Frame = new CGRect (140, 20, 180, 30);
			LabelIndustryName.Frame = new CGRect (140, 50, 180, 25);
			LabelLocation.Frame = new CGRect (140, 70, 180, 25);
			ViewSeperator.Frame = new CGRect (140, 105, 250,1);
			LabelProspect.Frame = new CGRect (140, 130, 180, 25);
			LabelBusinessNeeds.Frame = new CGRect (140, 150, 200, 25);

		}
	}
}


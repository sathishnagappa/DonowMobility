using System;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public class ReferralRequestTableCell : UITableViewCell  {

		UILabel LabelSellerName, LabelSellerTitle, LabelSellerCompany, LabelProspectName, LabelProspectCompany, LabelProspectTitle;
		UIImageView ImageViewLeadImage;
		UIView ViewSeperator;

		public ReferralRequestTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelSellerName = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelSellerName});

			LabelSellerCompany = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear,
				LineBreakMode = UILineBreakMode.WordWrap
			};
			ContentView.AddSubviews(new UIView[] {LabelSellerCompany});

			LabelSellerTitle = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				Text = "Lead Score:",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelSellerTitle});

			LabelProspectName = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 18f),   
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelProspectName});

			ViewSeperator = new UIView () {
				BackgroundColor = UIColor.FromRGB (147, 149, 151)
			};

			ContentView.AddSubviews(new UIView[] {ViewSeperator});

			LabelProspectCompany = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear,
			};
			ContentView.AddSubviews(new UIView[] {LabelProspectCompany});

			LabelProspectTitle = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear,
//				Lines = 0,
//				LineBreakMode = UILineBreakMode.WordWrap
			};
			ContentView.AddSubviews(new UIView[] {LabelProspectTitle});

			ImageViewLeadImage = new UIImageView () {
				Image = UIImage.FromBundle("Salesperson Logo_Small.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewLeadImage});

//			LabelNewRequest = new UILabel () {
//				Font = UIFont.FromName("Arial", 18f),
//				Text = "New",
//				TextColor = UIColor.FromRGB (35, 31, 32),
//				BackgroundColor = UIColor.Clear
//			};
//			ContentView.AddSubviews(new UIView[] {LabelNewRequest});

		}

		public void UpdateCell (ReferralRequest rrObj)
		{	
			if(rrObj.Status != 1)
			LabelSellerName.Text = "Seller: " + rrObj.SellerName;
			
			LabelSellerCompany.Text = rrObj.CompanyName;
			LabelSellerTitle.Text = rrObj.SellerTitle;
			LabelProspectName.Text = "Prospect: "+ rrObj.Prospect;
			LabelProspectTitle.Text = rrObj.LEAD_TITLE;
			LabelProspectCompany.Text = rrObj.COMPANY_NAME;
//			if (rrObj.Status == 1)
//				LabelNewRequest.Hidden = false;
//			else
//				LabelNewRequest.Hidden = true;
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewLeadImage.Frame = new CGRect (25, 22, 50, 50);

			LabelSellerName.Frame = new CGRect (100, 22, 270, 25);
			LabelSellerTitle.Frame = new CGRect (100, 42, 270, 25);
			LabelSellerCompany.Frame = new CGRect (100, 62, this.Frame.Width - 100, 25);

			ViewSeperator.Frame = new CGRect (90, 88, this.Frame.Width - 20,1);

			LabelProspectName.Frame = new CGRect (100, 92, 270, 25);
			LabelProspectTitle.Frame = new CGRect (100, 112, 270, 25);
			LabelProspectCompany.Frame = new CGRect (100, 132, this.Frame.Width -100, 25);
		}
	}
}
using System;
using UIKit;
using CoreGraphics;
using donow.PCL;

namespace donow.iOS
{
	public class CustomerViewTableCellLeads : UITableViewCell
	{
		UILabel LabelCustomerName, LabelCustomerCompany, LabelCustomerSince;
		UIImageView ImageViewCustomerImage, ImageViewDummyLeadImage;

		public CustomerViewTableCellLeads (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelCustomerName = new UILabel () {
				Font = UIFont.FromName("Arial-BoldMT", 19f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCustomerName});

			LabelCustomerCompany = new UILabel () {
				Font = UIFont.FromName("Arial", 15f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCustomerCompany});

			LabelCustomerSince = new UILabel () {
				Font = UIFont.FromName("Arial", 15f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelCustomerSince});

			ImageViewCustomerImage = new UIImageView () {
				Image = UIImage.FromBundle("Scott Anders_Large.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewCustomerImage});

			ImageViewDummyLeadImage = new UIImageView () {
				Image = UIImage.FromBundle("Leads Icon_Small.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewDummyLeadImage});
		}

		public void UpdateCell (Customer customerInfo)
		{
			LabelCustomerName.Text = customerInfo.Name;
			LabelCustomerCompany.Text = customerInfo.Company;
			LabelCustomerSince.Text =  "Customer since"; //+ customerInfo.;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewCustomerImage.Frame = new CGRect (25, 22, 50, 50);
//			ImageViewDummyLeadImage.Frame = new CGRect (316, 10, 20, 20);
			ImageViewDummyLeadImage.Frame = new CGRect ( this.Bounds.Size.Width - 60 , 10, 20, 20);
			LabelCustomerName.Frame = new CGRect (100,22,245,25);
			LabelCustomerCompany.Frame = new CGRect (100,40,200,25);
			LabelCustomerSince.Frame = new CGRect (100,62,220,25);
		}

	}
}


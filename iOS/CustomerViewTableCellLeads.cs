﻿using System;
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
				Font = UIFont.FromName("Arial-BoldMT", 15f),
				TextColor = UIColor.FromRGB (134,133,133),
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
			LabelCustomerSince.Text =  "Customer since May 2012";
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewCustomerImage.Frame = new CGRect (20, 20, 60, 60);
			ImageViewDummyLeadImage.Frame = new CGRect (350, 10, 30, 30);
			LabelCustomerName.Frame = new CGRect (100,12,245,25);
			LabelCustomerCompany.Frame = new CGRect (100,40,200,25);
			LabelCustomerSince.Frame = new CGRect (100,62,220,25);
		}

	}
}


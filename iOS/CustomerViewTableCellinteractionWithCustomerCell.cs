﻿using System;
using UIKit;
using CoreGraphics;
using donow.PCL;

namespace donow.iOS
{
	public class CustomerViewTableCellinteractionWithCustomerCell : UITableViewCell
	{
		UILabel LabelSentEmail, LabelDate, LabelTime;
		UIImageView ImageViewEmail;

		public CustomerViewTableCellinteractionWithCustomerCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelSentEmail = new UILabel () {
				Font = UIFont.FromName("Arial", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelSentEmail});

			LabelDate = new UILabel () {
				Font = UIFont.FromName("Arial", 14f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelDate});

			//			LabelTime = new UILabel () {
			//				Font = UIFont.FromName("Arial", 18f),
			//				TextColor = UIColor.FromRGB (127, 51, 0),
			//				BackgroundColor = UIColor.Clear
			//			};
			//			ContentView.AddSubviews(new UIView[] {LabelTime});

			ImageViewEmail = new UIImageView () {
				Image = UIImage.FromBundle("New Meeting Thumb.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewEmail});
		}

		public void UpdateCell (CustomerInteraction meetingsInfo)
		{
			LabelSentEmail.Text = meetingsInfo.Type == "Email" ? "Sent Email" : "Phone Call";
			LabelDate.Text =  DateTime.Parse(meetingsInfo.DateNTime).ToString("MMM dd, yyyy  hh:mm tt");
			if(meetingsInfo.Type == "Email")
				ImageViewEmail.Image = UIImage.FromBundle ("Email Sent Thumb.png"); 
			else
				ImageViewEmail.Image = UIImage.FromBundle ("Phone Thumb.png");	

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


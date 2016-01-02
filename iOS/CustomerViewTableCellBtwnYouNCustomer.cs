﻿using System;
using UIKit;
using CoreGraphics;
using donow.PCL;

namespace donow.iOS
{
	public class CustomerViewTableCellBtwnYouNCustomer : UITableViewCell
	{
		UILabel LabelSentEmail, LabelDate, LabelTime;
		UIImageView ImageViewEmail;

		public CustomerViewTableCellBtwnYouNCustomer (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelSentEmail = new UILabel () {
				Font = UIFont.FromName("Arial", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelSentEmail});

			LabelDate = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelDate});

			LabelTime = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelTime});

			ImageViewEmail = new UIImageView () {
				Image = UIImage.FromBundle("New Meeting Thumb.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewEmail});
		}

		public void UpdateCell (/*UserMeetings meetingsInfo*/)
		{
			LabelSentEmail.Text = "Sent Email: Checking In..";
			LabelDate.Text = "Sept. 15, 2015";
			LabelTime.Text =  "12:30 pm";
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewEmail.Frame = new CGRect (35, 23, 60, 55);
			LabelSentEmail.Frame = new CGRect (122, 17, 272, 32);
			LabelDate.Frame = new CGRect (124,57,127,21);
			LabelTime.Frame = new CGRect (264,57,127,21);
		}
	}
}
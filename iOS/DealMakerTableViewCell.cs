﻿using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;
using donow.PCL;

namespace donow.iOS
{
	public class DealMakerTableViewCell : UITableViewCell
	{
		UILabel LabelTitle, LabelDealMakerScoreName,LabelDealMakerScoreDigit, LabelDealMakerIndustryDiscription, LabelDealMakerLocationDescription, LabelLeadStatus;
		UIImageView ImageViewDealMakerImage;

		public DealMakerTableViewCell  (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			LabelDealMakerScoreName = new UILabel () {
				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerScoreName });

//			LabelDealMakerIndustry = new UILabel () {
//
//				Font = UIFont.FromName ("Arial", 18f),
//				TextColor = UIColor.FromRGB (35, 31, 32),
//				BackgroundColor = UIColor.Clear
//			};
//			ContentView.AddSubviews (new UIView[]{ LabelDealMakerIndustry });


//			LabelDealMakerLocation = new UILabel () {
//
//				Font = UIFont.FromName ("Arial", 18f),
//				TextColor = UIColor.FromRGB (35, 31, 32),
//				BackgroundColor = UIColor.Clear
//			};
//			ContentView.AddSubviews (new UIView[]{ LabelDealMakerLocation });


			LabelDealMakerScoreDigit = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerScoreDigit });

			LabelDealMakerIndustryDiscription = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerIndustryDiscription });

			LabelDealMakerLocationDescription = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerLocationDescription });

			LabelLeadStatus = new UILabel () {

				Font = UIFont.FromName ("Arial-BoldMT", 16f),
				TextColor = UIColor.Black,
				//				BackgroundColor = UIColor.Black,
				TextAlignment = UITextAlignment.Right
			};
			ContentView.AddSubviews (new UIView[]{ LabelLeadStatus });

//			LabelTitleHeading = new UILabel () {
//
//				Font = UIFont.FromName ("Arial", 18f),
//				TextColor = UIColor.FromRGB (35, 31, 32),
//				BackgroundColor = UIColor.Clear
//			};
//			ContentView.AddSubviews (new UIView[]{ LabelTitleHeading });

			LabelTitle = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB (35, 31, 32),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelTitle });

			ImageViewDealMakerImage = new UIImageView () {
				Image = UIImage.FromBundle("My Deal Makers Icon.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewDealMakerImage});
		}

		public void UpdateCell (Broker brokerObj)
		{
			LabelDealMakerScoreName.Text = "Maker Score: ";
//			LabelDealMakerIndustry.Text = "Industry: ";
//			LabelDealMakerLocation.Text = "Location: " ;
			LabelDealMakerScoreDigit.Text = brokerObj.BrokerScore;
			LabelDealMakerIndustryDiscription.Text = brokerObj.Industry;
			LabelDealMakerLocationDescription.Text = brokerObj.City; //+ ", " + brokerObj.State;
			LabelLeadStatus.Text = GetStatus(brokerObj.Status);
//			LabelTitleHeading.Text = "Title: ";
			LabelTitle.Text = brokerObj.BrokerTitle;				
			//				ImageViewDealMakerImage.Image = 
		}

		string  GetStatus(int Status)
		{

			switch (Status) {
			case 1: 
				return "New";
			case 2:
				return "Acceptance Pending";
			case 3:
				return "Passed";
			case 4:
				return "Working With";
			case 5:
				return "Worked With";
			default:
				return "";
			}

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewDealMakerImage.Frame = new CGRect (25,22,50,50);

//			LabelTitleHeading.Frame = new CGRect (100,25,150,25);
			LabelTitle.Frame = new CGRect (100, 22, this.Bounds.Size.Width - 120, 25);

			LabelDealMakerIndustryDiscription.Frame = new CGRect (100,42,this.Bounds.Size.Width - 120,25);

//			LabelDealMakerLocation.Frame = new CGRect (100, 75, 150, 25);
			LabelDealMakerLocationDescription.Frame = new CGRect (100,62,220,25);

			LabelDealMakerScoreName.Frame = new CGRect (100,82,150,25);
			LabelDealMakerScoreDigit.Frame = new CGRect (220,82,220,25);

			LabelLeadStatus.Frame = new CGRect (this.Bounds.Size.Width - 220, 5, 200, 25);
		}
	}
}



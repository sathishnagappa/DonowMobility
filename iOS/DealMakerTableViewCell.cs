using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;
using donow.PCL;

namespace donow.iOS
{
	public class DealMakerTableViewCell : UITableViewCell
	{
		UILabel LabelDealMakerScoreName,LabelDealMakerIndustry,LabelDealMakerLocation,LabelDealMakerScoreDigit, LabelDealMakerIndustryDiscription, LabelDealMakerLocationDescription, LabelLeadStatus;
		UIImageView ImageViewDealMakerImage;

		public DealMakerTableViewCell  (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			LabelDealMakerScoreName = new UILabel () {
				Font = UIFont.FromName ("Arial-BoldMT", 24f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerScoreName });
		
			LabelDealMakerIndustry = new UILabel () {

				Font = UIFont.FromName ("Arial", 22f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerIndustry });


			LabelDealMakerLocation = new UILabel () {

				Font = UIFont.FromName ("Arial", 22f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerLocation });


			LabelDealMakerScoreDigit = new UILabel () {

				Font = UIFont.FromName ("Arial", 16f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerScoreDigit });

			LabelDealMakerIndustryDiscription = new UILabel () {

				Font = UIFont.FromName ("Arial", 16f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerIndustryDiscription });

			LabelDealMakerLocationDescription = new UILabel () {

				Font = UIFont.FromName ("Arial", 16f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerLocationDescription });

			LabelLeadStatus = new UILabel () {

				Font = UIFont.FromName ("Arial", 16f),
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Black,
				TextAlignment = UITextAlignment.Center
			};
			ContentView.AddSubviews (new UIView[]{ LabelLeadStatus });

			ImageViewDealMakerImage = new UIImageView () {
				Image = UIImage.FromBundle("Scott Anders_Large.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewDealMakerImage});
		}

		public void UpdateCell (Broker brokerObj)
		{
			LabelDealMakerScoreName.Text = "Maker Score: ";
			LabelDealMakerIndustry.Text = "Industry: ";
			LabelDealMakerLocation.Text = "Location: " ;
			LabelDealMakerScoreDigit.Text = brokerObj.BrokerScore;
			LabelDealMakerIndustryDiscription.Text = brokerObj.Industry;
			LabelDealMakerLocationDescription.Text = brokerObj.City + ", " + brokerObj.State;
			LabelLeadStatus.Text = brokerObj.Status;
//				ImageViewDealMakerImage.Image = 
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewDealMakerImage.Frame = new CGRect (35,29,60,60);
			LabelDealMakerScoreName.Frame = new CGRect (120,29,127,21);
			LabelDealMakerIndustry.Frame = new CGRect (120,56,127,21);
			LabelDealMakerLocation.Frame = new CGRect (120,88,127,21);
			LabelDealMakerScoreDigit.Frame = new CGRect (285,25,100,21);
			LabelDealMakerIndustryDiscription.Frame = new CGRect (285,59,100,21);
			LabelDealMakerLocationDescription.Frame = new CGRect (285,88,100,21);
			LabelLeadStatus.Frame = new CGRect (241,0,173,21);
		}
	}
}



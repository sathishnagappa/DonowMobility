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
				Font = UIFont.FromName ("Arial-BoldMT", 18f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerScoreName });
		
			LabelDealMakerIndustry = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerIndustry });


			LabelDealMakerLocation = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
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
				Image = UIImage.FromBundle("My Deal Makers Icon.png")
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
			LabelDealMakerLocationDescription.Text = brokerObj.City; //+ ", " + brokerObj.State;
			LabelLeadStatus.Text = GetStatus(brokerObj.Status);
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

			ImageViewDealMakerImage.Frame = new CGRect (35,29,60,60);
			LabelDealMakerScoreName.Frame = new CGRect (120,29,150,21);
			LabelDealMakerIndustry.Frame = new CGRect (120,56,150,21);
			LabelDealMakerLocation.Frame = new CGRect (120,88,150,21);
			LabelDealMakerScoreDigit.Frame = new CGRect (250,29,100,21);
			LabelDealMakerIndustryDiscription.Frame = new CGRect (250,56,150,21);
			LabelDealMakerLocationDescription.Frame = new CGRect (250,88,150,21);
			LabelLeadStatus.Frame = new CGRect (241,5,160,21);
		}
	}
}



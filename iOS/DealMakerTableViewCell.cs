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
				Font = UIFont.FromName ("Arial", 19f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerScoreName });

			LabelDealMakerIndustry = new UILabel () {

				Font = UIFont.FromName ("Arial", 19f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerIndustry });


			LabelDealMakerLocation = new UILabel () {

				Font = UIFont.FromName ("Arial", 19f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerLocation });


			LabelDealMakerScoreDigit = new UILabel () {

				Font = UIFont.FromName ("Arial", 19f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerScoreDigit });

			LabelDealMakerIndustryDiscription = new UILabel () {

				Font = UIFont.FromName ("Arial", 19f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerIndustryDiscription });

			LabelDealMakerLocationDescription = new UILabel () {

				Font = UIFont.FromName ("Arial", 19f),
				TextColor = UIColor.FromRGB (77,72,73),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDealMakerLocationDescription });

			LabelLeadStatus = new UILabel () {

				Font = UIFont.FromName ("Arial", 18f),
				TextColor = UIColor.Black,
				//				BackgroundColor = UIColor.Black,
				TextAlignment = UITextAlignment.Left
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

			LabelLeadStatus.Frame = new CGRect (30,10,200,21);
			ImageViewDealMakerImage.Frame = new CGRect (25,45,50,50);
			LabelDealMakerScoreName.Frame = new CGRect (100,29,150,21);
			LabelDealMakerIndustry.Frame = new CGRect (100,56,150,21);
			LabelDealMakerLocation.Frame = new CGRect (100,88,150,21);
			LabelDealMakerScoreDigit.Frame = new CGRect (215,29,100,21);
			LabelDealMakerIndustryDiscription.Frame = new CGRect (215,56,150,21);
			LabelDealMakerLocationDescription.Frame = new CGRect (215,88,150,21);
		}
	}
}



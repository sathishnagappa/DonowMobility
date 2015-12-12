using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;
using donow.PCL;
namespace donow.iOS
{
	public class CustomerStreamTableViewCell :UITableViewCell
	{

		UILabel LabelCustomerName,LabelPlaceName,LabelDesc,LabelHourText;
		UIImageView ImageViewCustomerImage,ImageViewClockImage;
	
		public CustomerStreamTableViewCell  (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			LabelCustomerName = new UILabel () {
				Font = UIFont.FromName ("Arial-BoldMT", 24f),
				TextColor = UIColor.FromRGB (77,72,73),
				//BackgroundColor = UIColor.clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelCustomerName });

			LabelPlaceName = new UILabel () {
				
				Font = UIFont.FromName ("Arial", 22f),
				TextColor = UIColor.FromRGB (77,72,73),
				//BackgroundColor = UIColor.clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelPlaceName });


			LabelDesc = new UILabel () {

				Font = UIFont.FromName ("Arial", 22f),
				TextColor = UIColor.FromRGB (77,72,73),
				Text="Michelle is celebrating 5 years at\n Northwest Pacific Tech",
				//BackgroundColor = UIColor.clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelDesc });


			LabelHourText = new UILabel () {

				Font = UIFont.FromName ("Arial", 16f),
				TextColor = UIColor.FromRGB (77,72,73),
				Text="2h",
				//BackgroundColor = UIColor.clear
			};
			ContentView.AddSubviews (new UIView[]{ LabelHourText });


			ImageViewCustomerImage = new UIImageView () {
				Image = UIImage.FromBundle("Scott Anders_Large.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewCustomerImage});

			ImageViewClockImage = new UIImageView () {
				Image = UIImage.FromBundle("Deal History Icon_Small.png")
			};

			ContentView.AddSubviews(new UIView[] {ImageViewClockImage});


		}
		public void UpdateCell (Customer obj)
		{
			//			ImageViewLeadImage.Image = UIImage.FromBundle ();
			LabelCustomerName.Text = obj.Name;
			LabelPlaceName.Text = obj.City;


		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			LabelCustomerName.Frame = new CGRect (83, 8, 189,21);
			LabelPlaceName.Frame = new CGRect (83, 35, 189, 21);
			LabelHourText.Frame = new CGRect (330,8,20,15);
			LabelDesc.Frame = new CGRect (16, 80, 330, 21);
			ImageViewCustomerImage.Frame = new CGRect (16, 8, 42, 48);
			ImageViewClockImage.Frame = new CGRect (300, 8, 20, 20);

		}

	}
}

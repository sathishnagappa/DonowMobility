using System;
using UIKit;
using CoreGraphics;
using donow.PCL;

namespace donow.iOS
{
	public class CustomerViewTableCellUpcomingMeetings : UITableViewCell
	{
		UILabel LabelMeetingWith, LabelMeetingDate, LabelMeetingTime, LabelMeetingLocation;
		UIImageView ImageViewMeeting;

		public CustomerViewTableCellUpcomingMeetings (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelMeetingWith = new UILabel () {
				Font = UIFont.FromName("Arial", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingWith});

			LabelMeetingDate = new UILabel () {
				Font = UIFont.FromName("Arial", 16f),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingDate});

			//			LabelMeetingTime = new UILabel () {
			//				Font = UIFont.FromName("Arial", 18f),
			//				TextColor = UIColor.FromRGB (127, 51, 0),
			//				BackgroundColor = UIColor.Clear
			//			};
			//			ContentView.AddSubviews(new UIView[] {LabelMeetingTime});

			LabelMeetingLocation = new UILabel () {
				Font = UIFont.FromName("Arial", 13f),
				TextColor = UIColor.LightGray,
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingLocation});

			ImageViewMeeting = new UIImageView () {
				Image = UIImage.FromBundle("New Meeting Thumb.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewMeeting});
		}

		public void UpdateCell (UserMeetings userMeetings)
		{
			LabelMeetingWith.Text = userMeetings.Subject ;
			LabelMeetingDate.Text = DateTime.Parse(userMeetings.StartDate).ToString("MMM. dd, yyyy  hh:mm tt");
			LabelMeetingLocation.Text = "Location: " + userMeetings.City + ", " + userMeetings.State;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewMeeting.Frame = new CGRect (25, 19, 40, 35);
			LabelMeetingWith.Frame = new CGRect (85, 10, 272, 32);
			LabelMeetingDate.Frame = new CGRect (85,35,290,21);
			//			LabelMeetingTime.Frame = new CGRect (270,57,80,21);
			LabelMeetingLocation.Frame = new CGRect (85,65,290,25);
		}
	}
}


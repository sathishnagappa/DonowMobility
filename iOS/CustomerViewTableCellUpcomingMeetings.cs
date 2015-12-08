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
				Font = UIFont.FromName("Arial", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingWith});

			LabelMeetingDate = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingDate});

			LabelMeetingTime = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingTime});

			LabelMeetingLocation = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingLocation});

			ImageViewMeeting = new UIImageView () {
				Image = UIImage.FromBundle("New Meeting Thumb.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewMeeting});
		}

		public void UpdateCell ()
		{
			LabelMeetingWith.Text = "Meeting with Scott Anders";
			LabelMeetingDate.Text = "Nov. 30, 2015";
			LabelMeetingTime.Text =  "3:30 pm";
			LabelMeetingLocation.Text = "Location: Seattle, WA";
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewMeeting.Frame = new CGRect (35, 26, 60, 55);
			LabelMeetingWith.Frame = new CGRect (117, 17, 272, 32);
			LabelMeetingDate.Frame = new CGRect (117,57,127,21);
			LabelMeetingTime.Frame = new CGRect (270,57,80,21);
			LabelMeetingLocation.Frame = new CGRect (117,216,250,25);
		}
	}
}


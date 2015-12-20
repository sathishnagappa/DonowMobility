using System;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public class CustomerViewPreviousMeetings : UITableViewCell
	{
		UILabel LabelMeetingWith, LabelMeetingDate, LabelMeetingTime, LabelMeetingLocation;
		UIImageView ImageViewMeeting;

		public CustomerViewPreviousMeetings (string cellId) : base (UITableViewCellStyle.Default, cellId)
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
				Font = UIFont.FromName("Arial", 14f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelMeetingLocation});

			ImageViewMeeting = new UIImageView () {
				Image = UIImage.FromBundle("Previous Meeting Thumb.png")
			};
			ContentView.AddSubviews(new UIView[] {ImageViewMeeting});
		}

		public void UpdateCell (UserMeetings userMeetings)
		{
			//LabelMeetingWith.Text = userMeetings.CustomerName ;
			LabelMeetingDate.Text = DateTime.Parse(userMeetings.StartDate).ToString("MMM. dd, yyyy  hh:mm tt");
			LabelMeetingLocation.Text = "Location: " + userMeetings.City + ", " + userMeetings.State;
		} 

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ImageViewMeeting.Frame = new CGRect (35, 26, 60, 55);
			//LabelMeetingWith.Frame = new CGRect (117, 17, 272, 32);
			LabelMeetingDate.Frame = new CGRect (117,26,290,21);
//			LabelMeetingTime.Frame = new CGRect (270,57,80,21);
			LabelMeetingLocation.Frame = new CGRect (117,57,290,25);
		}
	}
}


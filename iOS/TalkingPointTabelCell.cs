using System;
using UIKit;
using CoreGraphics;

namespace donow.iOS
{
	public class TalkingPointTabelCell : UITableViewCell
	{
		UILabel LabelDot, LabelPoints;

		public TalkingPointTabelCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.White;
			LabelDot = new UILabel () {
				//Font = UIFont.FromName("Arial", 22f),
				//TextColor = UIColor.FromRGB (127, 51, 0),
				TextColor = UIColor.Black,
				Text = "●",
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {LabelDot});

			LabelPoints = new UILabel () {
				Font = UIFont.FromName("Arial", 18f),
				//TextColor = UIColor.FromRGB (127, 51, 0),
				TextColor = UIColor.Black,
				BackgroundColor = UIColor.Clear,
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines = 0
			};
			ContentView.AddSubviews(new UIView[] {LabelPoints});

		}

		public void UpdateCell (string talkingPoint)
		{

			LabelPoints.Text = talkingPoint;
		} 

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			LabelDot.Frame = new CGRect (10,26,10,10);
			LabelPoints.Frame = new CGRect (15,26,150,300);
		}
	}
	
}


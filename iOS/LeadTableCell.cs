using System;
using UIKit;
using donow.PCL.Model;
using CoreGraphics;

namespace donow.iOS
{
	public class LeadTableCell : UITableViewCell  {
		UILabel headingLabel;
		public LeadTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;ContentView.BackgroundColor = UIColor.FromRGB (218, 255, 127);
			headingLabel = new UILabel () {
				Font = UIFont.FromName("Cochin-BoldItalic", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			ContentView.AddSubviews(new UIView[] {headingLabel});

		}
		public void UpdateCell (Leads lead)
		{
			headingLabel.Text = lead.Name;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			headingLabel.Frame = new CGRect (5, 4, ContentView.Bounds.Width - 63, 25);

		}
	}
}


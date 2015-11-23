using System;
using UIKit;

namespace donow.iOS
{
	public class LeadTableCell : UITableViewCell  {
		UILabel headingLabel;
		public LeadTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
		}
		public void UpdateCell (string name, UIImage image)
		{
			headingLabel.Text = name;
		}
	}
}


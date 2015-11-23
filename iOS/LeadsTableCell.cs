using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace donow.iOS
{
	partial class LeadsTableCell : UITableViewCell
	{
		public LeadsTableCell (IntPtr handle) : base (handle)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
		}
		public void UpdateCell (string name, UIImage image)
		{
			label.Text = name;
		}
	}
}

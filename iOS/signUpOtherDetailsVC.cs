using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace donow.iOS
{
	public partial class signUpOtherDetailsVC : UIViewController
	{
		public signUpOtherDetailsVC(IntPtr handle) : base (handle)
		{
			
		}

//		public void changeTitle ( String title ) {
//
//			ButtonNext.TitleLabel.Text = title;
//		}

		public override void ViewDidLoad ()
		{
			TableViewState.Hidden = true;
			IList<string> colors = new List<string>
			{
				"AK","AZ","AR","CA","CO","CT","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
				"MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX",
				"UT","VT","VA","WA","WV","WI","WY"
			};
			ScrollViewSignUpDetails.ContentSize =  new SizeF (415f, 850f);
			ButtonNext.Layer.CornerRadius = 5.0f;
			TableViewState.Source = new TableSource(colors,this);
			TableViewState.ContentSize = new SizeF (100f,50f);
			ButtonState.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewState.Hidden = false;
			};

			ButtonDolater.TouchUpInside += (object sender, EventArgs e) => {
				signUpSocialLinksVC signUpSocialVC = this.Storyboard.InstantiateViewController ("signUpSocialLinksVC") as signUpSocialLinksVC;
				if (signUpSocialVC != null) {
					this.NavigationController.PushViewController (signUpSocialVC, true);
				}
			};
		}

		public void UpdateControls (string State)
		{
			TableViewState.Hidden = true;
			ButtonState.TitleLabel.Text = State;

		}
	}

	public class TableSource : UITableViewSource {

		IList<string> TableItems;
		string CellIdentifier = "TableCell";

		signUpOtherDetailsVC signupVC;
		public TableSource (IList<string> items, signUpOtherDetailsVC signupVC)
		{
			this.signupVC = signupVC;
			TableItems = items;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return TableItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			string item = TableItems[indexPath.Row];

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

			cell.TextLabel.Text = item;

			return cell;
		}


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
//			signupVC.ButtonState.TitleLabel.Text = TableItems[indexPath.Row].ToString();
//			signupVC.TableViewState.Hidden = true;
			signupVC.UpdateControls(TableItems[indexPath.Row]);
//			signupVC.changeTitle(TableItems[indexPath.Row]);
		}

	}
}

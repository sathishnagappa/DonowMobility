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
			TextBoxShouldReturn ();
			TableViewState.Hidden = true;
			IList<string> States = new List<string>
			{
				"AK","AZ","AR","CA","CO","CT","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
				"MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX",
				"UT","VT","VA","WA","WV","WI","WY"
			};

			ScrollViewSignUpDetails.ContentSize =  new SizeF (415f, 1200f);
			ButtonNext.Layer.CornerRadius = 5.0f;
			TableViewState.Source = new TableSource(States,this , "States");
			TableViewState.ContentSize = new SizeF (100f,50f);
			ButtonState.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewState.Hidden = false;
			};

			TableViewIndustry.Hidden = true;
			IList<string> Industries = new List<string>
			{
				"Automobiles","Chemicals","Construction & Capital Goods","Consumption products","Energy", "Financials","Healthcare","Industrials",
				"Infrastructure","Metals","Retail and other services","Technology", "Media and Telecomunication","Textiles","Transportation","Travel"
			};

			TableViewIndustry.Source = new TableSource(Industries,this, "Industry");
			TableViewIndustry.ContentSize = new SizeF (100f,50f);
			ButtonIndustry.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewIndustry.Hidden = false;
			};

			ButtonDolater.TouchUpInside += (object sender, EventArgs e) => {
				signUpSocialLinksVC signUpSocialVC = this.Storyboard.InstantiateViewController ("signUpSocialLinksVC") as signUpSocialLinksVC;
				if (signUpSocialVC != null) {
					this.NavigationController.PushViewController (signUpSocialVC, true);
				}
			};

			ButtonNext.TouchUpInside += (object sender, EventArgs e) => {				
				signUpSocialLinksVC signUpSocialVC = this.Storyboard.InstantiateViewController ("signUpSocialLinksVC") as signUpSocialLinksVC;
				if (signUpSocialVC != null) {
					this.NavigationController.PushViewController (signUpSocialVC, true);
				}
			};
		}

		void TextBoxShouldReturn()
		{
			TextBoxFullName.ShouldReturn = delegate {
				TextBoxFullName.ResignFirstResponder ();
				return true;
			};
			TextBoxTitle.ShouldReturn = delegate {
				TextBoxTitle.ResignFirstResponder ();
				return true;
			};
			TextBoxCompany.ShouldReturn = delegate {
				TextBoxCompany.ResignFirstResponder ();
				return true;
			};
			TextBoxOfficeAddress.ShouldReturn = delegate {
				TextBoxOfficeAddress.ResignFirstResponder ();
				return true;
			};
			TextBoxCity.ShouldReturn = delegate {
				TextBoxCity.ResignFirstResponder ();
				return true;
			};
			TextBoxZip.ShouldReturn = delegate {
				TextBoxZip.ResignFirstResponder ();
				return true;
			};
			TextBoxEmail.ShouldReturn = delegate {
				TextBoxEmail.ResignFirstResponder ();
				return true;
			};
			TextBoxPhone.ShouldReturn = delegate {
				TextBoxPhone.ResignFirstResponder ();
				return true;
			};
			TextBoxPayPalUser.ShouldReturn = delegate {
				TextBoxPayPalUser.ResignFirstResponder ();
				return true;
			};
			TextBoxPayPalPassword.ShouldReturn = delegate {
				TextBoxPayPalPassword.ResignFirstResponder ();
				return true;
			};
		}

		public void UpdateControls (string Parameter, string TableType)
		{
			if (TableType == "States") {
				TableViewState.Hidden = true;
				//				ButtonState.SetTitle (Parameter, UIControlState.Normal);
				TextBoxState.Text = Parameter;
			} else {
				TableViewIndustry.Hidden = true;
				TextBoxIndustry.Text = Parameter;
			}
		}
	}

	public class TableSource : UITableViewSource {

		IList<string> TableItems;
		string CellIdentifier = "TableCell";
		string TSTableType = string.Empty;

		signUpOtherDetailsVC signupVC;
		public TableSource (IList<string> items, signUpOtherDetailsVC signupVC, string TableType)
		{
			this.signupVC = signupVC;
			TableItems = items;
			TSTableType = TableType;
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
			signupVC.UpdateControls(TableItems[indexPath.Row], TSTableType);
			//			signupVC.changeTitle(TableItems[indexPath.Row]);
		}

	}

}

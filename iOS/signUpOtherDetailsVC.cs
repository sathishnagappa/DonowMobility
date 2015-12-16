using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
using CoreGraphics;
using donow.PCL;
using System.Linq;

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
			TextBoxEmail.Text = string.Empty;
			TextBoxCompany.Text = string.Empty;
			TextBoxTitle.Text = string.Empty;
			TextBoxFullName.Text = string.Empty;
			TextBoxIndustry.Text = string.Empty;
			TextBoxZip.Text = string.Empty;
			TextBoxPhone.Text = string.Empty;
			TextBoxOfficeAddress.Text = string.Empty;
			TextBoxCity.Text = string.Empty;
			TextBoxState.Text = string.Empty;
			TextBoxLineOfBusiness.Text = string.Empty;
			
			TextBoxShouldReturn ();
			if (AppDelegate.UserProfile.name != null) {
				LoadUserDetails ();
			}
			TableViewState.Hidden = true;
			IList<string> States = new List<string>
			{
				"AK","AZ","AR","CA","CO","CT","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
				"MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX",
				"UT","VT","VA","WA","WV","WI","WY"
			};

			ScrollViewSignUpDetails.ContentSize =  new SizeF (415f, 1100);
			ButtonNext.Layer.CornerRadius = 5.0f;
			TableViewState.Source = new TableSource(States,this , "States");
			TableViewState.ContentSize = new SizeF (100f,50f);
			ButtonState.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewState.Hidden = false;
			};

			TableViewIndustry.Hidden = true;
//			IList<string> Industries = new List<string>
//			{
//				"Agriculture", "Apparel", "Auto","Banking/Finance",  "Biotechnology","Chemicals", "Communications","Construction", "Consulting","Education", "Electronics","Energy", "Engineering", 
//				"Entertainment","Food and Beverage","Government","Healthcare","Hospitality","Insurance", "Machinery", "Manufacturing", "Media","Not for Profit","Other","Recreation", "Retail",
//				"Shipping","Technology","Telecommunications","Transportation","Utilities"
//			};
//

			IndustryBL industryBL = new IndustryBL();
			List<LineOfBusiness> listLOB = industryBL.GetLOB();
			List<string> Industries = industryBL.GetIndustry ();

			//TableViewIndustry.ContentSize = new SizeF (100f,50f);
			ButtonIndustry.TouchUpInside += (object sender, EventArgs e) =>  {
				TextBoxLineOfBusiness.Text = string.Empty;
				TableViewIndustry.Frame = new CGRect(47,215,320,122);
				TableViewIndustry.Hidden = false;
				TableViewIndustry.Source = new TableSource(Industries,this, "Industry");
			};

			ButtonLineOfBusiness.TouchUpInside+= (object sender, EventArgs e) => {
				TableViewIndustry.Frame = new CGRect(47,275,320,122);
				TableViewIndustry.Hidden = false;
				List<string> lob =  (from item in listLOB
					where item.IndustryName == TextBoxIndustry.Text
					select item.LineofBusiness).ToList();
				TableViewIndustry.Source = new TableSource(lob,this, "LOB");
			};

			ButtonNext.TouchUpInside += (object sender, EventArgs e) => {
				if(Validation()) {	
				SaveUserDetails();
				AccountManagementVC accountManagementVC = this.Storyboard.InstantiateViewController ("AccountManagementVC") as AccountManagementVC;
				if (accountManagementVC != null) {
					accountManagementVC.isFromSignUp = true;
					this.NavigationController.PushViewController (accountManagementVC, true);
				}
				}
			};
		}

		bool Validation()
		{
			UIAlertView alert = null;
			if (string.IsNullOrEmpty (TextBoxFullName.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please enter Full Name."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty (TextBoxEmail.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please enter Email ID."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty (TextBoxIndustry.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please enter City."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty (TextBoxState.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please select State."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty (TextBoxIndustry.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please select Industry."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			if (string.IsNullOrEmpty (TextBoxLineOfBusiness.Text)) {
				alert = new UIAlertView () { 
					Title = "Mandatory Field", 
					Message = "Please select Line of Business."
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}
			return true;
		}

		void SaveUserDetails()
		{
			AppDelegate.UserDetails.FullName = TextBoxFullName.Text;
			AppDelegate.UserDetails.Title = TextBoxTitle.Text;
			AppDelegate.UserDetails.Company = TextBoxCompany.Text;
			AppDelegate.UserDetails.Industry = TextBoxIndustry.Text;
			AppDelegate.UserDetails.OfficeAddress = TextBoxOfficeAddress.Text;
			AppDelegate.UserDetails.City = TextBoxCity.Text;
			AppDelegate.UserDetails.Zip = string.IsNullOrEmpty(TextBoxZip.Text) == true ? "" : TextBoxZip.Text;
			AppDelegate.UserDetails.Email = TextBoxEmail.Text;
			AppDelegate.UserDetails.Phone = TextBoxPhone.Text;
			AppDelegate.UserDetails.LineOfBusiness = TextBoxLineOfBusiness.Text;

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
//			TextBoxPayPalUser.ShouldReturn = delegate {
//				TextBoxPayPalUser.ResignFirstResponder ();
//				return true;
//			};
//			TextBoxPayPalPassword.ShouldReturn = delegate {
//				TextBoxPayPalPassword.ResignFirstResponder ();
//				return true;
//			};
		}

		void LoadUserDetails()
		{
			TextBoxEmail.Text = AppDelegate.UserProfile.email;
			TextBoxCompany.Text = AppDelegate.UserProfile.positions.values [0].company.name;
			TextBoxTitle.Text = AppDelegate.UserProfile.positions.values[0].title;
			TextBoxFullName.Text = AppDelegate.UserProfile.name;
			TextBoxIndustry.Text = AppDelegate.UserProfile.industry;
		}

		public void UpdateControls (string Parameter, string TableType)
		{
			if (TableType == "States") {
				TableViewState.Hidden = true;
				TextBoxState.Text = Parameter;
			} else if (TableType == "Industry") {
				TableViewIndustry.Hidden = true;
				TextBoxIndustry.Text = Parameter;

			} else {
				TableViewIndustry.Hidden = true;
				TextBoxLineOfBusiness.Text = Parameter;
			}
		}
	}

	public class TableSource : UITableViewSource {

		IList<string> TableItems;
		string CellIdentifier = "TableCell";
		string TSTableType = string.Empty;

		signUpOtherDetailsVC signupOtherDetailsVC;
		public TableSource (IList<string> items, signUpOtherDetailsVC signupOtherDetailsVC, string TableType)
		{
			this.signupOtherDetailsVC = signupOtherDetailsVC;
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
			signupOtherDetailsVC.UpdateControls(TableItems[indexPath.Row], TSTableType);
			//			signupVC.changeTitle(TableItems[indexPath.Row]);
		}

	}

}

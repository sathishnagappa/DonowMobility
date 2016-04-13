using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
using CoreGraphics;
using donow.PCL;
using System.Linq;
using System.Text.RegularExpressions;
using donow.Util;
using donow.PCL.Model;

namespace donow.iOS
{
	public partial class signUpOtherDetailsVC : UIViewController
	{
		public signUpOtherDetailsVC(IntPtr handle) : base (handle)
		{

		}

		List<LineOfBusiness> listLOB;
		List<string> Industries;
		LoadingOverlay loadingOverlay;
		public bool isFromDashBoard;

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Industries =  AppDelegate.industryBL.GetIndustry ();
			listLOB = AppDelegate.industryBL.GetLOB();

			if (isFromDashBoard == true) {
				ButtonNext.Hidden = true;
				ButtonCancel.Hidden = false;
//				ButtonNext.SetTitle ("Submit", UIControlState.Normal);
//				ButtonNext.Frame = new CGRect (this.View.Bounds.Size.Width/2 +10, 10, 154, 40);
				ButtonSubmit.Hidden = false;
				ScrollViewSignUpDetails.ContentSize = new SizeF (350.0f, 1030.0f);
			} else {
				ButtonCancel.Hidden = true;
				ButtonSubmit.Hidden = true;
				this.NavigationController.SetNavigationBarHidden (false, false);
			}
		//	LoadScreen ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			TableViewState.Source = null;
			TableViewIndustry.Source = null;
			listLOB = null;
			Industries = null;
		}

		public override void ViewDidLoad ()
		{
			this.NavigationItem.Title = "Sign Up: User Info";

			this.View.UserInteractionEnabled = true;

			loadingOverlay = new LoadingOverlay(this.View.Bounds);
			this.View.Add(loadingOverlay);

			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			//***************************************** Hiding Back Button ***********************//

//			NavigationItem.SetHidesBackButton (true, false);	
//			NavigationItem.SetLeftBarButtonItem(null, true);
			TextBoxShouldReturn ();
//			if (AppDelegate.UserProfile.name != null) {
//				LoadUserDetails ();
//			}
			TableViewState.Hidden = true;
			ButtonLineOfBusiness.Enabled = false;
			TextBoxLineOfBusiness.UserInteractionEnabled = false;

			IList<string> States = new List<string>
			{
				"AK","AZ","AR","CA","CO","CT","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
				"MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX",
				"UT","VT","VA","WA","WV","WI","WY"
			};

			ScrollViewSignUpDetails.ContentSize =  new SizeF (350.0f, 1050.0f);

			ScrollViewSignUpDetails.Scrolled += delegate {
				TableViewState.Hidden = true;
				TableViewIndustry.Hidden = true;
				TextBoxZip.ResignFirstResponder ();
				TextBoxPhone.ResignFirstResponder ();
			};

			ButtonCancel.Layer.CornerRadius = 3.0f;
			ButtonNext.Layer.CornerRadius = 3.0f;
			ButtonSubmit.Layer.CornerRadius = 3.0f;

			TableViewState.Source = new TableSource(States,this , "States");
			TableViewState.ContentSize = new SizeF (100f,50f);
			ButtonState.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewState.Hidden = false;
			};

			TableViewIndustry.Hidden = true;
			LoadScreen ();

//			Industries =  AppDelegate.industryBL.GetIndustry ();
//			listLOB = AppDelegate.industryBL.GetLOB();

			TableViewIndustry.Layer.BorderColor=UIColor.FromRGB(169,169,169).CGColor;
			//TableViewIndustry.ContentSize = new SizeF (100f,50f);
			ButtonIndustry.TouchUpInside += (object sender, EventArgs e) =>  {
				TextBoxLineOfBusiness.Text = string.Empty;
				TableViewIndustry.Frame = new CGRect(25,345,this.View.Bounds.Size.Width - 50,122);
				TableViewIndustry.Hidden = false;
				TableViewIndustry.Source = new TableSource(Industries,this, "Industry");
				TableViewIndustry.ReloadData ();
			};

			ButtonLineOfBusiness.TouchUpInside+= (object sender, EventArgs e) => {
				TableViewIndustry.Frame = new CGRect(25,435,this.View.Bounds.Size.Width - 50,122);
				TableViewIndustry.Hidden = false;
				TableViewIndustry.ReloadData ();
			};

			ButtonNext.TouchUpInside += (object sender, EventArgs e) => {	
				
				if(isFromDashBoard == false && Validation()) {	
					SaveUserDetails();
					AccountManagementVC accountManagementVC = this.Storyboard.InstantiateViewController ("AccountManagementVC") as AccountManagementVC;
					if (accountManagementVC != null) {
						accountManagementVC.isFromSignUp = true;
						this.NavigationController.PushViewController (accountManagementVC, true);
					}
				}
//				}else if (isFromDashBoard == true && Validation()) {
//					SaveUserDetails();
//					DismissViewController(true,null);
//				}
			};
			loadingOverlay.Hide ();

			ButtonSubmit.TouchUpInside += (object sender, EventArgs e) =>  {
				if (isFromDashBoard == true && Validation()) {
				SaveUserDetails();
				AppDelegate.userBL.UpdateUserDetails(AppDelegate.UserDetails);	
				DismissViewController(true,null);
				}
			};

			ButtonCancel.TouchUpInside += (object sender, EventArgs e) => {
				DismissViewController (true, null);
			};
		}

		void LoadScreen()
		{
			if (AppDelegate.UserDetails != null && AppDelegate.UserDetails.UserId != 0) {
				TextBoxEmail.Text = AppDelegate.UserDetails.Email;
				TextBoxCompany.Text = AppDelegate.UserDetails.Company;
				TextBoxTitle.Text = AppDelegate.UserDetails.Title;
				TextBoxFullName.Text = AppDelegate.UserDetails.FullName;
				TextBoxIndustry.Text = AppDelegate.UserDetails.Industry;
				TextBoxZip.Text = AppDelegate.UserDetails.Zip;
				TextBoxPhone.Text = AppDelegate.UserDetails.Phone;
				TextBoxOfficeAddress.Text = AppDelegate.UserDetails.OfficeAddress;
				TextBoxCity.Text = AppDelegate.UserDetails.City;
				TextBoxState.Text = AppDelegate.UserDetails.State;
				TextBoxLineOfBusiness.Text = AppDelegate.UserDetails.LineOfBusiness;
			} else if (AppDelegate.UserProfile != null && AppDelegate.UserProfile.name != null) {
				LoadUserDetails ();
			}
			else
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
			}
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

			if (!Regex.IsMatch (TextBoxEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
				   RegexOptions.IgnoreCase)) {
				alert = new UIAlertView () { 
					Title = "Invalid Email ID", 
					Message = "Please enter valid Email ID"
				};
				alert.AddButton ("OK");
				alert.Show ();
				return false;
			}

			if (string.IsNullOrEmpty (TextBoxCity.Text)) {
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

			if (isFromDashBoard == false || AppDelegate.UserDetails.Email != TextBoxEmail.Text) {
				UserDetails userDetails = AppDelegate.userBL.GetUserFromEmail (TextBoxEmail.Text);

				if (userDetails != null) {
					alert = new UIAlertView () { 
						Title = "Email ID", 
						Message = "A profile with this email address is already registered."
					};
					alert.AddButton ("OK");
					alert.Show ();
					return false;
				}
			}
//			if (string.IsNullOrEmpty (TextBoxLineOfBusiness.Text)) {
//				alert = new UIAlertView () { 
//					Title = "Mandatory Field", 
//					Message = "Please select Line of Business."
//				};
//				alert.AddButton ("OK");
//				alert.Show ();
//				return false;
//			}
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
			AppDelegate.UserDetails.State = TextBoxState.Text;
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
			AppDelegate.UserDetails = new UserDetails();
			AppDelegate.UserDetails.Name = AppDelegate.UserProfile.email;
			AppDelegate.UserDetails.Password = "MOicgNmn7qm756+877Cr4Yee7ryr6Yme";
			TextBoxEmail.Text = AppDelegate.UserProfile.email;
			TextBoxCompany.Text = AppDelegate.UserProfile.positions.values [0].company.name;
			TextBoxTitle.Text = AppDelegate.UserProfile.positions.values[0].title;
			TextBoxFullName.Text = AppDelegate.UserProfile.name;
			//TextBoxIndustry.Text = AppDelegate.UserProfile.industry;
		}

		public void UpdateControls (string Parameter, string TableType)
		{
			if (TableType == "States") {
				TableViewState.Hidden = true;
				TextBoxState.Text = Parameter;
			} else if (TableType == "Industry") {
				TableViewIndustry.Hidden = true;
				TextBoxIndustry.Text = Parameter;
				List<string> lob =  (from item in listLOB
					where item.IndustryName == TextBoxIndustry.Text
					select item.LineofBusiness).ToList();
				if (lob.Count > 0) {
					ButtonLineOfBusiness.Enabled = true;
					TableViewIndustry.Source = new TableSource (lob, this, "LOB");
				}
				else
					ButtonLineOfBusiness.Enabled = false;

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

			cell.BackgroundColor = UIColor.FromRGB (169, 169, 169);


			return cell;
		}


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			signupOtherDetailsVC.UpdateControls(TableItems[indexPath.Row], TSTableType);
		}

	}

}

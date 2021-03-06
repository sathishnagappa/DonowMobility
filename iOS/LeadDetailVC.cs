using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using System.Collections.Generic;
using MessageUI;
using EventKit;
using CoreGraphics;
using System.Drawing;
using donow.PCL;
using Xamarin;

namespace donow.iOS
{
	public partial class LeadDetailVC : UIViewController
	{
		public Leads leadObj;
//		bool isLeadAccepted = false;
		public string reasonForPass = string.Empty;

		public LeadDetailVC (IntPtr handle) : base (handle)
		{
		}

		IList<string> OptionsPassView = new List<string>
		{
			"Too Busy", "Score Lead too Low", "Not Right Fit"
		};

		public override void ViewWillAppear (bool animated)
		{
 			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(157,50,49);
			this.NavigationController.NavigationBar.TintColor = UIColor.White;

			ButtonBackgroundView.Hidden = true;
//			ButtonCompanyInfoExpand.Hidden = true;
//			ButtonBusinessNeedsExpand.Hidden = true;
//			ViewAccept.Hidden = true;
			ViewPass.Hidden = true;
		}

		public  override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Navigation
			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				LandingLeadsVC leadPage = this.Storyboard.InstantiateViewController ("LandingLeadsVC") as LandingLeadsVC;
				if(leadPage != null)
				this.NavigationController.PushViewController(leadPage,true);
				//this.NavigationController.PopViewController(false);
			};
			NavigationItem.LeftBarButtonItem = btn;

			ScrollViewLeadDetails.ContentSize = new CGSize (375f, 710.0f);
			this.Title = "Lead Details";

			LabelTitleName.Text = leadObj.LEAD_NAME;
			LabelScore.Text =  leadObj.LEAD_SOURCE == 2 ? (leadObj.LEAD_SCORE == 0 ? "NA" : leadObj.LEAD_SCORE.ToString()) : leadObj.LEAD_SCORE.ToString(); 
			LabelSourceName.Text = leadObj.LEAD_SOURCE == 2 ? "SFDC" : "DoNow" ;
			txtFieldCompanyInfo.Text = leadObj.COMPANY_INFO;// + " Testing - More significantly they expand the domain of research of the IS field by addressing new themes, such as the provision of ICT resources for a community";
			LabelTitleCompany.Text = leadObj.COMPANY_NAME; 
			LabelLocation.Text = leadObj.CITY + "," + leadObj.STATE;
			txtViewBusinessNeeds.Text = leadObj.BUSINESS_NEED;// + " Testing - More significantly they expand the domain of research of the IS field by addressing new themes, such as the provision of ICT resources for a community";
			LabelCustomerVsProspect.Text = leadObj.LEAD_TYPE == "Y" ? "Existing Customer" : "New Prospect" ;

			TableViewPassView.Source = new PassViewTableSource (OptionsPassView, this);

			LeadTitle.Text = "("+leadObj.LEAD_TITLE + ")";

			ButtonAccept.TouchUpInside += (object sender, EventArgs e) => {
				ButtonView.Hidden = true;
//				ButtonBackgroundView.Hidden = false;
//				ViewAccept.Hidden = false;
//				isLeadAccepted = true;
				AppDelegate.IsLeadAccepted = true;
				AppDelegate.leadsBL.UpdateStatus(leadObj.LEAD_ID,4,AppDelegate.UserDetails.UserId);

				var av = new UIAlertView ("", "Lead Accepted !", null, "Ok", null);
				av.Delegate = new alerViewDelegate (this, leadObj);
				av.Show ();

				//Xamarin Insights tracking
				Insights.Track("Lead Update Status", new Dictionary <string,string>{
					{"LEAD ID", leadObj.LEAD_ID.ToString()},
					{"Update Status", "4"}
				});
			};

			ButtonPass.Layer.BorderWidth = 2.0f;
			ButtonPass.Layer.BorderColor = UIColor.FromRGB (45, 125, 177).CGColor;
			ButtonPass.Layer.CornerRadius = 8.0f;

			ButtonPass.TouchUpInside += (object sender, EventArgs e) => {
				ButtonBackgroundView.Hidden = false;
				ViewPass.Hidden = false;
//				isLeadAccepted = false;
			};


			ButtonSubmitPassView.TouchUpInside+= (object sender, EventArgs e) => {
				ViewPass.Hidden = true;
				ButtonBackgroundView.Hidden = true;
				AppDelegate.leadsBL.UpdateReasonForPass(leadObj.LEAD_ID,reasonForPass,AppDelegate.UserDetails.UserId);
				LandingLeadsVC landingLeadsVC = this.Storyboard.InstantiateViewController ("LandingLeadsVC") as LandingLeadsVC;
				if (landingLeadsVC != null) {
					this.NavigationController.PushViewController(landingLeadsVC, true);
				}
				
			};
			TableViewPassView.Layer.BorderWidth = 1.0f;
			TableViewPassView.Layer.BorderColor = UIColor.LightGray.CGColor;
		}

		public void UpdateControls (string Parameter)
		{
			reasonForPass = Parameter;
//			TableViewPassView.Hidden = true;
		}
	}

	public class alerViewDelegate : UIAlertViewDelegate {

		LeadDetailVC leadDetailsObject;
		Leads leadObj;

		public alerViewDelegate (LeadDetailVC leadDetailsVC, Leads lead)
		{
			leadDetailsObject = leadDetailsVC;
			leadObj = lead;
		}

		public override void Clicked (UIAlertView alertview, nint buttonIndex) {
			if (buttonIndex == 0) {
				prospectDetailsVC prospectVC = leadDetailsObject.Storyboard.InstantiateViewController ("dummyViewController") as prospectDetailsVC;
				if (prospectVC != null) {
					prospectVC.localLeads = leadObj;
					//						this.PresentViewController (dummyVC, true, null);
					leadDetailsObject.NavigationController.PushViewController(prospectVC, true);
				}
			}
		}
	}

	public class PassViewTableSource : UITableViewSource 
	{
		IList<string> TableItems;
		string CellIdentifier = "TableCell";
		LeadDetailVC leadDetailsVC;

		public PassViewTableSource (IList<string> items, LeadDetailVC leadDetailsVC)
		{
			this.leadDetailsVC = leadDetailsVC;
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

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 50.0f;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
//			tableView.DeselectRow (indexPath, true);
			leadDetailsVC.UpdateControls(TableItems[indexPath.Row]);
		}
	}
}

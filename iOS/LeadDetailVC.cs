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
		bool isLeadAccepted = false;
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
			ViewAccept.Hidden = true;
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

			ScrollViewLeadDetails.ContentSize = new CGSize (375f, 700.0f);
			this.Title = "Lead Details";

			LabelTitleName.Text = leadObj.LEAD_NAME;
			LabelScore.Text = leadObj.LEAD_SCORE.ToString();
			LabelSourceName.Text = leadObj.LEAD_SOURCE == 2 ? "SFDC" : "DoNow" ;
			LabelCompanyInfo.Text = leadObj.COMPANY_INFO;
//			LabelCompanyInfo.Text = "aaaaaa aaaaaaaaaaav vvvvvvv vvvvfff fffffff fffffbbb bbbbbbb bbffffff fffggggg gggggtttttt ttttdddd dddddaa aaa af";
			LabelTitleCompany.Text = leadObj.COMPANY_NAME; 
			LabelLocation.Text = leadObj.CITY + "," + leadObj.STATE;
			LabelBusinessNeeds.Text = leadObj.BUSINESS_NEED;

			TableViewPassView.Source = new PassViewTableSource (OptionsPassView, this);

			ButtonAccept.TouchUpInside += (object sender, EventArgs e) => {
				ButtonBackgroundView.Hidden = false;
				ViewAccept.Hidden = false;
				isLeadAccepted = true;
				AppDelegate.IsLeadAccepted = true;
				AppDelegate.leadsBL.UpdateStatus(leadObj.LEAD_ID,4);

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
				isLeadAccepted = false;
			};

			ButtonBackgroundView.TouchUpInside += (object sender, EventArgs e) =>  {

				ButtonBackgroundView.Hidden = true;
				if (isLeadAccepted) {
					ViewAccept.Hidden = true;
					prospectDetailsVC prospectVC = this.Storyboard.InstantiateViewController ("dummyViewController") as prospectDetailsVC;
					if (prospectVC != null) {

						prospectVC.localLeads = leadObj;

//						this.PresentViewController (dummyVC, true, null);
						this.NavigationController.PushViewController(prospectVC, true);
					}
				} else {
					ViewPass.Hidden = true;
				}
			};

			ButtonPhoneAcceptView.TouchUpInside += (object sender, EventArgs e) => {

				var url = new NSUrl ("tel://" + leadObj.PHONE);
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				};

				CustomerInteraction customerinteract = new CustomerInteraction();
				customerinteract.CustomerName =  leadObj.LEAD_NAME;
				customerinteract.UserId = AppDelegate.UserDetails.UserId;
				customerinteract.Type = "Phone";
				customerinteract.DateNTime = DateTime.Now.ToString();
				AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);

				//Xamarin Insights tracking
				Insights.Track("Save CutomerInteraction", new Dictionary <string,string>{
					{"UserId", customerinteract.UserId.ToString()},
					{"CustomerName", customerinteract.CustomerName},
					{"Type", "Phone"}
				});
			};

			ButtonEmailAcceptView.TouchUpInside += (object sender, EventArgs e) =>  {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{leadObj.EMAILID});
					mailController.SetSubject ("Quick request");
					mailController.SetMessageBody ("Hello <Insert Name>,\n\nMy name is [My Name] and I head up business development efforts with [My Company]. \n\nI am taking an educated stab here and based on your profile, you appear to be an appropriate person to connect with.\n\nI’d like to speak with someone from [Company] who is responsible for [handling something that's relevant to my product]\n\nIf that’s you, are you open to a fifteen minute call on _________ [time and date] to discuss ways the [Company Name] platform can specifically help your business? If not you, can you please put me in touch with the right person?\n\nI appreciate the help!\n\nBest,\n\n[Insert Name]", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						CustomerInteraction customerinteract = new CustomerInteraction();
						customerinteract.CustomerName =  leadObj.LEAD_NAME;
						customerinteract.UserId = AppDelegate.UserDetails.UserId;
						customerinteract.Type = "Email";
						customerinteract.DateNTime = DateTime.Now.ToString();
						AppDelegate.customerBL.SaveCutomerInteraction(customerinteract);
						args.Controller.DismissViewController (true, null);
						//Xamarin Insights tracking
						Insights.Track("Save CutomerInteraction", new Dictionary <string,string>{
							{"UserId", customerinteract.UserId.ToString()},
							{"CustomerName", customerinteract.CustomerName},
							{"Type", "Email"}
						});
					};
						 
					this.PresentViewController (mailController, true, null);



//					Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
				}

			};

			ButtonCalendarAcceptView.TouchUpInside += (object sender, EventArgs e) => {

//				AppDelegate.IsCalendarClicked = true;

				CalenderHomeDVC calendarHomeDV = new CalenderHomeDVC ();
			   // if (calendarHomeDV != null)
				this.NavigationController.PushViewController(calendarHomeDV, true);
			    //PresentViewController(calendarHomeDV, true,null);

				//				label.AdjustsFontSizeToFitWidth = false;
			};

			ButtonCompanyInfoExpand.TouchUpInside += (object sender, EventArgs e) =>  {
			
				var maxHeight = 500.0f;
				float width = 300;// label.Frame.Width;  
				LabelCompanyInfo.Lines = 0;
				CGSize size = ((NSString)LabelCompanyInfo.Text).StringSize(LabelCompanyInfo.Font,  
					constrainedToSize:new SizeF(width, maxHeight) ,lineBreakMode:UILineBreakMode.WordWrap);
				var labelFrame = LabelCompanyInfo.Frame;
				labelFrame.Size = new CGSize(width,size.Height);
				LabelCompanyInfo.Frame = labelFrame; 

//				CompanyInfoView.Frame = new CGRect(0,265,414,200);
//				BusinessNeedsView.Frame = new CGRect (0,500, 414.0f, 120);
			};

			ButtonSubmitPassView.TouchUpInside+= (object sender, EventArgs e) => {
				ViewPass.Hidden = true;
				ButtonBackgroundView.Hidden = true;
				AppDelegate.leadsBL.UpdateReasonForPass(leadObj.LEAD_ID,reasonForPass);
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

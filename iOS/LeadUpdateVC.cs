// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using donow.PCL;
using donow.PCL.Model;
using Xamarin;

namespace donow.iOS
{
	public partial class LeadUpdateVC : UIViewController
	{
		public UserMeetings meetingObj;
		string localConfirmMeeting;
		string localReasonForDown;
		string localMeetingInfoHelpFull;	
		string localLeadAdvanced;
		string localCustomerCategorization;
		string localSalesStage;
		string localNextSteps;
		string localMeetingID;

		public LeadUpdateVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

//			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
//			this.NavigationController.SetNavigationBarHidden (false, false);
//			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (157, 50, 49);
//			this.NavigationController.NavigationBar.TintColor = UIColor.White;
		}
		 
		public void UpdateControls (string Parameter, string TableType)
		{
			if (TableType == "ReasonForPass") {
				ButtonMeetingDislikeReasonDropDown.SetTitle (Parameter, UIControlState.Normal);
				TableViewInteractionDislikeReason.Hidden = true;
				localReasonForDown = Parameter;
			} else if (TableType == "SalesStage") {
				ButtonSaleStageDropDown.SetTitle (Parameter, UIControlState.Normal);
				localSalesStage = Parameter;
				TableViewSalesStage.Hidden = true;
			} else if (TableType == "CustomerCategorisation") {
				ButtonCustomerCategorizationDropDown.SetTitle (Parameter, UIControlState.Normal);
				localCustomerCategorization = Parameter;
				TableViewCustomerCategorization.Hidden = true;
			} else {
				TableViewNextSteps.Hidden = true;
				ButtonNextStepsDropDown.SetTitle (Parameter, UIControlState.Normal);
				localNextSteps = Parameter;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ScrollViewF2F.ContentSize = new CGSize (414f, 1330.0f); 
			ViewF2FMeetingDown.Hidden = true;
			LabelConformMeeting.Text = "Confirm Your Meeting w/" + meetingObj.CustomerName; 
			IList<string> ListThumbsDownReason = new List<string>
			{
				"Customer Cancelled",
				"No Show",
				"Need to Reschedule"
			};
			IList<string> ListCustomerCategorisation = new List<string>
			{
				"Dreamer",
				"Climber",
				"Purist"
			};
			IList<string> ListSalesStages = new List<string>
			{
				"(1) Acquire Lead",
				"(2) Proposal",
				"(3) Follow Up",
				"(4) Close Sale"
			};
			IList<string> ListNextStep = new List<string>
			{
				"Get Product Info",
				"Purist"
			};


			List<LeadF2FFeedBack> leadf2ffeedbacklist = AppDelegate.leadsBL.GetLeadF2FFeedBack (meetingObj.LeadId);	
			LeadF2FFeedBack leadf2ffeedbackLast = leadf2ffeedbacklist.Count > 0 ? leadf2ffeedbacklist[leadf2ffeedbacklist.Count -1 ] : null;



			ButtonLikeMeeting.TouchUpInside += (object sender, EventArgs e) => {
				ButtonLikeMeeting.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Normal);
				ButtonDisLikeMeeting.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonConfirmMeetingSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				localConfirmMeeting = "UP";
				ViewF2FMeetingDown.Hidden = true;
				localReasonForDown = "";
				TableViewInteractionDislikeReason.Hidden = true;
				ViewSecond.Frame = new CGRect (0, 193, 414, 1134);
//				ViewFirst.Frame = new CGRect (0, 0, 414, 193);
			};

			ButtonDisLikeMeeting.TouchUpInside += (object sender, EventArgs e) => {
				ButtonLikeMeeting.SetImage(UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeMeeting.SetImage(UIImage.FromBundle ("Thumbs Down White.png"), UIControlState.Normal);
				ButtonConfirmMeetingSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				localConfirmMeeting = "DOWN";
				ViewF2FMeetingDown.Hidden = false;
//				ViewFirstDropDown.Frame = new CGRect(0,70,414,334);
//				ViewFirst.Frame = new CGRect (0, 0, 414, 334);
				ViewSecond.Frame = new CGRect (0, 327, 414, 1134);
				ButtonMeetingDislikeReasonDropDown.Enabled = true;
			};

			ButtonConfirmMeetingSide.TouchUpInside+= (object sender, EventArgs e) => {
				ButtonLikeMeeting.SetImage(UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeMeeting.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonConfirmMeetingSide.SetImage(UIImage.FromBundle ("Thumbs Side White.png"), UIControlState.Normal);
				localConfirmMeeting = "SIDE";
				localReasonForDown = "";
				ViewF2FMeetingDown.Hidden = true;
				ViewSecond.Frame = new CGRect (0, 193, 414, 1134);
				TableViewInteractionDislikeReason.Hidden = true;

			};

			ButtonLikeMeetingInfoHelpful.TouchUpInside += (object sender, EventArgs e) => {
				ButtonLikeMeetingInfoHelpful.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Normal);
				ButtonDisLikeMeetingInfoHelpful.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonMeetingInfoSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				localMeetingInfoHelpFull = "UP";

			};

			ButtonDisLikeMeetingInfoHelpful.TouchUpInside += (object sender, EventArgs e) => {
				ButtonLikeMeetingInfoHelpful.SetImage (UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeMeetingInfoHelpful.SetImage (UIImage.FromBundle ("Thumbs Down White.png"), UIControlState.Normal);
				ButtonMeetingInfoSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				localMeetingInfoHelpFull = "DOWN";


			};
			ButtonMeetingInfoSide.TouchUpInside+= (object sender, EventArgs e) => {
				ButtonLikeMeetingInfoHelpful.SetImage (UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeMeetingInfoHelpful.SetImage (UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonMeetingInfoSide.SetImage(UIImage.FromBundle ("Thumbs Side White.png"), UIControlState.Normal);
				localMeetingInfoHelpFull = "SIDE";
			};

//			ButtonLikeLeadAdvanced.TouchUpInside += (object sender, EventArgs e) => {
//				ButtonDisLikeLeadAdvanced.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
//				ButtonLikeLeadAdvanced.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Normal);
//				localLeadAdvanced = "UP";
//			};

			ButtonLikeLeadAdvanced.TouchUpInside += (object sender, EventArgs e) => {
				ButtonLikeLeadAdvanced.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Normal);
				ButtonDisLikeLeadAdvanced.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonLeadAdvancedSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				localLeadAdvanced = "UP";
				if(leadf2ffeedbackLast != null && leadf2ffeedbackLast.LeadAdvanced == "UP")
				{
					UIAlertView alert = new UIAlertView () { 
					Title = "", 
					Message = "Looks like things are going well. Would you like to update your sales stage?"
					};
					alert.AddButton ("OK");
					alert.Show ();
				}
			};

			ButtonDisLikeLeadAdvanced.TouchUpInside += (object sender, EventArgs e) => {
				ButtonLikeLeadAdvanced.SetImage (UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeLeadAdvanced.SetImage (UIImage.FromBundle ("Thumbs Down White.png"), UIControlState.Normal);
				ButtonLeadAdvancedSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				localLeadAdvanced = "DOWN";
			};
			ButtonLeadAdvancedSide.TouchUpInside+= (object sender, EventArgs e) => {
				ButtonLikeLeadAdvanced.SetImage (UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeLeadAdvanced.SetImage (UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonLeadAdvancedSide.SetImage(UIImage.FromBundle ("Thumbs Side White.png"), UIControlState.Normal);
				localLeadAdvanced = "SIDE";
			};

			TableViewInteractionDislikeReason.Hidden = true;
			TableViewInteractionDislikeReason.Source = new TableSource (ListThumbsDownReason, this,"ReasonForPass");
			ButtonMeetingDislikeReasonDropDown.TouchUpInside += (object sender, EventArgs e) => {
				localReasonForDown = "Customer Cancelled";

//				ViewFirst.Frame = 
				TableViewInteractionDislikeReason.Hidden = false;
			};

			TableViewSalesStage.Source = new TableSource (ListSalesStages, this,"SalesStage");
			localSalesStage = "(1) Acquire Lead";
			ButtonSaleStageDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewSalesStage.Hidden = false;
			};

			TableViewCustomerCategorization.Source = new TableSource (ListCustomerCategorisation, this, "CustomerCategorisation");
			localCustomerCategorization = "Dreamer";
			ButtonCustomerCategorizationDropDown.TouchUpInside += (object sender, EventArgs e) =>  {
				localCustomerCategorization = "Dreamer";
				TableViewCustomerCategorization.Hidden = false;
			};

			TableViewNextSteps.Source = new TableSource (ListNextStep, this, "NextStep");
			localNextSteps = "Get Product Info";
			ButtonNextStepsDropDown.TouchUpInside += (object sender, EventArgs e) =>  {
				localNextSteps = "Get Product Info";
				TableViewNextSteps.Hidden = false;
			};

			ButtonSubmit.TouchUpInside += (object sender, EventArgs e) => {
				LeadF2FFeedBack leadf2ffeedback = new LeadF2FFeedBack ();
				leadf2ffeedback.LeadID = meetingObj.LeadId;
				leadf2ffeedback.UserID = AppDelegate.UserDetails.UserId;
				leadf2ffeedback.ReasonForDown = localReasonForDown;
				leadf2ffeedback.ConfirmMeeting = localConfirmMeeting;
				leadf2ffeedback.CustomerCategorization = localCustomerCategorization;
				leadf2ffeedback.MeetingInfoHelpFull = localMeetingInfoHelpFull;
				leadf2ffeedback.LeadAdvanced = localLeadAdvanced;
				leadf2ffeedback.NextSteps = localNextSteps;
				leadf2ffeedback.SalesStage = localSalesStage;
				leadf2ffeedback.MeetingID = meetingObj.Id;
				AppDelegate.leadsBL.SaveLeadF2FFeedBack (leadf2ffeedback);
				//Xamarin Insights tracking
				Insights.Track("Save LeadF2F FeedBack", new Dictionary <string,string>{
					{"UserId", leadf2ffeedback.UserID.ToString()},
					{"LeadID", leadf2ffeedback.LeadID.ToString()},
					{"MeetingID", leadf2ffeedback.MeetingID.ToString()}
				});

				UserMeetings usermeeting = new UserMeetings();
				usermeeting.Id = meetingObj.Id;
				usermeeting.Status="Done";
				AppDelegate.userBL.UpdateMeetingList(usermeeting);
				//Xamarin Insights tracking
				Insights.Track("Update MeetingList", new Dictionary <string,string>{
					{"Id", usermeeting.Id.ToString()},
					{"Status", usermeeting.Status}
				});

				if(string.IsNullOrEmpty(AppDelegate.accessToken))
				{
					AppDelegate.accessToken = AppDelegate.leadsBL.SFDCAuthentication(AppDelegate.UserDetails.UserId);
				}
				string[] salesStageArray = localSalesStage.Split(' ');
				string salesStatus = salesStageArray.Length == 3 ? salesStageArray[1] + " " + salesStageArray[2] : salesStageArray[1];
					AppDelegate.leadsBL.UpdateSFDCData(AppDelegate.accessToken,meetingObj.LeadId,salesStatus);

				if(localSalesStage == "(4) Close Sale")
				{
					DealHistroy dealHistory = new DealHistroy();
					dealHistory.UserId = AppDelegate.UserDetails.UserId;
					dealHistory.State = meetingObj.State;
				    dealHistory.City = meetingObj.City;
				    dealHistory.Date = meetingObj.EndDate;
					dealHistory.country = "USA";
					dealHistory.CustomerName = meetingObj.CustomerName;
					dealHistory.LeadId = meetingObj.LeadId;
					AppDelegate.customerBL.SaveDealHistory(dealHistory);
					//Xamarin Insights tracking
					Insights.Track("Save DealHistory", new Dictionary <string,string>{
						{"UserId", dealHistory.UserId.ToString()},
						{"CustomerName", dealHistory.CustomerName},
						{"LeadId",dealHistory.LeadId.ToString()}
					});
				}
				AppDelegate.IsUpdateLeadDone = true;
				DismissViewController(true,null);
			};
		}

		public class TableSource : UITableViewSource {

			IList<string> TableItems;
			string CellIdentifier = "TableCell";
			public string TSTableType = string.Empty;
			LeadUpdateVC leadUpdateVC;

			public TableSource (IList<string> items, LeadUpdateVC leadUpdateVC, string tableType)
			{
				this.leadUpdateVC = leadUpdateVC;
				TableItems = items;
				TSTableType = tableType;
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

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);
				leadUpdateVC.UpdateControls(TableItems[indexPath.Row],TSTableType);
			}
		}
	}
}

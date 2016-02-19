// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using donow.PCL;
using CoreGraphics;
using donow.PCL.Model;
using Xamarin;

namespace donow.iOS
{
	public partial class InteractionLeadUpdateVC : UIViewController
	{
		public InteractionLeadUpdateVC (IntPtr handle) : base (handle)
		{
		}

		public InteractionLeadUpdateVC ()
		{
		}

		//public UserMeetings userMeetings;
		public Leads leadObj;
		string Interaction = string.Empty;
		string CustomerAcknowledge = string.Empty;
		string salesStage = string.Empty;
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			//this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			//this.NavigationController.SetNavigationBarHidden (false, false);
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ScrollViewInteractionPage.ContentSize = new CGSize (375.0f,761.0f);

			ViewInteractionThumbsDown.Hidden = true;
			IList<string> InteractionDislikerReason = new List<string>
			{
				"Wasn't Prepared",
				"Did Not Have Enough Info",
				"Customer Not Interested"
			};

			IList<string> ListSalesStages = new List<string>
			{
				"(1) Acquire Lead",
				"(2) Proposal",
				"(3) Follow Up",
				"(4) Close Sale"
			};



			cancelButton.TouchUpInside += (object sender, EventArgs e) => {

				DismissViewController(true,null);

			};

			LabelInteractionTitle.Text = "Your Interaction With " + leadObj.LEAD_NAME;
			ButtonSubmit.Layer.CornerRadius = 5.0f;
			ButtonLikeCustomerAcknowledge.TouchUpInside += (object sender, EventArgs e) => 
			{
				CustomerAcknowledge = "UP";
				ButtonLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Normal);
				ButtonDisLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonAcknowledgementSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
			};

			TextViewComments.Layer.BorderWidth = 2.0f;
			TextViewComments.Layer.BorderColor = UIColor.DarkGray.CGColor;

			ButtonDisLikeCustomerAcknowledge.TouchUpInside += (object sender, EventArgs e) => 
			{
				CustomerAcknowledge = "DOWN";
				ButtonLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Down White.png"), UIControlState.Normal);
				ButtonAcknowledgementSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
			};
			ButtonAcknowledgementSide.TouchUpInside+= (object sender, EventArgs e) => 
			{
				CustomerAcknowledge = "SIDE";
				ButtonLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDisLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonAcknowledgementSide.SetImage(UIImage.FromBundle ("Thumbs Side White.png"), UIControlState.Normal);
			};
			TableViewInteractionDislikerReason.Source = new TableSource (InteractionDislikerReason, this,"Interaction");

			TableViewInteractionDislikerReason.Hidden = true;

			ButtonInteractionDislikeReasonDropDown.TouchUpInside += (object sender, EventArgs e) => {
				//ButtonInteractionDislikeReasonDropDown.CurrentTitle = "Wasn't Prepared";
				TableViewInteractionDislikerReason.Hidden = false;
			};

			ButtonDislikeInteraction.TouchUpInside += (object sender, EventArgs e) => {
				ViewInteractionThumbsDown.Hidden = false;
				Interaction = "DOWN";
				ButtonLikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonDislikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Down White.png"), UIControlState.Normal);
				ButtonInteractionSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				ViewSecond.Frame = new CGRect (0, 312, this.View.Bounds.Size.Width, ViewSecond.Frame.Height);
				ScrollViewInteractionPage.ContentSize = new CGSize (375.0f,910.0f);
			};
			ButtonLikeInteraction.TouchUpInside += (object sender, EventArgs e) => {
				ViewInteractionThumbsDown.Hidden = true;
				Interaction = "UP";
				ButtonLikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Normal);
				ButtonDislikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonInteractionSide.SetImage(UIImage.FromBundle ("Grey Neutral.png"), UIControlState.Normal);
				ViewSecond.Frame = new CGRect (0, 163, this.View.Bounds.Size.Width, 875);
				ScrollViewInteractionPage.ContentSize = new CGSize (375.0f,845.0f);
				TableViewInteractionDislikerReason.Hidden = true;
			};
			ButtonInteractionSide.TouchUpInside+= (object sender, EventArgs e) => 
			{
				Interaction = "SIDE";
				ButtonDislikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Down Grey.png"), UIControlState.Normal);
				ButtonLikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Up Grey.png"), UIControlState.Normal);
				ButtonInteractionSide.SetImage(UIImage.FromBundle ("Thumbs Side White.png"), UIControlState.Normal);
				ViewSecond.Frame = new CGRect (0, 163, this.View.Bounds.Size.Width, 875);
				ScrollViewInteractionPage.ContentSize = new CGSize (375.0f,845.0f);
				TableViewInteractionDislikerReason.Hidden = true;
			};

			ButtonSubmit.TouchUpInside += (object sender, EventArgs e) => {
				LeadIntialContactFeedBack leadfeedback = new LeadIntialContactFeedBack();
				leadfeedback.LeadID = leadObj.LEAD_ID;
				leadfeedback.UserID = AppDelegate.UserDetails.UserId;
				leadfeedback.ReasonForDown = Interaction == "DOWN" ? ButtonInteractionDislikeReasonDropDown.CurrentTitle : "";
				leadfeedback.InteractionFeedBack = Interaction;
				leadfeedback.CustomerAcknowledged = CustomerAcknowledge;
				leadfeedback.Comments = TextViewComments.Text == "Enter comments here..." ? "" : TextViewComments.Text ;
				leadfeedback.MeetingID = AppDelegate.UserDetails.UserId;
				leadfeedback.SalesStage = salesStage;
				AppDelegate.leadsBL.SaveLeadFeedBack(leadfeedback);


				if(salesStage == "(4) Close Sale")
				{
					DealHistroy dealHistory = new DealHistroy();
					dealHistory.UserId = AppDelegate.UserDetails.UserId;
					dealHistory.State = leadObj.STATE;
					dealHistory.City = leadObj.CITY;
					dealHistory.Date = DateTime.Now.ToString();
					//dealHistory.country = "USA";
					dealHistory.CustomerName = leadObj.LEAD_NAME;
					dealHistory.LeadId = leadObj.LEAD_ID;
					dealHistory.BrokerID = 0; 
					dealHistory.LeadIndustry = AppDelegate.UserDetails.Industry;
					AppDelegate.customerBL.SaveDealHistory(dealHistory);
					//Xamarin Insights tracking
					Insights.Track("Save DealHistory", new Dictionary <string,string>{
						{"UserId", dealHistory.UserId.ToString()},
						{"CustomerName", dealHistory.CustomerName},
						{"LeadId",dealHistory.LeadId.ToString()}
					});
				}

				if(string.IsNullOrEmpty(AppDelegate.accessToken))
				{
					AppDelegate.accessToken = AppDelegate.leadsBL.SFDCAuthentication(AppDelegate.UserDetails.UserId);
				}
				//string[] salesStageArray = salesStage.Split(' ');
				//string salesStatus = salesStageArray.Length == 3 ? salesStageArray[1] + " " + salesStageArray[2] : salesStageArray[1];
				AppDelegate.leadsBL.UpdateSFDCData(AppDelegate.accessToken,leadObj.LEAD_ID,salesStage);

				DismissViewController(true,null);
				//Xamarin Insights tracking
				Insights.Track("Save Lead FeedBack", new Dictionary <string,string>{
					{"LeadID", leadfeedback.LeadID.ToString()},
					{"UserID", leadfeedback.UserID.ToString()}
				});
			};

			TableViewSalesStage.Source = new TableSource (ListSalesStages, this,"SalesStage");
			TableViewSalesStage.Hidden = true;
			salesStage = "(1) Acquire Lead";
			ButtonSalesStageDropDown.TouchUpInside += (object sender, EventArgs e) => {
				salesStage = "(1) Acquire Lead";
				TableViewSalesStage.Hidden = false;
			};
		}

		void updateCell (string parameter, string tabletype) {
			if (tabletype == "Interaction") {
				ButtonInteractionDislikeReasonDropDown.SetTitle (parameter, UIControlState.Normal);
				TableViewInteractionDislikerReason.Hidden = true;
			} else {
				TableViewSalesStage.Hidden = true;
				salesStage = parameter;
				ButtonSalesStageDropDown.SetTitle ("  " + parameter, UIControlState.Normal);
			}
		}

		public class TableSource : UITableViewSource {

			IList<string> TableItems;
			string CellIdentifier = "TableCell";
			string tableType = string.Empty;

			InteractionLeadUpdateVC owner;

			public TableSource (IList<string> items, InteractionLeadUpdateVC interactionLeadUpdateVC, string tabletype)
			{
				this.owner = interactionLeadUpdateVC;
				TableItems = items;
				tableType = tabletype;
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
				tableView.DeselectRow (indexPath, true);
				owner.updateCell (TableItems[indexPath.Row],tableType);
			}

		}

		}

	}



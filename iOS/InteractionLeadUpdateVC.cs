// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using donow.PCL;

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

		public UserMeetings userMeetings;
		string Interaction = string.Empty;
		string CustomerAcknowledge = string.Empty;
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			//this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			//this.NavigationController.SetNavigationBarHidden (false, false);
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			ViewInteractionThumbsDown.Hidden = true;
			IList<string> InteractionDislikerReason = new List<string>
			{
				"Wasn't Prepared",
				"Did Not Have Enough Info",
				"Customer Not Interested"
			};


			ButtonLikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Selected);
			ButtonLikeInteraction.TouchUpInside += (object sender, EventArgs e) => 
			ButtonDislikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Down White.png"), UIControlState.Selected);
			ButtonDislikeInteraction.TouchUpInside += (object sender, EventArgs e) => 
			ButtonLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Selected);
			ButtonLikeCustomerAcknowledge.TouchUpInside += (object sender, EventArgs e) => 
			{
				CustomerAcknowledge = "UP";
			};

			ButtonDisLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Down White.png"), UIControlState.Selected);
			ButtonLikeCustomerAcknowledge.TouchUpInside += (object sender, EventArgs e) => 
			{
				CustomerAcknowledge = "DOWN";
			};
			ButtonAcknowledgementSide.TouchUpInside+= (object sender, EventArgs e) => 
			{
				CustomerAcknowledge = "SIDE";
			};
			TableViewInteractionDislikerReason.Source = new TableSource (InteractionDislikerReason, this);

			TableViewInteractionDislikerReason.Hidden = true;

			ButtonInteractionDislikeReasonDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewInteractionDislikerReason.Hidden = false;
			};

			ButtonDislikeInteraction.TouchUpInside += (object sender, EventArgs e) => {
				ViewInteractionThumbsDown.Hidden = false;
				Interaction = "DOWN";
			};
			ButtonLikeInteraction.TouchUpInside += (object sender, EventArgs e) => {
				ViewInteractionThumbsDown.Hidden = true;
				Interaction = "UP";
			};
			ButtonInteractionSide.TouchUpInside+= (object sender, EventArgs e) => 
			{
				Interaction = "SIDE";
			};

			ButtonSubmit.TouchUpInside += (object sender, EventArgs e) => {
				LeadIntialContactFeedBack leadfeedback = new LeadIntialContactFeedBack();
				leadfeedback.LeadID = 165806841;
				leadfeedback.UserID = AppDelegate.UserDetails.UserId;
				leadfeedback.ReasonForDown = ButtonInteractionDislikeReasonDropDown.CurrentTitle;
				leadfeedback.InteractionFeedBack = Interaction;
				leadfeedback.CustomerAcknowledged = CustomerAcknowledge;
				leadfeedback.Comments = TextViewComments.Text;
				AppDelegate.leadsBL.SaveLeadFeedBack(leadfeedback);
				NavigationController.PopViewController(true);
			};

		}

		public class TableSource : UITableViewSource {

			IList<string> TableItems;
			string CellIdentifier = "TableCell";

			InteractionLeadUpdateVC interactionLeadUpdateVC;

			public TableSource (IList<string> items, InteractionLeadUpdateVC interactionLeadUpdateVC)
			{
				this.interactionLeadUpdateVC = interactionLeadUpdateVC;
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
				
			}

		}

		}

	}



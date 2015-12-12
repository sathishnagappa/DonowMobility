// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using System.Collections.Generic;

namespace donow.iOS
{
	public partial class InteractionLeadUpdateVC : UIViewController
	{
		public InteractionLeadUpdateVC (IntPtr handle) : base (handle)
		{
		}


		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ParentViewController.NavigationController.SetNavigationBarHidden (true, false);
			this.NavigationController.SetNavigationBarHidden (false, false);
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			ViewInteractionThumbsDown.Hidden = true;
			IList<string> InteractionDislikerReason = new List<string>
			{
				"Wasn't Prepared",
				"Did Not Have Enough Info"
			};


			ButtonLikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Selected);
			ButtonDislikeInteraction.SetImage(UIImage.FromBundle ("Thumbs Down White.png.png"), UIControlState.Selected);
			ButtonLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Up White.png"), UIControlState.Selected);
			ButtonDisLikeCustomerAcknowledge.SetImage(UIImage.FromBundle ("Thumbs Down White.png.png"), UIControlState.Selected);
			TableViewInteractionDislikerReason.Source = new TableSource (InteractionDislikerReason, this);

			TableViewInteractionDislikerReason.Hidden = true;

			ButtonInteractionDislikeReasonDropDown.TouchUpInside += (object sender, EventArgs e) => {
				TableViewInteractionDislikerReason.Hidden = false;
			};

			ButtonDislikeInteraction.TouchUpInside += (object sender, EventArgs e) => {
				ViewInteractionThumbsDown.Hidden = false;
			};

			ButtonLikeInteraction.TouchUpInside += (object sender, EventArgs e) => {
				ViewInteractionThumbsDown.Hidden = true;
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



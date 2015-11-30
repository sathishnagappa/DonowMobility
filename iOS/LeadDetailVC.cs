using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL.Model;
using System.Collections.Generic;
using MessageUI;

namespace donow.iOS
{
	public partial class LeadDetailVC : UIViewController
	{
		public Leads leadObj;
		bool isLeadAccepted = false;
		public LeadDetailVC (IntPtr handle) : base (handle)
		{
		}

		IList<string> OptionsPassView = new List<string>
		{
			"Too Busy", "Score Lead too Low", "Not Right Fit"
		};

		public  override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			LabelTitleName.Text = leadObj.Name;
			LabelScore.Text = leadObj.Source;
			LabelSourceName.Text = leadObj.Source;
			LabelCompanyInfo.Text = leadObj.CompanyInfo;
			LabelTitleCompany.Text = leadObj.Company;
			LabelLocation.Text = leadObj.City + "," + leadObj.State;
			LabelBusinessNeeds.Text = leadObj.BusinessNeeds;

			TableViewPassView.Hidden = true;
			ViewAccept.Hidden = true;
			ViewPass.Hidden = true;

			ButtonOptionPassView.TouchUpInside += (object sender, EventArgs e) =>  {
				TableViewPassView.Hidden = false;
			};

			TableViewPassView.Source = new PassViewTableSource (OptionsPassView, this);

			ButtonAccept.TouchUpInside += (object sender, EventArgs e) => {
				ImageViewTransparentBackground.Hidden = false;
				ViewAccept.Hidden = false;
				isLeadAccepted = true;
			};

			ButtonPass.TouchUpInside += (object sender, EventArgs e) => {
				ImageViewTransparentBackground.Hidden = false;
				ViewPass.Hidden = false;
				isLeadAccepted = false;
			};

			ButtonPhoneAcceptView.TouchUpInside += (object sender, EventArgs e) => {

				var url = new NSUrl ("tel:" );//+ leadObj .Text);
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					av.Show ();
				};
			};

			ButtonEmailAcceptView.TouchUpInside += (object sender, EventArgs e) =>  {
				MFMailComposeViewController mailController;
				if (MFMailComposeViewController.CanSendMail) {
					// do mail operations here
					mailController = new MFMailComposeViewController ();
					mailController.SetToRecipients (new string[]{"john@doe.com"});
					mailController.SetSubject ("mail test");
					mailController.SetMessageBody ("this is a test", false);

					mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
						Console.WriteLine (args.Result.ToString ());
						args.Controller.DismissViewController (true, null);
					};

					this.PresentViewController (mailController, true, null);


//					Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
				}
			};
		}

		public void UpdateControls (string Parameter)
		{
			ButtonOptionPassView.SetTitle (Parameter, UIControlState.Normal);
			TableViewPassView.Hidden = true;
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				//code here to handle touch
				if (this.ImageViewTransparentBackground.Frame.Contains (touch.LocationInView (this.View)))
				{
					// the touch event happened inside the UIView imgTouchMe.
					ImageViewTransparentBackground.Hidden = true;
				}
				if (isLeadAccepted) {
					ViewAccept.Hidden = true;
					dummyViewController dummyVC = this.Storyboard.InstantiateViewController ("dummyViewController") as dummyViewController;
					if (dummyVC != null) {
						this.PresentViewController (dummyVC, true);
					}
				} else {
					ViewPass.Hidden = true;
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
			return 35.0f;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			leadDetailsVC.UpdateControls(TableItems[indexPath.Row]);
		}
	}
}

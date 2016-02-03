// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using donow.PCL;
using CoreGraphics;

namespace donow.iOS
{
	public partial class DealMakerAcceptedReferralRequestlVC : UIViewController
	{
		public Broker brokerObj;
		public DealMakerAcceptedReferralRequestlVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Deal Maker";

			UIBarButtonItem btn = new UIBarButtonItem ();
			btn.Image = UIImage.FromFile("Navigation Back Icon.png");
			btn.Clicked += (sender , e)=>{
				//				MyDealMakerVC dealMaker = this.Storyboard.InstantiateViewController ("MyDealMakerVC") as MyDealMakerVC;
				//				this.NavigationController.PushViewController(dealMaker,true);
				this.NavigationController.PopViewController(true);
			};
			NavigationItem.LeftBarButtonItem = btn;

			LabelBrokerName.Text = brokerObj.BrokerName;
//			LabelDesignation.Text = brokerObj.BrokerDesignation;
			LabelCompanyName.Text = brokerObj.Company;
			LabelIndustryType.Text = "Industry: " + brokerObj.Industry;
			LabelCityAndState.Text = brokerObj.City + ", " + brokerObj.State;
			LabelScore.Text = brokerObj.BrokerScore;
			LabelReferralMade.Text = "0";
			LabelBrokerInfo.Text = brokerObj.Company + "\n" + brokerObj.Industry;
			LabelConnectionToLead.Text = brokerObj.ConnectionLead;
			LabelDomainExpertise.Text = brokerObj.DomainExpertise;

			ScrollViewDealMaker.ContentSize = new CGSize (375, 760);
		}
	}
}

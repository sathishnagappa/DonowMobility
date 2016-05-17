using System;

namespace donow.PCL
{
	public class Dashboard : BaseClass
	{
		public int total_customers {get;set;}
		/*public int crm_total_leads {get;set;}
		public int crm_leads_with_dealmakers  {get;set;}
		public int crm_leads_without_dealmakers  {get;set;}
		public int dn_total_leads  {get;set;}
		public int dn_total_leads_accepted {get;set;}
		public int dn_leads_with_dealmakers  {get;set;}
		public int dn_leads_without_dealmakers {get;set;} */
		public string  total_earning {get;set;}
		public int total_lead_requests {get;set;}
		public int total_accepted {get;set;}
		public int total_referred {get;set;}
		public string  next_meeting  {get;set;}
		public string title {get;set;}
		public string CustomerName {get;set;}
		public string City {get;set;}
		public string State {get;set;}
		public int MeetingID {get;set;}
		public string Comments {get;set;}
		public int CRMClosedWon { get; set; }
		public int CRMProposal { get; set; }
		public int CRMConnectionMade { get; set; }
		public int CRMWorking { get; set; }
		public int CRMNew { get; set; }
		public int DoNowClosedWon { get; set; }
		public int DoNowProposal { get; set; }
		public int DoNowConnectionMade { get; set; }
		public int DoNowWorking { get; set; }
		public int DoNowNew { get; set; }
	}
}


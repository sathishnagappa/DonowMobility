using System;
using System.Collections.Generic;

namespace donow.PCL
{
	public class CustomerDetails : BaseClass
	{ 
		public int LeadId { get; set; }
		public string Name { get; set; }
		public string Company { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string CompanyInfo { get; set; }
		public string BusinessNeeds { get; set; }
		public List<CustomerInteraction> customerInteractionList { get; set; }
		public List<UserMeetings> UserMeetingList { get; set; }
		public List<DealHistroy> dealHistoryList { get; set; }
		public Broker dealMaker { get; set; }
		public int LeadSource  { get; set; }
		public int LeadScore { get; set; }
		public string LeadTitle { get; set; }
		public string ADDRESS { get; set; }
		public string ZIPCODE { get; set; }
		public string COUNTRY { get; set; }
		public string FISCALYE { get; set; }
		public string REVENUE { get; set; }
		public string NETINCOME { get; set; }
		public string EMPLOYEES { get; set; }
		public string MARKETVALUE { get; set; }
		public string YEARFOUNDED { get; set; }
		public string INDUSTRYRISK { get; set; }
		public string COUNTY { get; set; }
		public string WebAddress { get; set; }
		public string SFDCLEAD_ID  { get; set; }
	}
}


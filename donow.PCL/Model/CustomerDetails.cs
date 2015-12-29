using System;
using System.Collections.Generic;

namespace donow.PCL
{
	public class CustomerDetails
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
		public Broker broker { get; set; }
	}
}


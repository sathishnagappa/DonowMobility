using System;

namespace donow.PCL
{
	public class DealHistroy
	{
		public int LeadId { get; set; }
		public int UserId { get; set; }
		public string Date { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string CustomerName { get; set; }
		//public object country { get; set; }
		public int BrokerID {get; set;} 
		public string Lead_Industry {get; set;} 
	}
}


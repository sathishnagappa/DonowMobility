﻿using System;

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
		public string LeadIndustry {get; set;} 
		public string BrokerName {get; set;} 
		public bool ApiResponse { get; set; }
		public string ErrorMessage { get; set; }
	}
}


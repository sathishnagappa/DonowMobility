using System;

namespace donow.PCL
{
	public class UserMeetings
	{
		public int id { get; set;}
		public int UserID { get; set;}
		public int LeadID { get; set;}
		public string Subject { get; set;}
		public DateTime Start { get; set;}
		public DateTime End { get; set;}
		public string CustomerName { get; set;}
		public string City { get; set;}
		public string State { get; set;}
		
	}
}


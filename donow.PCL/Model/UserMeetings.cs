using System;

namespace donow.PCL
{
	public class UserMeetings : BaseClass
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int LeadId { get; set; }
		public string Subject { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public string CustomerName { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Status { get; set; }
		public string Comments { get; set; }
		public string SFDCLead_ID { get; set; }
	}
}


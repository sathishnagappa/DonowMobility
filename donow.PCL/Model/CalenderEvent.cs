using System;

namespace donow.PCL.Model
{
	public class CalenderEvent
	{
		public int ID { get; set; }
		public string UserID { get; set; }
		public string LeadID { get; set; }
		public string Subject { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public bool ApiResponse { get; set; }
		public string ErrorMessage { get; set; }
	}
}


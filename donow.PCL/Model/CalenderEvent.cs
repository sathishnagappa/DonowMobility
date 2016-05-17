using System;

namespace donow.PCL.Model
{
	public class CalenderEvent : BaseClass
	{
		public int ID { get; set; }
		public string UserID { get; set; }
		public string LeadID { get; set; }
		public string Subject { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
	}
}


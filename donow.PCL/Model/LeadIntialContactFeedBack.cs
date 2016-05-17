using System;

namespace donow.PCL
{
	public class LeadIntialContactFeedBack : BaseClass
	{
		public int ID { get; set;}
		public long LeadID { get; set;}
		public int UserID { get; set;}
		public string InteractionFeedBack { get; set;}
		public string ReasonForDown { get; set;}
		public string CustomerAcknowledged { get; set;}	
		public string Comments { get; set;}
		public int MeetingID { get; set;}
		public string SalesStage { get; set;}
	}
}


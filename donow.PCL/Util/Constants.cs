using System;

namespace donow.Util
{
	public class Constants
	{
		public static string ConnectionBase = "http://donowservice.cloudapp.net:85/api/";  //Test
//		public static string ConnectionBase = "https://donowapi.azurewebsites.net/api/"; //Prod

		public static string LeadsAPI = ConnectionBase + "LeadDetails";
		public static string UserCreation=ConnectionBase + "UserDetails";
		public static string UserUpdate=ConnectionBase + "UserUpdate";
		public static string CustomerDetails=ConnectionBase + "CustomerDetails";
		public static string MeetingList=ConnectionBase + "MeetingList";
		public static string MeetingUpdate=ConnectionBase + "MeetingUpdate";
		public static string CustomerInteraction=ConnectionBase + "CustomerInteraction";
		public static string ReferralRequests=ConnectionBase + "ReferralRequests";
		public static string LeadFeedback=ConnectionBase + "LeadFeedback";
		public static string LeadF2FFeedback=ConnectionBase + "LeadF2FFeedBack";
//		public static string CustomerFeed=ConnectionBase + "CustomerFeed";
		public static string DealHistory=ConnectionBase + "DealHistory";
		public static string LeadTakingPoints=ConnectionBase + "LeadTakingPoints";
		public static string DealMaker=ConnectionBase + "Dealmaker";
		public static string Industry=ConnectionBase + "Industry";
		public static string LOB=ConnectionBase + "LineOfBusiness";
		public static string ReferralRequestsUpdate=ConnectionBase + "ReferralRequestsUpdate";
		public static string SFDCCrendentails=ConnectionBase + "SFDC";
		public static string OpenConnection=ConnectionBase + "Connection";
		public static string Dashboard=ConnectionBase + "Dashboard";
	}
}
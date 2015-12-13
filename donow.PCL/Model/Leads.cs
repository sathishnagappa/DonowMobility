using System;

namespace donow.PCL.Model
{
		public class Leads
		{
//			public int Id { get; set; }
//			public int LeadScore { get; set; }
//			public bool IsNew { get; set; }
//			public int UserId { get; set; }
//			public string Name { get; set; }
//			public string Company { get; set; }
//			public string City { get; set; }
//			public string State { get; set; }
//			public string Source { get; set; }
//		    public string Address   { get; set; }
//			public string CompanyInfo { get; set; }
//			public string BusinessNeeds { get; set; }
//			public string Status { get; set; }
//			public string ReasonForPass { get; set; }
//			public string SalesStage { get; set; }
//		    public string Phone   { get; set; }
//		    public string Email { get; set; }

		public int LEAD_ID { get; set; }
		public string LEAD_NAME { get; set; }
		public string COMPANY_NAME { get; set; }
		public string STATE { get; set; }
		public string CITY { get; set; }
		public int LEAD_SOURCE { get; set; }
		public string COMPANY_INFO { get; set; }
		public string BUSINESS_NEED { get; set; }
		public string LEAD_TYPE { get; set; }
		public string LEAD_CREATE_TIME { get; set; }
		public string LEAD_STATUS { get; set; } 
		public int LEAD_SCORE { get; set; }
		public string STATUS { get; set; }
		public string REASON_FOR_PASS { get; set; }
		public string PHONE { get; set; }
		public string EMAILID { get; set; }

		}	
}


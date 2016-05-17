using System;

namespace donow.PCL.Model
{
	public class Leads : BaseClass
		{

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
			public int USER_LEAD_STATUS { get; set; }
			public int USER_ID { get; set; }
			public string LEAD_TITLE { get; set; }
		    public string SFDCLEAD_ID  { get; set; }
		}	
}


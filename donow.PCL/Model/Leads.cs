using System;

namespace donow.PCL.Model
{
		public class Leads
		{
			public int Id { get; set; }
			public int LeadScore { get; set; }
			public bool IsNew { get; set; }
			public int UserId { get; set; }
			public string Name { get; set; }
			public string Company { get; set; }
			public string City { get; set; }
			public string State { get; set; }
			public string Source { get; set; }
			public string CompanyInfo { get; set; }
			public string BusinessNeeds { get; set; }
			public string Status { get; set; }
			public string ReasonForPass { get; set; }
			public string SalesStage { get; set; }
		}	
}


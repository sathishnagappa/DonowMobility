using System;

namespace donow.PCL
{
	public class Referrals
	{
		public int ID { get; set; }
		public int LeadId { get; set; }
		public int SenderId { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Industry { get; set; }
		public string BusiessNeeds { get; set; }
		public string UserName { get; set; }
		public bool ApiResponse { get; set; }
		public string ErrorMessage { get; set; }
	}
}


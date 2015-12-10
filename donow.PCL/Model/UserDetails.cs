using System;

namespace donow.PCL.Model
{
	public class UserDetails
	{

	    public int UserId { get; set; }
		public string Name { get; set; }
		public string FullName { get; set; }
		public string Password { get; set; }
		public string Title { get; set; }
		public string Company { get; set; }
		public string Industry { get; set; }
		public string LineOfBusiness { get; set; }
		public string OfficeAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string PreferredIndustry { get; set; }
		public string PreferredCompany { get; set; }
		public string PreferredCustomers { get; set; }
		public bool IsNewLeadNotificationRequired { get; set; }
		public bool IsReferralRequestRequired { get; set; }
		public bool IsCustomerFollowUpRequired { get; set; }
		public bool IsMeetingRemindersRequired { get; set; }
		public bool IsBusinessUpdatesRequired { get; set; }
		public string ImageUrl { get; set; }
	}
}


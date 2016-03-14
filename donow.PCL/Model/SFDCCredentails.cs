using System;

namespace donow.PCL
{
	public class SFDCCredentails
	{
		public int UserID { get; set;}
		public string Url { get; set;}
		public string UserName { get; set;}
		public string Password { get; set;}
		public string SecurityCode { get; set;}
		public string ClientID { get; set;}
		public string ClientSecret { get; set;}
		public bool ApiResponse { get; set; }
		public string ErrorMessage { get; set; }
	}
}


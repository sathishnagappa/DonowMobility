using System;
using System.Collections.Generic;

namespace donow.PCL.Model
{
	public class Profile
	{
		public string given_name { get; set; }
		public string family_name { get; set; }
		public string picture { get; set; }
		public string name { get; set; }
		public ApiStandardProfileRequest apiStandardProfileRequest { get; set; }
		public int distance { get; set; }
		public string headline { get; set; }
		public string industry { get; set; }
		public Location location { get; set; }
		public int numConnections { get; set; }
		public bool numConnectionsCapped { get; set; }
		public Positions positions { get; set; }
		public string publicProfileUrl { get; set; }
		public RelationToViewer relationToViewer { get; set; }
		public SiteStandardProfileRequest siteStandardProfileRequest { get; set; }
		public bool email_verified { get; set; }
		public string clientID { get; set; }
		public string updated_at { get; set; }
		public string user_id { get; set; }
		public string nickname { get; set; }
		public List<Identity> identities { get; set; }
		public string created_at { get; set; }
		public string email { get; set; }
	}


	public class Value
	{
		public string name { get; set; }
		public string value { get; set; }
	}

	public class Headers
	{
		public int _total { get; set; }
		public List<Value> values { get; set; }
	}

	public class ApiStandardProfileRequest
	{
		public Headers headers { get; set; }
		public string url { get; set; }
	}

	public class Country
	{
		public string code { get; set; }
	}

	public class Location
	{
		public Country country { get; set; }
		public string name { get; set; }
	}

	public class Company
	{
		public int id { get; set; }
		public string industry { get; set; }
		public string name { get; set; }
		public string size { get; set; }
		public string type { get; set; }
	}

	public class StartDate
	{
		public int month { get; set; }
		public int year { get; set; }
	}

	public class Value2
	{
		public Company company { get; set; }
		public int id { get; set; }
		public bool isCurrent { get; set; }
		public StartDate startDate { get; set; }
		public string summary { get; set; }
		public string title { get; set; }
	}

	public class Positions
	{
		public int _total { get; set; }
		public List<Value2> values { get; set; }
	}

	public class RelationToViewer
	{
		public int distance { get; set; }
	}

	public class SiteStandardProfileRequest
	{
		public string url { get; set; }
	}

	public class Identity
	{
		public string access_token { get; set; }
		public string provider { get; set; }
		public string user_id { get; set; }
		public string connection { get; set; }
		public bool isSocial { get; set; }
	}

}


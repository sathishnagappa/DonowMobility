using System;
using System.Collections.Generic;
using donow.Util;
using donow.Services;

namespace donow.PCL
{
	public class CustomerBL
	{
		public CustomerBL ()
		{
		}

		public List<Customer> GetAllCustomers()
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.CustomerDetails;
			string response = restSevice.GetData (restUrl);
			List<Customer> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(response.ToString());
			return parsedResponse;
		}
	}
}


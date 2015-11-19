using System;
using donow.Model;
using donow.Services;
using System.Json;
using System.Threading.Tasks;

namespace donow.PCL
{
	public class User
	{
		public User ()
		{
		}

		public UserDetails GetUserDetails()
		{
			RestService restSevice = new RestService ();
			string response =  restSevice.GetData ("https://mapi.move.com/forsale/v1/search/?loc=San%20Jose,%20CA%2095113&limit=20&sort=newest&offset=0&type=single_family,condo,mobile,multi_family,farm,land&include_newhomes=0&client_id=rdc_mobile_native,2.0.0929.0000,windowsStore&include_client_event_data=true&request_timestamp=1445340641");
			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return parsedResponse;
		}

		public string GetUserDetailsString()
		{
			RestService restSevice = new RestService ();
			string response =  restSevice.GetData ("198440");
			//string parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return response;
		}

	}
}


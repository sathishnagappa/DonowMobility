using System;
using donow.Services;
using System.Json;
using System.Threading.Tasks;
using donow.PCL.Model;
using donow.Util;

namespace donow.PCL
{
	public class UserBL
	{
		public UserBL()
		{
		}

//		public UserDetails GetUserDetails()
//		{
//			RestService restSevice = new RestService ();
//			string response =  restSevice.GetData ("https://mapi.move.com/forsale/v1/search/?loc=San%20Jose,%20CA%2095113&limit=20&sort=newest&offset=0&type=single_family,condo,mobile,multi_family,farm,land&include_newhomes=0&client_id=rdc_mobile_native,2.0.0929.0000,windowsStore&include_client_event_data=true&request_timestamp=1445340641");
//			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
//			return parsedResponse;
//		}

		public bool CreateUser(UserDetails userDetails)
		{
			RestService restSevice = new RestService ();
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);
			string response = restSevice.PostData (Constants.UserCreation, postData);
			bool parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(response.ToString());
			return parsedResponse;
		}


//		public UserDetails UpdateCredentails(string id,string UserName, string Password)
//		{
//			RestService restSevice = new RestService ();
//			string postData = "{ \"ID\" :" + id + ", \"UserName\" :" + UserName + ",\"Password\" :" + Password + "}"; 
//			string response = restSevice.PostData (Constants.UserCreation, postData);
//			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
//			return parsedResponse;
//		}
//
		public UserDetails UpdateUserDetails(UserDetails userDetails)
		{
			RestService restSevice = new RestService ();	
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);
			string response = restSevice.PostData (Constants.UserCreation, postData);
			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return parsedResponse;
		}
		
		public UserDetails GetUserDetails(string userName)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?name=" + userName;
			string response = restSevice.GetData (restUrl);
			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return parsedResponse;
		}

		public bool CheckUserExist(string userName)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?username=" + userName;
			string response = restSevice.GetData (restUrl);
			return Convert.ToBoolean (response);
		}


	}
}


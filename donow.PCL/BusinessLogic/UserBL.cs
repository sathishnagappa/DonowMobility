using System;
using donow.Services;
using System.Json;
using System.Threading.Tasks;
using donow.PCL.Model;
using donow.Util;
using System.Collections.Generic;

namespace donow.PCL
{
	public class UserBL
	{
		public UserBL()
		{
		}

//		public UserDetails GetUserDetails()
//		{
//			//RestService restSevice = new RestService ();
//			string response =  RestService.Instance.GetData ("https://mapi.move.com/forsale/v1/search/?loc=San%20Jose,%20CA%2095113&limit=20&sort=newest&offset=0&type=single_family,condo,mobile,multi_family,farm,land&include_newhomes=0&client_id=rdc_mobile_native,2.0.0929.0000,windowsStore&include_client_event_data=true&request_timestamp=1445340641");
//			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
//			return parsedResponse;
//		}

		public int CreateUser(UserDetails userDetails)
		{
			//RestService restSevice = new RestService ();
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);
			string response = RestService.Instance.PostData (Constants.UserCreation, postData);
			int parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}


//		public UserDetails UpdateCredentails(string id,string UserName, string Password)
//		{
//			//RestService restSevice = new RestService ();
//			string postData = "{ \"ID\" :" + id + ", \"UserName\" :" + UserName + ",\"Password\" :" + Password + "}"; 
//			string response = RestService.Instance.PostData (Constants.UserCreation, postData);
//			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
//			return parsedResponse;
//		}
//
		public int UpdateUserDetails(UserDetails userDetails)
		{
			//RestService restSevice = new RestService ();	
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);
			string response = RestService.Instance.PostData (Constants.UserUpdate, postData);
			int parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}
		
		public UserDetails GetUserDetails(string userName)
		{
			////RestService restSevice = new RestService ();
			UserDetails parsedResponse;
			string restUrl = Constants.UserCreation + "?name=" + userName;
			string response =  RestService.Instance.GetData (restUrl);
			if (response != "false") {
				parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails> (response.ToString ());
				parsedResponse.ApiResponse = true;
			} else {
				parsedResponse = new UserDetails ();
				parsedResponse.ApiResponse = false;
				parsedResponse.ErrorMessage = "Please check your netwok connection.";
			}
			return parsedResponse;

		}

		public bool CheckUserExist(string userName)
		{
			//RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?username=" + userName;
			string response = RestService.Instance.GetData (restUrl);
			return Convert.ToBoolean (response);
		}

		public UserDetails GetUserFromEmail(string EmailID)
		{
			//RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?EmailID=" + EmailID;
			string response = RestService.Instance.GetData (restUrl);
			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return parsedResponse;
		}

//		public List<UserMeetings> GetMeetings(string customername)
//		{
//			//RestService restSevice = new RestService ();
//			string restUrl = Constants.MeetingList + "?name=" + customername;
//			string response = RestService.Instance.GetData (restUrl);
//			List<UserMeetings> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserMeetings>> (response.ToString ());		
//			return parsedResponse;
//			
//
//		}

		public UserDetails GetUserByID(int userid)
		{
			//RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?id=" + userid;
			string response = RestService.Instance.GetData (restUrl);
			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return parsedResponse;
		}

		public List<UserMeetings> GetMeetingsByUserName(int userid)
		//public UserMeetings GetMeetingsByUserName(int userid)
		{
			//RestService restSevice = new RestService ();
			string restUrl = Constants.MeetingList + "?UserID=" + userid;
			string response = RestService.Instance.GetData (restUrl);
			List<UserMeetings> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserMeetings>>(response.ToString());
			//UserMeetings parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserMeetings>(response.ToString());
			return parsedResponse;
		}

		public int UpdateMeetingList(UserMeetings userMeetings)
		{			
			//RestService restSevice = new RestService ();	
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userMeetings);
			string response = RestService.Instance.PostData (Constants.MeetingUpdate, postData);
			int parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}

		public string OpenConnection()
		{
			//RestService restSevice = new RestService ();
			string response = RestService.Instance.GetData (Constants.OpenConnection);
			return response;
		}

		public Dashboard GetDashBoardDetails(int UserID)
		{
			//RestService restSevice = new RestService ();
			string restUrl = Constants.Dashboard + "?id=" + UserID;
			string response = RestService.Instance.GetData (restUrl);
			Dashboard parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Dashboard>(response.ToString());
			return parsedResponse;
		}


	}
}


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
//			RestService restSevice = new RestService ();
//			string response =  restSevice.GetData ("https://mapi.move.com/forsale/v1/search/?loc=San%20Jose,%20CA%2095113&limit=20&sort=newest&offset=0&type=single_family,condo,mobile,multi_family,farm,land&include_newhomes=0&client_id=rdc_mobile_native,2.0.0929.0000,windowsStore&include_client_event_data=true&request_timestamp=1445340641");
//			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
//			return parsedResponse;
//		}

		public int CreateUser(UserDetails userDetails)
		{
			RestService restSevice = new RestService ();
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);
			string response = restSevice.PostData (Constants.UserCreation, postData);
			int parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
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
		public int UpdateUserDetails(UserDetails userDetails)
		{
			RestService restSevice = new RestService ();	
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);
			string response = restSevice.PostData (Constants.UserUpdate, postData);
			int parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}
		
		public UserDetails GetUserDetails(string userName)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?name=" + userName;
			string response =  restSevice.GetData (restUrl);
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

		public UserDetails GetUserFromEmail(string EmailID)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?EmailID=" + EmailID;
			string response = restSevice.GetData (restUrl);
			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return parsedResponse;
		}

		public List<UserMeetings> GetMeetings(string customername)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.MeetingList + "?name=" + customername;
			string response = restSevice.GetData (restUrl);
			List<UserMeetings> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserMeetings>>(response.ToString());
			return parsedResponse;
		}

		public UserDetails GetUserByID(int userid)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.UserCreation + "?id=" + userid;
			string response = restSevice.GetData (restUrl);
			UserDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(response.ToString());
			return parsedResponse;
		}

		public List<UserMeetings> GetMeetingsByUserName(int userid)
		//public UserMeetings GetMeetingsByUserName(int userid)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.MeetingList + "?UserID=" + userid;
			string response = restSevice.GetData (restUrl);
			List<UserMeetings> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserMeetings>>(response.ToString());
			//UserMeetings parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserMeetings>(response.ToString());
			return parsedResponse;
		}

		public int UpdateMeetingList(UserMeetings userMeetings)
		{

		RestService restSevice = new RestService ();	
		string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userMeetings);
		string response = restSevice.PostData (Constants.MeetingUpdate, postData);
		int parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
		return parsedResponse;
			
		}

		//AppDelegate.UserDetails.UserName = "sathish";
		//AppDelegate.UserDetails.Password = Crypto.Encrypt("sathish");

		//				RestService rs = new RestService();
		//				string content = rs.SFDCAuthentication();
		//				await rs.UpdateSFDCData(content);




		//LeadsBL leadbl = new LeadsBL();
		//leadbl.UpdateStatus(125960876,"Accepted");
		//leadbl.UpdateReasonForPass(125960876,"Client Not interested");


		//				UserMeetings usermeeting = new UserMeetings();
		//				usermeeting.LeadId = 4;
		//				usermeeting.State="Done";
		//				UserBL userbl = new UserBL();
		//				userbl.UpdateMeetingList(usermeeting);


		//ReferralRequestBL rrbl = new ReferralRequestBL();
		//rrbl.GetReferralRequest();

		//				ReferralRequest rr = new ReferralRequest();
		//				rr.AcceptorId = 1;
		//				rr.LeadId = 1;
		//				rr.SenderId = 1;
		//				rr.Status = "Accepted";
		//				rrbl.SaveReferralRequest(rr);

		//				leadfeedback.LeadID = 1211;
		//				leadfeedback.QuestionNo = 1;
		//				leadfeedback.Options = 2;
		//				leadfeedback.AnswerType = 2;
		//				leadfeedback.Comments = "";
		//				leadbl.SaveLeadF2FFeedBack(leadfeedback);

		//				BrokerBL brokerbl = new BrokerBL();
		//				brokerbl.UpdateBrokerStatus(125960876,"Acceptance Pending");

		//				UserDetails userdetails = new UserDetails();
		//				userdetails.Password = "test";
		//				userdetails.UserId = 10;
		//				UserBL userbl = new UserBL();
		//				userbl.UpdateUserDetails(userdetails);

		//				CustomerBL customerbl = new CustomerBL();
		//				CustomerInteraction customerinteract = new CustomerInteraction();
		//				customerinteract.CustomerName = "Scott Anders";
		//				customerinteract.UserId = 12121;
		//				customerinteract.Type = "Email";
		//				customerinteract.DateNTime = DateTime.Now.ToString();
		//				customerbl.SaveCutomerInteraction(customerinteract);
	}
}


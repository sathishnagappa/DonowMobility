using System;
using donow.Services;
using donow.Util;
using System.Collections.Generic;
using donow.PCL.Model;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace donow.PCL
{
	public class LeadsBL
	{
		public LeadsBL ()
		{
		}

//		public async Task<List<Leads>> GetAllLeads(int UserID)
//		{
//			//RestService restSevice = new RestService ();
//			string leadsApicall = Constants.LeadsAPI + "?id=" + UserID + "&type=user";
//			var parsedResponse = new List<Leads>();
//			try
//			{
//				string response =  await RestService.Instance.GetDataForLogin (leadsApicall);
//			   parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Leads>>(response.ToString());
//			}
//			catch {
//				
//			}
//			return parsedResponse;
//		}

		public List<LeadMaster> GetAllLeads(int UserID)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI + "?id=" + UserID;
			var parsedResponse = new List<LeadMaster>();
			try
			{
				string response =  RestService.Instance.GetData (leadsApicall);
				parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeadMaster>>(response.ToString());
			}
			catch {

			}
			return parsedResponse;
		}

		public Leads GetLeadsDetails(int Leadid, int userID,int LeadSource)
		//public Leads GetLeadsDetails(int Leadid, int userID)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI + "?id=" + Leadid + "&userid=" + userID + "&LeadSource=" + LeadSource;
			//string leadsApicall = Constants.LeadsAPI + "?id=" + Leadid + "&userid=" + userID;
			var parsedResponse = new Leads();
			try
			{
				string response =  RestService.Instance.GetData (leadsApicall);
				parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Leads>(response.ToString());
			}
			catch {

			}
			return parsedResponse;
		}

		public List<LeadMaster> GetNewLeads(int UserID)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI + "?id=" + UserID + "&update=Y";
			var parsedResponse = new List<LeadMaster>();
			try
			{
				string response =  RestService.Instance.GetData (leadsApicall);
				parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeadMaster>>(response.ToString());
			}
			catch {

			}
			return parsedResponse;
		}

//
//		public Leads GetLeadDetails(int leadId)
//		{
//			//RestService restSevice = new RestService ();
//			string leadsApicall = Constants.LeadsAPI + "?leadid=" + leadId ;
//
//			string response =  RestService.Instance.GetData (leadsApicall);
//			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Leads>(response.ToString());
//			return parsedResponse;
//		}


		public string SaveMeetingEvent(UserMeetings userMeetings)
		{
			//RestService restSevice = new RestService ();
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userMeetings);
			string response = RestService.Instance.PostData (Constants.MeetingList, postData);
			string parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(response.ToString());
			return parsedResponse;
			
		}

		public List<UserMeetings> GetMeetingEvents(string customername)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.MeetingList + "?customername=" + customername ;
			string response =  RestService.Instance.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserMeetings>>(response.ToString());
			return parsedResponse;
		}

		public LeadIntialContactFeedBack GetLeadIntialFeedBack(int leadId)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadFeedback +  "?id=" + leadId;
			string response =  RestService.Instance.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LeadIntialContactFeedBack>(response.ToString());
			return parsedResponse;
		}

		public List<LeadF2FFeedBack> GetLeadF2FFeedBack(int leadId)
		//public LeadF2FFeedBack GetLeadF2FFeedBack(int leadId)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadF2FFeedback +  "?id=" + leadId;
			string response =  RestService.Instance.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeadF2FFeedBack>>(response.ToString());
			//var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LeadF2FFeedBack>(response.ToString());
			return parsedResponse;
		}

		public int SaveLeadFeedBack(LeadIntialContactFeedBack leadfeeback)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadFeedback;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leadfeeback);
			string response =  RestService.Instance.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}

		public int SaveLeadF2FFeedBack(LeadF2FFeedBack leadfeedback)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadF2FFeedback;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leadfeedback);
			string response =  RestService.Instance.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}

		public int UpdateStatus(int leadid,int status,int userID)
		{
			Leads leads = new Leads ();
			leads.LEAD_ID = leadid;
			leads.USER_LEAD_STATUS = status;
			leads.REASON_FOR_PASS = "";
			leads.USER_ID = userID;
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leads);
			string response =  RestService.Instance.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
			
		}

		public int UpdateReasonForPass(int leadid, string reasonForPass,int userID)
		{
			Leads leads = new Leads ();
			leads.LEAD_ID = leadid;
			leads.USER_LEAD_STATUS = 5;
			leads.REASON_FOR_PASS = reasonForPass;
			leads.USER_ID = userID;
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leads);
			string response =  RestService.Instance.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}

		public string SFDCAuthentication (string domainName)
		//public string SFDCAuthentication (int userID)
		{
			SFDCCredentails sfdcobj = GetSFDCCredentails (domainName);
			//SFDCCredentails sfdcobj = GetSFDCCredentails (userID);
//			SFDCCredentails sfdcobj = new SFDCCredentails ();
//			sfdcobj.UserID = 2;
//			sfdcobj.Url = "https://ap2.salesforce.com//services/oauth2/token";
//			sfdcobj.UserName = "DoNow_dev2@brillio.com";
//			sfdcobj.Password = "donow@dev2";
//			sfdcobj.SecurityCode = "k9n6IQFyBYvDeGM2g0nVmFHpg";
//			sfdcobj.ClientID = "3MVG9ZL0ppGP5UrC4rjQFkEhUnYTSNP_Tvanu8b30_TqkLH7cOg8UC9zHKCsX.mgW_hFVY2J0jRyO.Ev_VsH0";
//			sfdcobj.ClientSecret = "1975032834009986449";
//
			var request = HttpWebRequest.Create(sfdcobj.Url);
			request.ContentType = "application/x-www-form-urlencoded";
			request.Headers.Add ("X-PrettyPrint", "1");
			request.Method = "POST";

			var postData = "grant_type=password&username="+ sfdcobj.UserName + "&password="+ sfdcobj.Password  + sfdcobj.SecurityCode + "&client_id=" + sfdcobj.ClientID + "&client_secret=" + sfdcobj.ClientSecret + "";

			var data = Encoding.ASCII.GetBytes(postData);
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}
			var content = string.Empty;  
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{

				if (response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					content = reader.ReadToEnd();
					//return content;
				}
			}
			SFDCAuth sfdcAuthObj = Newtonsoft.Json.JsonConvert.DeserializeObject<SFDCAuth>(content.ToString());
			return sfdcAuthObj.access_token;

		}
		public SFDCCredentails GetSFDCCredentails(string domainName)
		//public SFDCCredentails GetSFDCCredentails(int userID)
		{
			//RestService restSevice = new RestService ();
			//string sfdcApicall = Constants.SFDCCrendentails +  "?Id=" + userID;
			string sfdcApicall = Constants.SFDCCrendentails +  "?DomainName=" + domainName;
			string response =  RestService.Instance.GetData (sfdcApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SFDCCredentails>(response.ToString());
			return parsedResponse;
		}

		public async Task<string> UpdateSFDCData (string accessCode,string leadid,string status)
		{

			HttpClient queryClient3 = new HttpClient ();
			string serviceURL3 = "https://ap2.salesforce.com/services/data/v35.0/sobjects/Lead/" + leadid + "?_HttpMethod=PATCH";

			string insertPacket = "{ \"Status\": \"" + status + "\" }";

			StringContent insertString = new StringContent(insertPacket,Encoding.UTF8,"application/json");
			HttpRequestMessage request3 = new HttpRequestMessage(HttpMethod.Post, serviceURL3);
			request3.Headers.Add("Authorization", "OAuth " + accessCode);
			request3.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request3.Headers.Add("X-Http-Method-Override", "PATCH");
			request3.Content = insertString;
			HttpResponseMessage response3 = await queryClient3.SendAsync(request3);
			string result = await response3.Content.ReadAsStringAsync();
			return result;

		}
		public Prospect GetProspectDetails(int Leadid, int userID, int LeadSource)
		//public Prospect GetProspectDetails(int Leadid, int userID)
		{
			//RestService restSevice = new RestService ();
			//string leadsApicall = Constants.LeadsAPI + "?LeadID=" + Leadid + "&UserID=" + userID + "&Type=Prospect";
			string leadsApicall = Constants.LeadsAPI + "?LeadID=" + Leadid + "&UserID=" + userID + "&Type=Prospect&LeadSource=" + LeadSource;
			var parsedResponse = new Prospect();
			try
			{
				string response =  RestService.Instance.GetData (leadsApicall);
				parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Prospect>(response.ToString());
			}
			catch {

			}
			return parsedResponse;
		}
	}
}


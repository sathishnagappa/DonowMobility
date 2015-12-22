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

		public List<Leads> GetAllLeads(int UserID)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI + "?id=" + UserID + "&type=user";
			var parsedResponse = new List<Leads>();
			try
			{
			   string response =  restSevice.GetData (leadsApicall);
			   parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Leads>>(response.ToString());
			}
			catch {
				
			}
			return parsedResponse;
		}


		public List<Leads> GetLeadsDetails(int Leadid)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI + "?id=" + Leadid + "&type=lead";
			var parsedResponse = new List<Leads>();
			try
			{
				string response =  restSevice.GetData (leadsApicall);
				parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Leads>>(response.ToString());
			}
			catch {

			}
			return parsedResponse;
		}


		public Leads GetLeadDetails(int leadId)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI + "?leadid=" + leadId ;

			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Leads>(response.ToString());
			return parsedResponse;
		}


		public string SaveMeetingEvent(UserMeetings userMeetings)
		{
			RestService restSevice = new RestService ();
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(userMeetings);
			string response = restSevice.PostData (Constants.MeetingList, postData);
			string parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(response.ToString());
			return parsedResponse;
			
		}

		public List<UserMeetings> GetMeetingEvents(string customername)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.MeetingList + "?customername=" + customername ;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserMeetings>>(response.ToString());
			return parsedResponse;
		}

		public LeadIntialContactFeedBack GetLeadIntialFeedBack(int leadId)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadFeedback +  "?id=" + leadId;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LeadIntialContactFeedBack>(response.ToString());
			return parsedResponse;
		}

		public List<LeadF2FFeedBack> GetLeadF2FFeedBack(int leadId)
		//public LeadF2FFeedBack GetLeadF2FFeedBack(int leadId)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadF2FFeedback +  "?id=" + leadId;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeadF2FFeedBack>>(response.ToString());
			//var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LeadF2FFeedBack>(response.ToString());
			return parsedResponse;
		}

		public int SaveLeadFeedBack(LeadIntialContactFeedBack leadfeeback)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadFeedback;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leadfeeback);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}

		public int SaveLeadF2FFeedBack(LeadF2FFeedBack leadfeedback)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadF2FFeedback;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leadfeedback);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}

		public int UpdateStatus(int leadid,int status)
		{
			Leads leads = new Leads ();
			leads.LEAD_ID = leadid;
			leads.USER_LEAD_STATUS = status;
			leads.REASON_FOR_PASS = "";
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leads);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
			
		}

		public int UpdateReasonForPass(int leadid, string reasonForPass)
		{
			Leads leads = new Leads ();
			leads.LEAD_ID = leadid;
			leads.USER_LEAD_STATUS = 5;
			leads.REASON_FOR_PASS = reasonForPass;
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leads);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;

		}

		public string SFDCAuthentication ()
		{

			var request = HttpWebRequest.Create("https://ap2.salesforce.com//services/oauth2/token");
			request.ContentType = "application/x-www-form-urlencoded";
			request.Headers.Add ("X-PrettyPrint", "1");
			request.Method = "POST";

			var postData = "grant_type=password&username=DoNow_dev2@brillio.com&password=donow@dev2k9n6IQFyBYvDeGM2g0nVmFHpg&client_id=3MVG9ZL0ppGP5UrC4rjQFkEhUnYTSNP_Tvanu8b30_TqkLH7cOg8UC9zHKCsX.mgW_hFVY2J0jRyO.Ev_VsH0&client_secret=1975032834009986449";

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

		public async Task<string> UpdateSFDCData (string accessCode,long leadid,string status)
		{

			HttpClient queryClient3 = new HttpClient ();
			string serviceURL3 = "https://ap2.salesforce.com/services/data/v35.0/sobjects/Lead/" + leadid + "?_HttpMethod=PATCH";

			string insertPacket = "{ \"Status\": \"Jack\" }";

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
	}
}


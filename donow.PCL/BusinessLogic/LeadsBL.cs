using System;
using donow.Services;
using donow.Util;
using System.Collections.Generic;
using donow.PCL.Model;
using System.Threading.Tasks;

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
			string leadsApicall = Constants.LeadsAPI + "?id" + UserID;
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

		// UpdateLeadStatus()
		// UpdateIntialFeedBack()
		// UpdateF2FFeedBack()
		// GetNewLeads() 

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

		public LeadF2FFeedBack GetLeadF2FFeedBack(int leadId)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadF2FFeedback +  "?id=" + leadId;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LeadF2FFeedBack>(response.ToString());
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

		public int UpdateStatus(int leadid,string status)
		{
			Leads leads = new Leads ();
			leads.LEAD_ID = leadid;
			leads.STATUS = status;
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
			leads.REASON_FOR_PASS = reasonForPass;
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(leads);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;

		}
	}
}


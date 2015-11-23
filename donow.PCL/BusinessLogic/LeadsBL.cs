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

		public List<Leads> GetAllLeads()
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Leads>>(response.ToString());
			return parsedResponse;
		}

		public Leads GetLeadDetails(int leadId)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LeadsAPI + leadId ;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Leads>(response.ToString());
			return parsedResponse;
		}

		// UpdateLeadStatus()
		// UpdateIntialFeedBack()
		// UpdateF2FFeedBack()
		// GetNewLeads() 
	}
}


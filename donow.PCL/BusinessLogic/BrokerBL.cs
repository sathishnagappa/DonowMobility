using System;
using donow.Services;
using donow.Util;
using System.Collections.Generic;

namespace donow.PCL
{
	public class BrokerBL
	{
		public int UpdateBrokerStatus(long brokerId,int status,long leadid)
		{
			Broker broker = new Broker ();
			broker.BrokerID = brokerId;
			broker.LeadID = leadid;
			broker.Status = status;
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(broker);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;

		}

		public List<Broker> GetAllBrokers(string IndustryName,string LOB)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker + "?IndustryName=" + IndustryName + "&LOB=" + LOB;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Broker>>(response.ToString());
			return parsedResponse;
		}

		public List<Broker> GetBrokerForProspect(long leadID)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker + "?id=" +  leadID;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Broker>>(response.ToString());
			return parsedResponse;
		}
	}
}


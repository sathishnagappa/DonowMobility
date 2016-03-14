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
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(broker);
			string response =  RestService.Instance.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;

		}

		public List<Broker> GetAllBrokers(string IndustryName,string LOB, int UserID)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker + "?IndustryName=" + IndustryName + "&LOB=" + LOB + "&UserId=" + UserID;
			string response =  RestService.Instance.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Broker>>(response.ToString());
			return parsedResponse;
		}

		public List<Broker> GetBrokerForProspect(long leadID, int UserID)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker + "?id=" +  leadID + "&UserId=" + UserID + "&type=lead";
			string response =  RestService.Instance.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Broker>>(response.ToString());
			return parsedResponse;
		}

//		public List<Broker> GetBrokerForStatus(long leadID,int UserId,int status)
//		{
//			//RestService restSevice = new RestService ();
//			string leadsApicall = Constants.DealMaker + "?id=" +  leadID + "&UserId=" + UserId + "&status=" + status;
//			string response =  RestService.Instance.GetData (leadsApicall);
//			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Broker>>(response.ToString());
//			return parsedResponse;
//		}

//		public List<Broker> GetBrokerForStatus(long leadID,int status)
//		{
//			//RestService restSevice = new RestService ();
//			string leadsApicall = Constants.DealMaker + "?LeadID=" + leadID + "&BrokerStatus=" + status;
//			string response = RestService.Instance.GetData (leadsApicall);
//			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Broker>> (response.ToString ());
//			return parsedResponse;
//		}

		public List<Broker> GetBrokerFromID(int UserID)
		{
			//RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker + "?BrokerID=" + UserID + "&IDType=broker";
			string response = RestService.Instance.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Broker>> (response.ToString ());
			return parsedResponse;
		}

	}
}
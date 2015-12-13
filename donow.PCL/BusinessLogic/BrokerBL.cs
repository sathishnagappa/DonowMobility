using System;
using donow.Services;
using donow.Util;

namespace donow.PCL
{
	public class BrokerBL
	{
		public int UpdateBrokerStatus(int brokerId,string status)
		{
			Broker broker = new Broker ();
			broker.BrokerID = brokerId;
			broker.Status = status;
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealMaker;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(broker);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;

		}
	}
}


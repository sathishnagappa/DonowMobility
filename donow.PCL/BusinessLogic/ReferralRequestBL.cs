using System;
using donow.Services;
using donow.Util;
using System.Collections.Generic;

namespace donow.PCL
{
	public class ReferralRequestBL
	{
		public List<ReferralRequest> GetReferralRequest(int userID)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.ReferralRequests + "?BrokerUserID=" + userID;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReferralRequest>>(response.ToString());
			return parsedResponse;
		}

		public string SaveReferralRequest(ReferralRequest rrDetails)
		{
			RestService restSevice = new RestService ();
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(rrDetails);
			string response = restSevice.PostData (Constants.ReferralRequests, postData);
			return response;
		}

		public string UpdateReferralRequest(int requestID,int Status)
		{
			ReferralRequest rrDetails = new ReferralRequest ();
			rrDetails.ID = requestID;
			rrDetails.Status = Status;
			RestService restSevice = new RestService ();
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(rrDetails);
			string response = restSevice.PostData(Constants.ReferralRequestsUpdate, postData);
			return response;
		}

	}
}


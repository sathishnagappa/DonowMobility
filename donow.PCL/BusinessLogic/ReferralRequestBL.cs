using System;
using donow.Services;
using donow.Util;
using System.Collections.Generic;

namespace donow.PCL
{
	public class ReferralRequestBL
	{
		public List<ReferralRequest> GetReferralRequest()
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.ReferralRequests;
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

	}
}


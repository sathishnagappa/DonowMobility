using System;
using System.Collections.Generic;
using donow.Services;
using donow.Util;

namespace donow.PCL
{
	public class IndustryBL
	{
		public List<LineOfBusiness> GetLOB()
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.LOB;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LineOfBusiness>>(response.ToString());
			return parsedResponse;

		}

		public List<string> GetIndustry()
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.Industry;
			string response =  restSevice.GetData (leadsApicall);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(response.ToString());
			return parsedResponse;
		}

	}
}


using System;
using System.Collections.Generic;
using donow.Util;
using donow.Services;
using Bing;
using System.Net;

namespace donow.PCL
{
	public class CustomerBL
	{
		public CustomerBL ()
		{
		}

		public List<Customer> GetAllCustomers()
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.CustomerDetails;
			string response = restSevice.GetData (restUrl);
			List<Customer> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(response.ToString());
			return parsedResponse;
		}

		public List<BingResult> GetBingResult(string parameter)
		{
			const string bingKey = "9F3eqLyfmNP0PInNOmS13FdSlPoajoJqLvrbvndqZFM";
			var bing = new BingSearchContainer(
				new Uri("https://api.datamarket.azure.com/Bing/Search/"))
			{ Credentials = new NetworkCredential(bingKey, bingKey) };

			var query = bing.Web(parameter, null, null, null, null, null, null, null);
			//var query = bing.News("Market trends", null, null, null, null, null, null, null, null);
			var results = query.Execute();
			List<BingResult> binglist = new List<BingResult> ();
			foreach (var result in results)
			{
				BingResult bingResult = new BingResult ();
				bingResult.ID = result.ID;
				bingResult.Url = result.Url;
				bingResult.Description = result.Description;
				bingResult.DisplayUrl = result.DisplayUrl;
				binglist.Add (bingResult);				 
			}
			return binglist;

		}

		public int SaveCutomerInteraction(CustomerInteraction customerInteraction)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.CustomerInteraction;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(customerInteraction);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}
	}
}


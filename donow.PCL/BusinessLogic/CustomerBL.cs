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

		public List<Customer> GetAllCustomers(long userID)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.CustomerDetails + "?UserID=" + userID;
			string response = restSevice.GetData (restUrl);
			List<Customer> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(response.ToString());
			return parsedResponse;
		}

		public CustomerDetails GetCustomersDetails(long leadID, int userID)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.CustomerDetails + "?LeadID=" + leadID + "&UserID=" + userID;
			string response = restSevice.GetData (restUrl);
			CustomerDetails parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerDetails>(response.ToString());
			return parsedResponse;
		}

		public List<BingResult> GetBingResult(string parameter)
		{
			const string bingKey = "9F3eqLyfmNP0PInNOmS13FdSlPoajoJqLvrbvndqZFM";
			var bing = new BingSearchContainer(
				new Uri("https://api.datamarket.azure.com/Bing/Search/"))
			{ Credentials = new NetworkCredential(bingKey, bingKey) };
		
			var	query = bing.Web(parameter, null, null, null, null, null, null, null);
			var results = query.Execute();

			List<BingResult> binglist = new List<BingResult> ();
			foreach (var result in results)
			{
				BingResult bingResult = new BingResult ();
				bingResult.ID = result.ID;
				bingResult.Url = result.Url;
				bingResult.Description = result.Description;
				bingResult.DisplayUrl = result.DisplayUrl;
				bingResult.Title = result.Title;
				binglist.Add (bingResult);				 
			}
			return binglist;

		}

		public List<BingResult> GetBingNewsResult(string parameter)
		{
			const string bingKey = "9F3eqLyfmNP0PInNOmS13FdSlPoajoJqLvrbvndqZFM";
			var bing = new BingSearchContainer(
				new Uri("https://api.datamarket.azure.com/Bing/Search/"))
			{ Credentials = new NetworkCredential(bingKey, bingKey) };

			var	query = bing.News(parameter, null, null, null, null, null, null, null,"Date");
			var results = query.Execute();

			List<BingResult> binglist = new List<BingResult> ();
			foreach (var result in results)
			{
				BingResult bingResult = new BingResult ();
				bingResult.ID = result.ID;
				bingResult.Url = result.Url;
				bingResult.Description = result.Description;
				bingResult.Date = result.Date;
				bingResult.Title = result.Title;
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

		public List<Feed> GetCustomerFeed()
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.CustomerFeed;
			string response = restSevice.GetData (restUrl);
			List<Feed> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Feed>>(response.ToString());
			return parsedResponse;
		}

		public List<CustomerInteraction> GetCustomerInteraction(string customerName, int userID)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.CustomerInteraction + "?CustomerName=" + customerName + "&UserId=" + userID;
			string response = restSevice.GetData (restUrl);
			List<CustomerInteraction> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CustomerInteraction>>(response.ToString());
			return parsedResponse;
		}

		public List<DealHistroy> GetDealHistroy(long leadID, int userID)
		{
			RestService restSevice = new RestService ();
			string restUrl = Constants.DealHistory + "?LeadID=" + leadID + "&UserID=" + userID;
			string response = restSevice.GetData (restUrl);
			List<DealHistroy> parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DealHistroy>>(response.ToString());
			return parsedResponse;
		}

		public int SaveDealHistory(DealHistroy dealHistroy)
		{
			RestService restSevice = new RestService ();
			string leadsApicall = Constants.DealHistory;
			string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dealHistroy);
			string response =  restSevice.PostData (leadsApicall, postData);
			var parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.ToString());
			return parsedResponse;
		}

			


	}
}


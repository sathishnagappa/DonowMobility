using System;
using System.Threading.Tasks;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace donow.PCL
{
	public class TwitterUtil
	{

		public static async Task<List<TwitterStream>> Search(string paramter)
		{
			List<TwitterStream> _return = new List<TwitterStream>();

			//const string oauthConsumerKey = "AyKQbgOrgmHOii41sXqkfLWdz";  //for testing bUYWCEojQ3Ob0iyqz6xPP9V6O
			//const string oauthConsumerSecret = "cgv3d0JR9F8irOnT2UVLGDCelztVAufyp5Yt9pURwyrTKdawhC"; //for testing Bes8uI7pSaLmsL3ME9nYa0W7QrU9Uxdy2fjXcO0pZift3FGBco
			const string oauthConsumerKey = "xPw5UAX0AhVPP1wIdFm7oWz28";  //for testing bUYWCEojQ3Ob0iyqz6xPP9V6O
			const string oauthConsumerSecret = "7SHZBl4k2R8LHVYrV9kkpFe3hnso9RUjLCVAgs9mWcU2M9kD7q"; //for testing Bes8uI7pSaLmsL3ME9nYa0W7QrU9Uxdy2fjXcO0pZift3FGBco
			string accessToken;

			// get authentication token
			HttpMessageHandler handler = new HttpClientHandler();
			HttpClient httpClient = new HttpClient(handler);
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
			var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(oauthConsumerKey + ":" + oauthConsumerSecret));
			request.Headers.Add("Authorization", "Basic " + customerInfo);
			request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

			HttpResponseMessage response = await httpClient.SendAsync(request);

			var s = await response.Content.ReadAsStringAsync();
			var returnJson = JValue.Parse(s);
			accessToken = returnJson["access_token"].ToString();

//			// get timeline for user buzzfrog
//			HttpRequestMessage requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com//1.1/statuses/user_timeline.json?count=100&screen_name=LyndsyMFonseca");
//			//HttpRequestMessage requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com/1.1/search/tweets.json?q=%23superbowl&result_type=recent");
//			requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
//			HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
//			var returnJsonUserTimeline = JValue.Parse(await responseUserTimeLine.Content.ReadAsStringAsync());
//
//			List<ExpandoObject> timeLineResult = _return.Item1;
//			foreach (var token in returnJsonUserTimeline)
//			{
//				dynamic twitterObject = new ExpandoObject();
//				twitterObject.from_user = token["user"]["name"].ToString();
//				twitterObject.profile_image_url = token["user"]["profile_image_url"].ToString();
//				twitterObject.text = token["text"].ToString();
//				timeLineResult.Add(twitterObject);
//			}

			//HttpRequestMessage requestSearch = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com/1.1/search/tweets.json?count=100&q=wpdev");
			//HttpRequestMessage requestSearch = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com/1.1/search/tweets.json?count=50&q=%23" + paramter +"&result_type=recent");
			HttpRequestMessage requestSearch = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com/1.1/search/tweets.json?lang=en&count=20&q=%23" + paramter +"&result_type=recent");
			requestSearch.Headers.Add("Authorization", "Bearer " + accessToken);
			HttpResponseMessage responseSearch = await httpClient.SendAsync(requestSearch);
			var returnJsonSearch = JValue.Parse(await responseSearch.Content.ReadAsStringAsync());

			//List<ExpandoObject> searchResult = _return.Item2;
			foreach (var token in returnJsonSearch["statuses"])
			{
				TwitterStream twitterObject = new TwitterStream();
				twitterObject.user = token["user"]["name"].ToString();
				twitterObject.profile_image_url = token["user"]["profile_image_url"].ToString();
				twitterObject.text = token["text"].ToString();
				//created_at
				if(token["entities"]["urls"].Count() > 0 )
					twitterObject.url = token["entities"]["urls"][0]["url"].ToString();
				else if(token["user"]["entities"]["url"] != null && token["user"]["entities"]["url"]["urls"].Count() > 0)
					twitterObject.url = token["user"]["entities"]["url"]["urls"][0]["url"].ToString();
				else
					twitterObject.url = "";
				_return.Add(twitterObject);
			}
			//_return.Item1.FirstOrDefault();

			return _return;
		}

	}
}


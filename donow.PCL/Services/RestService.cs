using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Json;
using donow.Util;
using System.Text;
using donow.PCL;

namespace donow.Services
{
	public class RestService
	{

		public string GetData (string RestURL)
		{
			// Create an HTTP web request using the URL:
//			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
//			request.ContentType = "application/json";
//			request.Method = "GET";
//
//			// Send the request to the server and wait for the response:
//			using (WebResponse response = await request.GetResponseAsync())
//			{
//				// Get a stream representation of the HTTP web response:
//				using (Stream stream = response.GetResponseStream ())
//				{
//					// Use this stream to build a JSON document object:
//					string jsonDoc = await Task.Run (() => JsonObject.Load (stream));
//
//					// Return the JSON document:
//					return jsonDoc;
//				}
//			}
//			HttpClient client = new HttpClient();
//			HttpResponseMessage response = await client.GetAsync(url);
//			response.EnsureSuccessStatusCode();
//			string contentString = await response.Content.ReadAsStringAsync();
//			return contentString;
			var request = HttpWebRequest.Create(RestURL);
			request.ContentType = "application/json";
			request.Method = "GET";

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if (response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					var content = reader.ReadToEnd();
					return content;
				}
			}

		}


		public string SFDCAuthentication ()
		{
			
			var request = HttpWebRequest.Create("https://ap2.salesforce.com//services/oauth2/token");
			request.ContentType = "application/x-www-form-urlencoded";
			request.Headers.Add ("X-PrettyPrint", "1");
			request.Method = "POST";

			var postData = "grant_type=password&username=DoNow_dev2@brillio.com&password=donow@dev2k9n6IQFyBYvDeGM2g0nVmFHpg&client_id=3MVG9ZL0ppGP5UrC4rjQFkEhUnYTSNP_Tvanu8b30_TqkLH7cOg8UC9zHKCsX.mgW_hFVY2J0jRyO.Ev_VsH0&client_secret=1975032834009986449";

			var data = Encoding.ASCII.GetBytes(postData);
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}
			var content = string.Empty; 
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				
				if (response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					content = reader.ReadToEnd();
					//return content;
				}
			}
			SFDCAuth sfdcAuthObj = Newtonsoft.Json.JsonConvert.DeserializeObject<SFDCAuth>(content.ToString());
			return sfdcAuthObj.access_token;

		}

		public string UpdateData (string accessCode)
		{

			var request = HttpWebRequest.Create("https://ap2.salesforce.com/services/data/v35.0/sobjects/Lead/");
			request.ContentType = "application/json";
			request.Headers.Add ("X-PrettyPrint", "1");
			request.Headers.Add ("Authorization", accessCode);
			request.Method = "POST";

			var postData = "{ \"First Name\": \"John\",\"Last Name\": \"Gibson\",\"Company\" : \"Brillio\",\"Email\": \"john.gibson@brillio.com\" }";

			var data = Encoding.ASCII.GetBytes(postData);
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if (response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					var content = reader.ReadToEnd();
					return content;
				}
			}

		}


		public string PostData(string RestURL, string postData)
		{

			var request = HttpWebRequest.Create(RestURL);
			request.ContentType = "application/json";
			request.Method = "POST";


			var data = Encoding.ASCII.GetBytes(postData);
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if (response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					var content = reader.ReadToEnd();
					return content;
				}
			}
		}

	}
}

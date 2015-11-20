using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Json;
using donow.Util;

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

	}
}

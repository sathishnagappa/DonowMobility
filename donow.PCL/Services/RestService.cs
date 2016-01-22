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
using System.Net.Http.Headers;
using System.Net.NetworkInformation;

namespace donow.Services
{
	public class RestService
	{

		public string GetData (string RestURL)
		{
			var request = HttpWebRequest.Create(RestURL);
			request.ContentType = "application/json";
			request.Method = "GET";

			if (CheckInternetAccess ()) {
				using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine ("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
						var content = reader.ReadToEnd ();
						return content;
					}
				}
			}
			else				
				return "false";	

		}

		private bool CheckInternetAccess()
		{
			try
			{
				if(NetworkInterface.GetIsNetworkAvailable())
				{
					return true;
				}
				return false;
			}
			catch {
				return false;
			}
		}



		public async Task<string> GetDataAsync (string RestURL)
		{
			var request = HttpWebRequest.Create(RestURL);
			request.ContentType = "application/json";
			request.Method = "GET";

			using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
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

			if (CheckInternetAccess ()) {
				using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine ("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
						var content = reader.ReadToEnd ();
						return content;
					}
				}
			}
		    else				
				return "No Network";
		}


//		public string PutData(string RestURL, string postData)
//		{
//
//			var request = HttpWebRequest.Create(RestURL);
//			request.ContentType = "application/json";
//			request.Method = "PUT";
//
//
//			var data = Encoding.ASCII.GetBytes(postData);
//			request.ContentLength = data.Length;
//
//			using (var stream = request.GetRequestStream())
//			{
//				stream.Write(data, 0, data.Length);
//			}
//
//			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
//			{
//				if (response.StatusCode != HttpStatusCode.OK)
//					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
//				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
//				{
//					var content = reader.ReadToEnd();
//					return content;
//				}
//			}
//		}



	}
}

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

namespace donow.Services
{
	public class RestService
	{

		public string GetData (string RestURL)
		{
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

		public async Task<string> UpdateSFDCData (string accessCode)
		{

			HttpClient queryClient3 = new HttpClient ();
			string serviceURL3 = "https://ap2.salesforce.com/services/data/v35.0/sobjects/Lead/";

			string insertPacket = "{ \"FirstName\": \"John\",\"LastName\": \"Gibson\",\"Company\" : \"Brillio\",\"Email\": \"john.gibson@brillio.com\" }";

			StringContent insertString = new StringContent(insertPacket,Encoding.UTF8,"application/json");
			HttpRequestMessage request3 = new HttpRequestMessage(HttpMethod.Post, serviceURL3);
			request3.Headers.Add("Authorization", "OAuth " + accessCode);
			request3.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request3.Content = insertString;
			HttpResponseMessage response3 = await queryClient3.SendAsync(request3);
			string result = await response3.Content.ReadAsStringAsync();
			return result;

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

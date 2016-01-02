using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Xamarin.Auth;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace donow.iOS
{
	partial class LinkedInLoginVC : UIViewController
	{

		public LinkedInLoginVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			var auth = new OAuth2Authenticator (
				           clientId: "750em9s4eyig1t",
				           clientSecret: "TV10hcFfWYITtFUv",
				           scope: "r_fullprofile r_contactinfo",
				           authorizeUrl: new Uri ("https://www.linkedin.com/uas/oauth2/authorization"),
						   redirectUrl: new Uri ("https://is.brillio.com/MyApp.html"),
				           accessTokenUrl: new Uri ("https://www.linkedin.com/uas/oauth2/accessToken")

			           );

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.AllowCancel = true;
			auth.Completed += (sender, eventArgs) =>{
				if (eventArgs.IsAuthenticated) 
				{
					string dd =eventArgs.Account.Username;
					var values=eventArgs.Account.Properties;
					var access_token=values["access_token"];
					try
					{

						var request = HttpWebRequest.Create(string.Format (@"https://api.linkedin.com/v1/people/~/connections:(headline,first-name,last-name)?oauth2_access_token="+access_token+"&format=json",""));
						request.ContentType = "application/json";
						request.Method = "GET";

						using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
						{
							System.Console.Out.WriteLine("Stautus Code is: {0}", response.StatusCode);

							using (StreamReader reader = new StreamReader(response.GetResponseStream()))
							{
								var content = reader.ReadToEnd();
								if(!string.IsNullOrWhiteSpace(content)) 
								{

									System.Console.Out.WriteLine(content);
								}
								var result= JsonConvert.DeserializeObject<dynamic>(content);
							}
						}
					}
					catch(Exception exx)
					{
						System.Console.WriteLine(exx.ToString());
					}
				}
			};

			try{
				var linkedinvc = auth.GetUI ();
			}
			catch(Exception ex) {
			}
		}
	}


}

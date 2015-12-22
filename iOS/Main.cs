using System;
using Foundation;
using UIKit;

namespace donow.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
<<<<<<< HEAD
			Xamarin.Insights.Initialize("34feb972c36b74ccb19e33d0f8e23916f1513942");
=======
			Xamarin.Insights.Initialize (XamarinInsights.ApiKey);
>>>>>>> origin/master

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}

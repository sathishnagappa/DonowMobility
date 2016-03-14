using System;

namespace donow.PCL
{
	public class TwitterStream
	{
		public string user { get; set;}
		public string profile_image_url { get; set;}
		public string text { get; set;}
		public string url { get ; set;}
		public bool ApiResponse { get; set; }
		public string ErrorMessage { get; set; }
	}
}


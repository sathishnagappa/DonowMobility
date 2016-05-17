using System;

namespace donow.PCL
{
	public class BingResult : BaseClass
	{		
			public Guid ID { get; set; }
			public string DisplayUrl { get; set; }
			public string Description { get; set; }
			public string Title { get; set; }
			public string Url { get; set; }
			public DateTime?  Date { get; set; }
	}
}


﻿using System;

namespace donow.PCL
{
	public class CustomerInteraction
	{
		public int UserId { get; set; }
		public string CustomerName { get; set; }
		public string Type { get; set; }
		public string DateNTime { get; set; }
		public int LeadID { get; set; }
		public bool ApiResponse { get; set; }
		public string ErrorMessage { get; set; }

	}
}


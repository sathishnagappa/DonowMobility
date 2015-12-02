using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using donow.PCL;
using System.Collections.Generic;
using donow.PCL.Model;

namespace donow.iOS
{
	partial class prospectDetailsVC : UIViewController
	{
		public Leads localLeads;

		public prospectDetailsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			IList<Leads> leads = new  List<Leads> ();


		}

	}
}

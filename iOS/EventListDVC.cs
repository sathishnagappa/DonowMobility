
using System;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Dialog;

using Foundation;
using UIKit;
using EventKit;

namespace donow.iOS
{
	public partial class EventListDVC : DialogViewController
	{
		protected RootElement itemListRoot = new RootElement ( "Calendar/Reminder Items" );

		protected EKCalendarItem[] events;
		protected EKEntityType eventType;

		public EventListDVC (EKCalendarItem[] events, EKEntityType eventType ) : base (UITableViewStyle.Grouped, null)
		{
			this.events = events;
			this.eventType = eventType;

			Section section;
			if (events == null) {
				section = new Section () { new StringElement ("No calendar events") };
			} else {
				section = new Section ();
				section.AddAll (
					from items in this.events
					select new StringElement (items.Title)
				);
			}
			itemListRoot.Add (section);
			// set our element root
			this.InvokeOnMainThread ( () => { this.Root = itemListRoot; } );
		}
	}
}

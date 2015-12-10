using System;
using System.Collections.Generic;

namespace donow.PCL
{
	public class IndustryBL
	{

		public static List<LineOfBusiness> GetLOB()
		{
			List<LineOfBusiness> listOfLOB = new List<LineOfBusiness> ();
			listOfLOB.Add (new LineOfBusiness () { Industry="Advertising",LOB="Information retrieval services" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Advertising",LOB="Management Consulting Services" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Agriculture",LOB="Farm machinery and equipment" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Apparel",LOB="Business Services" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Apparel",LOB="Clothing" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Apparel",LOB="Shoes" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Automobile",LOB="New and used car dealers" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Automobile",LOB="Used car dealers" } );
			listOfLOB.Add (new LineOfBusiness () { Industry="Automobile",LOB="New car dealers" } );
			return listOfLOB;
		}

//		Advertising
//		Information retrieval services
//		Management Consulting Services
//		            Agriculture         
//		Farm machinery and equipment
//		            Apparel
//		Business Services
//		Clothing
//		Shoes
//		            Automobile
//		New and used car dealers
//		Used car dealers
//		New car dealers
//		Vehicle Parts and accessories
//		Auto and home supply stores
//		Automotive repair shops
//		Tires
//		Boats
//		          Banking/Finance
//		          Chemicals
//		          Communications
//		          Computers
//		Electronic computers
//		Prepackaged software
//		Office Equipment
//		Computer integrated systems design
//		Data processing and preparation
//		          Construction
//		          Consulting
//		Management Consulting Services
//		Business Consulting
//		Technical Consulting
//		          Education
//		Schools and education services
//		Data processing schools
//		Amusement and Recreation
//		Vocational Schools
//		Prepackaged Software
//		          Electronics
//		Electric Services
//		Electronic Parts and Equipment
//		Electrical apparatus and equipment
//		Switchgear and switchboard apparatus
//		Hardware
//		Repair Services
//		          Energy
//		Fuel oil dealers
//		          Engineering
//		Engineering Services
//		Prepackaged Services
//		          Entertainment
//		Amusement Parks
//		Services
//		Entertainers and Entertainment Groups
//		Racing
//		Membership Sports
//		Eating Places
//		Toys and hobbies
//		          Food and Beverage
//		Groceries
//		          Government
//		          Healthcare
//		Medical Laboratories
//		Health and Allied Services
//		Offices and Clinics of Medical Doctors
//		Offices and Clinics of Dentists
//		Hospital and medical service plans
//		Residential Care
//		          Hospitality
//		Hotels and motels
//		          Information Technology Services
//		Custom Computer Programming Services
//		Computer related services
//		Management consulting services
//		Computer integrated systems design
//		          Insurance
//		          Machinery
//		Internal combustion engines
//		          Manufacturing
//		          Media
//		 Catalog
//		Libraries
//		Commercial photography
//		Information retrieval services
//		Radio and TV
//		          Not for Profit
//			Social Services
//			          Other
//			          Recreation
//			          Retail
//			Catalog
//			Clothing Stores
//			Department Stores
//			Direct Selling
//			Automotive
//			Gift & Novelty
//			Miscellaneous
//			          Shipping
//			          Technology
//			          Telecommunications
//			          Transportation
//			          Utilities
	}
}


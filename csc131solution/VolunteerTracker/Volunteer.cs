
using System;

namespace VolunteerTracker
{


	public class Volunteer : VolunteerTracker.ActiveRecord
	{
		public string FirstName 
		{
			get; 
			set;
		}
		
		public string LastName 
		{
			get;
			set;
		}
		
		public string CellPhone 
		{
			get;
			set;
		}
		
		public string HomePhone
		{
			get;
			set;
		}
		
		public string OfficePhone 
		{
			get;
			set;
		}
		
		public string Address 
		{
			get;
			set;
		}
		
		public string Email 
		{
			get;
			set;
		}
		
		public Volunteer ()
		{
		}
		
		public Volunteer(long id) : base(id) 
		{
		}
	}
}

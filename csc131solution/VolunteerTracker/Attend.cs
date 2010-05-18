
using System;

namespace VolunteerTracker
{


	public class Attend : ActiveRecord
	{

		
		public long EventId 
		{
			get;
			set;
		}
		
		public long VolunteerId
		{
			get;
			set;
		}
		
		public long Hours 
		{
			get;
			set;
		}
		
		public long Minutes 
		{
			get;
			set;
		}
		
		public string Notes 
		{
			get;
			set;
		}
		
		public Attend () : base()
		{
			
		}
		
		public Attend(long id) : base(id)
		{
		}
	}
}


using System;
using System.Collections.Generic;

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
		
		public long Male 
		{
			get;
			set;
		}
		
		public string Employer
		{
			get;
			set;
		}
		public string JobTitle
		{
			get;
			set;
		}
		
		public string Ethnicity
		{
			get;
			set;
		}
		
		public DateTime Birthday
		{
			get;
			set;
		}
		
		[ActiveRecordSerialize]
		public List<string> Interests
		{
			get;
			set;
			
		}
		
		public string SerializeInterests()
        {         
            return FlattenInterests(Interests);
        }

        public List<string> DeserializeInterests(string value)
        {                        
            return ExtractInterests(value);
        }
		
		public static string FlattenInterests(List<string> interests)
        {
                
                string flattenedInterests = "";

                if (interests == null || interests.Count == 0)
                {
                    return flattenedInterests;
                }
                //^ is the separator for interests.
                foreach (string interest in interests)
                    flattenedInterests += interest + "^";
                
                flattenedInterests = flattenedInterests.Substring(0, flattenedInterests.Length - 1);
                return flattenedInterests;
            }

            public static List<string> ExtractInterests(string interests)
            {
                string[] rTypes = interests.Split(new char[] { '^' });
                List<string> interestsList = new List<string>();
                foreach (string interest in rTypes)
                    interestsList.Add(interest);
                return interestsList;
            }

		
		
		public Volunteer ()
		{
		}
		
		public Volunteer(long id) : base(id) 
		{
		}
	}
}

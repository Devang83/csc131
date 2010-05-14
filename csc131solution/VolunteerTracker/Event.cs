
using System;

namespace VolunteerTracker
{


public class Event : VolunteerTracker.ActiveRecord
    {

        public DateTime Date
        {
            get;
            set;
        }
        
        public string Name
        {
            get;
            set;
        }
		
		public string Email 
		{
			get;
			set;
		}
		
        private void SetDefaults() 
        {
            Name = "";            
            //ACL = new AccessControlList();
            //ACL.Users[QuickPM.Database.GetUserId()] = AccessControlList.MAX_ACCESS;
            //SetDefaultACL();
        }
				

        
        public Event(long id) : base(id)
        {            
        }
		
		public Event() : base()
        {            
        }
}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolunteerTracker
{
    public class AccessControlList
    {
        public const int MAX_ACCESS = int.MaxValue;
        public const int CAN_WRITE = 2;
        public const int CAN_READ = 1;
        public const int NO_ACCESS = 0;

        public const long ROOT_USERID = long.MaxValue;

        /// <summary>
        /// Dictionary of user id's and whether they have no access (0), can view (1), or can modify (2).
        /// </summary>        
        public Dictionary<long, int> Users
        {
            get;
            set;
        }


        public bool CanRead(long userId)
        {
            return userId == ROOT_USERID || (Users.ContainsKey(userId) && Users[userId] >= AccessControlList.CAN_READ);
        }

        public bool CanWrite(long userId)
        {
            return userId == ROOT_USERID || (Users.ContainsKey(userId) && Users[userId] >= AccessControlList.CAN_WRITE);
        }

        public bool NoAccess(long userId)
        {
            return (!Users.ContainsKey(userId) || Users[userId] == AccessControlList.NO_ACCESS) && userId != ROOT_USERID;
        }

        public static string SerializeUsers(Dictionary<long, int> users)
        {
            string txt = "";
            foreach (long userId in users.Keys)
            {
                txt += userId + ":" + users[userId] + ",";
            }

            return txt.Length > 0 ? txt.Substring(0, txt.Length - 1) : txt;
        }

        public static Dictionary<long, int> DeserializeUsers(string value)
        {
            value = value.Trim();
            if (value.Length == 0) return new Dictionary<long, int>();
            string[] sids = value.Split(new char[] { ',' });
            Dictionary<long, int> users = new Dictionary<long, int>();
            foreach (string id in sids)
            {
                string[] v = id.Split(new char[] { ':' });
                long userId = long.Parse(v[0]);
                int accessValues = int.Parse(v[1]);
                users[userId] = accessValues;
            }
            //return ids;
            return users;
        }
                

        private void SetDefaults()
        {
            Users = new Dictionary<long, int>();            
        }

        public AccessControlList() 
        {
            SetDefaults();
        }
        
    }
    public class User : VolunteerTracker.ActiveRecord
    {

        public string Email
        {
            get;
            set;
        }
        
        public string Name
        {
            get;
            set;
        }        

        private void SetDefaults() 
        {
            Name = "";
            Email = "";
            ACL = new AccessControlList();
            ACL.Users[VolunteerTracker.Database.GetUserId()] = AccessControlList.MAX_ACCESS;
            //SetDefaultACL();
        }

        public User() 
        {
            SetDefaults();
        }

        public User(string email)
        {
			long savedUserId = VolunteerTracker.Database.GetUserId();
            //set the root user, they have access to all the users.
            VolunteerTracker.Database.SetUserId(AccessControlList.ROOT_USERID);
            List<VolunteerTracker.User> users = VolunteerTracker.User.Find<User>("Email", email);
            if (users.Count == 0)
            {
                SetDefaults();
                Email = email;
            }
            else
            {
                this.Email = users[0].Email;
                this.ACL = users[0].ACL;
                this.Id = users[0].Id;
                this.Name = users[0].Name;
            }
           	VolunteerTracker.Database.SetUserId(savedUserId);			
        }

        public User(long id) : base(id)
        {            
        }
    }
}

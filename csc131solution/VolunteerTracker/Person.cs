using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlTypes;

namespace VolunteerTracker
{
    public class Person : ActiveRecord, IHasParentProperty
    {
        /// <summary>
        /// AssociatedKey can be either a TenantId or PropertyId.
        /// </summary>
        public string AssociatedKey
        {
            get;
            set;
        }

		public long GetPropertyId()
        {            
			if (AssociatedKey == null || AssociatedKey == "")
			{
				return -1;
			}
			string[] vals = AssociatedKey.Split(new char[]{' '});
			if (vals.Length != 2) 
			{
				return -1;
			}
			string typeName = vals[0];
			long objId;
			if (!long.TryParse(vals[1], out objId))
			{
				return -1;
			}
			//if (typeName == typeof(Tenant).Name) 
			//{
			//	Tenant tenant = new Tenant(objId);
			//	return tenant.GetPropertyId();
			//} 
			//if (typeName == typeof(Property).Name) {
			//	return objId;
			//}
			return -1;			            
        }
		
		public bool SetAssociatedKey(ActiveRecord obj) 
		{
			return obj != null ? (AssociatedKey = obj.GetType().Name + " " + obj.Id) != null : false;
		}

        public string Name
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string CellPhone
        {
            get;
            set;
        }

        public string OfficePhone
        {
            get;
            set;
        }

        public string HomePhone
        {
            get;
            set;
        }

        public string Fax
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public Person()
        {
            SetDefaults();
        }

        public Person(long id)
            : base(id)
        {
            if (!Database.RecordExists<Person>(this))
            {
                SetDefaults();
            }
        }

        public static List<Person> GetContacts(ActiveRecord obj)
        {
            return ActiveRecord.Find<Person>("AssociatedKey", obj.GetType().Name + " " + obj.Id);            
        }

        private void SetDefaults()
        {
            Id = -1;
            Name = "";
            Title = "";
            OfficePhone = "";
            HomePhone = "";
            CellPhone = "";
            Email = "";
            Fax = "";
            Address = "";
            AssociatedKey = "";
            SetDefaultACL();
        }




        public class Util
        {

            public static List<Person> GetPeople(string name)
            {
                return ActiveRecord.Find<Person>("Name", name);
            }
        }            
    }
}

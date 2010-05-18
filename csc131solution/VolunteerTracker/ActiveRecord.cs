using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
//using System.Linq;
using System.Text;

namespace VolunteerTracker
{
    public interface IHasParentProperty
    {
        long GetPropertyId();
        
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ActiveRecordSerializeAttribute : Attribute
    {
        
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ActiveRecordDontSerializeAttribute : Attribute
    {
    }

    public class ActiveRecord
    {
        public long Id
        {
            get;
            set;
        }
		
		public long ModifiedBy
		{
			get;
			set;
		}

        public DateTime ModifiedDate
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        /// <summary>
        /// List of users that can modify the object.
        /// </summary>
        [ActiveRecordSerialize]
        public AccessControlList ACL
        {
            get;
            set;
        }
		
		public string GetTimeSinceModification() 
		{
			return Util.GetSimpleTimeDifference(DateTime.Now, ModifiedDate);
		}
		
        public string GetTimeSinceCreation()
		{
			return Util.GetSimpleTimeDifference(DateTime.Now, CreatedDate);
		}
		
		public string SerializeACL()
        {
            return AccessControlList.SerializeUsers(ACL.Users);
        }

        public AccessControlList DeserializeACL(string value)
        {
            AccessControlList acl = new AccessControlList();
            acl.Users = AccessControlList.DeserializeUsers(value);
            return acl;
        }
        public void SetDefaultACL()
        {
            if (ACL == null)
            {
                ACL = new AccessControlList();
            }
            ACL.Users[Database.GetUserId()] = AccessControlList.MAX_ACCESS;
            if (Id != -1)
			{
				//AddParentPropertyUsersToACL();            
			}
        }

        public ActiveRecord()
        {
            Id = -1;
            SetDefaultACL();
        }

        public ActiveRecord(long id)
        {
            this.Id = id;
            if (!Database.RecordExists(this))
            {
                SetDefaultACL();
            }
            PopulateValues();            
        }
		
		protected void SetEqual(ActiveRecord record)
		{
			Type type = record.GetType();     			
            PropertyInfo[] properties = type.GetProperties();						
			Dictionary<string, object> nameValues = new Dictionary<string, object>();
			foreach (PropertyInfo pInfo in properties)
            {            	
                
				bool skip = false;
				
				foreach(Attribute attribute in pInfo.GetCustomAttributes(true))
				{					
					if (attribute is ActiveRecordDontSerializeAttribute)
                    {
                    	skip = true;
                    }
				}
                if (pInfo.CanWrite && !skip)
				{
					object value = pInfo.GetValue(record, null);
					pInfo.SetValue(this, value, null);								                                                                           								            			
				}
			}
		}
		
		protected static void ClearValues(ActiveRecord record)
		{
			Type type = record.GetType();     			
            PropertyInfo[] properties = type.GetProperties();						
			Dictionary<string, object> nameValues = new Dictionary<string, object>();
			foreach (PropertyInfo pInfo in properties)
            {            	
                
				bool skip = false;
				
				foreach(Attribute attribute in pInfo.GetCustomAttributes(true))
				{					
					if (attribute is ActiveRecordDontSerializeAttribute)
                    {
                    	skip = true;
                    }
				}
				skip |= pInfo.Name == "ACL";
				skip |= pInfo.Name == "Id";
                if (pInfo.CanWrite && !skip)
				{
					object value = null;
					pInfo.SetValue(record, value, null);								                                                                           								            			
				}
			}
		}
		

		//private TimeSpan r1 = new TimeSpan(0);
		//private TimeSpan r2 = new TimeSpan(0);
/*        public ActiveRecord(string key, object value)
        {
            
        }
*/
        public void PopulateValues(long id)
        {
            this.Id = id;
			//DateTime t1 = DateTime.Now;			
			bool exists = Database.RecordExists(this);
			//r1 += DateTime.Now - t1;
			
            if (exists)
            {
				//t1 = DateTime.Now;
                //Database.GetRecordValues(Id, out this, "Id");                
				List<IDataParameter> parameters = new List<IDataParameter>();
				parameters.Add(DatabaseAccess.CreaterParameter("@Id", Database.GetDbType(Id.GetType()), Id));
				ActiveRecord.GetRecords("select * from " + Util.Pluralize(this.GetType().Name) + " where Id = @Id", parameters, this);
				//r2 += DateTime.Now - t1;
            }            
        }


        protected void PopulateValues()
        {
            PopulateValues(this.Id);
        }

		
        public virtual bool Save()
        {            
            if (!ACL.CanWrite(Database.GetUserId()))
            {
                return false;
            }            			
            return Database.CreateOrUpdate(this);            
        }

        public virtual int Delete()
        {
            if (!ACL.CanWrite(Database.GetUserId()))
            {
                return 0;
            }
            string deleteString = "delete from " + Util.Pluralize(this.GetType().Name);
            deleteString += " where Id = @Id";
            int retVal = 0;
            using (DatabaseAccess databaseAccess = Database.NewDatabaseAccess())
			{
            	IDataParameter p = DatabaseAccess.CreaterParameter("@Id", DbType.Int32, this.Id);
            	List<IDataParameter> ps = new List<IDataParameter>();
            	ps.Add(p);
            	retVal = databaseAccess.ExecuteNonQuery(deleteString, ps);
            }
            return retVal;
        }
		
		public static List<E> GetRecentActivity<E>(long numRecords) where E : ActiveRecord, new()
		{
			return Find<E>(" order by ModifiedDate desc", numRecords, false);						
		}

		public static List<E> GetRecords<E>(string selectString, List<IDataParameter> parameters) where  E : ActiveRecord, new()			
		{
			return GetRecords<E>(selectString, parameters, (E)null);
		}
		
		public static List<E> GetRecords<E>(string selectString, List<IDataParameter> parameters, E obj1) where  E : ActiveRecord, new()			
		{			
			List<E> records = new List<E>();
			Type type = typeof(E);
			if (obj1 != null)
			{
				type = obj1.GetType();     			
			}
            PropertyInfo[] properties = type.GetProperties();			
			using (DatabaseAccess dbAccess = new DatabaseAccess(Database.ConnectionString))
			{ 
				using (IDataReader reader = dbAccess.ExecuteReader(selectString, parameters))
				{            		
					while (true)
            		{
						bool s = reader.Read();
						if(!s)
						{
							break;
						}
						E record = obj1 != null ? obj1 : new E();						                		
						Dictionary<string, object> nameValues = new Dictionary<string, object>();
						foreach (PropertyInfo pInfo in properties)
                		{

                    		bool serialize = false;
                    		bool skip = false;
							foreach (Attribute attr in pInfo.GetCustomAttributes(true))
        		            {
                		        if (attr is ActiveRecordSerializeAttribute)
                        		{
                            		serialize = true;                            
                        		}
                        		if (attr is ActiveRecordDontSerializeAttribute)
                        		{
                            		skip = true;
                        		}
                    		}
                    		if (!skip)
                    		{
                        		if (serialize)
                        		{
                            
                            		MethodInfo deserializeMethod = type.GetMethod("Deserialize" + pInfo.Name, new Type[] { typeof(string) } );						
                            		string[] parameters2 = new string[1];
									parameters2[0] = (string)reader.GetValue(reader.GetOrdinal(pInfo.Name));
									nameValues[pInfo.Name] = pInfo.GetValue(record, null);
                            		pInfo.SetValue(record, deserializeMethod.Invoke(record, parameters2), null);
                        		}
                        		else
                        		{
									object obj = reader.GetValue(reader.GetOrdinal(pInfo.Name));									
                            		if (obj is System.DBNull)
                            		{
										Console.WriteLine(pInfo.Name + " NULL");
										nameValues[pInfo.Name] = pInfo.GetValue(record, null);
                                		pInfo.SetValue(record, null, null);
                            		}
                            		else
                            		{			
										
										//.......................................................................................................................................			
										pInfo.SetValue(record, obj, null);											
                            		}
                        		}
                    		}
                		}
						
						//record.AddParentPropertyUsersToACL();
						
						if(record.ACL.CanRead(Database.GetUserId()))
						{
							records.Add(record);						
							if (obj1 != null)
							{

								return records;
							}
						}
						if (obj1 != null && !record.ACL.CanRead(Database.GetUserId()))
						{
							ActiveRecord.ClearValues(record);
							return null;							
						}						
            		}
				}
			}
			records.Sort((r1, r2) => r1.Id.CompareTo(r2.Id));
			return records;			
		}		

        public static List<E> Find<E>() where E : ActiveRecord, new()
        {
            return Find<E>(new Dictionary<string, object>());
        }

        public static List<E> Find<E>(string key, object value) where E : ActiveRecord, new()
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues[key] = value;
            return Find<E>(keyValues);
        }
		
		
		/// <summary>
		/// Find records matching the constraints string, example: sqlStringContrains=" where Id = 10", would generate "select Id from Tenants where Id = 10"
		/// </summary>
		/// <param name="sqlContraintsClause">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="numRecords>
		/// A <see cref="System.Int64>
		/// numRecords = -1 means read all the records.
		/// </param>
		/// <returns>
		/// A <see cref="List"/>
		/// </returns>
		
		public static List<E> Find<E>(string sqlStringContraints, long numRecords, bool useLimit) where E : ActiveRecord, new()
		{
			return Find<E>(sqlStringContraints, numRecords, useLimit, new Dictionary<string, object>());
		}
		
		/// <summary>
		/// Find records matching the where clause, example: whereClause="Id = 10", would generate "select Id from Tenants where Id = 10"
		/// </summary>
		/// <param name="whereClause">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="List"/>
		/// </returns>
		public static List<E> Find<E>(string whereClause) where E : ActiveRecord, new()
		{			
            return Find<E>(" where " + whereClause, -1, false);
        }
		
		public static List<E> Find<E>(Dictionary<string, object> keyValues) where E : ActiveRecord, new()
        {
			string constraintString = keyValues.Keys.Count == 0 ? "" : " where ";
			foreach (string key in keyValues.Keys)
            {
               	constraintString += key + " = @" + key + " and ";               	
            }
			if (constraintString.EndsWith(" and "))
			{
				constraintString = constraintString.Substring(0, constraintString.Length - " and ".Length);
			}
            return Find<E>(constraintString, -1, false, keyValues);
        }			
		
		
		
		/// <summary>
		/// Find records matching the constraints string, example: sqlStringContrains=" where Id = 10", would generate "select Id from Tenants where Id = 10"
		/// </summary>
		/// <param name="sqlContraintsClause">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="numRecords>
		/// A <see cref="System.Int64>
		/// numRecords = -1 means read all the records.
		/// </param>
		/// <returns>
		/// A <see cref="List"/>
		/// </returns>
		public static List<E> Find<E>(string sqlStringConstraints, long numRecords, bool useLimit, Dictionary<string, object> parameters) where E : ActiveRecord, new()
		{
			string selectString = "select * from " + Util.Pluralize(typeof(E).Name) + " " + sqlStringConstraints; 
            List<System.Data.IDataParameter> dbParameters = new List<IDataParameter>();
			foreach (string paramName in parameters.Keys)
			{
				dbParameters.Add(DatabaseAccess.CreaterParameter(paramName, Database.GetDbType(parameters[paramName].GetType()), parameters[paramName]));
			}			
			DateTime d1 = DateTime.Now;
			List<E> l = ActiveRecord.GetRecords<E>(selectString, dbParameters);
			return l;
		}											
    }
}

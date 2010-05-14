using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;




namespace VolunteerTracker
{
    public class Database
    {
        private static long currentUserId = 0;//new User();
        public static long GetUserId()
        {  
            return currentUserId;
        }

        public static void SetUserId(long userId)
        {
            currentUserId = userId;
        }
        
		
        
        public static string ConnectionString
        {
            get;
			set;
        }       

        public static void SetConnectionString(string connectionString)
        {
            Database.ConnectionString = connectionString;
        }

        public static string CreateConnectionString(string databasePath, string databaseName, string dbUser, string password)
        {
			return DatabaseAccess.GetDatabaseType().CreateConnectionString(databasePath, databaseName, dbUser, password);      
        }

        public static DatabaseAccess NewDatabaseAccess()
        {			
            if (Database.ConnectionString == null | Database.ConnectionString == "")
            {
                return new DatabaseAccess("");
            }
            else
			{
                return new DatabaseAccess(Database.ConnectionString);
            }
        }

        /*public static DatabaseAccess NewDatabaseAccess(string databasePath, string databaseName)
        {
            return new DatabaseAccess(CreateConnectionString(databasePath, databaseName));
        }*/

        /// <summary>
        /// Formats a string so it can be used in a SQL statement. So ' is replaced by '' etc.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatSqlString(string str)
        {
            return str.Replace("'", "''");
        }

        public static bool CreateOrUpdate(ActiveRecord record)
        {
            if (RecordExists(record))
            {
                return Update(record);
            }
            else
            {
                return Create(record);
            }
        }

        public static bool Create(ActiveRecord record)
        {
            // the current user either isn't in the ACL list (so they obviously don't have permission to delete the object
            // or the current user doesn't have sufficient permissions to delete the object.
            if (!record.ACL.CanWrite(VolunteerTracker.Database.GetUserId()))
            {
                return false;
            }
            
            Type type = record.GetType();
            string tableName = Util.Pluralize(type.Name);         
			using (DatabaseAccess dbAccess = new DatabaseAccess(Database.ConnectionString)) {
            	System.Data.IDbConnection connection = dbAccess.Connect();
				record.CreatedDate = DateTime.Now;
				record.ModifiedDate = DateTime.Now;
				record.ModifiedBy = Math.Min(GetUserId(), Int32.MaxValue);
            	string commandText = @"insert into " + tableName + " (";
            	List<string> propertyNames = GetPropertyNames(record);
            	foreach (string propertyName in propertyNames)
            	{
                	commandText += propertyName + ", ";                
            	}
            	commandText = commandText.Trim(new char[] { ',', ' ' });
            	commandText += ") values (";
            	foreach (string propertyName in propertyNames)
            	{
                	if (propertyName == "Id" && record.Id == -1)
                	{
						if (DatabaseAccess.GetDatabaseType() is SQLite)
						{
                    		commandText += "NULL, ";						
						} 
						else if (DatabaseAccess.GetDatabaseType() is PSQL)
						{
							commandText += "DEFAULT, ";
						} else {
							throw new Exception("Unknown DatabaseType:" + DatabaseAccess.GetDatabaseType());
						}
					
                	}
                	else
                	{
                    	commandText += "@" + propertyName + ", ";
                	}
            	}
            	commandText = commandText.Trim(new char[] { ',', ' ' });
            	commandText += ")";            
            	using (IDbCommand command = connection.CreateCommand()) 
				{
					command.CommandText = commandText;
            		AddParameters(command, record);
            		foreach (IDbDataParameter p in command.Parameters)
            		{						
                		if (p.ParameterName == "@Id" && record.Id == -1)
                		{
                    		command.Parameters.Remove(p);
                    		break;
                		}
            		}					
            		command.ExecuteNonQuery();  
					if (record.Id == -1)
					{
						if (DatabaseAccess.GetDatabaseType() is SQLite)
						{
							command.CommandText = "select last_insert_rowid()";					
						} else if (DatabaseAccess.GetDatabaseType() is PSQL)
						{
							command.CommandText = "select currval('" + tableName + "_Id_seq')";					
						} else 
						{
							throw new Exception("Unknown DatabaseType:" + DatabaseAccess.GetDatabaseType());
						}
						using (IDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{						
								record.Id = (long)reader[0];
							} else 
							{
								throw new Exception("Can't Read Row");
							}
						}
					}
				
				}
			}
			return true;
        }

        public static bool Update(ActiveRecord record)
        {
            // the current user either isn't in the ACL list (so they obviously don't have permission to delete the object
            // or the current user doesn't have sufficient permissions to delete the object.
            if (!record.ACL.CanWrite(VolunteerTracker.Database.GetUserId()))
            {
                return false;
            }
            
            Type type = record.GetType();
            string tableName = Util.Pluralize(type.Name);            
            using (DatabaseAccess dbAccess = new DatabaseAccess(Database.ConnectionString)) {
				IDbConnection connection = dbAccess.Connect();            

            	record.ModifiedDate = DateTime.Now;
				record.ModifiedBy = Math.Min(GetUserId(), Int32.MaxValue);
            	string commandText = @"update " + tableName + " set ";
            	List<string> propertyNames = GetPropertyNames(record);
            	for (int i = 0; i < propertyNames.Count; i++)
            	{
                	string propertyName = propertyNames[i];
                	if (i < propertyNames.Count - 1)
                	{
                    	commandText += " " + propertyName + " = @" + propertyName + ", ";
                	}
                	else
                	{
                    	commandText += " " + propertyName + " = @" + propertyName + " ";
                	}
            	}
            	commandText +=" where Id = @Id";
            	using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = commandText;
            		AddParameters(command, record);
            		command.ExecuteNonQuery();
				}            	
			}
			return true;
        }

        public static List<string> GetPropertyNames(ActiveRecord record)
        {
            List<string> propertyNames = new List<string>();
            foreach (PropertyInfo info in record.GetType().GetProperties())
            {                
                bool skip = false;

                foreach (Attribute attr in info.GetCustomAttributes(true))
                {
                    if (attr is ActiveRecordDontSerializeAttribute)
                    {
                        skip = true;
                        break;
                    }
                }
                if (!skip)
                {
                    propertyNames.Add(info.Name);    
                }
                
            }
            return propertyNames;
        }
		
        
        public static DbType GetDbType(Type type)
        {
            DbType dType = DbType.Object;            
            switch (type.Name)
            {
                case "Int16":
                    dType = DbType.Int16;
                    break;
                case "Int32":
                    dType = DbType.Int32;
                    break;
                case "Int64":
                    dType = DbType.Int64;
                    break;
                case "String":
                    dType = DbType.String;
                    break;
                case "Decimal":
                    dType = DbType.Decimal;
                    break;
                case "Boolean":
                    dType = DbType.Boolean;
                    break;
                case "Byte":
                    dType = DbType.Byte;
                    break;
                case "DateTime":
                    dType = DbType.DateTime;
                    break;
                case "Guid":
                    dType = DbType.Guid;
                    break;
                default:
                    dType = DbType.String;					
                    break;
            }
            return dType;
            
        }

        public static void AddParameters(IDbCommand command, ActiveRecord record)
        {
            Type type = record.GetType();
            IDbDataParameter pId = command.CreateParameter();
			pId.ParameterName = "@Id";			
            PropertyInfo id = type.GetProperty("Id");			
            pId.DbType = GetDbType(id.PropertyType);
            
            pId.Value = id.GetValue(record, null);
            command.Parameters.Add(pId);
            foreach (PropertyInfo pInfo in type.GetProperties())
            {
                if (pInfo.Name == "Id")
                {
                    continue;
                }
                IDbDataParameter sParam = command.CreateParameter();
				sParam.ParameterName = "@" + pInfo.Name;
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
                        MethodInfo serializeMethod = type.GetMethod("Serialize" + pInfo.Name, new Type[0]);
                        sParam.Value = serializeMethod.Invoke(record, null).ToString();
						sParam.DbType = GetDbType(typeof(String));
                    }
                    else
                    {
                        sParam.Value = pInfo.GetValue(record, null);
						sParam.DbType = GetDbType(pInfo.PropertyType);
                    }
                    
                    command.Parameters.Add(sParam);
                }
            }            
            
        }

        

        public static bool RecordExists<E>(E record)
        {
			bool readerHasRows = false;
            using (DatabaseAccess dbAccess = new DatabaseAccess(Database.ConnectionString))
			{
            	string tableName = Util.Pluralize(record.GetType().Name);
            
            	IDbConnection connection = dbAccess.Connect();				            
            	string commandText = @"select Id from " + tableName + " where Id = @Id";
            	using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = commandText;			

            		IDbDataParameter pId = command.CreateParameter();
					pId.ParameterName = "@Id";			
            		pId.DbType = System.Data.DbType.Int64;
            		Type recordType = typeof(E);
            		PropertyInfo id = recordType.GetProperty("Id");
            		pId.Value = (long)id.GetValue(record, null);
            		if ((long)pId.Value == -1)
            		{
                		return false;
            		}
            

            		command.Parameters.Add(pId);
            		using (IDataReader reader = command.ExecuteReader())
					{
						readerHasRows = reader.Read();	            
					}
				}
			}
            return readerHasRows;
        }


        
		
     
	

        public static bool HasRows<T>(string tableName, string columnName, T rowValue)
        {

			bool readerHasRows = false;
            using (DatabaseAccess dbAccess = new DatabaseAccess(Database.ConnectionString))
			{
            	IDbConnection connection = dbAccess.Connect();            
            	string commandText = @"select " + columnName + " from " + tableName + " where " + columnName + " = @" + columnName;            	
				using (IDbCommand command = connection.CreateCommand()) {
					command.CommandText = commandText;
            		IDbDataParameter pId = command.CreateParameter();
					pId.ParameterName = "@" + columnName;			
            		if (rowValue is string)
            		{
                		pId.DbType = System.Data.DbType.String;
            		}
            		else if (rowValue is Guid)
            		{
                		pId.DbType = System.Data.DbType.Guid;
            		}
            		else
            		{
                		throw new Exception("Unknown type:" + rowValue.GetType().ToString());
            		}
            		pId.Value = rowValue;
            		command.Parameters.Add(pId);
				
            		using (IDataReader reader = command.ExecuteReader())
					{
            			readerHasRows = reader.Read();            	
					}
				}
			}
			return readerHasRows;
        }        

   



    }
}

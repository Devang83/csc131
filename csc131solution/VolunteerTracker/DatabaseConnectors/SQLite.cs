using Mono.Data.Sqlite;
using System;
using System.Data;

namespace VolunteerTracker
{


	public class SQLite : DatabaseType
	{

		public SQLite ()
		{
		}
		
		public override IDbConnection NewConnection(string connectionString)
		{			
			return new Mono.Data.Sqlite.SqliteConnection(connectionString);
		}
		
		public override IDataParameter CreateParameter(string name, DbType dbType, object value)
		{
			SqliteParameter p = new SqliteParameter(name);
            p.DbType = dbType;
            p.Value = value;
            return p;
		}
		
		public override string CreateConnectionString(string filePath, string dbName, string dbUser, string password)
		{
			
            string filePathName;    
			if (filePath == "") {
					filePathName = dbName;
			}
             else if (filePath == "|DataDirectory|")
            {
                filePathName = filePath + dbName;
            }
            else
            {
                if (filePath[filePath.Length - 1] != System.IO.Path.DirectorySeparatorChar) 
				{
					filePathName = filePath + System.IO.Path.DirectorySeparatorChar + dbName;
				} else 
				{
					filePathName = filePath + dbName;
				} 				
            }            
			
            return "Data Source=" + filePathName; 
		}
		
		public override string GetDbColumnType (Type type)
		{
			return GetDbColumnType(type, -1);
		}
		
		public override string GetDbColumnType (Type type, long length)
		{			
			if (type == typeof(Int32))
			{
				return "integer";
			}
			
			if (type == typeof(Int64))
			{
				return "integer";
			}
			
			if (type == typeof(string))
			{				
				return "text";				
			}
			
			if (type == typeof(DateTime))
			{
				return "datetime";
			}
			
			if (type == typeof(Decimal))
			{
				return "money";
			}
			
			if (type == typeof(Boolean))
			{
				return "bit";
			}
			return "text";
		}
		
		public override string GetPrimaryKey()
		{
			return " integer primary key ";
		}

	}
}

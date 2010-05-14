
using System;
using System.Data;

namespace VolunteerTracker
{


	public abstract class DatabaseType
	{

		public DatabaseType ()
		{
		}
		
		public abstract IDbConnection NewConnection(string connectionString);
		
		public abstract IDataParameter CreateParameter(string name, DbType dbType, object value);
		
		public abstract string CreateConnectionString(string filePath, string dbName, string dbUser, string password);
		
		
		public abstract string GetDbColumnType(Type type);
		
		public abstract string GetDbColumnType(Type type, long length);
		
		public abstract string GetPrimaryKey();			
			
	}
}

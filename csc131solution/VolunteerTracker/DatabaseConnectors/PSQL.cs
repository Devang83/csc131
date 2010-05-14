
using System;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace VolunteerTracker
{


	public class PSQL : DatabaseType
	{

		public PSQL ()
		{			
		}
		
		public override IDbConnection NewConnection(string connectionString)
		{			
			Npgsql.NpgsqlEventLog.Level = LogLevel.None;
			//Npgsql.NpgsqlEventLog.LogName = "Npgsql.logfile";
			return new Npgsql.NpgsqlConnection(connectionString);
		}
		
		public override IDataParameter CreateParameter(string name, DbType dbType, object value)
		{
			Npgsql.NpgsqlParameter p = new Npgsql.NpgsqlParameter(name, dbType);
			p.Value = value;
			return p;
		}
		
		public override string CreateConnectionString(string filePath, string dbName, string dbUser, string password)
		{
			return 	"Server=localhost;" +
          			"Database=" + dbName + ";" +
          			"User ID=" + dbUser + ";" +
          			"Password=" + password + "; Pooling = True;";
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
				int defaultLength = 256;
				if (length == -1)
				{
					return "varchar(1024)";
				} else if (length == long.MaxValue)
				{
					return "text";
				}
				else 
				{
					return "varchar(" + length + ")";
				}
			}
			
			if (type == typeof(DateTime))
			{
				return "timestamp with time zone";
			}
			
			if (type == typeof(Decimal))
			{
				return "numeric";
			}
			
			if (type == typeof(Boolean))
			{
				return "boolean";
			}
			return "text";
		}
		
		public override string GetPrimaryKey()
		{
			return " serial primary key ";
		}

	}
}

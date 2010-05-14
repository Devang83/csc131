
using System;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace QuickPM
{


	public class PSQL : DatabaseType
	{

		public PSQL ()
		{			
		}
		
		public override IDbConnection NewConnection(string connectionString)
		{			
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
          			"Database=" + databaseName + ";" +
          			"User ID=" + dbUser + ";" +
          			"Password=" + password + ";";
		}
	}
}

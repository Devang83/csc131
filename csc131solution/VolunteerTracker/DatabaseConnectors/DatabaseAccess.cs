using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VolunteerTracker
{    
    /// <summary>
    /// Does all sql accesses with the database. This way we can switch databases without having to
    /// alter any other code.
    /// </summary>
    public class DatabaseAccess : IDisposable
    {
        private string connectionString = "";
        private IDbConnection connection = null;        
		private IDbCommand command = null;
        private static DatabaseType databaseType = new PSQL();
		
		public static DatabaseType GetDatabaseType()
		{
			return databaseType;
		}
		
		public static void SetDatabaseType(DatabaseType dbType)
		{
			databaseType = dbType;
		}
		
        public DatabaseAccess(string connectionString)
        {
            this.connectionString = connectionString;
            Connect();
        }		

        public IDbConnection Connect()
        {   
			if (connection != null && connection.State == ConnectionState.Open)
			{
				return connection;
			}
			if (connection != null && connection.State != ConnectionState.Open)
			{
				connection.Close();
				connection.Dispose();
				connection = null;
			}
            if (connection == null)
			{				
				connection = databaseType.NewConnection(connectionString);            
            	connection.Open();
			}
			return connection;
        }			

        public void Dispose()
        {							
			if (command != null)
			{
				command.Dispose();
				command = null;
			}
            connection.Close();
			connection.Dispose();
			connection = null;
			GC.SuppressFinalize(this);				            
        }
		
		~ DatabaseAccess()
		{
			if (command != null)
			{
				command.Dispose();
				command = null;
			}
			if (connection != null)
			{
				connection.Close();
				connection.Dispose();
				connection = null;
			}
		}
		
	

        public string KillForeignKeyConstraints(string sqlString)
        {
            Regex reg = new Regex("foreign key references [^,]+");
            Match m = reg.Match(sqlString);
            while (m.Success)
            {
                sqlString = sqlString.Replace(m.Value, "");
                m = m.NextMatch();
            }
            return sqlString;            
        }

        public int ExecuteNonQuery(string insertString)
        {
            return ExecuteNonQuery(insertString, new List<IDataParameter>());
        }

        public static IDataParameter CreaterParameter(string name, DbType dbType, object val)
        {
			return databaseType.CreateParameter(name, dbType, val);            
        }        

        public int ExecuteNonQuery(string insertString, List<IDataParameter> parameters)
        {
			int retVal = 0;
            using (IDbCommand command = connection.CreateCommand()) {
            	foreach (IDataParameter p in parameters)
            	{
                	command.Parameters.Add(p);
            	}
            	command.CommandText = KillForeignKeyConstraints(insertString);                        
				retVal = command.ExecuteNonQuery();
			}
            return retVal;
        }

        public IDataReader ExecuteReader(string selectString)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            return ExecuteReader(selectString, parameters);
        }



        public IDataReader ExecuteReader(string selectString, List<IDataParameter> parameters)
        {
            command = connection.CreateCommand();
            foreach (IDataParameter p in parameters)
            {
                command.Parameters.Add(p);
            }
            command.CommandText = KillForeignKeyConstraints(selectString);                        
            return command.ExecuteReader();
        }

        public string GetConnectionString()
        {
            return connectionString;
        }
       
    }
}

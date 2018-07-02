using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SlackRecorder.DAL
{
    public abstract class DbConnection: IDisposable
    {
        protected readonly SqlConnection Connection;

        protected DbConnection()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CallsDB"].ToString());
            InitializeConnection();
        }

        public void Dispose()
        {
            this.Connection.Close();
        }

        private void InitializeConnection()
        {
            this.Connection.Open();
        }
    }
}
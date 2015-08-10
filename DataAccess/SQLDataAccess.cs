using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class SQLDataAccess : DataAccess
    {
        #region Variables
        private SqlConnection _connection;
        #endregion
    
        #region Contructor
        public SQLDataAccess()
        {
            _connectionString = GetConnectionString(Connections.SqlDataAccess);
        }
        #endregion

        #region Methods

        #region Public
        public override void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed) _connection.Close();
                _connection.Dispose();
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Execute the Command
        /// </summary>
        /// <param name="cmd">Command to Execute</param>
        /// <returns>DataTable</returns>
        protected DataTable ExecuteGetData(SqlCommand cmd)
        {
            try
            {
                var dataTable = new DataTable();
                cmd.Connection.Open();
                var dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                SaveInLog(e.Message);
                return null;
            }
        }

        protected SqlConnection GetConnection()
        {
            return _connection ?? (_connection = new SqlConnection(_connectionString));
        }
        #endregion

        #region Private
        /// <summary>
        /// Close the connecction
        /// </summary>
        private void CloseConnection()
        {
            if (_connection == null) return;
            if (_connection.State != ConnectionState.Closed) _connection.Close();
        }
        #endregion

        #endregion
    }
}

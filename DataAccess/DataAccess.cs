using System;
using Library.Entities;

namespace DataAccess
{
    public abstract class DataAccess : IDisposable
    {
        #region Enumerators
        /// <summary>
        /// Name of Connections
        /// </summary>
        public enum Connections
        {
            SqlDataAccess
        }
        #endregion

        #region Variables
        /// <summary>
        /// Connection String
        /// </summary>
        protected string _connectionString;
        /// <summary>
        /// Log File Path
        /// </summary>
        protected string _logFilePath;
        #endregion

        #region Properties
        /// <summary>
        /// Return the Connection String
        /// </summary>
        public String ConnectionString { get { return _connectionString; } }
        #endregion

        #region Constructor

        protected DataAccess()
        {
            _connectionString = string.Empty;
            //_logFilePath = AppDomain.CurrentDomain.BaseDirectory + CSUtilities.GetAppValue("SQLErrorLogFile") + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            _logFilePath = Utilities.GetParentPath(Utilities.GetAppValue("SQLErrorLogFolder")) + "\\"
                + Utilities.GetAppValue("SQLErrorLogFile") + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        }

        public abstract void Dispose();

        #endregion

        #region Methods

        #region Internal
        /// <summary>
        /// Get a Specific Connection String
        /// </summary>
        /// <param name="connection">Name of the Connection String</param>
        /// <returns>Connection String</returns>
        internal string GetConnectionString(Connections connection)
        {
            try
            {
                return Utilities.GetConnectionString(connection.ToString());
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Save exceptions into Log File
        /// </summary>
        /// <param name="text">Text to save</param>
        public void SaveInLog(string text)
        {
            try
            {
                Utilities.WriteInFile(text, _logFilePath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #endregion
    }
}

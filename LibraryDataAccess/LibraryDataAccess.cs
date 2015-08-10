using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace Library.DataAccess
{
    public class LibraryDataAccess : SQLDataAccess
    {
        #region Constructor
        public LibraryDataAccess()
        {

        }
        #endregion

        #region Methods
        public DataTable GetBookList(string bookName, string authorName, string category, int order)
        {
            SqlCommand cmd = new SqlCommand("SP_SearchBooks", GetConnection()) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@BookName", SqlDbType.VarChar, 100).Value = bookName;
            cmd.Parameters.Add("@BookAuthor", SqlDbType.VarChar, 50).Value = authorName;
            cmd.Parameters.Add("@BookCategory", SqlDbType.VarChar, 20).Value = category;
            cmd.Parameters.Add("@Order", SqlDbType.Int).Value = order;

            return ExecuteGetData(cmd);
        }
        #endregion
    }
}

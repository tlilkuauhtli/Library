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
        /// <summary>
        /// Get book list base on the filter data and order
        /// </summary>
        /// <param name="bookName">Book name filter</param>
        /// <param name="authorName">Author name filter</param>
        /// <param name="category">Category filter</param>
        /// <param name="order">Order</param>
        /// <returns>List of books</returns>
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

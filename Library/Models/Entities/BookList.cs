using System.Data;
using System.Collections.Generic;


namespace Library.Models.Entities
{
    public class BookList
    {
        #region Variables
        private readonly List<Book> _bookList;
        #endregion

        #region Properties
        public List<Book> Books
        {
            get { return _bookList; }
        }
        #endregion

        public BookList()
        {
            _bookList = new List<Book>();
        }

        public void AddData(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    var counter = dt.Rows.Count;
                    for (var index = 0; index < counter; index++)
                    {
                        Book newBook = ProcessDataRow(dt.Rows[index]);
                        _bookList.Add(newBook);
                    }
                }
            }
        }

        private Book ProcessDataRow(DataRow dr)
        {
            if (dr != null)
            {
                if (dr.ItemArray.Length > 0)
                {
                    var newBook = new Book
                    {
                        Id = (int)dr["BookId"],
                        Name = (string)dr["BookName"],
                        AuthorName = (string)dr["AuthorName"],
                        CategoryName = (string)dr["CategoryName"]
                    };

                    return newBook;

                }
            }

            return null;
        }

    }
}
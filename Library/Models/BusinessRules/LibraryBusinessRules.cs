using Library.Models.Entities;
using Library.DataAccess;

namespace Library.Models.BusinessRules
{
    public class LibraryBusinessRules
    {
        public BookList GetBookList(BookListRequest bookListRequest)
        {
            BookList bookList;
            using (var dataAccess = new LibraryDataAccess())
            {
                var dt = dataAccess.GetBookList(bookListRequest.BookName, bookListRequest.AuthorName, bookListRequest.CategoryName, bookListRequest.Order);
                bookList = new BookList();
                bookList.AddData(dt);
            }

            return bookList;
        }
    }
}
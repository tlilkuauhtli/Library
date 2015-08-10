
namespace Library.Models.Entities
{
    public class BookListRequest
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public int Order { get; set; }

        public BookListRequest(string bookName, string authorName, string categoryName, int order)
        {
            BookName = bookName;
            AuthorName = authorName;
            CategoryName = categoryName;
            Order = order;
        }
    }
}
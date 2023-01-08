namespace Pustok_book_sales_app.Models
{
    public class Author
    {

        public int Id { get; set; }
        public string FullName { get; set; }

        public List<Book> Books { get; set; }
    }
}

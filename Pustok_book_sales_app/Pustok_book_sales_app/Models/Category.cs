namespace Pustok_book_sales_app.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}

using Pustok_book_sales_app.Models;

namespace Pustok_book_sales_app.ViewModel
{
    public class HomeViewModel
    {
        public List<Hero> Heroes { get; set; }
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> NewBooks { get; set; }
        public List<Book> DiscountBooks { get; set; }

    }
}

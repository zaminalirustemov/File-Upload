using Microsoft.EntityFrameworkCore;

namespace Pustok_book_sales_app.Models
{
    public class PustokDbContext:DbContext
    {
        public PustokDbContext(DbContextOptions<PustokDbContext> options):base(options) {}

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
    }
}

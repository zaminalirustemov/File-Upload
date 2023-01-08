using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok_book_sales_app.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }


        [StringLength(maximumLength: 100)]
        public string? ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPrice { get; set; }
        public bool IsAvailable { get; set; }
        public string Code { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public Author? Author { get; set; }
        public Category? Category { get; set; }
    }
}

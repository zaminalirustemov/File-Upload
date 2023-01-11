using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok_book_sales_app.Models
{
    public class Hero
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100)]
        public string? ImageUrl { get; set; }
        [StringLength(maximumLength:20)]
        public string TitleUp { get; set; }
        [StringLength(maximumLength: 20)]
        public string TitleDown { get; set; }
        [StringLength(maximumLength: 300)]
        public string Description { get; set; }
        public double Price { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

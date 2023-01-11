using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_book_sales_app.Models;
using Pustok_book_sales_app.ViewModel;
using System.Diagnostics;

namespace Pustok_book_sales_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly PustokDbContext _pustokDbContext;

        public HomeController(PustokDbContext pustokDbContext)
        {
            _pustokDbContext = pustokDbContext;
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Heroes = _pustokDbContext.Heroes.ToList(),
                FeaturedBooks = _pustokDbContext.Books.
                                    Include(x=>x.Author).
                                    Include(x=>x.BookImages).
                                    Where(x=>x.IsFeatured).ToList(),
                NewBooks = _pustokDbContext.Books.
                                    Include(x => x.Author).
                                    Include(x => x.BookImages).
                                    Where(x => x.IsNew ).ToList(),
                DiscountBooks = _pustokDbContext.Books.
                                    Include(x => x.Author).
                                    Include(x => x.BookImages).
                                    Where(x => x.DiscountPrice >0).ToList()
            };
            return View(homeViewModel);
        }

        public IActionResult Detail(int id)
        {
            Book book = _pustokDbContext.Books
                                .Include(x=>x.Author)
                                .Include(x=>x.Category)
                                .Include(x=>x.BookImages)
                                .FirstOrDefault(x => x.Id == id);
            if (book == null) return View("Error");

            BookViewModel bookVM = new BookViewModel
            {
                Book = book,
                Books=_pustokDbContext.Books.Where(x=>x.IsFeatured).ToList()
            };
            return View(bookVM);
        }

    }
}
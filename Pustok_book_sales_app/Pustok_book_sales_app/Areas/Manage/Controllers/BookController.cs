using Microsoft.AspNetCore.Mvc;
using Pustok_book_sales_app.Helpers;
using Pustok_book_sales_app.Models;

namespace Pustok_book_sales_app.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly PustokDbContext _pustokDbContext;

        public readonly IWebHostEnvironment _environment;

        public BookController(PustokDbContext pustokDbContext, IWebHostEnvironment environment)
        {
            _pustokDbContext = pustokDbContext;
            _environment = environment;
        }

        //Read-----------------------------------
        public IActionResult Index()
        {
            List<Book> books = _pustokDbContext.Books.ToList();
            return View(books);
        }


        //Create---------------------------------
        public IActionResult Create()
        {
            ViewBag.Authors = _pustokDbContext.Authors.ToList();
            ViewBag.Category = _pustokDbContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (book.ImageFile.ContentType != "image/png" && book.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                return View();
            }
            if (book.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                return View();
            }

            book.ImageUrl = book.ImageFile.SaveFile(_environment.WebRootPath, "uploads/books");


            ViewBag.Authors = _pustokDbContext.Authors.ToList();
            ViewBag.Category = _pustokDbContext.Categories.ToList();

            if (!ModelState.IsValid) return View();

            _pustokDbContext.Books.Add(book);
            _pustokDbContext.SaveChanges();

            return RedirectToAction("index");
        }



        //Edit-------------------------------------

        public IActionResult Edit(int id)
        {

            ViewBag.Authors = _pustokDbContext.Authors.ToList();
            ViewBag.Category = _pustokDbContext.Categories.ToList();
            Book book = _pustokDbContext.Books.Find(id);

            if (book is null) return View("Error");

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book newBook)
        {
            Book existBook = _pustokDbContext.Books.Find(newBook.Id);
            if (newBook.ImageFile!=null)
            {

                if (newBook.ImageFile.ContentType != "image/png" && newBook.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                    return View();
                }

                if (newBook.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                    return View();
                }
            }

            FileManager.DeleteFile(_environment.WebRootPath, "uploads/books", existBook.ImageUrl);
            newBook.ImageUrl = newBook.ImageFile.SaveFile(_environment.WebRootPath, "uploads/books");


            ViewBag.Authors = _pustokDbContext.Authors.ToList();
            ViewBag.Category = _pustokDbContext.Categories.ToList();

            if (existBook is null) return View("Error");

            existBook.AuthorId = newBook.AuthorId;
            existBook.CategoryId = newBook.CategoryId;
            existBook.Name = newBook.Name;
            existBook.Description = newBook.Description;
            existBook.CostPrice = newBook.CostPrice;
            existBook.DiscountPrice = newBook.DiscountPrice;
            existBook.SalePrice = newBook.SalePrice;
            existBook.IsAvailable = newBook.IsAvailable;
            existBook.Code = newBook.Code;
            existBook.ImageUrl = newBook.ImageUrl;

            _pustokDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        //Delete-------------------------------------

        public IActionResult Delete(int id)
        {
            ViewBag.Authors = _pustokDbContext.Authors.ToList();
            ViewBag.Category = _pustokDbContext.Categories.ToList();
            Book book = _pustokDbContext.Books.Find(id);
            if (book is null) return View("Error");
            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _pustokDbContext.Books.Remove(book);
            _pustokDbContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}

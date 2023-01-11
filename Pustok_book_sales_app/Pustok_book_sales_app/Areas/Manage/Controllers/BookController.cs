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
            ViewBag.Authors = _pustokDbContext.Authors.ToList();
            ViewBag.Category = _pustokDbContext.Categories.ToList();

            if (!ModelState.IsValid) return View();
            //PosterImage------------------
            if (book.PosterImageFile != null)
            {
                if (book.PosterImageFile.ContentType != "image/png" && book.PosterImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                    return View();
                }
                if (book.PosterImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                    return View();
                }

                BookImage bookImage = new BookImage
                {
                    Book = book,
                    ImageUrl = book.PosterImageFile.SaveFile(_environment.WebRootPath, "uploads/books"),
                    IsPoster = true
                };
                _pustokDbContext.BookImages.Add(bookImage);
            }
            //HoverImage----------------
            if (book.HoverImageFile != null)
            {
                if (book.HoverImageFile.ContentType != "image/png" && book.HoverImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                    return View();
                }
                if (book.HoverImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                    return View();
                }

                BookImage bookImage = new BookImage
                {
                    Book = book,
                    ImageUrl = book.HoverImageFile.SaveFile(_environment.WebRootPath, "uploads/books"),
                    IsPoster = false
                };
                _pustokDbContext.BookImages.Add(bookImage);
            }

            //MultipleImageFile--------------------------
            if (book.ImageFiles !=null)
            {
                foreach (IFormFile imageFile in book.ImageFiles)
                {

                    if (imageFile.ContentType != "image/png" && imageFile.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                        return View();
                    }
                    if (imageFile.Length > 2097152)
                    {
                        ModelState.AddModelError("ImageFiles", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                        return View();
                    }

                    BookImage bookImage = new BookImage
                    {
                        Book = book,
                        ImageUrl = imageFile.SaveFile(_environment.WebRootPath, "uploads/books"),
                        IsPoster=null
                    };
                    _pustokDbContext.BookImages.Add(bookImage);
                }
            }

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
            //PosterImage------------------
            if (newBook.PosterImageFile != null)
            {
                if (newBook.PosterImageFile.ContentType != "image/png" && newBook.PosterImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                    return View();
                }
                if (newBook.PosterImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                    return View();
                }

                BookImage bookImage = new BookImage
                {
                    Book = newBook,
                    ImageUrl = newBook.PosterImageFile.SaveFile(_environment.WebRootPath, "uploads/books"),
                    IsPoster = true
                };
                _pustokDbContext.BookImages.Add(bookImage);
            }
            //HoverImage----------------
            if (newBook.HoverImageFile != null)
            {
                if (newBook.HoverImageFile.ContentType != "image/png" && newBook.HoverImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                    return View();
                }
                if (newBook.HoverImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                    return View();
                }

                BookImage bookImage = new BookImage
                {
                    Book = newBook,
                    ImageUrl = newBook.HoverImageFile.SaveFile(_environment.WebRootPath, "uploads/books"),
                    IsPoster = false
                };
                _pustokDbContext.BookImages.Add(bookImage);
            }

            //MultipleImageFile--------------------------
            if (newBook.ImageFiles != null)
            {
                foreach (IFormFile imageFile in newBook.ImageFiles)
                {

                    if (imageFile.ContentType != "image/png" && imageFile.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                        return View();
                    }
                    if (imageFile.Length > 2097152)
                    {
                        ModelState.AddModelError("ImageFiles", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                        return View();
                    }

                    BookImage bookImage = new BookImage
                    {
                        Book = newBook,
                        ImageUrl = imageFile.SaveFile(_environment.WebRootPath, "uploads/books"),
                        IsPoster = null
                    };
                    _pustokDbContext.BookImages.Add(bookImage);
                }
            }


            FileManager.DeleteFile(_environment.WebRootPath, "uploads/books", existBook.ImageUrl);


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

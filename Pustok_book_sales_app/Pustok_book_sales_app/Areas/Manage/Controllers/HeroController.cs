using Microsoft.AspNetCore.Mvc;
using Pustok_book_sales_app.Helpers;
using Pustok_book_sales_app.Models;

namespace Pustok_book_sales_app.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HeroController : Controller
    {
        private readonly PustokDbContext _pustokDbContext;

        public readonly IWebHostEnvironment _environment;

        public HeroController(PustokDbContext pustokDbContext, IWebHostEnvironment environment)
        {
            _pustokDbContext = pustokDbContext;
            _environment = environment;
        }

        //Read-----------------------------------
        public IActionResult Index()
        {
            List<Hero> heroList = _pustokDbContext.Heroes.ToList();
            return View(heroList);
        }


        //Create---------------------------------
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Hero hero)
        {
            if (hero.ImageFile.ContentType != "image/png" && hero.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                return View();
            }
            if (hero.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                return View();
            }


            hero.ImageUrl = hero.ImageFile.SaveFile(_environment.WebRootPath, "uploads/heroes");

            if (!ModelState.IsValid) return View();

            _pustokDbContext.Heroes.Add(hero);
            _pustokDbContext.SaveChanges();

            return RedirectToAction("index");
        }



        //Edit-------------------------------------

        public IActionResult Edit(int id)
        {
            Hero hero = _pustokDbContext.Heroes.Find(id);

            if (hero is null) return View("Error");

            return View(hero);
        }

        [HttpPost]
        public IActionResult Edit(Hero newHero)
        {
            Hero existHero = _pustokDbContext.Heroes.Find(newHero.Id);
            if (existHero is null) return View("Error");
            if (newHero.ImageUrl != null)
            {

                if (newHero.ImageFile.ContentType != "image/png" && newHero.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Yalniz png ve jpeg fayillari yuklemek mumkundur");
                    return View();
                }

                if (newHero.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Olcusu 2 mb'dan artiq sekil yuklemek mumkun deyil");
                    return View();
                }
            }
            FileManager.DeleteFile(_environment.WebRootPath, "uploads/heroes", existHero.ImageUrl);
            existHero.ImageUrl = newHero.ImageFile.SaveFile(_environment.WebRootPath, "uploads/heroes");

            existHero.TitleUp = newHero.TitleUp;
            existHero.TitleDown = newHero.TitleDown;
            existHero.Description = newHero.Description;
            existHero.Price = newHero.Price;

            _pustokDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        //Delete-------------------------------------

        public IActionResult Delete(int id)
        {
            Hero hero = _pustokDbContext.Heroes.Find(id);
            if (hero is null) return View("Error");
            return View(hero);
        }

        [HttpPost]
        public IActionResult Delete(Hero hero)
        {

            _pustokDbContext.Heroes.Remove(hero);
            _pustokDbContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}

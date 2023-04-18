using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication4.Controllers
{
    public class bookController : Controller
    {
        libraryContext db;
        public bookController(libraryContext db)
        {
            this.db = db;
        }
        public IActionResult create()
        {
            if (HttpContext.Session.GetInt32("student_id") == null)
            {
                return RedirectToAction("login", "student");
            }
            else
            {

                List<catalog> ct = db.catalogs.ToList();
                ViewBag.cat = new SelectList(ct, "cat_id", "cat_name");
                return View();
            }
        }
        [HttpPost]
        public IActionResult create(book b ,IFormFile photo)
        {
            string path = $"wwwroot/attatch/{photo.FileName}";
            FileStream fs = new FileStream(path, FileMode.Create);
            photo.CopyTo(fs);
            b.img =$"/attatch/{photo.FileName}";
            b.student_id = HttpContext.Session.GetInt32("student_id");
            b.date = DateTime.Now;
            db.books.Add(b);
            db.SaveChanges();




            return RedirectToAction("display");
        }
        public IActionResult display()
        {
            int?id=HttpContext.Session.GetInt32("student_id");
           List<book> b= db.books.Include(n=>n.cat).Where(n => n.student_id == id).OrderByDescending(n=>n.date).ToList();
           

            return View(b);
        }
        public IActionResult details(int id)
        {
            return View(db.books.Include(n => n.cat).Where(n => n.id == id).FirstOrDefault());
        }

        public IActionResult allbooks()
        {
            return View(db.books.Include(n => n.cat).Include(n => n.student).OrderByDescending(n => n.date).ToList());
        }
    }
}

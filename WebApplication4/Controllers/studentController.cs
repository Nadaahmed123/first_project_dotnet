using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class studentController : Controller
    {
        libraryContext db;
        public studentController(libraryContext db)
        { 
            this.db = db;
        }
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(student s)
        {
            db.students.Add(s);
            db.SaveChanges();
            return RedirectToAction("login");
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string name,string password)
        {
            student s=db.students.Where(n=>n.name==name && n.password==password).FirstOrDefault();
            if (s != null)
            {
                HttpContext.Session.SetInt32("student_id", s.id);
                return RedirectToAction("profile");

            }
            else
            {
                ViewBag.status = "incorrect name or password";
                return View();

            }
            return View();
        }
        public ActionResult profile()
        {
            int? id = HttpContext.Session.GetInt32("student_id");
            if (id == null)
            {

                return RedirectToAction("login");
            }
            student s=db.students.Where(n=>n.id==id).FirstOrDefault();
            return View(s);
        }
        public ActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }
    } 
}

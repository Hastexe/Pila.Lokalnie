using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MajsterFinale.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mainpage()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        //rejestracja na ten moment zawiera tylko dane do logowania, czyli podajemy login i hasło i po kliknięciu przycisku dane zostają zapisane w bazie
        [HttpPost]
        public ActionResult Registration(User obj)
        {
            if (ModelState.IsValid)
            {
                MajsterRejestracja db = new MajsterRejestracja();
                db.Users.Add(obj);
                db.SaveChanges();
                ViewBag.message = "Rejestracja przebiegła pomyślnie";
            }
            return View(obj);
        }
        public ActionResult Login()
        {
            return View();
        }

        //logowanie jak na razie bez sesji
        [HttpPost]
        public ActionResult Login(User User)
        {
            MajsterLogowanie db = new MajsterLogowanie();
            var userLoggedIn = db.Users.SingleOrDefault(x => x.Login == User.Login && x.Password == User.Password);
            if (userLoggedIn != null)
            {
                ViewBag.message = "Zalogowano";
                ViewBag.triedOnce = "Tak";
                return RedirectToAction("mainpage", "home");

            }
            else
            {
                ViewBag.triedOnce = "Tak";
                return View();
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
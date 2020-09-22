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
            if (Session["Login"] == null)
            {
                return RedirectToAction("Logowanie", "home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logowanie()
        {
            if (Session["Login"] != null)
            {
                return RedirectToAction("mainpage", "home", new { Login = Session["Login"].ToString() });
            }
            else
            {
                return View();
            }
        }

        //logowanie z sesją
        [HttpPost]
        public ActionResult Logowanie(User User)
        {
            BazaLocal db = new BazaLocal();
            var userLoggedIn = db.Users.SingleOrDefault(x => x.Login == User.Login && x.Password == User.Password);
            if (userLoggedIn != null)
            {
                ViewBag.message = "Zalogowano";
                ViewBag.triedOnce = "Tak";

                Session["Login"] = User.Login;


                return RedirectToAction("mainpage", "home", new { Login = User.Login });

            }
            else
            {
                ViewBag.triedOnce = "Tak";
                return View();
            }
        }
        public ActionResult Rejestracja(int id = 0)
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public ActionResult Rejestracja(User user)
        {
            using (BazaLocal db = new BazaLocal())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Rejestracja przebiegła pomślnie";
            return View("Rejestracja", new User());
        }
        /*public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }*/

        public ActionResult AddAdvertisement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdvertisement(Advertisement obj)

        {
            if (ModelState.IsValid)
            {
                BazaLocal db = new BazaLocal();
                obj.Specialist = Session["Login"].ToString();
                obj.Customer = Session["Login"].ToString();
                db.Advertisements.Add(obj);
                db.SaveChanges();
            }
            return View(obj);
        }
        public ActionResult Regulamin()
        {
            ViewBag.Message = "Tutaj będzie regulamin";

            return View();
        }
        public ActionResult Message()
        {

            return View();
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
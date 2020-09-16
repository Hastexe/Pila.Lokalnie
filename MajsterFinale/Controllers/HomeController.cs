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
                return RedirectToAction("login", "home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Registration()
        {
            return View();
        }

        //Rejestracja, gdzie podajemy Login 2x hasło i 2xmaila
        [HttpPost]
        public ActionResult Registration(User obj)
        {
            if (ModelState.IsValid)
            {
                BazaLocal db = new BazaLocal();
                db.Users.Add(obj);
                db.SaveChanges();
                ViewBag.message = "Rejestracja przebiegła pomyślnie";
                //return RedirectToAction("login", "home");
            }
            //ModelState.Clear();
            //ViewBag.SuccessMessage = "Rejestracja przebiegła pomyślnie";
            return View(obj);
        }
        public ActionResult Login()
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
        public ActionResult Login(User User)
        {
            BazaLocal db = new BazaLocal();
            var userLoggedIn = db.Users.SingleOrDefault(x => x.Login == User.Login && x.Password == User.Password);
            if (userLoggedIn != null)
            {
                ViewBag.message = "Zalogowano";
                ViewBag.triedOnce = "Tak";

                Session["Login"] = User.Login;


                return RedirectToAction("mainpage", "home", new { Login=User.Login});

            }
            else
            {
                ViewBag.triedOnce = "Tak";
                return View();
            }
        }

        /*public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }*/
        public ActionResult Regulamin()
        {
            ViewBag.Message = "Tutaj będzie regulamin";

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
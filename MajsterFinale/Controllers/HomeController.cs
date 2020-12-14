﻿using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using System.Web.Security;

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
            if (Session["ID"] == null)
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
            if (Session["ID"] != null)
            {
                return RedirectToAction("mainpage", "home", new { ID = Session["ID"].ToString() });
            }
            else
            {
                return View();
            }
        }

        //logowanie z sesją
        [HttpPost]
        public ActionResult Logowanie(USERS USERS)
        {
            BazaLocal db = new BazaLocal();
            var userLoggedIn = db.USERS.SingleOrDefault(x => x.LOGIN == USERS.LOGIN && x.PASSWORD == USERS.PASSWORD);
            if (userLoggedIn != null)
            {
                ViewBag.message = "Zalogowano";
                ViewBag.triedOnce = "Tak";

                Session["ID"] = USERS.USER_ID;
                Session["Login"] = USERS.LOGIN;

                //po zalogowaniu przenosi nas mainpage(nie można wejść na tą stronę jeżeli nie jest się zalogowanym: jest to test sesji)
                return RedirectToAction("mainpage", "home", new { Login = USERS.LOGIN });

            }
            else
            {
                ViewBag.triedOnce = "Tak";
                return View();
            }
        }
        public ActionResult Rejestracja(int id = 0)
        {
            USERS USERS = new USERS();
            return View(USERS);
        }
        [HttpPost]
        public ActionResult Rejestracja(USERS USERS)
        {
            if (!ModelState.IsValid)
            {
                return View(USERS);
            }
            else

            using (BazaLocal db = new BazaLocal())
                {
                    db.USERS.Add(USERS);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.SuccessMessage = "Rejestracja przebiegła pomślnie";
                return View("Rejestracja", new USERS());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AddAdvertisement()
        {
            BazaLocal db = new BazaLocal();
            IEnumerable<SelectListItem> List = new SelectList(db.CATEGORIES, "ID", "NAME");
            ViewBag.CATLIST = List;
            return View();
        }
        [HttpPost]
        public ActionResult AddAdvertisement(ADVERTS obj)

        {
            if (ModelState.IsValid)
            {
                BazaLocal db = new BazaLocal();
                int uID = Convert.ToInt32(Session["ID"]);
                obj.USER_ID = uID;
                obj.IS_ARCHIVED = false;
                db.ADVERTS.Add(obj);
                db.SaveChanges();
            }
            return View(obj);
        }

        public ActionResult MojeOgloszenia()
        {
            BazaLocal db = new BazaLocal();
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                ViewBag.ID = uID;
                return View(db.ADVERTS.Where(x => x.USER_ID == uID).ToList());
            }
            else return HttpNotFound();
        }

        public ActionResult Regulamin()
        {
            ViewBag.Message = "Tutaj będzie regulamin";

            return View();
        }
        public ActionResult Polityka()
        {
            ViewBag.Message = "Tutaj będzie Polityka Ochrony Prywatności";

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
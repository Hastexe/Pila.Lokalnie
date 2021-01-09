﻿using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using System.Web.Security;
using System.IO;
using System.Dynamic;

namespace MajsterFinale.Controllers
{
    public class UserController : Controller
    {
        private BazaLocal db = new BazaLocal();
        private AdvertRepository advertRepository = new AdvertRepository();
        private MessageModel messageModel = new MessageModel();
        private PanelMessageModel panelMessageModel = new PanelMessageModel();
        private UserRepository userRepository = new UserRepository();
        private RegisterRepository registerRepository = new RegisterRepository();
        private UserEditPassword userEditPassword = new UserEditPassword();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Panel()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);
                return View(Currentuser);
            }
            else return RedirectToAction("Logowanie", "Home");
        }

        public ActionResult Messages()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                panelMessageModel.UserMessages = new UserRepository().GetUserMessages(uID).ToList();
                panelMessageModel.Users = new UserRepository().GetUsers().ToList();
                panelMessageModel.MessageAdvertDetails = new AdvertRepository().GetAds().ToList();
                return View(panelMessageModel);
            }
            return RedirectToAction("Logowanie", "Home");
        }
        public ActionResult Conversation(int? AdvertId, int? UserA, int? UserB)
        {
            if (AdvertId != null && UserA != null && UserB != null) 
                {
                if (Session["ID"] != null)
                {
                    int userID = Convert.ToInt32(Session["ID"]);
                    if (userID != UserA || userID != UserB)
                    {
                        if (userID == UserA)
                        {
                            messageModel.LoggedUser = new UserRepository().GetUserData((int)UserA);
                            messageModel.SecondConversationUser = new UserRepository().GetUserData((int)UserB);
                            messageModel.LoggedUserAdverts = new AdvertRepository().GetUserAdverts((int)UserA).ToList();
                        }
                        if (userID == UserB)
                        {
                            messageModel.LoggedUser = new UserRepository().GetUserData((int)UserB);
                            messageModel.SecondConversationUser = new UserRepository().GetUserData((int)UserA);
                            messageModel.LoggedUserAdverts = new AdvertRepository().GetUserAdverts((int)UserB).ToList();
                        }
                        messageModel.CoversationMessages = new UserRepository().GetConversation((int)AdvertId, (int)UserA, (int)UserB).ToList();
                        return View(messageModel);
                    }
                    return RedirectToAction("messages", "User");
                }
                return RedirectToAction("Logowanie", "Home");
            }
            else return RedirectToAction("Mainpage", "Home");
        }

        [HttpPost]
        public ActionResult Conversation(string message, int UserTo, int AdvertId)
        {
            MESSAGE NewMessage = new MESSAGE()
            {
                MSG_FROM = Convert.ToInt32(Session["ID"]),
                MSG_TO = UserTo,
                TEXT = message,
                DATE = System.DateTime.Now,
                ADVERT_ID = AdvertId
            };
            int UserFrom = Convert.ToInt32(Session["ID"]);
            db.MESSAGE.Add(NewMessage);
            db.SaveChanges();
            return RedirectToAction("Conversation", new {AdvertId = AdvertId, UserA = UserTo, UserB=UserFrom });
        }

        public ActionResult EditPassword()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                UserEditPassword model = new UserEditPassword
                {
                    user = new UserRepository().GetUserData(uID)
                };
                return View(model);
            }
            else return RedirectToAction("Logowanie", "Home");
        }
        [HttpPost]
        public ActionResult EditPassword(UserEditPassword form)
        {
            ViewBag.Message = null;
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);
                if (ModelState.IsValid)
                {
                    var EncryptedOldPassword = registerRepository.Encryption(form.OldPassword);
                    if (EncryptedOldPassword == Currentuser.PASSWORD)
                    {
                        Currentuser.PASSWORD = registerRepository.Encryption(form.NewPassword);
                        db.Entry(Currentuser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Message = "Hasło zmienione";
                        return View();

                    }
                    else
                    {
                        ViewBag.Message = "Podano złe aktualne hasło";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Logowanie", "Home");
        }

        public ActionResult EditData()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);

                USERS currentUser = new UserRepository().GetUserData(uID);

                return View(currentUser);
            }
            else return RedirectToAction("Logowanie", "Home");
        }
        [HttpPost]
        public ActionResult EditData(USERS form)
        {
            ViewBag.Message = null;
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);
                if (ModelState.IsValid)
                {
                        Currentuser.FNAME = form.FNAME;
                        Currentuser.LNAME = form.LNAME;
                        Currentuser.PHONE_NUMBER = form.PHONE_NUMBER;
                        db.Entry(Currentuser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Message = "Zaktualizowano dane";
                        return View();
                }
                else
                {
                    ViewBag.Message = "Popraw dane";
                    return View();
                }
            }
            return RedirectToAction("Logowanie", "Home");
        }

    }
}

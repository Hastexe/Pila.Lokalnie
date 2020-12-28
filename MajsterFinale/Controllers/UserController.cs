using MajsterFinale.Models;
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
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        /*public ActionResult Panel()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);
            }
            return View();
        }*/

        public ActionResult Panel()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);
                return View(Currentuser);
            }
            else return View("Index", "Home");
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
            return View("Index", "Home");
        }
        public ActionResult Conversation(int AdvertId, int UserA, int UserB)
        {
            if (Session["ID"] != null)
            {
                int userID = Convert.ToInt32(Session["ID"]);
                if (userID == UserA)
                {
                    messageModel.LoggedUser = new UserRepository().GetUserData(UserA);
                    messageModel.SecondConversationUser = new UserRepository().GetUserData(UserB);
                    messageModel.LoggedUserAdverts = new AdvertRepository().GetUserAdverts(UserA).ToList();
                    messageModel.SecondUserAdverts = new AdvertRepository().GetUserAdverts(UserB).ToList();
                }
                if (userID == UserB)
                {
                    messageModel.LoggedUser = new UserRepository().GetUserData(UserB);
                    messageModel.SecondConversationUser = new UserRepository().GetUserData(UserA);
                    messageModel.LoggedUserAdverts = new AdvertRepository().GetUserAdverts(UserB).ToList();
                    messageModel.SecondUserAdverts = new AdvertRepository().GetUserAdverts(UserA).ToList();
                }
                messageModel.CoversationMessages = new UserRepository().GetConversation(AdvertId, UserA, UserB).ToList();

                return View(messageModel);
            }
            return View("Home", "Index");
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
            return View("Conversation", "Messages", new { AdvertId, UserFrom, UserTo });
        }

        public ActionResult Edit()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);

                return View(Currentuser);
            }
            return View("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(string OldPassword, string NewPassword, string ConfirmNewPassword, string NewEmail, string ConfirmEmail, int Change)
        {
            ViewBag.Message = null;
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                USERS Currentuser = new UserRepository().GetUserData(uID);
                if (Change == 0)
                {
                    if (OldPassword != "" && NewPassword != "" && ConfirmNewPassword != "")
                    {
                        var EncryptedOldPassword = registerRepository.Encryption(OldPassword);
                        if (EncryptedOldPassword == Currentuser.PASSWORD)
                        {
                            if (NewPassword == ConfirmNewPassword)
                            {
                                Currentuser.PASSWORD = registerRepository.Encryption(NewPassword);
                                db.SaveChanges();
                                ViewBag.Message = "Hasło zmienione";
                                return View();
                            }
                            else
                            {
                                ViewBag.Message = "Nowe hasła różnią się od siebie";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Podano złe obecne hasło " + EncryptedOldPassword + " " + Currentuser.PASSWORD;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Uzupelnij wszystkie pola - hasło";
                    }
                }
                else if (Change == 1)
                {
                    if (NewEmail != "" && ConfirmEmail != "")
                    {
                        if (registerRepository.IsEmailExist(NewEmail) == false)
                        {
                            if (NewEmail == ConfirmEmail)
                            {
                                Currentuser.MAIL = NewEmail;
                                db.SaveChanges();
                                ViewBag.Message = "Adres email zmieniony";
                                return View();
                            }
                            else
                            {
                                ViewBag.Message = "Podane adresy różnią się od siebie";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Podany email jest już w użyciu ";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Uzupelnij wszystkie pola - email";
                    }
                }

            }
            return View();
        }


    }
}

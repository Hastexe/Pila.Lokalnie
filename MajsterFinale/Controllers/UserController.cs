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
        private UserEditPassword userEditPassword = new UserEditPassword();
        // GET: User
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
            if (TempData["SizeError"] != null)
            {
                ViewBag.Error = "Maksymalny rozmiar zdjęć to 2MB";
                TempData.Remove("SizeError");
            }
            if (TempData["FormatError"] != null)
            {
                ViewBag.Error = "Użyto nieobsługiwanego formatu zdjęć. Dozwolone formaty: .jpg .jpeg .png";
                TempData.Remove("FormatError");
            }
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
                        messageModel.Images = new UserRepository().GetConversationImages((int)AdvertId, (int)UserA, (int)UserB).ToList();
                        foreach (var item in messageModel.CoversationMessages)
                        {
                            var msgID = (item.ID);
                            if (item.MSG_TO == userID )
                            {
                                var ID = msgID;
                                MESSAGE MsgToChange = new UserRepository().GetMessage(ID);
                                MsgToChange.IS_READ = true;
                                db.Entry(MsgToChange).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        };
                        return View(messageModel);
                    }
                    return RedirectToAction("messages", "User");
                }
                return RedirectToAction("Logowanie", "Home");
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Conversation(string message, int UserTo, int AdvertId, IEnumerable<HttpPostedFileBase> files)
        {
            ViewBag.Message = null;
            int UserFrom = Convert.ToInt32(Session["ID"]);
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 2097152)  // 2MB?
                        {
                            TempData["SizeError"] = "Maksymalny rozmiar zdjęć to 2MB";
                            return RedirectToAction("Conversation", new { AdvertId, UserA = UserTo, UserB = UserFrom});
                        }
                    }
                }
            }
            MESSAGE NewMessage = new MESSAGE()
            {
                MSG_FROM = Convert.ToInt32(Session["ID"]),
                MSG_TO = UserTo,
                TEXT = message,
                DATE = System.DateTime.Now,
                ADVERT_ID = AdvertId,
                IS_READ = false
            };
            db.MESSAGE.Add(NewMessage);
            db.SaveChanges();
            foreach (var file in files)
            {
                if (file != null)
                {
                        var filename = Guid.NewGuid() + file.FileName;
                        var supportedTypes = new[] { "jpg", "jpeg", "png", "JPG", "JPEG", "PNG" };
                        var fileExt = System.IO.Path.GetExtension(filename).Substring(1);
                        if (supportedTypes.Contains(fileExt))
                        {
                            file.SaveAs(Server.MapPath("/UploadImage/" + filename));
                            IMAGES_MESSAGE img = new IMAGES_MESSAGE
                            {
                                IMAGE_TITLE = filename,
                                IMAGE_PATH = "/UploadImage/" + filename,
                                MESSAGE_ID = NewMessage.ID,
                                MSG_FROM = NewMessage.MSG_FROM,
                                MSG_TO = NewMessage.MSG_TO,
                                ADVERT_ID = NewMessage.ADVERT_ID
                            };
                            db.IMAGES_MESSAGE.Add(img);
                            db.SaveChanges();
                        }
                        else
                        {
                            TempData["FormatError"] = "Użyto nieobsługiwanego formatu zdjęć. Dozwolone formaty: .jpg .jpeg .png";
                            return RedirectToAction("Conversation", new { AdvertId, UserA = UserTo, UserB = UserFrom });
                        }
                }
            }
            return RedirectToAction("Conversation", new { AdvertId, UserA = UserTo, UserB=UserFrom });
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

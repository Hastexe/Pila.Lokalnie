using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using PagedList;
using System.Web.UI;
using System.Windows.Documents;
//using UploadImageInDataBase.Models;

namespace MajsterFinale.Controllers
{
    public class HomeController : Controller
    {
        private BazaLocal db = new BazaLocal();
        private AdvertRepository advertRepository = new AdvertRepository();
        private DisplayRepository displayRepository = new DisplayRepository();
        private RegisterRepository registerRepository = new RegisterRepository();
        private AddingAdsRepository addingAdsRepository = new AddingAdsRepository();
        private DisplayAdsRepository displayAdsRepository = new DisplayAdsRepository();
        public ActionResult Index()
        {
            AddingAdsRepository model = new AddingAdsRepository();
            model.CategoryID = -1;
            model.Categories = addingAdsRepository.GetList();
            return View(model);
        }
        public ActionResult PilaLokalnie()
        {
            return View();
        }
        public ActionResult Wyszukiwanie(int? page, string search, string sortOrder, string currentFilter)
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayAdsRepository.LoggedUser = advertRepository.GetUserData(uID);
                displayAdsRepository.FavUsera = advertRepository.GetUserFav(uID);
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            var adverts = db.ADVERTS.Where(x => x.IS_ARCHIVED == false);
            if (!String.IsNullOrEmpty(search))
            {
                adverts = adverts.Where(s => s.IS_ARCHIVED == false && ((s.TITLE.Contains(search)) || (s.DESCRIPTION.Contains(search))));
            }
            else if (String.IsNullOrEmpty(search))
            {
                adverts = adverts.Where(s => s.IS_ARCHIVED == false);
            }
            switch (sortOrder)
            {

                case "name_desc":
                    adverts = adverts.OrderByDescending(s => s.TITLE);
                    break;
                case "name":
                    adverts = adverts.OrderBy(s => s.TITLE);
                    break;
                case "Price":
                    adverts = adverts.OrderBy(s => s.PRICE);
                    break;
                case "Price_desc":
                    adverts = adverts.OrderByDescending(s => s.PRICE);
                    break;
                case "Date":
                    adverts = adverts.OrderBy(s => s.DATE);
                    break;
                case "date_desc":
                    adverts = adverts.OrderByDescending(s => s.DATE);
                    break;
                default:
                    adverts = adverts.OrderByDescending(s => s.DATE);
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            displayAdsRepository.ADVERTS = adverts.ToPagedList(pageNumber, pageSize);
            displayAdsRepository.IMAGES = new AdvertRepository().GetAdsImages().ToList(); ;
            return View(displayAdsRepository);
        }
        [HttpPost]
        public ActionResult Wyszukiwanie(string Obserwuj)
        {
            if (Obserwuj != null)
            {
                return (AddToFavFromList(Obserwuj));
            }
            else
            {
                return View();
            }
        }
        private ActionResult AddToFavFromList(string id)
        {
            if (Session["ID"] != null)
            {
                int AdID = Convert.ToInt32(id);
                int uID = Convert.ToInt32(Session["ID"]);
                FAV fAV = new FAV();
                int check = db.FAV.Count(x => x.USER == uID && x.ADV == AdID);
                if (check == 0)
                {
                    fAV.USER = uID;
                    fAV.ADV = AdID;
                    db.FAV.Add(fAV);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Adverts", new { id = id });
                }
                else
                {
                    db.FAV.Remove(db.FAV.Single(x => x.ADV == AdID && x.USER == uID));
                    db.SaveChanges();
                    return RedirectToAction("Details", "Adverts", new { id = id });
                }
            }
            else
                return RedirectToAction("Logowanie", "home");
        }
        public ActionResult Logowanie()
        {
            if (Session["ID"] != null)
            {
                return RedirectToAction("Index", "Home", new { ID = Session["ID"].ToString() });
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
            var isPasswordsNull = registerRepository.PasswordNotNull(USERS);
            var IsMailNotNull = registerRepository.IsMailNotNull(USERS);
            if (IsMailNotNull)
            {
                ModelState.AddModelError("MAIL", "Należy podać maila");
                return View();
            }
            else if (isPasswordsNull)
            {
                ModelState.AddModelError("PASSWORD", "Należy podać hasło");
                return View();
            }
            else
            {
                USERS.PASSWORD = registerRepository.Encryption(USERS.PASSWORD);
                var userLoggedIn = db.USERS.SingleOrDefault(x => x.MAIL == USERS.MAIL && x.PASSWORD == USERS.PASSWORD);
                if (userLoggedIn != null)
                {
                    Session["ID"] = userLoggedIn.USER_ID;
                    Session["MAIL"] = userLoggedIn.MAIL;
                    Session["FNAME"] = userLoggedIn.FNAME;

                    return RedirectToAction("Index", "Home", new { ID = USERS.USER_ID });
                }
                else
                {
                    ViewBag.Message = "Podane dane logowania są błędne";
                    ModelState.Clear();
                    return View();
                }
            }
           
        }

        public ActionResult Rejestracja(int id = 0)
        {
            if (Session["ID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                USERS USERS = new USERS();
                return View(USERS);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rejestracja(USERS USERS)
        {
            if (ModelState.IsValid)
            {
                    var arePasswordsSame = registerRepository.ArePasswordsSame(USERS);
                    var arePasswordsNull = registerRepository.IsPasswordNotNull(USERS);
                    var areMailsNull = registerRepository.IsMailNotNull(USERS);
                    var areTermsAccepted = registerRepository.AreTermsAccepted(USERS);
                    var mail = db.USERS.SingleOrDefault(x => x.MAIL == USERS.MAIL);
                    if (mail != null)
                    {
                        ModelState.AddModelError("MAIL", "Adres email jest juz zajęty");
                        return View();
                    }
                    else if (areMailsNull)
                    {
                        ModelState.AddModelError("MAIL", "Należy podać maila");
                        return View();
                    }
                    else if (arePasswordsNull)
                    {
                        ModelState.AddModelError("REGISTERPASSWORD", "Należy uzupełnić oba pola hasła");
                        ModelState.AddModelError("REPASSWORD", "Należy uzupełnić oba pola hasła");
                        return View();
                    }
                    else if (arePasswordsSame)
                    {
                        ModelState.AddModelError("REGISTERPASSWORD", "Hasła muszą być takie same");
                        ModelState.AddModelError("REPASSWORD", "Hasła muszą być takie same");
                        return View();
                    }
                    else if (areTermsAccepted)
                    {
                        ModelState.AddModelError("TERMS", "Należy zaakceptować regulamin");
                        return View();
                    }
                    else
                    {
                        USERS.PASSWORD = registerRepository.Encryption(USERS.REGISTERPASSWORD);
                        USERS.REPASSWORD = registerRepository.Encryption(USERS.REPASSWORD);
                        USERS.FNAME = USERS.FNAME;
                        USERS.VERIFIED = false;
                        USERS.IS_ADMIN = false;
                        USERS.REGISTER_DATE = DateTime.Now;
                        USERS.LASTRESETPASSDATE = DateTime.Now.AddDays(-1);

                        db.USERS.Add(USERS);
                        db.SaveChanges();
                        SendVerificationLinkEmail(USERS.MAIL, USERS.USER_ID);
                    }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Na maila został przesłany link aktywujący konto. Bez aktywacji nie będziesz w stanie w pełni korzystać z konta!";
            return View();
         }
         else
         {
           return View();
         }
        }

        [HttpPost]
        private void SendVerificationLinkEmail(string MAIL, int UID)
        {
            var regInfo = db.USERS.Where(x => x.USER_ID == UID).FirstOrDefault();
            var verifyUrl = "/Home/Weryfikacja?uID=" + UID;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            string host = "smtp.gmail.com";
                SmtpClient client = new SmtpClient(host, 587);
                client.Credentials = new NetworkCredential("pilalokalnie@gmail.com", "Jklmynuilop.acx.qfe");
                client.EnableSsl = true;
                MailAddress from = new MailAddress("pilalokalnie@gmail.com");

                MailAddress to = new MailAddress(MAIL);
                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;

            message.Body =
                @"
<html lang=""en"">
    <head>    
        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
        <style>
            h1 {text-align: center;}
            div {text-align: center;}
            a {text-align: center; font-size:22px;}
        </style>
    </head>
    <body>
        <h1>Dziękujemy za rejestracje na Piła.Lokalnie.</h1>
        <h1>W celu zakończenia rejestracji proszę kliknąć w poniższy link:</h1></br>
        <a href = ""{URL}"">Aktywuj</a>
          </body>
</html>
";
            string body = message.Body;
            message.Body = body.Replace("{URL}", link);
            message.Subject = "Konto zostało utworzone!";

                client.Send(message);
                client.Dispose();
        }

        public ActionResult Weryfikacja(int uID)
        {
            ViewBag.UID = uID;
            var user = db.USERS.SingleOrDefault(x => x.USER_ID == uID);
            if (user != null)
            {
                ActivationUserModel model = new ActivationUserModel();
                model.uID = uID;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult AccountConfirm(ActivationUserModel model)
        {
            USERS Data = db.USERS.FirstOrDefault(x => x.USER_ID == model.uID);
            Data.VERIFIED = true;
            db.SaveChanges();
            return RedirectToAction("Logowanie", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
 
        public ActionResult Regulamin()
        {
            return File("~/Content/files/regulamin.pdf", "application/pdf");
        }
        public ActionResult Polityka()
        {
            return File("~/Content/files/polityka.pdf", "application/pdf");
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
        public ActionResult Odzyskaj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Odzyskaj(string MAIL, USERS USERS)
        {
            string resetCode = Guid.NewGuid().ToString();
            using (BazaLocal db = new BazaLocal())
            {

                var mail = db.USERS.SingleOrDefault(x => x.MAIL == USERS.MAIL);
                var currentdate = DateTime.Now;
                TimeSpan diff = currentdate.Subtract((DateTime)mail.LASTRESETPASSDATE);
                double hours = diff.TotalHours;
                if (mail != null)
                {
                    if (hours < 24)
                    {
                        ModelState.AddModelError("MAIL", "Hasło można resetować raz na 24h!");
                        return View();
                    }
                    else
                    {
                        mail.RESETPASSWORDCODE = resetCode;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();

                        SendResetPasswordEmail(mail.MAIL, resetCode);
                        ViewBag.SuccessMessage = "Na maila został przesłany link zmiany hasła.";
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("MAIL", "Nie ma takiego mail!");
                    return View();
                }
            }
        }
        [HttpPost]
        public void SendResetPasswordEmail(string MAIL, string resetCode)
        {
            var verifyUrl = "/home/NoweHaslo/" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            string host = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(host, 587);
            client.Credentials = new NetworkCredential("pilalokalnie@gmail.com", "Jklmynuilop.acx.qfe");
            client.EnableSsl = true;
            MailAddress from = new MailAddress("pilalokalnie@gmail.com");

            MailAddress to = new MailAddress(MAIL);
            MailMessage message = new MailMessage(from, to);
            message.IsBodyHtml = true;

            message.Body =
            @"
<html lang=""en"">
    <head>    
        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
        <style>
            h1 {text-align: center;}
            div {text-align: center;}
            a {text-align: center; font-size:22px;}
        </style>
    </head>
    <body>
        <h1>Otrzymaliśmy prośbę o zmianę hasła dla twojego konta.</h1>
        <h1>Hasło zmienisz pod poniższym linkiem:</h1></br>
        <a href = ""{URL}"">Reset hasła</a>
          </body>
</html>
";
            string body = message.Body;
            message.Body = body.Replace("{URL}", link);
            message.Subject = "Resetowanie hasła";

            client.Send(message);
            client.Dispose();
        }
        public ActionResult NoweHaslo(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }
            using (BazaLocal db = new BazaLocal())
            {
                var user = db.USERS.SingleOrDefault(x => x.RESETPASSWORDCODE == id);
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoweHaslo(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using(BazaLocal db = new BazaLocal())
                {

                    var user = db.USERS.SingleOrDefault(x => x.RESETPASSWORDCODE == model.ResetCode);
                        //return db.USERS.AsNoTracking().SingleOrDefault(x => x.USER_ID == uID);
                        var EncryptedNewPassword = registerRepository.Encryption(model.NewPassword);
                    if (user != null)
                    {
                        
                        if (EncryptedNewPassword == user.PASSWORD)
                        {
                            ModelState.AddModelError("NewPassword", "Nowe hasło musi być różne od obecnego");
                            return View();
                        }
                        else if (model.NewPassword != model.ConfirmPassword)
                        {
                            ModelState.AddModelError("NewPassword", "Hasła muszą być takie same");
                            ModelState.AddModelError("ConfirmPassword", "Hasła muszą być takie same");
                            return View();
                        }
                        else 
                        {
                            //szyfrowanie nowego hasła
                            user.PASSWORD = EncryptedNewPassword;
                            //resetujemy kod resetowania hasła
                            user.RESETPASSWORDCODE = "";
                            user.LASTRESETPASSDATE = DateTime.Now;
                            //db.Configuration.ValidateOnSaveEnabled = false;
                            db.SaveChanges();
                            ViewBag.SuccessMessage = "Udało się zmienić hasło.";
                            return RedirectToAction("Logowanie", "home");
                        }
                    }
                }
                ModelState.Clear();
                return View(model);
            }
            else
            {
                message = "Nie można zmienić hasła. Upewnij się czy wprowadzone hasła są identyczne oraz czy prośba o zmiane hasła nie została już wcześniej zakończona";
            }
            ViewBag.Message = message;
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult MessageCounter()
        {
            int counter = 0;
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                counter = new UserRepository().GetUserNotReadMessages(uID).ToList().Count;
                ViewBag.Counter = counter;
                return PartialView("DisplayMsgPartialView");
            }
            else
            {
                ViewBag.Counter = counter;
                return PartialView("DisplayMsgPartialView");
            }
        }
    }
}
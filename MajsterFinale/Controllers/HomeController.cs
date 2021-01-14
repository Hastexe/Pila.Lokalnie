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
        public ActionResult Index()
        {
            AddingAdsRepository model = new AddingAdsRepository();
            model.CategoryID = -1;
            model.Categories = addingAdsRepository.GetList();
            return View(model);
        }

        public ActionResult SearchedAds(int? page, string search, AddingAdsRepository obj)
        {
            var category = obj.CategoryID;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetAdsBySearch(search, category).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Mainpage()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Index", "home");
            }
            else
            {
                AddingAdsRepository model = new AddingAdsRepository();
                model.CategoryID = -1;
                model.Categories = addingAdsRepository.GetList();
                return View(model);
            }
        }
        public ActionResult Logowanie()
        {
            if (Session["ID"] != null)
            {
                return RedirectToAction("Index", "home", new { ID = Session["ID"].ToString() });
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
            USERS.PASSWORD = registerRepository.Encryption(USERS.PASSWORD);
            
            var userLoggedIn = db.USERS.SingleOrDefault(x => x.MAIL == USERS.MAIL && x.PASSWORD == USERS.PASSWORD);
            
            if (userLoggedIn != null)
            {
                Session["ID"] = userLoggedIn.USER_ID;
                Session["MAIL"] = userLoggedIn.MAIL;
                
                return RedirectToAction("Index", "home", new { ID = USERS.USER_ID });
            }
            else
            {
                ViewBag.Message = "Podane dane logowania są błędne";
                ModelState.Clear();
                return View();
            }
        }

        public ActionResult Rejestracja(int id = 0)
        {
            if (Session["ID"] != null)
            {
                return RedirectToAction("Index", "home");
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
            ViewBag.Message = null;
            if (ModelState.IsValid)
            {
              

                using (BazaLocal db = new BazaLocal())
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
                        ModelState.AddModelError("TERMS", "Należy zaakceptować warunki");
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
        public void SendVerificationLinkEmail(string MAIL, int UID)
        {
            //var link = "https://localhost:44318/Home/AktywacjaKonta";
            var regInfo = db.USERS.Where(x => x.USER_ID == UID).FirstOrDefault();
            var verifyUrl = "/Home/Confirm?uID=" + UID;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            string host = "smtp.gmail.com";
                SmtpClient client = new SmtpClient(host, 587);
                client.Credentials = new NetworkCredential("pilalokalnie@gmail.com", "Jklmynuilop.acx.qfe");
                client.EnableSsl = true;
                MailAddress from = new MailAddress("pilalokalnie@gmail.com");

                MailAddress to = new MailAddress(MAIL);
                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                
                message.Body = "Dziękujemy za rejestracje na Piła.Lokalnie."+ "<br/>" 
                +"W celu zakończenia rejestracji należy kliknąć przycisk na stronie z poniższego linku: <br/>"
                + "<a href = "+link+">Przejdz na stronę</a>";
                message.Subject = "Konto zostało utworzone!";

                client.Send(message);
                client.Dispose();
        }

        public ActionResult Confirm(int uID)
        {
            ViewBag.UID = uID;
            return View();
        }
        public ActionResult AccountConfirm(int uID)
        {
            BazaLocal db = new BazaLocal();
            USERS Data = db.USERS.Where(x => x.USER_ID == uID).FirstOrDefault();
            //USERS Data = db.USERS.SingleOrDefault(x => x.USER_ID == uID);
            Data.VERIFIED = true;
            db.SaveChanges();
            var msg = "Twoje konto zostało zweryfikowane!";
            return Json(msg, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Logowanie", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
 
        public ActionResult ImageUpload(ImageViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            BazaLocal db = new BazaLocal();
            int imgId = 0;

            foreach (var file in files)
            {
                //var file = model.ImageFile;
                byte[] imagebyte = null;    
                if (file != null)
                {
                    file.SaveAs(Server.MapPath("/UploadImage/" + file.FileName));
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    imagebyte = reader.ReadBytes(file.ContentLength);
                    IMAGES img = new IMAGES();
                    img.IMAGE_TITLE = file.FileName;
                    img.IMAGE_BYTE = imagebyte;
                    img.IMAGE_PATH = "/UploadImage/" + file.FileName;
                    db.IMAGES.Add(img);
                    db.SaveChanges();
                    imgId = img.IMAGE_ID;
                }
            }
            return RedirectToAction("DodawanieZdjec", "Home");
        }

        public ActionResult DisplayingImage(int imgid)
        {
            BazaLocal db = new BazaLocal();

            var img = db.IMAGES.SingleOrDefault(x => x.IMAGE_ID == imgid);
            return File(img.IMAGE_BYTE, "image/jpg");
        }
        public ActionResult DodawanieZdjec()
        {
            return View();
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
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string MAIL, USERS USERS)
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
            var verifyUrl = "/home/ResetPassword/" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            string host = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(host, 587);
            client.Credentials = new NetworkCredential("pilalokalnie@gmail.com", "Jklmynuilop.acx.qfe");
            client.EnableSsl = true;
            MailAddress from = new MailAddress("pilalokalnie@gmail.com");

            MailAddress to = new MailAddress(MAIL);
            MailMessage message = new MailMessage(from, to);
            message.IsBodyHtml = true;

            message.Body = "Przyjeliśmy twoją prośbę o reset hasła"
            + "Resetowanie hasła zakończysz na przesłanym linku:<br>"
            + "<a href = " + link + ">Przejdz na stronę</a>";
            message.Subject = "Resetowanie hasła";

            client.Send(message);
            client.Dispose();
        }
        public ActionResult ResetPassword(string id)
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
        public ActionResult ResetPassword(ResetPasswordModel model)
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
                            return RedirectToAction("Index", "home");
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
    }
}
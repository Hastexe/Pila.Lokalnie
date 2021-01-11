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
                ViewBag.message = "Zalogowano";
                ViewBag.triedOnce = "Tak";

                //int ID = db.USERS.Where(x => x.LOGIN == USERS.LOGIN).Select(x => x.USER_ID);
                Session["ID"] = userLoggedIn.USER_ID;
                //Session["Login"] = userLoggedIn.LOGIN;

                //po zalogowaniu przenosi nas index
                return RedirectToAction("Mainpage", "home", new { ID = USERS.USER_ID });

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
        [ValidateAntiForgeryToken]
        public ActionResult Rejestracja(USERS USERS)
        {
            if (!ModelState.IsValid)
            {
                return View(USERS);
            }
            else

            using (BazaLocal db = new BazaLocal())
                {
                    var isMailExist = registerRepository.IsEmailExist(USERS.MAIL);
                     if (isMailExist)
                     {
                         ViewBag.SuccessMessage = "Email jest już w użyciu";
                         return View();
                     }
                    /*
                    var isLoginExist = registerRepository.IsLoginExist(USERS.LOGIN);
                    if (isLoginExist)
                    {
                        ViewBag.SuccessMessage = "Login jest już w użyciu";
                        return View();
                    } 
                    */
                    var arePasswordsSame = registerRepository.ArePasswordsSame(USERS);
                    if (arePasswordsSame)
                    {
                        ViewBag.SuccessMessage = "Podane hasła muszą być takie same";
                        return View();
                    }
                    /*
                    var areMailsSame = registerRepository.AreMailsSame(USERS);
                    if (areMailsSame)
                    {
                        ViewBag.SuccessMessage = "Podane maile muszą być takie same";
                        return View();
                    }
                    */
                    var arePasswordsNull = registerRepository.IsPasswordNotNull(USERS);
                    if (arePasswordsNull)
                    {
                        ViewBag.SuccessMessage = "Oba pola przeznaczone do wprowadzenia hasła muszą być uzupełnione";
                        return View();
                    }
                    var areMailsNull = registerRepository.IsMailNotNull(USERS);
                    if (areMailsNull)
                    {
                        ViewBag.SuccessMessage = "Pole przeznaczone do wprowadzenia maila muszą być uzupełnione";
                        return View();
                    }
                    var areTermsAccepted = registerRepository.AreTermsAccepted(USERS);
                    if (areTermsAccepted)
                    {
                        ViewBag.SuccessMessage = "Należy zaakceptować regulamin i politykę prywatności";
                        return View();
                    }
                    USERS.PASSWORD = registerRepository.Encryption(USERS.PASSWORD);
                    USERS.REPASSWORD = registerRepository.Encryption(USERS.REPASSWORD);
                    USERS.VERIFIED = false;
                    //USERS.ACTIVATIONCODE = Guid.NewGuid();
                    db.USERS.Add(USERS);
                    db.SaveChanges();
                    SendVerificationLinkEmail(USERS.MAIL, USERS.USER_ID);
                }
                ModelState.Clear();
                ViewBag.SuccessMessage = "Rejestracja przebiegła pomyślnie." +
                "Przesłaliśmy maila aktywacyjnego na maila:" + USERS.MAIL;
                return View();
        }
       

        [HttpPost]
        public void SendVerificationLinkEmail(string MAIL, int UID)
        {
            //var link = "https://localhost:44318/Home/AktywacjaKonta";
            var regInfo = db.USERS.Where(x => x.USER_ID == UID).FirstOrDefault();
            var link = "https://localhost:44318/" + "Home/Confirm?uID=" + UID;
                string host = "smtp.gmail.com";
                SmtpClient client = new SmtpClient(host, 587);
                client.Credentials = new NetworkCredential("pilalokalnie@gmail.com", "Jklmynuilop.acx.qfe");
                client.EnableSsl = true;
                MailAddress from = new MailAddress("pilalokalnie@gmail.com");

                MailAddress to = new MailAddress(MAIL);
                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                
                message.Body = "Dziękujemy za rejestracje na Piła.Lokalnie"+ "<br/><br/>" 
                +"W celu zakończenia rejestracji należy kliknąć przycisk na stronie z poniższego linku.<br/><br/>"
                + "<a href = "+link+">Przejdz na stronę</a>";
                message.Subject = "Konto zostało utworzone!";

                client.Send(message);
                client.Dispose();
                MessageBox.Show("Wyslano maila");
            
        }
        public ActionResult Confirm(int uID)
        {
            ViewBag.UID = uID;
            return View();
        }
        
        public ActionResult AccountConfirm(int uID)
        {
            try
            {
            BazaLocal db = new BazaLocal();
            USERS Data = db.USERS.Where(x => x.USER_ID == uID).FirstOrDefault();
            //USERS Data = db.USERS.SingleOrDefault(x => x.USER_ID == uID);
            
            Data.VERIFIED = true;
            
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            var msg = "Twoje konto zostało zweryfikowane!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
 
        public JsonResult ImageUpload(ImageViewModel model)
        {
            BazaLocal db = new BazaLocal();
            int imgId = 0;
            var file = model.ImageFile;
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
            return Json(imgId, JsonRequestBehavior.AllowGet);
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
        
    }
}
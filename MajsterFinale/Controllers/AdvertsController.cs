using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using System.Web.Security;
using System.IO;
using PagedList;
using System.Dynamic;

namespace MajsterFinale.Controllers
{
    public class AdvertsController : Controller
    {
        private BazaLocal db = new BazaLocal();
        private AdvertRepository advertRepository = new AdvertRepository();
        private DisplayRepository displayRepository = new DisplayRepository();
        private AddingAdsRepository addingAdsRepository = new AddingAdsRepository();
        private DisplayAdsRepository displayAdsRepository = new DisplayAdsRepository();
        //
        // HTTP-GET: /Adverts/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /Adverts/Details/id

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                displayRepository.AdvertDetails = advertRepository.GetDetails((int)id);
                displayRepository.Images = advertRepository.GetAdImages((int)id);
                // var login = Convert.ToInt32(Session["Login"]);
                if (Session["ID"] != null)
                {
                    int uID = Convert.ToInt32(Session["ID"]);
                    displayRepository.LoggedUser = advertRepository.GetUserData(uID);
                    displayRepository.FAV = db.FAV.SingleOrDefault(x => x.ADV == id && x.USER == uID);
                }
                if (displayRepository.AdvertDetails == null)
                    return HttpNotFound();
                else
                    return View("Details", displayRepository);
            }
            return RedirectToAction("Wyszukiwanie", "Home");
        }
        [HttpPost]
        public ActionResult Details(int id, string message, IEnumerable<HttpPostedFileBase> files)
        {
            ViewBag.Message = null;
            displayRepository.AdvertDetails = advertRepository.GetDetails(id);
            displayRepository.Images = advertRepository.GetAdImages((int)id);
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            }
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 2097152)  // 2MB?
                        {
                            ViewBag.Message = "Maksymalny rozmiar zdjęć to 2MB";
                            return View("Details", displayRepository);
                        }
                    }
                }
            }
            MESSAGE NewMessage = new MESSAGE()
            {
                MSG_FROM = Convert.ToInt32(Session["ID"]),
                MSG_TO = displayRepository.AdvertDetails.USER_ID,
                TEXT = message,
                DATE = System.DateTime.Now,
                ADVERT_ID = displayRepository.AdvertDetails.ID,
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
                        ViewBag.Message = "Użyto nieobsługiwanego formatu zdjęć. Dozwolone formaty: .jpg .jpeg .png";
                        return View("Details", displayRepository);
                    }
                }
            }
            ViewBag.Message = "Wiadomość wysłana";
            return View("Details", displayRepository);
        }

        public ActionResult Kategorie(int id, int? page, string sortOrder, string searchString, string currentFilter)
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayAdsRepository.LoggedUser = advertRepository.GetUserData(uID);
                displayAdsRepository.FavUsera = advertRepository.GetUserFav(uID);
            }
            if (id == 1 || id >= 29 && id <= 995)
            {
                return RedirectToAction("Wyszukiwanie", "Home");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
                if (searchString != null)
                {
                    page = 1;
                }
                else if (searchString == null)
                {
                    searchString = currentFilter;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                var adverts = db.ADVERTS.Where(a => a.CATEGORY == id);
                if (id == 999)
                {
                    adverts = db.ADVERTS.Where(a => a.CATEGORY >= 2 && a.CATEGORY <= 9);
                }
                else if (id == 998)
                {
                    adverts = db.ADVERTS.Where(a => a.CATEGORY >= 10 && a.CATEGORY <= 15);
                }
                else if (id == 997)
                {
                    adverts = db.ADVERTS.Where(a => a.CATEGORY >= 16 && a.CATEGORY <= 21);
                }
                else if (id == 996)
                {
                    adverts = db.ADVERTS.Where(a => a.CATEGORY >= 22 && a.CATEGORY <= 28);
                }
                else
                {
                    adverts = db.ADVERTS.Where(a => a.CATEGORY == id);
                }


                if (!String.IsNullOrEmpty(searchString))
                {
                    adverts = adverts.Where(s => s.IS_ARCHIVED == false && s.IS_HIDDEN == false && ((s.TITLE.Contains(searchString))
                                           || (s.DESCRIPTION.Contains(searchString))));
                }
                else if (String.IsNullOrEmpty(searchString))
                {
                    adverts = adverts.Where(s => s.IS_ARCHIVED == false);
                }
                switch (sortOrder)
                {

                    case "name_desc":
                        adverts = adverts.OrderByDescending(s => s.TITLE);
                        break;
                    case "Date":
                        adverts = adverts.OrderBy(s => s.DATE);
                        break;
                    case "date_desc":
                        adverts = adverts.OrderByDescending(s => s.DATE);
                        break;
                    case "Price":
                        adverts = adverts.OrderBy(s => s.PRICE);
                        break;
                    case "Price_desc":
                        adverts = adverts.OrderByDescending(s => s.PRICE);
                        break;
                    default:
                        adverts = adverts.OrderBy(s => s.TITLE);
                        break;
                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                ViewBag.id = id;
                //return View(adverts.ToPagedList(pageNumber, pageSize));
                displayAdsRepository.ADVERTS = adverts.ToPagedList(pageNumber, pageSize);
                displayAdsRepository.IMAGES = new AdvertRepository().GetAdsImages().ToList();
                return View(displayAdsRepository);
            }

        }
        [HttpPost]
        public ActionResult Kategorie(string Obserwuj)
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

        public ActionResult OgloszeniaUsera(int? page, int id, string sortOrder, string searchString, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayAdsRepository.LoggedUser = advertRepository.GetUserData(uID);
                displayAdsRepository.FavUsera = advertRepository.GetUserFav(uID);
            }
            if (searchString != null)
            {
                page = 1;
            }
            else if (searchString == null)
            {
                searchString = "";
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var adverts = db.ADVERTS.Where(a => a.USER_ID == id && a.IS_ARCHIVED == false && a.IS_HIDDEN == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                adverts = adverts.Where(s => s.TITLE.Contains(searchString)
                                       || s.DESCRIPTION.Contains(searchString));
            }
            else if (String.IsNullOrEmpty(searchString))
            {
                adverts = adverts.Where(s => s.IS_ARCHIVED == false);
            }
            switch (sortOrder)
            {

                case "name_desc":
                    adverts = adverts.OrderByDescending(s => s.TITLE);
                    break;
                case "Date":
                    adverts = adverts.OrderBy(s => s.DATE);
                    break;
                case "date_desc":
                    adverts = adverts.OrderByDescending(s => s.DATE);
                    break;
                case "Price":
                    adverts = adverts.OrderBy(s => s.PRICE);
                    break;
                case "Price_desc":
                    adverts = adverts.OrderByDescending(s => s.PRICE);
                    break;
                default:
                    adverts = adverts.OrderBy(s => s.TITLE);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            displayAdsRepository.ADVERTS = adverts.ToPagedList(pageNumber, pageSize);
            displayAdsRepository.IMAGES = new AdvertRepository().GetAdsImages().ToList();
            return View(displayAdsRepository);

        }
        [HttpPost]
        public ActionResult OgloszeniaUsera(string Obserwuj)
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
        public ActionResult AddAdvertisement()
        {
            if (Session["ID"] != null)
            {
                AddingAdsRepository model = new AddingAdsRepository();
                model.CategoryID = -1;
                model.Categories = addingAdsRepository.GetList();

                return View(model);
            }
            return RedirectToAction("Logowanie", "home");

        }
        [HttpPost]
        public ActionResult AddAdvertisement(AddingAdsRepository AddingAdsRepository, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                ADVERTS newAdvert = new ADVERTS();

                newAdvert.CATEGORY = AddingAdsRepository.CategoryID;
                newAdvert.TITLE = AddingAdsRepository.Advert.TITLE;
                newAdvert.USER_ID = uID;
                newAdvert.DESCRIPTION = AddingAdsRepository.Advert.DESCRIPTION;
                newAdvert.DATE = System.DateTime.Now;
                newAdvert.IS_ARCHIVED = false;
                newAdvert.IS_HIDDEN = false;
                Convert.ToString(AddingAdsRepository.Advert.PRICE);
                var areTermsAccepted = AddingAdsRepository.AreTermsAccepted(AddingAdsRepository);
                if (AddingAdsRepository.Advert.PRICE == null)
                {
                    newAdvert.PRICE = 0;
                }
                else
                {
                    newAdvert.PRICE = AddingAdsRepository.Advert.PRICE;
                }


                if (areTermsAccepted)
                {
                    AddingAdsRepository.Categories = addingAdsRepository.GetList();
                    AddingAdsRepository.CategoryID = -1;
                    ModelState.AddModelError("TERMS", "Należy zaakceptować regulamin");
                    return View(AddingAdsRepository);
                }
                else if (newAdvert.CATEGORY != 1)
                {

                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            //var file = model.ImageFile;
                            var filename = Guid.NewGuid() + file.FileName;
                            var supportedTypes = new[] { "jpg", "jpeg", "png", "JPG", "JPEG", "PNG" };
                            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                            if (!supportedTypes.Contains(fileExt))
                            {
                                return Content("<script language='javascript' type='text/javascript'>alert('Niebsługiwany typ pliku. Dozwolone formaty: .jpg .jpeg .png');location = location;</script>");
                            }
                            else
                            {
                                file.SaveAs(Server.MapPath("/UploadImage/" + filename));
                                BinaryReader reader = new BinaryReader(file.InputStream);
                                IMAGES_ADVERT img = new IMAGES_ADVERT();

                                if (file.ContentLength > 5242880)  // 2MB?
                                {

                                    return Content("<script language='javascript' type='text/javascript'>alert('Plik jest zbyt duży');location = location;</script>");
                                }

                                else
                                {
                                    img.IMAGE_TITLE = filename;
                                    img.IMAGE_PATH = "/UploadImage/" + filename;
                                    img.ADVERT_ID = newAdvert.ID;
                                    db.IMAGES_ADVERT.Add(img);
                                    db.ADVERTS.Add(newAdvert);
                                }
                            }

                        }
                        else
                        {
                            db.ADVERTS.Add(newAdvert);
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("MojeOgloszenia", "adverts");
                }
                else
                {
                    AddingAdsRepository.Categories = addingAdsRepository.GetList();
                    AddingAdsRepository.CategoryID = -1;
                    ModelState.AddModelError("CATEGORY", "Musisz wybrać kategorię z listy.");
                    return View(AddingAdsRepository);
                }
            }
            AddingAdsRepository.Categories = addingAdsRepository.GetList();
            AddingAdsRepository.CategoryID = -1;
            return View(AddingAdsRepository);

        }

        public ActionResult MojeOgloszenia(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                var adverts = db.ADVERTS.Where(a => a.USER_ID == uID && a.IS_ARCHIVED == false && a.IS_HIDDEN == false).OrderBy(x => x.ID);
                displayAdsRepository.ADVERTS = adverts.ToPagedList(pageNumber, pageSize);
                displayAdsRepository.IMAGES = new AdvertRepository().GetAdsImages().ToList();
                return View(displayAdsRepository);
            }
            else
            {
                return RedirectToAction("Logowanie", "Home");
            }
        }

        [HttpPost]
        public ActionResult MojeOgloszenia(string Delete, string Edit)
        {
            if (Delete != null)
            {
                return (ArchiveAdvertisement(Delete));
            }
            if (Edit != null)
            {
                int AdID = Int32.Parse(Edit);
                //var AdData = new AdvertRepository().GetAdData(AdID);
                var AdData = new EditAdModel { ID = AdID };
                return RedirectToAction("EditAdvertisement", AdData);
            }
            else
            {
                return View();
            }
        }

        private ActionResult ArchiveAdvertisement(string id)
        {
            int AdID = Int32.Parse(id);
            db.ADVERTS.Single(s => s.ID == AdID).IS_ARCHIVED = true;
            db.SaveChanges();
            int uID = Convert.ToInt32(Session["ID"]);
            displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            return Content("<script language='javascript' type='text/javascript'>location = location;alert('Ogłoszenie zostało przeniesione do archiwum');</script>");
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

        [HttpGet]
        public ActionResult EditAdvertisement(EditAdModel obj)
        {
            if (Session["ID"] != null)
            {
                var AdID = obj.ID;
                AddingAdsRepository model = new AddingAdsRepository();
                model.CategoryID = -1;
                model.Categories = addingAdsRepository.GetList();
                ADVERTS AdData = new AdvertRepository().GetAdData(AdID);

                return View(AdData);
            }
            else
            {
                return RedirectToAction("Logowanie", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditAdvertisement(ADVERTS obj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                // model.Categories = addingAdsRepository.GetList();
                //model.CategoryID = -1;
                db.SaveChanges();
                ViewBag.Message = "Zaktualizowano ogłoszenie";
                int uID = Convert.ToInt32(Session["ID"]);
                displayRepository.LoggedUser = advertRepository.GetUserData(uID);
                return RedirectToAction("MojeOgloszenia", "adverts");
            }
            else
            {
                ViewBag.Message = "Popraw dane";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ArchiveAd(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                var adverts = db.ADVERTS.Where(a => a.USER_ID == uID && a.IS_ARCHIVED == true && a.IS_HIDDEN == false).OrderBy(x => x.ID);
                displayAdsRepository.ADVERTS = adverts.ToPagedList(pageNumber, pageSize);
                displayAdsRepository.IMAGES = new AdvertRepository().GetAdsImages().ToList();
                return View(displayAdsRepository);
            }
            else
            {
                return RedirectToAction("Logowanie", "Home");
            }
        }
        [HttpPost]
        public ActionResult ArchiveAd(string Delete, string Restore)
        {
            if (Delete != null)
            {
                return (DeleteAdvertisement(Delete));
            }
            else if (Restore != null)
            {
                return (RestoreAdvertisement(Restore));
            }
            else
            {
                return RedirectToAction("MojeOgloszenia", "Adverts");
            }
        }
        private ActionResult DeleteAdvertisement(string id)
        {
            int AdID = Int32.Parse(id);
            //db.ADVERTS.Remove(db.ADVERTS.Single(s => s.ID == AdID));
            //db.FAV.RemoveRange(db.FAV.Where(x => x.ADV == AdID));
            //db.IMAGES_ADVERT.RemoveRange(db.IMAGES_ADVERT.Where(x => x.ADVERT_ID == AdID));
            db.ADVERTS.Single(x => x.ID == AdID).IS_HIDDEN = true;
            db.SaveChanges();
            int uID = Convert.ToInt32(Session["ID"]);
            displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            return Content("<script language='javascript' type='text/javascript'>alert('Ogłoszenie zostało usunięte');location = location;</script>");
        }
        private ActionResult RestoreAdvertisement(string id)
        {
            int AdID = Int32.Parse(id);
            db.ADVERTS.Single(s => s.ID == AdID).IS_ARCHIVED = false;
            db.SaveChanges();
            int uID = Convert.ToInt32(Session["ID"]);
            displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            return Content("<script language='javascript' type='text/javascript'>alert('Ogłoszenie zostało przywrócone');location = location;</script>");
        }

        public ActionResult Obserwuj(int id)
        {
            if (Session["ID"] != null)
            {
                int AdID = id;
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

        public ActionResult Obserwowane(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                var fav = db.FAV.Where(z => z.USER == uID).Select(y => y.ADV).ToList();
                List<ADVERTS> adverts = new List<ADVERTS>();
                foreach (int item in fav)
                {
                    adverts.AddRange(db.ADVERTS.Where(a => a.ID == item && a.IS_ARCHIVED == false && a.IS_HIDDEN == false).OrderBy(x => x.ID).ToList());
                }
                displayAdsRepository.ADVERTS = adverts.ToPagedList(pageNumber, pageSize);
                displayAdsRepository.IMAGES = new AdvertRepository().GetAdsImages().ToList();
                return View(displayAdsRepository);
            }
            else
            {
                return RedirectToAction("Logowanie", "home");
            }
        }
        [HttpPost]
        public ActionResult Obserwowane(string Delete)
        {
            if (Delete != null)
            {
                int AdID = Int32.Parse(Delete);
                int uID = Convert.ToInt32(Session["ID"]);
                db.FAV.Remove(db.FAV.Single(x => x.ADV == AdID && x.USER == uID));
                db.SaveChanges();
                displayRepository.LoggedUser = advertRepository.GetUserData(uID);
                return Content("<script language='javascript' type='text/javascript'>location = location;alert('Ogłoszenie zostało usunięte z obserwowanych');</script>");

            }
            else
            {
                return View();
            }
        }
    }
}

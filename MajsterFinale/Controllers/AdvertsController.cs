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
                }
                if (displayRepository.AdvertDetails == null)
                    return HttpNotFound();
                else
                    return View("Details", displayRepository);
            }
            return RedirectToAction("Show", "Adverts");
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
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                    var fileExt = System.IO.Path.GetExtension(filename).Substring(1);
                    if (supportedTypes.Contains(fileExt))
                    {
                        file.SaveAs(Server.MapPath("/UploadImage/" + filename));
                        BinaryReader reader = new BinaryReader(file.InputStream);
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


        public ActionResult Category(int id, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetAdsByCategory(id).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Show(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            displayAdsRepository.ADVERTS = new AdvertRepository().GetAdsList().ToPagedList(pageNumber, pageSize);
            displayAdsRepository.IMAGES = new AdvertRepository().GetAdsImages().ToPagedList(pageNumber, pageSize);
            return View(displayAdsRepository);

        }

        public ActionResult Message()
        {
            return View(new AdvertRepository().GetMessage());
            //return View(new AdvertRepository().GetMessage().ToList());
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
            return RedirectToAction("Index", "home");

        }
        [HttpPost]
        public ActionResult AddAdvertisement(AddingAdsRepository obj, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
                {
                int uID = Convert.ToInt32(Session["ID"]);
                ADVERTS newAdvert = new ADVERTS();

                newAdvert.CATEGORY = obj.CategoryID;
                newAdvert.TITLE = obj.Advert.TITLE;
                newAdvert.USER_ID = uID;
                newAdvert.DESCRIPTION = obj.Advert.DESCRIPTION;
                newAdvert.DATE = System.DateTime.Now;
                newAdvert.IS_ARCHIVED = false;
                newAdvert.PRICE = obj.Advert.PRICE;


                if (newAdvert.CATEGORY != 1)
                {
                    db.ADVERTS.Add(newAdvert);
                    db.SaveChanges();
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                        //var file = model.ImageFile;
                        var filename = Guid.NewGuid() + file.FileName;
                        var supportedTypes = new[] { "jpg", "jpeg", "png" };
                        var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                            if (!supportedTypes.Contains(fileExt))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                file.SaveAs(Server.MapPath("/UploadImage/" + filename));
                                BinaryReader reader = new BinaryReader(file.InputStream);
                                IMAGES_ADVERT img = new IMAGES_ADVERT();

                                if (file.ContentLength > 2097152)  // 2MB?
                                {

                                    return RedirectToAction("Index", "Home");
                                }

                                else
                                {
                                    img.IMAGE_TITLE = filename;
                                    img.IMAGE_PATH = "/UploadImage/" + filename;
                                    img.ADVERT_ID = newAdvert.ID;
                                    db.IMAGES_ADVERT.Add(img);
                                    db.SaveChanges();;
                                }
                            }

                        }
                    }
                    return RedirectToAction("Index", "home");
                } 
                else
                {
                    obj.Categories = addingAdsRepository.GetList();
                    obj.CategoryID = -1;
                    ModelState.AddModelError("CATEGORY", "Musisz wybrać kategorię z listy.");
                    return View(obj);
                }
                }
            obj.Categories = addingAdsRepository.GetList();
                obj.CategoryID = -1;
            return View(obj);

        }

        [HttpGet]
        public ActionResult MojeOgloszenia()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayRepository.LoggedUser = advertRepository.GetUserData(uID);
                return View(new AdvertRepository().GetUserAdverts(uID));
            }
            else return RedirectToAction("AddAdvertisement", "Adverts");
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
                return View(MojeOgloszenia());
            }
        }

        private ActionResult ArchiveAdvertisement(string id)
        {
            int AdID = Int32.Parse(id);
            db.ADVERTS.Single(s => s.ID == AdID).IS_ARCHIVED = true;
            db.SaveChanges();
            int uID = Convert.ToInt32(Session["ID"]);
            displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            return View(new AdvertRepository().GetUserAdverts(uID));
        }
        [HttpGet]
        public ActionResult EditAdvertisement(EditAdModel obj)
        {
            var AdID = obj.ID;
            AddingAdsRepository model = new AddingAdsRepository();
            model.CategoryID = -1;
            model.Categories = addingAdsRepository.GetList();
            ADVERTS AdData = new AdvertRepository().GetAdData(AdID);

            return View(AdData);
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
        public ActionResult ArchiveAd()
        {
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayRepository.LoggedUser = advertRepository.GetUserData(uID);
                return View(new AdvertRepository().GetUserArchivedAdverts(uID));
            }
            else return HttpNotFound();
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
                return View(MojeOgloszenia());
            }
        }
        private ActionResult DeleteAdvertisement(string id)
        {
            int AdID = Int32.Parse(id);
            db.ADVERTS.Remove(db.ADVERTS.Single(s => s.ID == AdID));
            db.SaveChanges();
            int uID = Convert.ToInt32(Session["ID"]);
            displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            return View(new AdvertRepository().GetUserArchivedAdverts(uID));
        }
        private ActionResult RestoreAdvertisement(string id)
        {
            int AdID = Int32.Parse(id);
            db.ADVERTS.Single(s => s.ID == AdID).IS_ARCHIVED = false;
            db.SaveChanges();
            int uID = Convert.ToInt32(Session["ID"]);
            displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            return View(new AdvertRepository().GetUserArchivedAdverts(uID));
        }



    }
}
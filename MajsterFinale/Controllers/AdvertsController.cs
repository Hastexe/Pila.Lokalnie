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
        //
        // HTTP-GET: /Adverts/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /Adverts/Details/id

        public ActionResult Details(int id)
        {

            displayRepository.AdvertDetails = advertRepository.GetDetails(id);
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
        [HttpPost]
        public ActionResult Details(int id, string message)
        {
            displayRepository.AdvertDetails = advertRepository.GetDetails(id);
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayRepository.LoggedUser = advertRepository.GetUserData(uID);
            }
            MESSAGE NewMessage = new MESSAGE()
            {
                MSG_FROM = Convert.ToInt32(Session["ID"]),
                MSG_TO = displayRepository.AdvertDetails.USER_ID,
                TEXT = message,
                DATE = System.DateTime.Now,
                ADVERT_ID = displayRepository.AdvertDetails.ID
            };
            db.MESSAGE.Add(NewMessage);
            db.SaveChanges();
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
            return View(new AdvertRepository().GetAdsList().ToPagedList(pageNumber, pageSize));

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
            else return View("~/Views/Home/Index.cshtml");

        }
        [HttpPost]
        public ActionResult AddAdvertisement(AddingAdsRepository obj)

        {
            if (ModelState.IsValid)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                ADVERTS newAdvert = new ADVERTS()
                {
                    CATEGORY = obj.CategoryID,
                    TITLE = obj.Advert.TITLE,
                    USER_ID = uID,
                    DESCRIPTION = obj.Advert.DESCRIPTION,
                    DATE = System.DateTime.Now,
                    IS_ARCHIVED = obj.Advert.IS_ARCHIVED,
                    PRICE = obj.Advert.PRICE
                };
                db.ADVERTS.Add(newAdvert);
                db.SaveChanges();
                return View("~/Views/Home/Index.cshtml");
            }
            obj.Categories = addingAdsRepository.GetList();
            obj.CategoryID = -1;
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
            else return View("Logowanie");
        }

    }
}
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
        AdvertRepository advertRepository = new AdvertRepository();
        //
        // HTTP-GET: /Adverts/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /Adverts/Details/id
        public ActionResult Details(int id)
        {
            ADVERTS advert = advertRepository.GetDetails(id);
            var uID = Convert.ToInt32(Session["ID"]);
            USERS LoggedUser = advertRepository.GetUserData(uID);
            if (advert == null)
                return HttpNotFound();
            else
                return View("Details", advert);
            //if(message)
        }

        /*[HttpPost]
        public ActionResult Display()
        {
            string message = Request["Message"].ToString();
            ViewBag.triedOnce = "Tak";
            return View();
        }
        */
        public ActionResult Category(int id, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetAdsByCategory(id).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Adverts(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetAdsList().ToPagedList(pageNumber, pageSize));

        }

        /// <summary>  
        /// Render Student List  
        /// </summary>  
        /// <returns></returns>  
        public PartialViewResult RenderDetails()
        {
            return PartialView();
        }

        public ActionResult Message()
        {
            return View(new AdvertRepository().GetMessage());
            //return View(new AdvertRepository().GetMessage().ToList());
        }


    }
}
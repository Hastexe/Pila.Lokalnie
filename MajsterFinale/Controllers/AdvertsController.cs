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
        DisplayModel displayModel= new DisplayModel();
        //
        // HTTP-GET: /Adverts/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /Adverts/Details/id

        public ActionResult Details(int id)
        {

            displayModel.AdvertDetails = advertRepository.GetDetails(id);
            // var login = Convert.ToInt32(Session["Login"]);
            if (Session["ID"] != null)
            {
                int uID = Convert.ToInt32(Session["ID"]);
                displayModel.LoggedUser = advertRepository.GetUserData(uID);
            }
            if (displayModel.AdvertDetails == null)
                return HttpNotFound();
            else
                return View("Details", displayModel);
        }
        [HttpPost]
        public ActionResult Details(string message)
        {
            MESSAGE NewMessage = new MESSAGE
            {
                MSG_FROM = Convert.ToInt32(Session["ID"]),
                MSG_TO = displayModel.AdvertDetails.USER_ID,
                TEXT = message,
                DATE = System.DateTime.Now
            };
            db.MESSAGE.Add(NewMessage);
            db.SaveChanges();
            return View();
        }
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

        public ActionResult Message()
        {
            return View(new AdvertRepository().GetMessage());
            //return View(new AdvertRepository().GetMessage().ToList());
        }


    }
}
using MajsterFinale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrypt;
using System.Web.Security;
using System.IO;


namespace MajsterFinale.Controllers
{
    public class AdvertsController : Controller
    {
        AdvertRespository advertRespository = new AdvertRespository();
        //
        // HTTP-GET: /Ads/
        public ActionResult Index()
        {
            int pagesize = 35;

            var rowdata = ADVERTS.GetAds(1, pagesize);

            return View(rowdata);
        }

        //
        // GET: /Ads/Details/id
        public ActionResult Details(int id)
        {
            ADVERTS advert = advertRespository.GetAdverts(id);
            if (advert == null)
                return HttpNotFound();
            else
                return View("Details", advert);
        }

        public ActionResult Adverts()

        {

            int pagesize = 20;

            var rowdata = ADVERTS.GetAds(1, pagesize);

            return View(rowdata);

        }

        protected string renderPartialViewtostring(string Viewname, object model)

        {

            if (string.IsNullOrEmpty(Viewname))



                Viewname = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())

            {

                ViewEngineResult viewresult = ViewEngines.Engines.FindPartialView(ControllerContext, Viewname);

                ViewContext viewcontext = new ViewContext(ControllerContext, viewresult.View, ViewData, TempData, sw);

                viewresult.View.Render(viewcontext, sw);

                return sw.GetStringBuilder().ToString();

            }


        }

        public class JsonModel

        {

            public string HTMLString { get; set; }

            public bool NoMoredata { get; set; }

        }

        [ChildActionOnly]

        public ActionResult PartialAdverts(List<ADVERTS> Model)
        {

            return PartialView(Model);

        }

        [HttpPost]

        public ActionResult InfiniteScroll(int pageindex)

        {

            System.Threading.Thread.Sleep(1000);

            int pagesize = 20;

            var tbrow = ADVERTS.GetAds(pageindex, pagesize);

            JsonModel jsonmodel = new JsonModel();

            jsonmodel.NoMoredata = tbrow.Count < pagesize;

            jsonmodel.HTMLString = renderPartialViewtostring("PartialAdverts", tbrow);

            return Json(jsonmodel);

        }
    }
}
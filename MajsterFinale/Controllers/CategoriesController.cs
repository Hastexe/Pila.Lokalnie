using MajsterFinale.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MajsterFinale.Controllers
{
    public class CategoriesController : Controller
    {
        private BazaLocal db = new BazaLocal();
        private AdvertRepository advertRepository = new AdvertRepository();
        private DisplayRepository displayRepository = new DisplayRepository();
        private AddingAdsRepository addingAdsRepository = new AddingAdsRepository();

        public ActionResult AranzacjaWnetrz(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetAranzaciaWnetrzList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AranzacjaOgrodow(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetAranzaciaOgrodowList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Catering(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetCateringList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Remonty(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetRemontyList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Sprzatanie(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetSprzatanieList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UslugiOgrodnicze(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiOgrodniczeList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PraceBudowlane(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetPraceBudowlaneList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PraceWykonczeniowe(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetPraceWykonczenioweList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Dekarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetDekarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Elektryka(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetElektrykaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Gazownictwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetGazownictwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Hydraulika(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetHydraulikaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Malarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetMalarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Stolarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetStolarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Tynkarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetTynkarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Korepetycje(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetKorepetycjeList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UslugiInformatyczne(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiInformatyczneList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UslugiKosmetyczne(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiKosmetyczneList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UslugiMotoryzacyjne(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiMotoryzacyjneList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UslugiNaprawaSerwis(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiNaprawaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UslugiTransport(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiTransportoweList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DomiOgrod(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetDomiOgrodList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BudowaRemont(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiBudowlaneList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Uslugi(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiList().ToPagedList(pageNumber, pageSize));
        }
    }
}
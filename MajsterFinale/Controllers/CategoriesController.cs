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
            return View(new AdvertRepository().GetAranzacjaWnetrzList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Hydraulika(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetHydrualikaList().ToPagedList(pageNumber, pageSize));
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
        public ActionResult Malarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetMalarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Kominiarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetKominiarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Stolarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetStolarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult StolarstwoOgrod(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetStolarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DomInne(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetDomInneList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AranzacjaOgrodow(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetAranzacjaOgrodowList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Ogrodnictwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetOgrodnictwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Brukarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetBrukarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Ogrodzenia(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetOgrodzeniaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Kamieniarstwo(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetKamieniarstwoList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult OgrodInne(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetOgrodInneList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Mechanik(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetMechanikList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Blacharz(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetBlacharzList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Lakiernik(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetLakiernikList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Detaling(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetDetalingList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Wulkanizacja(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetWulkanizacjaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult MotoryzacjaInne(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetMotoryzacjaInneList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Korepetycje(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetKorepetycjeList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Informatyka(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetInformatykaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Uroda(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUrodaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NaprawaSerwis(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetNaprawaSerwisList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Transport(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetTransportList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Monter(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetMonterList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UslugiInne(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiInneList().ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Dom(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetDomList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Ogrod(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetOgrodList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Motoryzacja(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetMotoryzacjaList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Uslugi(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new AdvertRepository().GetUslugiList().ToPagedList(pageNumber, pageSize));
        }
    }
}
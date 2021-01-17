using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;

namespace MajsterFinale.Models
{
    public class AdvertRepository
    {
        private BazaLocal db = new BazaLocal();
        public ADVERTS GetDetails(int id)
        {
            return db.ADVERTS.SingleOrDefault(d => d.ID == id);
        }
        public List<ADVERTS> GetAdsByCategory(int id)
        {
            return db.ADVERTS.Where(x => x.CATEGORY == id).ToList();
        }
        public List<ADVERTS> GetAdsBySearch(string search, int category)
        {
            if(category == 1)
            {
                return db.ADVERTS.Where(x => x.TITLE.Contains(search) || x.DESCRIPTION.Contains(search)).OrderByDescending(x => x.ID).ToList();
            }
            else
            {
                return db.ADVERTS.Where(x => x.CATEGORY == category && ((x.TITLE.Contains(search)) || (x.DESCRIPTION.Contains(search)))).OrderByDescending(x => x.ID).ToList();
            }
            //return db.ADVERTS.Where(x=>x.CATEGORY == category && ((x.TITLE.Contains(search))||(x.DESCRIPTION.Contains(search)))).OrderByDescending(x => x.ID).ToList();
            //return db.ADVERTS.Where((x => x.CATEGORY == category && (x => x.CATEGORY == category && x.TITLE == search) ).ToList();
            //return db.MESSAGE.Where(x => x.ADVERT_ID == AdvertId && ((x.MSG_TO == UserA && x.MSG_FROM == UserB) || (x.MSG_TO == UserB && x.MSG_FROM == UserA))).OrderByDescending(x => x.ID).ToList();
        }
        public List<CATEGORIES> GetCategoriesNames()
        {
            return db.CATEGORIES.ToList();
        }

        public List<ADVERTS> GetAdsList()
        {
            return db.ADVERTS.OrderByDescending(a => a.DATE).ToList();
        }
        public List<ADVERTS> GetAds()
        {
            return db.ADVERTS.ToList();
        }

        public List<MESSAGE> GetMessage()
        {
            return db.MESSAGE.ToList();
        }

        public USERS GetUserData(int uID)
        {
            return db.USERS.SingleOrDefault(x => x.USER_ID == uID);
        }
        
        public List<ADVERTS> GetUserAdverts(int uID)
        {
            return db.ADVERTS.Where(x => x.USER_ID == uID && x.IS_ARCHIVED==false).ToList();
        }
        public List<ADVERTS> GetUserArchivedAdverts(int uID)
        {
            return db.ADVERTS.Where(x => x.USER_ID == uID && x.IS_ARCHIVED == true).ToList();
        }
        public ADVERTS GetAdData(int id)
        {
            return db.ADVERTS.SingleOrDefault(d => d.ID == id);
        }

        public List<ADVERTS> GetAranzaciaWnetrzList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 2).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetAranzaciaOgrodowList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 3).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetCateringList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 4).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetRemontyList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 5).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetSprzatanieList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 6).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiOgrodniczeList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 7).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetPraceBudowlaneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 8).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetPraceWykonczenioweList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 9).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetDekarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 10).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetElektrykaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 11).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetGazownictwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 12).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetHydraulikaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 13).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetMalarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 14).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetStolarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 15).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetTynkarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 16).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetKorepetycjeList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 17).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiInformatyczneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 18).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiKosmetyczneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 19).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiMotoryzacyjneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 20).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiNaprawaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 21).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiTransportoweList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 22).OrderByDescending(x => x.ID).ToList();
        }

        public List<ADVERTS> GetDomiOgrodList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY >= 2 && a.CATEGORY <= 7).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiBudowlaneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY >= 8 && a.CATEGORY <= 17).OrderByDescending(x => x.ID).ToList();
        }
        public List<ADVERTS> GetUslugiList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY >= 18 && a.CATEGORY <= 22).OrderByDescending(x => x.ID).ToList();
        }
    }

}

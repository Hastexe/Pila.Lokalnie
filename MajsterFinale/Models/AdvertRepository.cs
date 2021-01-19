﻿using System;
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
        public List<IMAGES_ADVERT> GetAdImages(int id)
        {
            return db.IMAGES_ADVERT.Where(x => x.ADVERT_ID == id).ToList();
        }
        public List<ADVERTS> GetAdsByCategory(int id)
        {
            return db.ADVERTS.Where(x => x.CATEGORY == id).ToList();
        }
        public List<IMAGES_ADVERT> GetAdsImages()
        {
            return db.IMAGES_ADVERT.ToList();
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

        public List<ADVERTS> GetAranzacjaWnetrzList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 2).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetHydrualikaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 3).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetElektrykaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 4).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetGazownictwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 5).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetMalarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 6).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetKominiarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 7).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetStolarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 8).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetDomInneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 9).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetAranzacjaOgrodowList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 10).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetOgrodnictwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 11).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetBrukarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 12).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetOgrodzeniaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 13).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetKamieniarstwoList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 14).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetOgrodInneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 15).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetMechanikList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 16).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetBlacharzList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 16).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetLakiernikList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 17).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetDetalingList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 18).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetWulkanizacjaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 19).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetMotoryzacjaInneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 20).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetKorepetycjeList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 21).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetInformatykaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 22).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetUrodaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 23).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetNaprawaSerwisList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 24).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetTransportList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 25).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetMonterList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 26).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetUslugiInneList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY == 27).OrderByDescending(x => x.DATE).ToList();
        }

        public List<ADVERTS> GetDomList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY >= 2 && a.CATEGORY <= 9).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetOgrodList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY >= 10 && a.CATEGORY <=15 && a.CATEGORY == 8 ).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetMotoryzacjaList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY >= 16 && a.CATEGORY <= 21).OrderByDescending(x => x.DATE).ToList();
        }
        public List<ADVERTS> GetUslugiList()
        {
            return db.ADVERTS.Where(a => a.CATEGORY >= 22 && a.CATEGORY <= 28).OrderByDescending(x => x.DATE).ToList();
        }
    }

}

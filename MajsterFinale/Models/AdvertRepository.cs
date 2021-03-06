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
        public List<FAV> GetUserFav(int uID)
        {
            return db.FAV.Where(x => x.USER == uID).ToList();
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
    }

}

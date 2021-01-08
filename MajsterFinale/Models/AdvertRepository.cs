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
            return db.ADVERTS.Where(x=>x.CATEGORY == category && ((x.TITLE.Contains(search))||(x.DESCRIPTION.Contains(search)))).OrderByDescending(x => x.ID).ToList(); ;
            //return db.ADVERTS.Where((x => x.CATEGORY == category && (x => x.CATEGORY == category && x.TITLE == search) ).ToList();
            //return db.MESSAGE.Where(x => x.ADVERT_ID == AdvertId && ((x.MSG_TO == UserA && x.MSG_FROM == UserB) || (x.MSG_TO == UserB && x.MSG_FROM == UserA))).OrderByDescending(x => x.ID).ToList();
        }
        public List<CATEGORIES> GetCategoriesNames()
        {
            return db.CATEGORIES.ToList();
        }

        public List<ADVERTS> GetAdsList()
        {
            return db.ADVERTS.OrderByDescending(a => a.ID).ToList();
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
            return db.ADVERTS.Where(x => x.USER_ID == uID).ToList();
        }
    }

}

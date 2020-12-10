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
        public List<CATEGORIES> GetCategoriesNames()
        {
            return db.CATEGORIES.ToList();
        }

        public List<ADVERTS> GetAdsList()
        {
            return db.ADVERTS.OrderByDescending(a => a.ID).ToList();
        }

        public List<MESSAGE> GetMessage()
        {
            return db.MESSAGE.ToList();
        }

        public USERS GetUserData(int uID)
        {
            return db.USERS.SingleOrDefault(x => x.USER_ID == uID);
        }
    }

}

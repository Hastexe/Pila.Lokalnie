using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class AdvertRespository
    {
        private BazaLocal db = new BazaLocal();
        public ADVERTS GetAdverts(int id)
        {
            return db.ADVERTS.SingleOrDefault(d => d.ID == id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class AddingAdsRepository
    {

        private BazaLocal db = new BazaLocal();
        public ADVERTS Advert { get; set; }
        public int CategoryID { get; set; }
        public IEnumerable<CATEGORIES> Categories { get; set; }
        public List<CATEGORIES> GetList()
        {
            return db.CATEGORIES.ToList();
        }
    }
}
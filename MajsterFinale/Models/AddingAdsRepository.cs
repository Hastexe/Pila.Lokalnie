using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [NonAction]
        public bool AreTermsAccepted(AddingAdsRepository AddingAdsRepository)
        {
            var terms = AddingAdsRepository.Advert.TERMS;

            if (terms != true)
            {
                return true;
            }
            return false;
        }
    }
}
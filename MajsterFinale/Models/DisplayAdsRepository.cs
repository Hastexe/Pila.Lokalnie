using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class DisplayAdsRepository
    {
        public PagedList.IPagedList<ADVERTS> ADVERTS { get; set; }
        public USERS LoggedUser { get; set; }
        public List<IMAGES_ADVERT> IMAGES { get; set; }
        public PagedList.IPagedList<CATEGORIES> CATEGORIES { get; set; }
        public List<FAV> FAV { get; set; }
        public List<FAV> FavUsera { get; set; }
    }
}
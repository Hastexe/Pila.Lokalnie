﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class DisplayAdsRepository
    {
        public PagedList.IPagedList<ADVERTS> ADVERTS { get; set; }
        public List<IMAGES_ADVERT> IMAGES { get; set; }
    }
}
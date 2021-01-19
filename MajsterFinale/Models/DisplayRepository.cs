using System.Collections.Generic;

namespace MajsterFinale.Models
{
    public class DisplayRepository
    {
        public ADVERTS AdvertDetails { get; set; }
        public USERS LoggedUser { get; set; }
        public List<IMAGES_ADVERT> Images { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class DisplayRepository
    {
        public IEnumerable<MajsterFinale.Models.ADVERTS> AdvertsList { get; set; }
        public IEnumerable<MajsterFinale.Models.USERS> UsersList { get; set; }
        public IEnumerable<MajsterFinale.Models.MESSAGE> MessageList { get; set; }
    }
}

using System;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;

namespace MajsterFinale.Models
{
    public class DisplayRepository
    {
        public ADVERTS AdvertDetails { get; set; }
        public USERS LoggedUser { get; set; }
    }
}

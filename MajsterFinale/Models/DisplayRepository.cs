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
    public class DisplayRepository
    {
        public ADVERTS AdvertDetails { get; set; }
        public USERS LoggedUser { get; set; }
    }
    public class MessageModel
    {
        public List<ADVERTS> LoggedUserAdverts { get; set; }
        public List<ADVERTS> SecondUserAdverts { get; set; }
        public USERS LoggedUser { get; set; }
        public USERS SecondConversationUser { get; set; }
        public List<MESSAGE> CoversationMessages { get; set; }

    }

    public class PanelMessageModel
    {
        public List<USERS> Users { get; set; }
        public List<MESSAGE> UserMessages { get; set; }
        public List<ADVERTS> MessageAdvertDetails { get; set; }

    }
}

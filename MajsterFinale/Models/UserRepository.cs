using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class UserRepository
    {
        private BazaLocal db = new BazaLocal();
        public USERS GetUserData(int uID)
        {
            return db.USERS.SingleOrDefault(x => x.USER_ID == uID);
        }
        public MESSAGE GetUserMessages(int uID)
        {
            return null;
        }
    }
}
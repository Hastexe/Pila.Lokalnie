using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class UserRepository
    {
        private RegisterRepository registerRepository = new RegisterRepository();
        private BazaLocal db = new BazaLocal();
        public USERS GetUserData(int uID)
        {
            return db.USERS.AsNoTracking().SingleOrDefault(x => x.USER_ID == uID);
        }
        public List<MESSAGE> GetUserMessages(int uID)
        {
            return db.MESSAGE.Where(x => x.MSG_TO == uID || x.MSG_FROM == uID ).OrderByDescending(x => x.DATE).ToList();
        }
        public List<MESSAGE> GetUserNotReadMessages(int uID)
        {
            return db.MESSAGE.Where(x => x.MSG_TO == uID && x.IS_READ == false).ToList();
        }
        public List<MESSAGE> GetConversation(int AdvertId, int UserA, int UserB)
        {
            return db.MESSAGE.Where(x => x.ADVERT_ID == AdvertId && ((x.MSG_TO == UserA && x.MSG_FROM == UserB) || (x.MSG_TO == UserB && x.MSG_FROM == UserA))).OrderByDescending(x => x.ID).ToList();
        }
        public List<USERS> GetUsers()
        {
            return db.USERS.ToList();
        }

        public MESSAGE GetMessage(int ID)
        {
            return db.MESSAGE.AsNoTracking().SingleOrDefault(x => x.ID == ID);
        }
        public List<IMAGES_MESSAGE> GetConversationImages(int AdvertId, int UserA, int UserB)
        {
            return db.IMAGES_MESSAGE.Where(x => x.ADVERT_ID == AdvertId && ((x.MSG_TO == UserA && x.MSG_FROM == UserB) || (x.MSG_TO == UserB && x.MSG_FROM == UserA))).ToList();
        }

    }
}


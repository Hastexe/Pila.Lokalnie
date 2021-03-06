using System.Collections.Generic;

namespace MajsterFinale.Models
{
    public class MessageModel
    {
        public List<ADVERTS> LoggedUserAdverts { get; set; }
        public USERS LoggedUser { get; set; }
        public USERS SecondConversationUser { get; set; }
        public List<MESSAGE> CoversationMessages { get; set; }
        public List<IMAGES_MESSAGE> Images { get; set; }
        public List<ADVERTS> MessageAdvertDetails { get; set; }
    }
}

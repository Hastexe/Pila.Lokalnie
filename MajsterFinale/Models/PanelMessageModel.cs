using System.Collections.Generic;

namespace MajsterFinale.Models
{
    public class PanelMessageModel
    {
        public List<USERS> Users { get; set; }
        public List<MESSAGE> UserMessages { get; set; }
        public List<ADVERTS> MessageAdvertDetails { get; set; }

    }
}

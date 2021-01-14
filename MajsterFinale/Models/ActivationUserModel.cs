using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class ActivationUserModel
    {
        [Required]
        public int uID { get; set; }
    }
}
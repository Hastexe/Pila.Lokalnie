using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Nowe hasło jest wymagane", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same")]
        [DisplayName("Powtórz Hasło")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}
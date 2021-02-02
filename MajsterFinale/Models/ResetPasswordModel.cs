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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&.-]).{8,30}$",
        ErrorMessage = "Hasło nie może być krótsze niż 8 znaków. Musi zawierać dodatkowo przynajmniej 1 cyfre, znak specjalny oraz dużą literę.")]
        [StringLength(300, MinimumLength = 8, ErrorMessage = "Hasło musi składać sie z minumum 8 znaków")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same")]
        [DisplayName("Powtórz Hasło")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}
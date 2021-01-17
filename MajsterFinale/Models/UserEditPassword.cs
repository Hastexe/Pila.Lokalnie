using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MajsterFinale.Models
{
    public class UserEditPassword
    {

        [Required]
        [DataType("Password")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Nowe hasło jest wymagane", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&.-]).{8,}$",
        ErrorMessage = "Hasło nie może być krótsze niż  znaków. Musi zawierać dodatkowo przynajmniej 1 cyfre, znak specjalny oraz dużą literę.")]
        [StringLength(300, MinimumLength = 8, ErrorMessage = "Hasło musi składać sie z minumum 8 znaków")]
        public string NewPassword { get; set; }

        [DataType("Password")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasła do siebie nie pasują.")]
        public string ConfirmNewPassword { get; set; }

        public USERS user { get; set; }
    }
}


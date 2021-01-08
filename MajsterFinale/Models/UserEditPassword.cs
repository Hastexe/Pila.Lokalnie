using System.ComponentModel.DataAnnotations;

namespace MajsterFinale.Models
{
    public class UserEditPassword
    {
        [Required]
        [DataType("Password")]
        public string OldPassword { get; set; }


        [Required]
        [DataType("Password")]
        public string NewPassword { get; set; }

        [DataType("Password")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasła do siebie nie pasują.")]
        public string ConfirmNewPassword { get; set; }

        public USERS user { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace MajsterFinale.Models
{
    public class RegisterViewModel
    {
        public int USER_ID { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
        [Compare("REPASSWORD", ErrorMessage = "*Hasła muszą być takie same.")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [DisplayName("Powtórz Hasło")]
        [DataType(DataType.Password)]
        [Compare("PASSWORD", ErrorMessage = "*Hasła muszą być takie same.")]
        public string REPASSWORD { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [DisplayName("E-mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [DataType(DataType.EmailAddress)]
        public string MAIL { get; set; }

        [DisplayName("Imię")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string FNAME { get; set; }

        public string LNAME { get; set; }
        public Nullable<bool> VERIFIED { get; set; }
        public Nullable<bool> IS_ADMIN { get; set; }

        [Display(Name = "Numer Telefonu")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(9)]
        [MaxLength(9)]
        [RegularExpression(@"^\(?([1-9]{1})\)?([0-9]{8})$", ErrorMessage = "Nieprawidłowy numer telefonu. Poprawny format: 123456789")]
        public string PHONE_NUMBER { get; set; }
        public bool TERMS { get; set; }
        public bool rememberMe { get; set; }
    }
}
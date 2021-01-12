//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MajsterFinale.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;    

    public partial class USERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USERS()
        {
            this.ADVERTS = new HashSet<ADVERTS>();
            this.FAV = new HashSet<FAV>();
            this.MESSAGE = new HashSet<MESSAGE>();
            this.MESSAGE1 = new HashSet<MESSAGE>();
        }

        public int USER_ID { get; set; }
        [DisplayName("Has�o")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^((?=.*[A - Z])(?=.*\d)(?=.*[a - z]) | (?=.*[A - Z])(?=.*\d)(?=.*[!@#$%&\/=?_.-])|(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%&\/=?_.-])|(?=.*\d)(?=.*[a-z])(?=.*[!@#$%&\/=?_.-])).{7,}$", 
        //ErrorMessage = "Password must bt atleast 8-10 characters with characters,numbers,1 upper case letter and special characters.")]
        [StringLength(300, MinimumLength = 6, ErrorMessage = "Has�o musi sk�ada� sie z minumum 6 znak�w")]
        public string PASSWORD { get; set; }

        [DisplayName("Powt�rz Has�o")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
        //ErrorMessage = "Has�o musi sk�ada� si� z minimum 8 znak�w i zawiera� przynajmnej po 1 znaku specjalnym, du�ej literze, ma�ej literze i cyfrze")]
        public string REPASSWORD { get; set; }
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Prosz� poda� prawid�owy mail")]
        public string MAIL { get; set; }

        [DisplayName("Imi�")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Prosz� poda� prawid�owe imie")]
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public Nullable<bool> VERIFIED { get; set; }
        public Nullable<bool> IS_ADMIN { get; set; }

        [Display(Name = "Numer Telefonu")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(9)]
        [MaxLength(9)]
        [RegularExpression(@"^\(?([1-9]{1})\)?([0-9]{8})$", ErrorMessage = "Nieprawid�owy numer telefonu. Poprawny format: 123456789")]
        public string PHONE_NUMBER { get; set; }
        public bool TERMS { get; set; }
        public System.DateTime REGISTER_DATE { get; set; }
        public string RESETPASSWORDCODE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADVERTS> ADVERTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAV> FAV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE> MESSAGE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE> MESSAGE1 { get; set; }
    }
}

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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Advertisements = new HashSet<Advertisement>();
            this.Advertisements1 = new HashSet<Advertisement>();
            this.Comments = new HashSet<Comment>();
            this.Comments1 = new HashSet<Comment>();
        }
        [Required(ErrorMessage ="Pole jest wymagane")]
        public string Login { get; set; }

        [DisplayName("Has�o")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string Password { get; set; }

        [DisplayName("Powt�rz Has�o")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Compare("Password")]
        public string RePassword { get; set; }

        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string Mail { get; set; }

        [DisplayName("Powt�rz E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Compare("Mail")]
        public string ReMail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Advertisement> Advertisements1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments1 { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual Users_Data Users_Data { get; set; }
    }
}

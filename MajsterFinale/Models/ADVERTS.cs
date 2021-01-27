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
    using System.Web.Mvc;

    public partial class ADVERTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADVERTS()
        {
            this.FAV = new HashSet<FAV>();
            this.MESSAGE = new HashSet<MESSAGE>();
            this.IMAGES_ADVERT = new HashSet<IMAGES_ADVERT>();
        }

        public int ID { get; set; }
        public int USER_ID { get; set; }
        [DisplayName("Kategoria")]
        public int CATEGORY { get; set; }
        [DisplayName("Tytu�")]
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string TITLE { get; set; }
        [DisplayName("Opis")]
        [StringLength(750, MinimumLength = 5)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string DESCRIPTION { get; set; }
        [DefaultValue(0)]
        public int? PRICE { get; set; }
        [DisplayName("Zdj�cia")]
        public Nullable<bool> IS_ARCHIVED { get; set; }
        public System.DateTime DATE { get; set; }
        public string PHONE_NUMBER { get; set; }
        public Nullable<bool> IS_HIDDEN { get; set; }


        public virtual CATEGORIES CATEGORIES { get; set; }
        public virtual USERS USERS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAV> FAV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE> MESSAGE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMAGES_ADVERT> IMAGES_ADVERT { get; set; }

        internal class MustBeTrueAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value is bool && (bool)value;
            }

        }
    }
}

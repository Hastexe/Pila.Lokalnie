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
    using System.Web.Mvc;

    public partial class ADVERTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADVERTS()
        {
            this.FAV = new HashSet<FAV>();
            this.MESSAGE = new HashSet<MESSAGE>();
            this.DATE = DateTime.Now;
        }
    
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public int CATEGORY { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public string PRICE { get; set; }
        public string IMAGE { get; set; }
        public Nullable<bool> IS_ARCHIVED { get; set; }
        public System.DateTime DATE { get; set; }
        public string PHONE_NUMBER { get; set; }
    
        public virtual CATEGORIES CATEGORIES { get; set; }
        public virtual USERS USERS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAV> FAV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE> MESSAGE { get; set; }
    }
}

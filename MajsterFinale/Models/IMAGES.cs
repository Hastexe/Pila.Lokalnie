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
    
    public partial class IMAGES
    {
        public int IMAGE_ID { get; set; }
        public string IMAGE_TITLE { get; set; }
        public byte[] IMAGE_BYTE { get; set; }
        public string IMAGE_PATH { get; set; }
        public Nullable<int> ADVERT_ID { get; set; }
        public Nullable<int> MESSAGE_ID { get; set; }
    }
}

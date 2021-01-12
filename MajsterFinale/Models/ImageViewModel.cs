using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MajsterFinale.Models
{
    public class ImageViewModel
    {
        public int IMAGE_ID { get; set; }
        public string IMAGE_TITLE { get; set; }
        public byte[] IMAGE_BYTE { get; set; }
        public string IMAGE_PATH { get; set; }
        IEnumerable<HttpPostedFileWrapper> ImageFile { get; set; }
    }
}
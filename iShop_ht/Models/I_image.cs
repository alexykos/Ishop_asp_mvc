using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace iShop_ht.Models
{
    public class I_image
    {
        [Key]
        public int Code { get; set; }
        public int Upcode { get; set; }
        public string Alias { get; set; }
        public string Ext { get; set; }
        public string Img { get; set; }
        public byte[] Img150x150 { get; set; }

        public byte[] Img300x300 { get; set; }

        [Timestamp]
        public Byte[] Stamp { get; set; }
    }
}
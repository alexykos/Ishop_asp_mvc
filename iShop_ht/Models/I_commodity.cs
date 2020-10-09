using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace iShop_ht.Models
{
    public class I_commodity
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int I_class_code { get; set; }

        public bool is_top { get; set; }

        [Timestamp]
        public Byte[] Stamp { get; set; }
        public virtual I_class I_class { get; set; }


    }
}
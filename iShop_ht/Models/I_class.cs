using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace iShop_ht.Models
{
    public class I_class
    {
        [Key]
        public int Code { get; set; }
        public int Upcode { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool Isgroup { get; set; }

        [Timestamp]
        public Byte[] Stamp { get; set; }

        public virtual List<I_commodity> I_commodities { get; set; }

    }
}
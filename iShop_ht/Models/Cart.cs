using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace iShop_ht.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }

        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }

        public int I_commodityCode { get; set; }
        public virtual I_commodity I_commodity { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace iShop_ht.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }


        [ForeignKey("I_commodity")]
        public int I_commodity_Code { get; set; }
        public virtual I_commodity I_commodity { get; set; }
        public virtual Order Order { get; set; }
    }
}
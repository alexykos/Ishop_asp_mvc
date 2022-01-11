using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iShop_ht.Models
{
    public class Ishop_Entities : DbContext
    {
        public Ishop_Entities() : base("iShop_source") { }
        public DbSet<I_class> I_classes { get; set; }
        public DbSet<I_commodity> I_commodities { get; set; }

        public DbSet<I_delivery> I_deliveries { get; set; }

        public DbSet<I_image> I_images { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Cart> Carts { get; set; }

    }
}
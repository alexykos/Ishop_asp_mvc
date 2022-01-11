using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace iShop_ht.Models
{
    [Bind(Exclude = "OrderId")]
    public partial class Order
    {

        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Требуется заполнить поле FIO")]
        [StringLength(200)]
        //[Display(Name = "")]
        public string FIO { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        [ScaffoldColumn(false)]
        public string City { get; set; }

        [ScaffoldColumn(false)]
        public string Country { get; set; }
        [Required(ErrorMessage = "Требуется заполнить поле телефон")]
        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Phone2 { get; set; }

        public string Contact_info { get; set; }

        public string Company_name { get; set; }

        [StringLength(12)]
        public string INN { get; set; }

        [StringLength(9)]
        public string KPP { get; set; }

        public string Legal_Address { get; set; }

        public string Bank { get; set; }

        [StringLength(9)]
        public string BIK { get; set; }

        [StringLength(20)]
        public string RS { get; set; }

        [StringLength(20)]
        public string KS { get; set; }

        [Required(ErrorMessage = "Требуется заполнить поле емайл")]
        [DisplayName("Email Address")]

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        [Timestamp]
        public byte[] Stamp { get; set; }

        public string Message { get; set; }

        public int is_company { get; set; }

        public int Delivery { get; set; } = 195032;
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
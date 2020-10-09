using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iShop_ht.Models;

namespace iShop_ht.ViewModels
{
    public class ClassListModel
    {
        public int Seed { get; set; }
        public IEnumerable<I_class> clss { get; set; }
    }
}
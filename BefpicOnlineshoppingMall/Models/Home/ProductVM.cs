using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BefpicOnlineshoppingMall.Models.Home
{
    public class ProductVM
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}
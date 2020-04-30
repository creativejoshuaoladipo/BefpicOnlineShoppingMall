using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace BefpicOnlineshoppingMall.Models
{
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100,ErrorMessage ="The Category Name must be a minimum of 3 Characters and Maximum of 100", MinimumLength =3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }

    public class ProductDetails
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage ="ProductName is required")]
        [StringLength(100,MinimumLength =3,ErrorMessage ="Character must be a minimum of 3 and Maximum of 100")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        [Range(1,50)]
        public Nullable<int> CategoryId { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required(ErrorMessage ="Description is Required")]
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        [Required (ErrorMessage ="Quantity is Required")]
        [Range(typeof(int), "1","500",ErrorMessage ="Invalid Quantity----Quantity must not be greater than 500")]
        public Nullable<int> Quantity { get; set; }
        [Required]
        [Range(typeof(decimal),"1","2000000000", ErrorMessage ="Invalid Price")]
        public Nullable<decimal> Price { get; set; }

        public SelectList Catergory { get; set; }


    }
}
using BefpicOnlineshoppingMall.DAL;
using BefpicOnlineshoppingMall.Repository;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BefpicOnlineshoppingMall.Models.Home
{
    public class HomeIndexModel
    {
       // GenericUnitOfWork genericUnit = new GenericUnitOfWork();

        dbMyOnlineShoppingEntities context = new dbMyOnlineShoppingEntities();
        //public IPagedList<Tbl_Product> productList { get; set; }

        public IPagedList<ProductVM> productList { get; set; }


        public HomeIndexModel CreateModel(string search, int pageSize, int? page)
        {

            SqlParameter[] param = new SqlParameter[] {
                   new SqlParameter("@search",search??(object)DBNull.Value)
                   };
            List<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList();//
            //
         
            List<ProductVM> prodList = new List<ProductVM>();
            data.ForEach(p => {
                prodList.Add(
                    new ProductVM 
                    { 
                        ProductId = p.ProductId,
                        IsFeatured = p.IsFeatured,
                        Price = p.Price, 
                        ProductImage = p.ProductImage, 
                        ProductName = p.ProductName,
                        Quantity = p.Quantity
                    });
            });

            IPagedList<ProductVM> prodPageList = prodList.ToPagedList(page ?? 1, pageSize);

            return new HomeIndexModel
            {
                productList = prodPageList
            };
        }

    }
}
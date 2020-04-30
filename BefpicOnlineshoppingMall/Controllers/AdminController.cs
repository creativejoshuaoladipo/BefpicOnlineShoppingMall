using BefpicOnlineshoppingMall.DAL;
using BefpicOnlineshoppingMall.Models;
using BefpicOnlineshoppingMall.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BefpicOnlineshoppingMall.Controllers
{
    public class AdminController : Controller
    {
        GenericUnitOfWork unit = new GenericUnitOfWork();
        public ActionResult Dashboard()
        {
            return View();
        }

        //To fetch all the Category Names needed for the Dropdownlist on Your Form using SelectListItem(to link catId and CatName together)
        public List<SelectListItem> GetCategories()
        {
            //Created an Instance of the SelectListItem class
            List<SelectListItem> list = new List<SelectListItem>();

            //Fetched all Records in our Category Table
             var category = unit.GetRepositoryInstance<Tbl_Category>().GetAllRecords();

            //Looped throught the records to get CategoryName and CategoryId
            foreach(var cat in category)
            {
                //Using the add Method, we stored datas to the Text and Value objects in the selectListItem Class
                list.Add(new SelectListItem { Text = cat.CategoryName, Value = cat.CategoryId.ToString() });

            }

            return list;
        }

        public ActionResult Categories()
        {
            List<Tbl_Category> categories = unit.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(x => x.IsDelete == false).ToList();
            return View(categories);
        }

        public ActionResult CategoryEdit(int categoryId)
        {
            
            return View(unit.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId));
        }


        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category category)
        {

            unit.GetRepositoryInstance<Tbl_Category>().Update(category);
            return RedirectToAction("Categories");
        }

        
        public ActionResult CategoryDelete(int categoryId)
        {
            Tbl_Category det = unit.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId);
            det.IsDelete = true;
             unit.GetRepositoryInstance<Tbl_Category>().Update(det);
            return RedirectToAction("Categories");
        }


        public ActionResult AddCategory()
        {
            return View() ;
        }

        [HttpPost]
        public ActionResult AddCategory(Tbl_Category catID)
        {
            //Tbl_Category cat;

            unit.GetRepositoryInstance<Tbl_Category>().Add(catID);

            //if (catID != null)
            //{
            //    cat = JsonConvert.DeserializeObject<CategoryDetails>(JsonConvert.SerializeObject(unit.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catID)));
            //}
            //else
            //    cat = new Tbl_Category();

            return RedirectToAction("Categories");
        }


        public ActionResult Product()
        {
            //AnotherWay of Doing It
            //List<Tbl_Product> product = unit.GetRepositoryInstance<Tbl_Product>().GetAllRecordsIQueryable().Where(x => x.IsDelete == false).ToList();
            //return View(product);

            IEnumerable<Tbl_Product> product = unit.GetRepositoryInstance<Tbl_Product>().GetProduct().Where(x => x.IsDelete == false);
           return View(product);
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.Category = GetCategories();
            return View(unit.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product product, HttpPostedFileBase productFile)
        {
            string pic = null;
            if (productFile != null)
            {
                pic = System.IO.Path.GetFileName(productFile.FileName);

                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), pic);

                //Saving into the Folder
                productFile.SaveAs(path);
            }
            //Saving into Database
            product.ProductImage = productFile != null? pic: product.ProductImage;

            product.ModifiedDate = DateTime.Now;
            unit.GetRepositoryInstance<Tbl_Product>().Update(product);
            return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product product, HttpPostedFileBase productFile)
        {
            string pic = null;
            if(productFile != null)
            {
                 pic = System.IO.Path.GetFileName(productFile.FileName);

                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), pic);

                //Saving into the Folder
                productFile.SaveAs(path);
            }
            //Saving into Database
            product.ProductImage = pic;
            product.CreatedDate = DateTime.Now;
            unit.GetRepositoryInstance<Tbl_Product>().Add(product);
            return RedirectToAction("Product");
        }

        public ActionResult ProductDelete(int productId)
        {
            Tbl_Product delete = unit.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId);
            delete.IsDelete = true;
            unit.GetRepositoryInstance<Tbl_Product>().Update(delete);
            return RedirectToAction("Product");
        }

    }
}
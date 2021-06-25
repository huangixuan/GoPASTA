using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;
using PagedList;



namespace foodfun.Areas.Admin.Controllers
{
    public class ProductBackController : Controller
    {
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            // return View(repo_product.ReadAll().OrderBy(m => m.mno));

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                return View(db.Products.OrderBy(m => m.product_no).ToPagedList(page, pageSize));

            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {

                Products model = new Products();
                return View(model);

            }
        }

        [HttpPost]
        public ActionResult Create(Products model)
        {
            if (!ModelState.IsValid) return View(model);
           

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                model.product_no = Backend.CreateProductNo(model.category_no);
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
       
        public ActionResult Edit(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Products.Where(m => m.product_no == id).FirstOrDefault();
                return View(model);
            }
        }



        [HttpPost]
        
        public ActionResult Edit(Products model)
        {
            if (!ModelState.IsValid) return View(model);
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var data = db.Products.Where(m => m.product_no == model.product_no).FirstOrDefault();
                data.product_no = model.product_no;
                data.product_name = model.product_name;
                data.product_spec = model.product_spec;
                data.category_no = model.category_no;
                data.price_avgcost = model.price_avgcost;
                data.price_sale = model.price_sale;
                data.discount_price = model.discount_price;
                data.stock_qty = model.stock_qty;
                data.image_path = model.image_path;
                data.description = model.description;
                data.browse_count = model.browse_count;
                data.ishot = model.ishot;
                data.issale = model.issale;
                data.istop = model.istop;
                data.remark = model.remark;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
        [HttpGet]
      
        public ActionResult Delete(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Products.Where(m => m.product_no == id).FirstOrDefault();
                if (model != null)
                {
                    db.Products.Remove(model);
                    db.SaveChanges();
                }
                //return RedirectToAction("Index", "Product");    //Product為controller可略,會以目前controller為主
                return RedirectToAction("Index");
            }
        }
       
        public ActionResult Upload( string product_no)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Products.Where(m => m.product_no == product_no).FirstOrDefault();

                ImageService.ReturnAction("", "ProductBack", "Index");               
                ImageService.ImageTitle = string.Format("{0} {1} 圖片上傳", model.product_no, model.product_name);
                ImageService.ImageTitle = string.Format("圖片上傳");
                ImageService.ImageFolder = "~/img/product";
                ImageService.ImageSubFolder = model.category_no;
                ImageService.ImageName = model.product_no;
                ImageService.ImageExtention = "jpg";
                ImageService.UploadImageMode = true;
                return RedirectToAction("UploadImage", "Image");
            }
        }




    }
}
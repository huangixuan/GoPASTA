using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Staff.Controllers
{
    public class StaffHomeController : Controller
    {
        GoPASTAEntities db;
        public StaffHomeController()
        {
            db = new GoPASTAEntities();
        }

        [LoginAuthorize(RoleList = "Admin/Staff")]
        // GET: Staff/Home
        public ActionResult Index(string id = "HOT")
        {
            //找商品類別名稱
            string int_id = "";         //Category 的rowid
            ViewBag.CategoryNo = id;       //分類的編號   
            ViewBag.CategoryName = Shop.GetCategoryName(id, ref int_id);    //int_id 用ref 傳入改寫原值，把商品分類子類名稱給VB.CategoryName

            //商品list
            AdminOrderViewModel model = new AdminOrderViewModel()
            {
                Order = new Orders(),
                mealServiceList = db.MealService.OrderBy(m => m.mealservice_no).ToList(),
                PaymentsList = db.Payments.OrderBy(m => m.paid_no).ToList(),
                
            };
            if (id == "HOT")
            {
                model.products = Shop.GetHotProducts();
            }
            else
            {
                //prod1 這個編號的商品有沒有   
                var prod1 = db.Products.Where(m => m.category_no == id).ToList();
                model.products = prod1;
            }


            return View(model);
        }

       


        public JsonResult AddToCart(string id)
        {
            var currentCart = CartTemp.GetCurrentCart();
            currentCart.AddCart(id);
            return Json(currentCart, JsonRequestBehavior.AllowGet);
        }

 
        public ActionResult OrderBox(string id) 
        {
            var currentCart = CartTemp.GetCurrentCart();
            currentCart.AddCart(id);

            return PartialView("_PartialOrderBox");

        }

    }
}
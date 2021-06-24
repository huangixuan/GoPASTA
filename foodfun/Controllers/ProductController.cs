using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Controllers
{
    public class ProductController : Controller
    {
        GoPASTAEntities db;
        public ProductController()
        {
            db = new GoPASTAEntities();
        }


        // GET: Product

        //id是點入時傳進來的參數
        [AllowAnonymous]
        public ActionResult CategoryList(string id = "HOT")
        {
            //找商品類別名稱
            string int_id = "";         //Category 的rowid
            ViewBag.CategoryNo = id;       //分類的編號   
            ViewBag.CategoryName = Shop.GetCategoryName(id, ref int_id);    //int_id 用ref 傳入改寫原值，把商品分類子類名稱給VB.CategoryName

            //商品list
            List<Products> model = new List<Products>();
            if (id == "HOT")
            {
                model = Shop.GetHotProducts();
            }
            else
            {
                //prod1 這個編號的商品有沒有   
                var prod1 = db.Products.Where(m => m.category_no == id).ToList();
                if (prod1 != null && prod1.Count > 0)
                {
                    foreach (var item in prod1)
                    { model.Add(item); }
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CategoryList(FormCollection collection)
        {
            int int_qty = 0;
            string str_property_no = collection["product_no"].ToString();
            string str_product_spec = "";
            string str_category_no;
            Shop.ProductNo = str_property_no;
            int.TryParse(collection["qty"].ToString(), out int_qty);
            List<SelectListItem> list = Shop.GetPropertyList(Shop.ProductNo);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (collection[item.Value] != null)
                    {
                        str_product_spec += string.Format("{0} : {1} ", item.Text, collection[item.Value].ToString());
                    }
                }
            }
            Cart.AddCart(Shop.ProductNo, str_product_spec, int_qty);
            str_category_no = Shop.GetCategoryNo(Shop.ProductNo);
            return RedirectToAction("ReturnCategoryList", "Product", new { id = str_category_no });
        }

        [AllowAnonymous]
        public ActionResult ReturnCategoryList(string id)
        {
            return RedirectToAction("CategoryList", "Product", new { id = id });
        }


        [AllowAnonymous]
        public ActionResult CartList()
        {
            IEnumerable<CartViewModel> carts =
                (from objCart in db.Carts join objProd in db.Products
                 on objCart.product_no equals objProd.product_no
                 select new CartViewModel()
                 {
                     rowid = objCart.rowid,
                     cart_lotno = objCart.cart_lotno,
                     mno = objCart.mno,
                     category_no = objProd.category_no,
                     product_no = objCart.product_no,
                     product_name = objProd.product_name,
                     product_spec = objProd.product_spec,
                     Property_select = objCart.Property_select,
                     price_sale = objProd.price_sale,
                     qty = objCart.qty,
                     image_path = objProd.image_path,
                     each_item_amount = objCart.each_item_amount
                 }).ToList();

            if (UserAccount.IsLogin)
            {

                var data1 = carts
                    .Where(m => m.mno == UserAccount.UserNo)
                    .ToList();
                return View(data1);
            }
            var data2 = carts
               .Where(m => m.cart_lotno == Cart.LotNo)
               .ToList();
            return View(data2);
        }


        [AllowAnonymous]
        public ActionResult CartDelete(int id)
        {
            var data = db.Carts
                .Where(m => m.rowid == id).FirstOrDefault();
            if (data != null)
            {
                db.Carts.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("CartList", "Product");

        }

        [AllowAnonymous]
        public ActionResult CartPlus(int id, string prod_no)
        {
            var data = db.Carts
                .Where(m => m.rowid == id)
                .FirstOrDefault();
            if (data != null)
            {
                data.qty += 1;
                data.each_item_amount = data.qty * Shop.GetProductPrice(prod_no);
                db.SaveChanges();
            }
            return RedirectToAction("CartList");
        }


        [AllowAnonymous]
        public ActionResult CartMinus(int id ,string prod_no)
        {
            var data = db.Carts
                .Where(m => m.rowid == id)
                .FirstOrDefault();
            if (data != null)
            {
                if (data.qty > 1)
                {

                    data.qty -= 1;
                    data.each_item_amount = data.qty * Shop.GetProductPrice(prod_no);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("CartList");
        }

 





    }

}
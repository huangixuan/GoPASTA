using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Admin.Controllers
{
    public class PropertysController : Controller
    {
        // GET: Admin/Propertys
        public ActionResult Index()
        {
            // return View(repo_product.ReadAll().OrderBy(m => m.mno));

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                return View(db.Propertys.OrderBy(m => m.rowid).ToList());

            }
        }

        public ActionResult Create()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {

                Propertys model = new Propertys();

                return View(model);

            }
        }

        [HttpPost]
        public ActionResult Create(Propertys model)
        {
            if (!ModelState.IsValid) return View(model);


            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                db.Propertys.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(int id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                //if (id == 0)
                //{
                //    Categorys new_model = new Categorys();
                //    return View(new_model);
                //}
                var models = db.Propertys.Where(m => m.rowid == id).FirstOrDefault();
                return View(models);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(Propertys model)
        {

            if (!ModelState.IsValid) return View(model);
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                // var data = db.Propertys.Where(m => m.property_no == model.property_no).FirstOrDefault();
                var data = db.Propertys.Where(m => m.rowid == model.rowid).FirstOrDefault();
                // data.rowid = model.rowid;
                data.property_no = model.property_no;
                data.property_name = model.property_name;
                data.add_price = model.add_price;
                data.property_value = model.property_value;
                data.remark = model.remark;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        [HttpGet]

        public ActionResult Delete(int id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Propertys.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    db.Propertys.Remove(model);
                    db.SaveChanges();
                }
                //return RedirectToAction("Index", "Product");    //Product為controller可略,會以目前controller為主
                return RedirectToAction("Index");
            }
        }


    }
}
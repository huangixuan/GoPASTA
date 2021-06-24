using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;

namespace foodfun.Areas.Admin.Controllers
{
    public class CategorysController : Controller
    {
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Index()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                return View(db.Categorys.OrderBy(m => m.category_no).ToList());

            }
        }


        public ActionResult Create()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {

                Categorys model = new Categorys();
                
                return View(model);

            }
        }

        [HttpPost]
        public ActionResult Create(Categorys model)
        {
            if (!ModelState.IsValid) return View(model);
           

            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                db.Categorys.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

     
        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                //if (id == 0)
                //{
                //    Categorys new_model = new Categorys();
                //    return View(new_model);
                //}
                var models = db.Categorys.Where(m => m.category_no == id).FirstOrDefault();
                return View(models);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(Categorys model)
        {

            if (!ModelState.IsValid) return View(model);
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var data = db.Categorys.Where(m => m.category_no == model.category_no).FirstOrDefault();
                data.rowid = model.rowid;
                data.parentid = model.parentid;
                data.category_no = model.category_no;
                data.category_name = model.category_name;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

       

        [HttpGet]

        public ActionResult Delete(string id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Categorys.Where(m => m.category_no == id).FirstOrDefault();
                if (model != null)
                {
                    db.Categorys.Remove(model);
                    db.SaveChanges();
                }
                //return RedirectToAction("Index", "Product");    //Product為controller可略,會以目前controller為主
                return RedirectToAction("Index");
            }
        }
    }
}
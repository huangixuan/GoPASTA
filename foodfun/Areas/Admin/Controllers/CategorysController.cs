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




        public ActionResult GetDataList()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                int int_pid = int.Parse(Session["CategoryID"].ToString());
                var models = db.Categorys
                    .Where(m => m.parentid == int_pid)
                    .OrderBy(m => m.category_no).ToList();
                return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                if (id == 0)
                {
                    Categorys new_model = new Categorys();
                    return View(new_model);
                }
                var models = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
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
                    var data = db.Categorys.Where(m => m.rowid == model.rowid).FirstOrDefault();
                    //data.rowid = model.rowid;
                    data.parentid = model.parentid;
                    data.category_no = model.category_no;
                    data.category_name = model.category_name;
                   
                
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
           
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Delete(int id)
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [LoginAuthorize(RoleList = "Admin")]
        public ActionResult DeleteData(int id)
        {
            bool status = false;
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    db.Categorys.Remove(model);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult ReturnToParent()
        {
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                int int_pid = int.Parse(Session["CategoryID"].ToString());
                var model = db.Categorys.Where(m => m.rowid == int_pid).FirstOrDefault();
                if (model != null)
                {
                    int_pid = model.parentid.GetValueOrDefault();
                }
                return RedirectToAction("Index", "Category", new { id = int_pid });
            }
        }

        private void SetSessionValue(int id = 0)
        {
            Session["CategoryID"] = id;
            Session["CategoryNo"] = "";
            Session["CategoryName"] = "";
            Session["ParentID"] = "";
            Session["ParentName"] = "";
            int int_pid = 0;
            string str_parent_id = id.ToString();
            string str_parent_name = "";
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var model = db.Categorys.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    Session["CategoryNo"] = model.category_no;
                    Session["CategoryName"] = model.category_name;
                    int_pid = model.parentid.GetValueOrDefault();
                    str_parent_name = model.category_name;

                    while (int_pid > 0)
                    {
                        var parent = db.Categorys.Where(m => m.rowid == int_pid).FirstOrDefault();
                        if (parent == null) break;

                        int_pid = parent.parentid.GetValueOrDefault();
                        str_parent_id = parent.rowid.ToString() + "," + str_parent_id;
                        str_parent_name = parent.category_name + "," + str_parent_name;
                    }
                }
            }
            Session["ParentID"] = str_parent_id;
            Session["ParentName"] = str_parent_name;
        }
    }
}
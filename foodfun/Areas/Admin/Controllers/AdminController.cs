using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace foodfun.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
       // [LoginAuthorize(RoleList = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
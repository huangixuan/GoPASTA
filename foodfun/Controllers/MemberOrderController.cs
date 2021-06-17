using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using foodfun.Models;


namespace foodfun.Controllers
{
    public class MemberOrderController : Controller
    {
        // GET: MemberOrder

        [LoginAuthorize(RoleList = "Member")]
        public ActionResult Index()
        {
            using (GoPASTAEntities db = new GoPASTAEntities()) { 
            
            }



                return View();
        }
    }
}
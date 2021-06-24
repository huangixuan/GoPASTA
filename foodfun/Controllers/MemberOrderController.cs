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
        GoPASTAEntities db = new GoPASTAEntities();
        // GET: MemberOrder

        [LoginAuthorize(RoleList = "Member")]
        public ActionResult Index()
        {
                return View(Order.GetOrderList(true));
        }


    }


}
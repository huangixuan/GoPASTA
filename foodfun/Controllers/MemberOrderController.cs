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
            var order = db.Orders.Where(m => m.mno == UserAccount.UserNo).OrderBy(m => m.mno).ToList();
            int num = order.Count();

            List<OrdersViewModel> ordersViewModels = new List<OrdersViewModel>();
            for (int i = 0; i < num; i++)
            {
                string orders_no = order[i].orderstatus_no;
                string meal_no = order[i].mealservice_no;

                ordersViewModels.Add(new OrdersViewModel()
                {
                    order_no = order[i].order_no,
                    order_date = order[i].order_date,
                    total = order[i].total,
                    ispaided = order[i].ispaided,
                    orderstatus_name = db.OrderStatus.Where(m => m.orderstatus_no == orders_no).FirstOrDefault().orderstatus_name,
                    mealservice_name = db.MealService.Where(m => m.mealservice_no == meal_no).FirstOrDefault().mealservice_name
                }); 
            }
                return View(ordersViewModels);
        }
    }
}
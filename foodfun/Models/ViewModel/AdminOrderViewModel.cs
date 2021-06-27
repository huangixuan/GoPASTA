using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class AdminOrderViewModel
    {
        public List<Products> products { get; set; }
        public Orders Order { get; set; }
        public List<Carts> carts { get; set; }
        public List<MealService> mealServiceList { get; set; }
        public List<Payments> PaymentsList { get; set; }




    }
}
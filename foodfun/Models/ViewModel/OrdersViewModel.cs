using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class OrdersViewModel
    {
        [Display(Name = "訂單編號")]
        public string order_no { get; set; }

        [Display(Name = "訂單日期")]
        public Nullable<System.DateTime> order_date { get; set; }
        [Display(Name = "總計")]
        public Nullable<decimal> total { get; set; }


        [Display(Name = "是否結帳")]
        public Nullable<bool> ispaided { get; set; }


        [Display(Name = "訂單狀態")]
        public string orderstatus_name { get; set; }

        [Display(Name = "用餐方式編碼")]
        public string mealservice_name { get; set; }


    }
}
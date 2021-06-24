using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class MealServiceViewModel
    {
        [Display(Name = "用餐方式")]
        [Required(ErrorMessage ="請選擇用餐方式")]
        public string mealservice_no { get; set; }

        public List<MealService> mealServiceList { get; set; }
        [Display(Name ="桌號")]
        public string Table_no { get; set; }

        [Display(Name ="取餐時間")]
        public DateTime SchedulOrderTime { get; set; } = DateTime.Now;

        [Display(Name ="外送地址")]
        //[Required(ErrorMessage ="請入外送地址")]
        public string receive_address { get; set; }
    }
}
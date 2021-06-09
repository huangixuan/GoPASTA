using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class CartViewModel
    {
        [Display(Name = "流水編碼")]
        public int rowid { get; set; }
        [Display(Name = "購物車批次碼")]
        public string cart_lotno { get; set; }

        [Display(Name = "使用者編號")]
        public string mno { get; set; }


        [Display(Name = "子類別編號")]
        public string category_no { get; set; }

        [Display(Name ="商品編號")]
        public string product_no { get; set; }


        [Display(Name = "商品名稱")]
        public string product_name { get; set; }

        [Display(Name = "商品規格")]
        public string product_spec { get; set; }

        [Display(Name = "選擇屬性")]
        public string Property_select { get; set; }

        [Display(Name = "售價")]
        public Nullable<int> price_sale { get; set; }


        [Display(Name = "數量")]
        public Nullable<int> qty { get; set; }

        [Display(Name = "產品圖片路徑")]
        public string image_path { get; set; }

        [Display (Name = "品項小計")]
        public Nullable<int> each_item_amount { get; set; }



    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    [MetadataType(typeof(CartsMetaData))]
    public partial class Carts
    {
        private class CartsMetaData
        {
            [Display(Name = "流水編碼")]
            public int rowid { get; set; }

            [Display(Name = "購物車批次碼")]
            public string cart_lotno { get; set; }

            [Display(Name = "使用者編號")]
            public string mno { get; set; }

            [Display(Name = "商品編號")]
            public string product_no { get; set; }


            [Display(Name ="選擇屬性")]
            public string Property_select { get; set; }

            [Display(Name ="數量")]
            public Nullable<int> qty { get; set; }
            [Display(Name ="建立時間")]
            public Nullable<System.DateTime> create_time { get; set; }
            [Display(Name = "品項小計")]
            public Nullable<int> each_item_amount { get; set; }







        }
    }
}
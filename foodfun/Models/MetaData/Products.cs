using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{


   

    [MetadataType(typeof(ProductsMetaData))]

    public partial class Products
    {
        


        private class ProductsMetaData
        {
            [Key]
            [Display(Name ="商品編號")]
            public string product_no { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入商品名稱")]
            [Required(ErrorMessage ="商品名稱不可空白")]
            [Display(Name ="商品名稱")]
            public string product_name { get; set; }

            [Required(ErrorMessage ="商品規格不可空白")]
            [Display(Name ="商品規格")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入商品規格")]
            public string product_spec { get; set; }

            [Display(Name = "類別編號")]
            public string category_no { get; set; }


            [Display(Name ="平均成本")]
            public Nullable<int> price_avgcost { get; set; }

            [Display(Name ="售價")]
            [DisplayFormat(DataFormatString = "{0:C0}")]
            public Nullable<int> price_sale { get; set; }

            [Display(Name = "折扣金額")]
            public Nullable<int> discount_price { get; set; }

            [Display(Name ="庫存數量")]
            public int stock_qty { get; set; }
           
            [Display(Name ="產品圖片")]
            public string image_path { get; set; }

            [Display(Name="商品描述")]
            public string description { get; set; }

            [Display(Name ="瀏覽次數")]
            public Nullable<int> browse_count { get; set; }

            [Display(Name = "熱銷商品")]
            public Nullable<bool> ishot { get; set; }

            [Display(Name = "首頁顯示")]
            public Nullable<bool> istop { get; set; }

            [Display(Name = "上架銷售")]
            public Nullable<bool> issale { get; set; }

            [Display(Name ="備註")]
            public string remark { get; set; }


        }



    }
}
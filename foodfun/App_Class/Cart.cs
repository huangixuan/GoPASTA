using foodfun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// 購物車類別
/// </summary>
public static class Cart
{
    #region 公開屬性
    /// <summary>
    /// 訂單編號
    /// </summary>
    public static string OrderNo { get; set; }

    /// <summary>
    /// 購物批號 LotNo
    /// </summary>
    public static string LotNo
    {
        get { return GetLotNo(); }
        set { HttpContext.Current.Session["CartLotNo"] = value; }
    }
    /// <summary>
    /// 購物批號建立時間
    /// </summary>
    public static DateTime LotCreateTime
    {
        get { return GetLotCreateTime(); }
        set { HttpContext.Current.Session["CartCreateTime"] = value; }
    }
    /// <summary>
    /// 購物車筆數
    /// </summary>
    public static int Counts { get { return GetCartCount(); } }

    public static int Count_Prop_Num { get { return GetPropNum(); } }



    /// <summary>
    /// 購物車合計
    /// </summary>
    public static int Totals { get { return GetCartTotals(); } }
    #endregion
    #region 公用函數
    /// <summary>
    /// 更新購物批號
    /// </summary>
    /// <returns></returns>
    public static string NewLotNo()
    {
        string str_lot_no = "";
        if (!UserAccount.IsLogin)
            str_lot_no = Guid.NewGuid().ToString().Substring(0, 15).ToUpper();
        LotNo = str_lot_no;
        LotCreateTime = DateTime.Now;
        return str_lot_no;
    }
    #endregion
    #region 公用事件
    /// <summary>
    /// 登入時將現有遊客的購物車加入客戶的購物車
    /// </summary>
    public static void LoginCart()
    {
        if (!string.IsNullOrEmpty(LotNo))
        {
            int int_qty = 0;
            using (GoPASTAEntities db = new GoPASTAEntities())
            {
                var datas = db.Carts
                   .Where(m => m.cart_lotno == LotNo)
                   .ToList();
                if (datas != null)
                {
                    foreach (var item in datas)
                    {
                        int_qty = item.qty.GetValueOrDefault();
                        AddCart(item.product_no, item.Property_select, int_qty);
                        db.Carts.Remove(item);
                    }
                    db.SaveChanges();
                }
            }
            NewLotNo();
        }
    }
    /// <summary>
    /// 加入購物車
    /// </summary>
    /// <param name="productNo">商品編號</param>
    public static void AddCart(string productNo)
    {
        AddCart(productNo, "", "", 1);
    }

    /// <summary>
    /// 加入購物車
    /// </summary>
    /// <param name="productNo">商品編號</param>
    /// <param name="prod_Spec">商品規格</param>
    /// <param name="buyQty">數量</param>
    public static void AddCart(string productNo, string prod_Spec, int buyQty)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            int int_price = Shop.GetProductPrice(productNo);
            int int_amount = (buyQty * int_price);
            var datas = db.Carts
                .Where(m => m.cart_lotno == LotNo)
                .Where(m => m.mno == UserAccount.UserNo)
                .Where(m => m.product_no == productNo)
                .Where(m => m.Property_select == prod_Spec)
                .FirstOrDefault();

            if (datas == null)
            {
                Carts models = new Carts();
                models.cart_lotno = LotNo;
                models.mno = UserAccount.UserNo;
                models.create_time = LotCreateTime;
                models.product_no = productNo;
                models.Property_select = prod_Spec;
                models.qty = buyQty;
                models.each_item_amount = int_amount;
                db.Carts.Add(models);
                db.SaveChanges();
            }
            else
            {
                datas.qty += buyQty;
                datas.each_item_amount = buyQty * int_price;
                db.SaveChanges();
            }
        }
    }

    /// <summary>
    /// 加入購物車
    /// </summary>
    /// <param name="productNo">商品編號</param>
    /// <param name="color_no">商品顏色</param>
    /// <param name="size_no">商品尺寸</param>
    /// <param name="buyQty">數量</param>
    public static void AddCart(string productNo, string color_no, string size_no, int buyQty)
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            string str_spec = "";
            if (!string.IsNullOrEmpty(color_no)) str_spec += string.Format("顏色:{0}", color_no);
            if (!string.IsNullOrEmpty(size_no))
            {
                if (!string.IsNullOrEmpty(str_spec)) str_spec += " ";
                str_spec += string.Format("尺寸:{0}", size_no);
            }
            AddCart(productNo, str_spec, buyQty);
        }
    }

    /// <summary>
    /// 消費者付款
    /// </summary>
    //public static int CartPayment(ConfirmationViewModel model)
    //{
    //    int int_order_id = 0;
    //    OrderNo = CreateNewOrderNo(model);
    //    using (GoPASTAEntities db = new GoPASTAEntities())
    //    {
    //        var datas = db.Carts
    //           .Where(m => m.mno == UserAccount.UserNo)
    //           .ToList();
    //        if (datas != null)
    //        {
    //            int int_amount = datas.Sum(m => m.amount).GetValueOrDefault();
    //            decimal dec_tax = 0;
    //            if (int_amount > 0)
    //            {
    //                dec_tax = Math.Round((decimal)(int_amount * 5 / 100), 0);
    //            }
    //            int int_total = int_amount + (int)dec_tax;
    //            var data = db.Orders.Where(m => m.order_no == OrderNo).FirstOrDefault();
    //            if (data != null)
    //            {
    //                data.amounts = int_amount;
    //                data.taxs = (int)dec_tax;
    //                data.totals = int_total;
    //                db.SaveChanges();
    //            }

    //            foreach (var item in datas)
    //            {
    //                OrdersDetail detail = new OrdersDetail();
    //                detail.order_no = OrderNo;
    //                detail.product_no = item.product_no;
    //                detail.product_name = item.product_name;
    //                detail.vendor_no = Shop.GetVendorNoByProduct(item.product_no);
    //                detail.category_name = Shop.GetCategoryName(item.product_no);
    //                detail.product_spec = item.product_spec;
    //                detail.qty = item.qty;
    //                detail.price = item.price;
    //                detail.amount = item.amount;
    //                detail.remark = "";
    //                db.OrdersDetail.Add(detail);
    //                db.SaveChanges();

    //                db.Carts.Remove(item);
    //                db.SaveChanges();
    //            }
    //        }
    //    }
    //    return int_order_id;
    //}

    #endregion
    #region 私有函數
    /// <summary>
    /// 取得購物批號建立時間
    /// </summary>
    /// <returns></returns>
    private static DateTime GetLotCreateTime()
    {
        object obj_time = HttpContext.Current.Session["CartCreateTime"];
        return (obj_time == null) ? DateTime.Now : DateTime.Parse(obj_time.ToString());
    }

    /// <summary>
    /// 取得購物批號
    /// </summary>
    /// <returns></returns>
    private static string GetLotNo()
    {
        return (HttpContext.Current.Session["CartLotNo"] == null) ? NewLotNo() : HttpContext.Current.Session["CartLotNo"].ToString();
    }

    /// <summary>
    /// 取得目前購物車筆數
    /// </summary>
    /// <returns></returns>
    private static int GetCartCount()
    {
        int int_count = 0;
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            if (UserAccount.IsLogin)
            {
                var data1 = db.Carts
                    .Where(m => m.mno == UserAccount.UserNo)
                    .ToList();
                if (data1 != null) int_count = data1.Count;
            }
            else
            {
                var data2 = db.Carts
                   .Where(m => m.cart_lotno == LotNo)
                   .ToList();
                if (data2 != null) int_count = data2.Count;
            }
        }
        return int_count;
    }
    private static int GetPropNum()
    {
        int int_pro_num = 0;
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            if (UserAccount.IsLogin)
            {
                var data1 = db.Carts.Where(m => m.mno == UserAccount.UserNo).ToList();
                if (data1 != null)
                {
                    int_pro_num = (int)data1.Sum(m => m.qty);
                }
                else
                {
                    var data2 = db.Carts
                       .Where(m => m.cart_lotno == LotNo)
                       .ToList();
                    if (data2 != null) int_pro_num = (int)data2.Sum(m => m.qty);
                }

            }
        }
        return int_pro_num;
    }




    private static int GetCartTotals()
    {
        int? int_totals = 0;
        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            if (UserAccount.IsLogin)
            {

                var data1 = db.Carts
                    .Where(m => m.mno == UserAccount.UserNo)
                    .ToList();
                if (data1 != null) int_totals = data1.Sum(m => m.each_item_amount);
            }
            else
            {
                var data2 = db.Carts
                   .Where(m => m.cart_lotno == LotNo)
                   .ToList();
                if (data2 != null) int_totals = data2.Sum(m => m.each_item_amount);
            }
        }
        if (int_totals == null) int_totals = 0;
        return int_totals.GetValueOrDefault();
    }

    /// <summary>
    /// 新增New Order
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static void AddNewOrder(ConfirmationViewModel model)
    {
        Shop.OrderID = 0;

        string str_guid = Guid.NewGuid().ToString().Substring(0, 25);

        using (GoPASTAEntities db = new GoPASTAEntities())
        {
            Orders orders = new Orders();

            orders.isclosed = false;
            orders.ispaided = false;
            orders.order_date = DateTime.Now;
            orders.mno = UserAccount.UserNo;
            orders.orderstatus_no = "TBC";
            orders.total = model.Order.total;
            orders.mealservice_no = model.Order.mealservice_no;
            orders.SchedulOrderTime = model.Order.SchedulOrderTime;
            orders.table_no = model.Order.table_no;
            orders.paid_no = model.Order.paid_no;
            orders.receive_name = model.Order.receive_name;
            orders.receive_phone = model.Order.receive_phone;
            orders.receive_address = model.Order.receive_address;
            orders.cancelorder = false;
            orders.cancelreason = "";
            orders.order_guid = str_guid;
            orders.remark = "";


            db.Orders.Add(orders);
            db.SaveChanges();

            var neword = db.Orders.Where(m => m.order_guid == str_guid).FirstOrDefault();
            if (neword != null)
            {
                Shop.OrderID = neword.rowid;
            }


        }
    }
    public static string GetOrderNO() 
    {
        using (GoPASTAEntities db = new GoPASTAEntities()) 
        {
            Shop.OrderNo = "";
            var neword = db.Orders.Where(m => m.rowid == Shop.OrderID).FirstOrDefault();
            if (neword != null)
            {
                Shop.OrderNo = neword.order_no;
            }
            return Shop.OrderNo;

        }
    }

    public static void AddNewOrderDetail()
    {
        using (GoPASTAEntities db = new GoPASTAEntities())
        {

            List<Carts> datas;
            if (UserAccount.IsLogin)
            { datas = db.Carts.Where(m => m.mno == UserAccount.UserNo).ToList(); }
            else
            { datas = db.Carts.Where(m => m.cart_lotno == Cart.LotNo).ToList(); }

            if (datas != null)
            {
                foreach (var item in datas)
                {
                    OrdersDetails ordersDetail = new OrdersDetails()
                    {
                        order_no = Shop.OrderNo,
                        product_no = item.product_no,
                        Property_select = item.Property_select,
                        each_item_amount = item.each_item_amount,
                        qty = item.qty,
                        remark = "",
                    };
                    db.OrdersDetails.Add(ordersDetail);
                    db.SaveChanges();
                }
                db.Carts.RemoveRange(datas);
                db.SaveChanges();
            }
 

        }
    }
    #endregion
}




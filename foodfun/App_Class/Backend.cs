using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foodfun.Models;



public static class Backend
{
    public static string CreateProductNo(string id)
    {


        GoPASTAEntities db = new GoPASTAEntities();

        string productno;
        var P = db.Products.Where(m => m.product_no.StartsWith(id)).Select(m => m.product_no).Max();
       if (P != null)
        {
            int X = Convert.ToInt32(P.Substring(3));
            X += 1;
           
            productno = id + X.ToString("D4");
        }
      else
        {
            productno = id + "0001"; 

        }

        return productno;


    }

}

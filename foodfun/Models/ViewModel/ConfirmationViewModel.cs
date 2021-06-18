using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class ConfirmationViewModel
    {
        public Orders Order { get; set; }
        public IEnumerable<Carts> Cart { get; set; }

        [Display(Name ="用餐方式")]
        public string mealservice_name { get; set; }

        public List<Payments> PaymentsList { get; set; }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class ConfirmationViewModel
    {
        public Orders Order { get; set; }
        public IEnumerable<Carts> Cart { get; set; }


    }
}
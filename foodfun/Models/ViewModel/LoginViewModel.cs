using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class LoginViewModel
    {
        [Display(Name ="登入帳號")]
        [Required(ErrorMessage ="登入帳號不可空白!!")]
        public string UserNo { get; set; }


        [Display(Name ="登入密碼")]
        [Required(ErrorMessage ="登入密碼不可空白")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
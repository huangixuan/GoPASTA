using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "請輸入電子信箱")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "電子郵件不可空白")]
        public string email { get; set; }
    }
}
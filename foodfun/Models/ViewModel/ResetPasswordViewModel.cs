using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace foodfun.Models
{
    public class ResetPasswordViewModel
    {
        [Display(Name ="新的密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="新的密碼不可空白")]
        public string NewPassword { get; set; }



        [Display(Name = "確認密碼")]
        [Required(ErrorMessage = "確認密碼不可空白")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "新密碼與確認密碼不同!!")]
        public string ConfirmPassword { get; set; }

    }
}
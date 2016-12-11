using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModels.Account
{
    public class AccountLoginView
    {
        [StringLength(20)]
        [DisplayName("使用者帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [StringLength(50)]
        [DisplayName("使用者密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModels
{
    public class AccountChangePasswordView
    {
        [DisplayName("輸入密碼")]
        public string Password { get; set; }

        [DisplayName("新密碼")]
        public string NewPassword { get; set; }
    }
}

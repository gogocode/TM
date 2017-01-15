using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class UserEditProfileView
    {
        [DisplayName("員工編號")]
        public string EmployeeId { get; set; }

        [DisplayName("使用者帳號")]
        [Required(ErrorMessage = "請輸入使用者帳號")]
        public string Account { get; set; }

        [DisplayName("中文名稱")]
        [Required(ErrorMessage = "請輸入中文名稱")]
        public string UserName { get; set; }

        [DisplayName("電子郵件")]
        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式錯誤")]
        public string Email { get; set; }

    }
}

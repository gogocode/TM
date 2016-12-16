using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class UserIndexView
    {
        [DisplayName("使用者名稱")]
        public string UserName { get; set; }
        public User AddUser { get; set; }
        public IPagedList<User> Users { get; set; }
        public int CurrentPage { get; set; }
    }
}

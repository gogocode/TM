using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class UserEditRoleView
    {
        public UserEditRoleView()
        {
            SelectedRoleIds = new List<int>();
        }

        public int UserId { get; set; }

        public List<Role> Roles { get; set; }
        public List<int> SelectedRoleIds { get; set; }
    }
}

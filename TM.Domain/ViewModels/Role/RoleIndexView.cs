using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class RoleIndexView
    {
        public Role AddRole { get; set; }
        public List<Role> Roles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class RoleEditCatalogView
    {
        public int RoleId { get; set; }

        public List<Catalog> Catalogs { get; set; }

        public List<int> SelectedCatalogIds { get; set; }
    }
}

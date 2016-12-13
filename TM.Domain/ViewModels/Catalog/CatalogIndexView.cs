using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class CatalogIndexView
    {
        public Catalog AddCatalog { get; set; }
        public List<Catalog> Catalogs { get; set; }
    }
}

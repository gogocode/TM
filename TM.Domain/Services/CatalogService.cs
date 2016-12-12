using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.Services
{
    public class CatalogService
    {
        private TMDbContext _db;

        public CatalogService()
        {
            _db = new TMDbContext();
        }

    }
}

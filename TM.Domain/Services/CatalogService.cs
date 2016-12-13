using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;
using System.Data.Entity;

namespace TM.Domain.Services
{
    public class CatalogService
    {
        private TMDbContext _db;

        public CatalogService()
        {
            _db = new TMDbContext();
        }

        public Catalog Find(int id)
        {
            return _db.Catalogs.Include(c => c.ParentCatalog).Where(x=>x.CatalogId == id).FirstOrDefault();
        }

        public List<Catalog> FindAll()
        {
            return _db.Catalogs.Include(c => c.ParentCatalog).OrderBy(x=>x.CatalogId).ToList();
        }

        public int Create(Catalog catalog)
        {
            try
            {
                _db.Catalogs.Add(catalog);
                return _db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int Modify(Catalog catalog)
        {
            try
            {
                _db.Entry(catalog).State = EntityState.Modified;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

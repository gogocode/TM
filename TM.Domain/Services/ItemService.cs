using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.Services
{
    public class ItemService
    {
        private TMDbContext _db;

        public ItemService()
        {
            _db = new TMDbContext();
        }

        public Item Find(int id)
        {
            return _db.Items.Where(x => x.ItemId == id).FirstOrDefault();
        }

        public IEnumerable<Item> FindAll()
        {
            return _db.Items.AsEnumerable();
        }

        public int Create(Item item)
        {
            try
            {
                _db.Items.Add(item);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Modify(Item item)
        {
            try
            {
                _db.Entry(item).State = EntityState.Modified;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            try
            {
                Item item = _db.Items.Where(x => x.ItemId == id).FirstOrDefault();
                _db.Items.Remove(item);

                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

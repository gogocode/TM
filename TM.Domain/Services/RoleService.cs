using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.Services
{
    public class RoleService
    {
        private TMDbContext _db;

        public RoleService()
        {
            _db = new TMDbContext();
        }

        public Role Find(int id)
        {
            return _db.Roles.Where(x => x.RoleId == id).FirstOrDefault();
        }

        public IEnumerable<Role> FindAll()
        {
            return _db.Roles.AsEnumerable();
        }

        public int Create(Role role)
        {
            try
            {
                _db.Roles.Add(role);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Modify(Role role)
        {
            try
            {
                _db.Entry(role).State = EntityState.Modified;
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
                Role role = _db.Roles.Where(x => x.RoleId == id).FirstOrDefault();
                _db.Roles.Remove(role);

                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

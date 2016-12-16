using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;
using TM.Domain.ViewModels;

namespace TM.Domain.Services
{
    public class UserService
    {
        private TMDbContext _db;

        public UserService()
        {
            _db = new TMDbContext();
        }

        public User Find(int id)
        {
            return _db.Users.FirstOrDefault(x => x.UserId == id);
        }

        public User FindUser(AccountLoginView vm)
        {
            return _db.Users.FirstOrDefault(x => x.Account == vm.Account && x.Password == vm.Password);
        }

        public IPagedList<User> FindPagedUsers(string userName,int pageNumber,int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            var users = _db.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                users = users.Where(x => x.UserName.Contains(userName));
            }

            users = users.OrderBy(x => x.UserId);

            return users.ToPagedList(pageNumber, pageSize);
        }

        public User FindUserByAccount(string account)
        {
            return _db.Users.FirstOrDefault(x => x.Account == account);
        }

        public int Create(User user)
        {
            try
            {
                _db.Users.Add(user);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Modify(User user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Modified;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ModifyUserRoles(int userId,List<int> selectedRoleIds)
        {
            try
            {
                User user = _db.Users.Where(x => x.UserId == userId).FirstOrDefault();
                List<Role> delRoles = user.Roles.ToList();

                List<Role> roles = _db.Roles.Where(x => selectedRoleIds.Contains(x.RoleId)).ToList();

                foreach (var delRole in delRoles)
                {
                    user.Roles.Remove(delRole);
                }

                user.Roles = roles;
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
                User user = _db.Users.Where(x => x.UserId == id).FirstOrDefault();
                _db.Users.Remove(user);

                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

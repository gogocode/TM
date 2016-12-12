using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;
using TM.Domain.ViewModels.Account;

namespace TM.Domain.Services
{
    public class UserService
    {
        private TMDbContext _db;

        public UserService()
        {
            _db = new TMDbContext();
        }

        public User FindUser(AccountLoginView vm)
        {
            return _db.Users.FirstOrDefault(x => x.Account == vm.Account && x.Password == vm.Password);
        }

        public User FindUserByAccount(string account)
        {
            return _db.Users.FirstOrDefault(x => x.Account == account);
        }

        public void Test()
        {
            var xx = _db.Users.Select(x=>x);
        }
    }
}

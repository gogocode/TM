using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.Services
{
    public class UserService
    {
        TMDbContext _db = new TMDbContext();

        public UserService()
        {
        }

        public void Test()
        {
            var xx = _db.Users.Select(x=>x);
        }
    }
}

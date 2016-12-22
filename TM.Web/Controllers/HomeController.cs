using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain.Models;
using TM.Domain.Services;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class HomeController : Controller
    {
        private UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Menu()
        {
            List<Catalog> catalogs = new List<Catalog>();
            
            User user = _userService.FindUserByAccount(User.Identity.Name);
            if (user != null)
            {
                foreach(var role in user.Roles)
                {
                    catalogs.AddRange(role.Catalogs);
                }
            }

            List<Catalog> catalogsByDistinc = (from c in catalogs
                                              group c by c.CatalogId into g
                                              select g.First()).ToList();

            return PartialView(catalogsByDistinc);
        }

        public ActionResult ProfileMenu()
        {
            return PartialView();

        }

    }
}
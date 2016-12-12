using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain.Models;
using TM.Domain.Services;

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
            _userService.Test();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
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

    }
}
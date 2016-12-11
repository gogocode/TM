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

     
        //public PartialViewResult Navigation()
        //{
        //    List<Catalog> catalogList = new List<Catalog>();

        //    User me = _userService.FindByAccount(User.Identity.Name);
        //    if (me != null)
        //    {
        //        catalogList = catalogsService.FindAllByUserId(me.UserId).ToList();
        //    }

        //    return PartialView(catalogList);
        //}
      
    }
}
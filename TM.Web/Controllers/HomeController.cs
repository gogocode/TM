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
        UserService _userService = new UserService();

        public HomeController()
        {
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
    }
}
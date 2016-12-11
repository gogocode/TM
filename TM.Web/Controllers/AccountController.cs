using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain.ViewModels.Account;

namespace TM.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPost(AccountLoginView vm)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Login",vm);
        }
    }
}
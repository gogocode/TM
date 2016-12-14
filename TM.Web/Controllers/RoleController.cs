using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class RoleController : Controller
    {
        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            return View();
        }
    }
}
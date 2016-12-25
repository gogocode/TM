using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class ExportController : Controller
    {
        //匯入工作日誌
        [HttpGet]
        [CheckAuth]
        public ActionResult Diary()
        {
            return View();
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult DiaryPost(HttpPostedFileBase file)
        {
            return View();
        }
    }
}

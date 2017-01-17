using LinqToExcel;
using LinqToExcel.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain.ViewModels.Export;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class ExportController : Controller
    {
        #region 匯入工作日誌
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
        #endregion

        #region 匯入文件
        [HttpGet]
        [CheckAuth]
        public ActionResult Doc()
        {
            return View();
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult DocPost(HttpPostedFileBase file)
        {
            return View();
        }
        #endregion
    }
}

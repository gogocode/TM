using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain.Models;
using TM.Domain.Services;
using TM.Domain.ViewModels;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class CatalogController : Controller
    {
        private CatalogService _catalogService;

        public CatalogController()
        {
            _catalogService = new CatalogService();
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            CatalogIndexView vm = new CatalogIndexView();
            vm.Catalogs = _catalogService.FindAll();

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult CreatePost(CatalogIndexView vm)
        {
            if(ModelState.IsValid)
            { 
                _catalogService.Create(vm.AddCatalog);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult EditPost(Catalog catalog)
        {
            int cnt = _catalogService.Modify(catalog);

            if (cnt > 0)
            {
                TempData["Message"] = string.Format("{0},{1}", "success", "修改成功");
            }
            else
            {
                TempData["Message"] = string.Format("{0},{1}", "warning", "修改失敗");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult Delete(int id)
        {
            int cnt = _catalogService.Delete(id);

            if (cnt > 0)
            {
                TempData["Message"] = string.Format("{0},{1}", "success", "刪除成功");
            }
            else
            {
                TempData["Message"] = string.Format("{0},{1}", "warning", "刪除失敗");
            }

            return RedirectToAction("Index");
        }

        #region Ajax
        [HttpGet]
        [CheckAuth]
        public ActionResult Edit(int id)
        {
            Catalog catalog = _catalogService.Find(id);

            return PartialView("_EditView",catalog);
        }     
        #endregion
    }
}
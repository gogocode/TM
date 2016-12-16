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
    public class RoleController : Controller
    {
        private RoleService _RoleService;
        private CatalogService _CatalogService;

        public RoleController()
        {
            _RoleService = new RoleService();
            _CatalogService = new CatalogService();
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            RoleIndexView vm = new RoleIndexView();
            vm.Roles = _RoleService.FindAll().ToList();

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult CreatePost(RoleIndexView vm)
        {
            if (ModelState.IsValid)
            {
                _RoleService.Create(vm.AddRole);
            }

            vm.Roles = _RoleService.FindAll().ToList();

            return View("Index",vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult EditPost(Role role)
        {
            int cnt = _RoleService.Modify(role);

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
            int cnt = _RoleService.Delete(id);

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

        [HttpPost]
        [CheckAuth]
        public ActionResult EditCatalogPost(RoleEditCatalogView vm)
        {
            int cnt = 0;

            if (ModelState.IsValid)
            {
                cnt = _CatalogService.ModifyRoleCatalogs(vm.RoleId, vm.SelectedCatalogIds);
            }

            if (cnt > 0)
            {
                TempData["Message"] = string.Format("{0},{1}", "success", "修改成功");
            }
            else
            {
                TempData["Message"] = string.Format("{0},{1}", "warning", "修改失敗");
            }

            vm.Catalogs = _CatalogService.FindAll();

            return View("EditCatalog",vm);
        }

        #region Ajax
        [HttpGet]
        [CheckAuth]
        public ActionResult Edit(int id)
        {
            Role role = _RoleService.Find(id);

            return PartialView("_EditView", role);
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult EditCatalog(int id)
        {
            Role role = _RoleService.Find(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            RoleEditCatalogView vm = new RoleEditCatalogView();
            vm.RoleId = role.RoleId;
            vm.Catalogs = _CatalogService.FindAll();
            vm.SelectedCatalogIds = role.Catalogs.Select(x=>x.CatalogId).ToList();

            return View(vm);
        }
        #endregion
    }
}
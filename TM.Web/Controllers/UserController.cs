using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using TM.Domain.Manager;
using TM.Domain.Models;
using TM.Domain.Services;
using TM.Domain.ViewModels;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class UserController : BaseController
    {
        private UserService _UserService;
        private RoleService _RoleService;

        public UserController()
        {
            _UserService = new UserService();
            _RoleService = new RoleService();
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            UserIndexView vm = new UserIndexView();
            vm.Users = _UserService.FindPagedUsers(vm.UserName, 1, _PageSize);

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult IndexPost(UserIndexView vm)
        {
            vm.Users = _UserService.FindPagedUsers(vm.UserName, vm.CurrentPage, _PageSize);

            return View("Index",vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult CreatePost(UserIndexView vm)
        {
            int cnt = 0;

            if (ModelState.IsValid)
            {
                cnt = _UserService.Create(vm.AddUser);
            }

            if (cnt > 0)
            {
                TempData["Message"] = string.Format("{0},{1}", "success", "新增成功");
            }
            else
            {
                TempData["Message"] = string.Format("{0},{1}", "warning", "新增失敗");
            }

            vm.Users = _UserService.FindPagedUsers(vm.UserName, 1, _PageSize);

            return View("Index", vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult EditPost(User user)
        {
            int cnt = _UserService.Modify(user);

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
            int cnt = _UserService.Delete(id);

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
        public ActionResult EditRolePost(UserEditRoleView vm)
        {
            int cnt = _UserService.ModifyUserRoles(vm.UserId,vm.SelectedRoleIds);

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

        #region Ajax
        [HttpGet]
        [CheckAuth]
        public ActionResult Edit(int id)
        {
            User user = _UserService.Find(id);

            return PartialView("_EditView", user);
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult EditRole(int id)
        {
            User user = _UserService.Find(id);

            UserEditRoleView vm = new UserEditRoleView();
            vm.UserId = id;
            vm.SelectedRoleIds = user.Roles.Select(x=>x.RoleId).ToList();
            vm.Roles = _RoleService.FindAll().ToList();

            return PartialView("_EditRoleView", vm);
        }
        #endregion
    }
}
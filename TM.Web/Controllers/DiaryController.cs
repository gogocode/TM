using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain;
using TM.Domain.Models;
using TM.Domain.Services;
using TM.Domain.ViewModels;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class DiaryController : BaseController
    {
        private DiaryService _DiaryService;

        public DiaryController()
        {
            _DiaryService = new DiaryService();
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            DiaryIndexView vm = new DiaryIndexView();

            vm.Diaries = _DiaryService.FindByUserId(null,LoginState.LoginUserId, 1, _PageSize);

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult IndexPost(DiaryIndexView vm)
        {
            vm.Diaries = _DiaryService.FindByUserId(vm.SearchWorkDate,LoginState.LoginUserId, vm.CurrentPage, _PageSize);

            return View("Index", vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult CreatePost(DiaryIndexView vm)
        {
            int cnt = 0;

            if (ModelState.IsValid)
            {
                vm.AddDiary.UserId = LoginState.LoginUserId;
                cnt = _DiaryService.Create(vm.AddDiary);

                if (cnt > 0)
                {
                    TempData["Message"] = string.Format("{0},{1}", "success", "新增成功");
                }
                else
                {
                    TempData["Message"] = string.Format("{0},{1}", "warning", "新增失敗");
                }
            }

            vm.Diaries = _DiaryService.FindByUserId(null,LoginState.LoginUserId, 1, _PageSize);

            return View("Index", vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult EditPost(Diary diary)
        {
            diary.UserId = LoginState.LoginUserId;
            int cnt = _DiaryService.Modify(diary);

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
            int cnt = _DiaryService.Delete(id);

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

        [HttpGet]
        [CheckAuth]
        public ActionResult LookUserDiary()
        {
            DiaryLookUserDiaryView vm = new DiaryLookUserDiaryView();

            vm.Diaries = _DiaryService.FindGroupByUserId(null, 0, 1, _PageSize);

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult LookUserDiaryPost(DiaryLookUserDiaryView vm)
        {
            vm.Diaries = _DiaryService.FindGroupByUserId(vm.SearchWorkDate, 0, 1, _PageSize);

            return View("LookUserDiary", vm);
        }

        #region Ajax
        [HttpGet]
        [CheckAuth]
        public ActionResult Edit(int id)
        {
            Diary diary = _DiaryService.Find(id);

            return PartialView("_EditView", diary);
        }
        #endregion
    }
}

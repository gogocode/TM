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
        private ItemService _ItemService;

        public DiaryController()
        {
            _DiaryService = new DiaryService();
            _ItemService = new ItemService();
        }

        #region 查詢
        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            DiaryIndexView vm = new DiaryIndexView();

            vm.Diaries = _DiaryService.FindByUserId(null, LoginState.LoginUserId, 1, _PageSize);

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult IndexPost(DiaryIndexView vm)
        {
            vm.Diaries = _DiaryService.FindByUserId(vm.SearchWorkDate, LoginState.LoginUserId, vm.CurrentPage, _PageSize);

            return View("Index", vm);
        }
        #endregion

        #region 新增
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

            vm.Diaries = _DiaryService.FindByUserId(null, LoginState.LoginUserId, 1, _PageSize);

            return RedirectToAction("Index");
        }
        #endregion

        #region 編輯

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
        #endregion

        #region 刪除
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
        #endregion

        #region 檢視使用者日誌
        [HttpGet]
        [CheckAuth]
        public ActionResult LookUserDiary()
        {
            DiaryLookUserDiaryView vm = new DiaryLookUserDiaryView();

            vm.Diaries = _DiaryService.FindGroupByUserId(null, null, 0, 1, _PageSize);

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult LookUserDiaryPost(DiaryLookUserDiaryView vm)
        {
            vm.Diaries = _DiaryService.FindGroupByUserId(vm.SearchWorkDate, vm.EmployeeId, 0, 1, _PageSize);

            return View("LookUserDiary", vm);
        }
        #endregion

        #region 項目管理
        [HttpGet]
        [CheckAuth]
        public ActionResult ItemIndex()
        {
            DiaryItemView vm = new DiaryItemView();
            vm.Items = _ItemService.FindAll();

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult ItemCreate(DiaryItemView vm)
        {
            int cnt = 0;

            if (ModelState.IsValid)
            {
                cnt = _ItemService.Create(vm.AddItem);

                if (cnt > 0)
                {
                    TempData["Message"] = string.Format("{0},{1}", "success", "新增成功");
                }
                else
                {
                    TempData["Message"] = string.Format("{0},{1}", "warning", "新增失敗");
                }
            }

            return RedirectToAction("ItemIndex");
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult ItemEdit(Item item)
        {
            int cnt = _ItemService.Modify(item);

            if (cnt > 0)
            {
                TempData["Message"] = string.Format("{0},{1}", "success", "修改成功");
            }
            else
            {
                TempData["Message"] = string.Format("{0},{1}", "warning", "修改失敗");
            }

            return RedirectToAction("ItemIndex");
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult ItemDelete(int id)
        {
            int cnt = _ItemService.Delete(id);

            if (cnt > 0)
            {
                TempData["Message"] = string.Format("{0},{1}", "success", "刪除成功");
            }
            else
            {
                TempData["Message"] = string.Format("{0},{1}", "warning", "刪除失敗");
            }

            return RedirectToAction("ItemIndex");
        }
        #endregion

        #region Ajax
        [HttpGet]
        [CheckAuth]
        public ActionResult Edit(int id)
        {
            Diary diary = _DiaryService.Find(id);

            return PartialView("_EditView", diary);
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult ItemEdit(int id)
        {
            Item diary = _ItemService.Find(id);

            return PartialView("_ItemEditView", diary);
        }
        #endregion
    }
}

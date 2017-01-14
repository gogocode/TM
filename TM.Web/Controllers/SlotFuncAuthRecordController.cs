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
    public class SlotFuncAuthRecordController : BaseController
    {
        private SlotFuncAuthRecordService _SlotFuncAuthRecordService;

        public SlotFuncAuthRecordController()
        {
            _SlotFuncAuthRecordService = new SlotFuncAuthRecordService();
        }

        #region 查詢
        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            SlotFuncAuthRecordIndexView vm = new SlotFuncAuthRecordIndexView();
            vm.SlotFuncAuthRecords = _SlotFuncAuthRecordService.FindByPageds(null,null,null,1,_PageSize);

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult IndexPost(SlotFuncAuthRecordIndexView vm)
        {
            vm.SlotFuncAuthRecords = _SlotFuncAuthRecordService.FindByPageds(vm.SearchEmployeeId, vm.SearchEmployeeName,vm.SearchIsCompleted, vm.CurrentPage, _PageSize);

            return View("Index",vm);
        }
        #endregion

        #region 新增

        [HttpPost]
        [CheckAuth]
        public ActionResult CreatePost(SlotFuncAuthRecordIndexView vm)
        {
            int cnt = 0;

            if (ModelState.IsValid)
            {
                vm.AddSlotFuncAuthRecord.Creator = LoginState.LoginEmployeeId;
                vm.AddSlotFuncAuthRecord.CreateDateTime = System.DateTime.Now;
                cnt = _SlotFuncAuthRecordService.Create(vm.AddSlotFuncAuthRecord);

                if (cnt > 0)
                {
                    TempData["Message"] = string.Format("{0},{1}", "success", "新增成功");
                }
                else
                {
                    TempData["Message"] = string.Format("{0},{1}", "warning", "新增失敗");
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region 編輯
        [HttpGet]
        [CheckAuth]
        public ActionResult Edit(int id)
        {
           var model =  _SlotFuncAuthRecordService.Find(id);

            return PartialView("_EditView",model);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult EditPost(SlotFuncAuthRecord model)
        {
            model.Editor = LoginState.LoginEmployeeId;
            model.EditDateTime = System.DateTime.Now;

            int cnt = _SlotFuncAuthRecordService.Modify(model);

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
            int cnt = _SlotFuncAuthRecordService.Delete(id);

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

    }
}
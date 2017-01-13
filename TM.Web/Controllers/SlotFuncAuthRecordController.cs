using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Domain;
using TM.Domain.Services;
using TM.Domain.ViewModels;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class SlotFuncAuthRecordController : BaseController
    {
        private SlotFuncAuthRecordService _SlotFuncAuthRecord;

        public SlotFuncAuthRecordController()
        {
            _SlotFuncAuthRecord = new SlotFuncAuthRecordService();
        }

        #region 查詢
        [HttpGet]
        [CheckAuth]
        public ActionResult Index()
        {
            SlotFuncAuthRecordIndexView vm = new SlotFuncAuthRecordIndexView();
            vm.SlotFuncAuthRecords = _SlotFuncAuthRecord.FindByPageds(null,null,1,_PageSize);

            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult IndexPost(SlotFuncAuthRecordIndexView vm)
        {
            vm.SlotFuncAuthRecords = _SlotFuncAuthRecord.FindByPageds(vm.SearchEmployeeId, vm.SearchEmployeeName, vm.CurrentPage, _PageSize);

            return View("Index",vm);
        }
        #endregion

        #region 新增

        [HttpGet]
        [CheckAuth]
        public ActionResult CreatePost(SlotFuncAuthRecordIndexView vm)
        {
            int cnt = 0;

            if (ModelState.IsValid)
            {
                vm.AddSlotFuncAuthRecord.Creator = LoginState.LoginEmployeeId;
                vm.AddSlotFuncAuthRecord.CreateDateTime = System.DateTime.Now;
                cnt = _SlotFuncAuthRecord.Create(vm.AddSlotFuncAuthRecord);

                if (cnt > 0)
                {
                    TempData["Message"] = string.Format("{0},{1}", "success", "新增成功");
                }
                else
                {
                    TempData["Message"] = string.Format("{0},{1}", "warning", "新增失敗");
                }
            }

            vm.SlotFuncAuthRecords = _SlotFuncAuthRecord.FindByPageds(vm.SearchEmployeeId, vm.SearchEmployeeName, vm.CurrentPage, _PageSize);

            return RedirectToAction("Index");
        }
        #endregion

        #region 編輯
        [HttpGet]
        [CheckAuth]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        [CheckAuth]
        public ActionResult EidtPost()
        {
            return View();
        }
        #endregion

        #region 刪除
        [HttpGet]
        [CheckAuth]
        public ActionResult Delete()
        {
            return View();
        }
        #endregion

    }
}
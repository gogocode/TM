using Newtonsoft.Json;
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
    public class ChartController : BaseController
    {
        DiaryService _DiaryService;

        public ChartController()
        {
            _DiaryService = new DiaryService();
        }

        //暫無使用
        [CheckAuth]
        public ActionResult Index()
        {
            return View();
        }

        #region 工作統計圖表
        [HttpGet]
        [CheckAuth]
        public ActionResult DiaryChart()
        {
            DiaryChartView vm = new DiaryChartView();
            vm.SearchYear = System.DateTime.Now.Year;
            return View(vm);
        }

        [HttpPost]
        [CheckAuth]
        public ActionResult DiaryChartPost(string year, string month,string userId)
        {
            int searchUserId;

            if(!string.IsNullOrWhiteSpace(userId))
            {
                searchUserId = int.Parse(userId);
            }
            else
            {
                searchUserId = LoginState.LoginUserId;
            }

            JobWeightChart chart = _DiaryService.FindJobWeightData(year, month, searchUserId);
            string Series = JsonConvert.SerializeObject(chart.Series);
            string Legend = JsonConvert.SerializeObject(chart.Legend);

            return Json(new { isSuccess = chart.Series.Count() > 0, series = Series, legend = Legend }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
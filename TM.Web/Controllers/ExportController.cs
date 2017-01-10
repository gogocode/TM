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

        //蝦皮拍賣
        [HttpPost]
        public ActionResult Shopee(HttpPostedFileBase file)
        {
            string msg = string.Empty;
            string filePath = string.Empty;

            try
            {
                #region 檢查匯入的Excel

                Random rnd = new Random();
                filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/FileUploads"), rnd.Next() + Path.GetFileName(file.FileName));
                file.SaveAs(filePath);

                ExcelQueryFactory newFile = new ExcelQueryFactory(filePath);
                string sheetName = newFile.GetWorksheetNames().FirstOrDefault();

                //Mapping的Excel轉成自訂的Model，並取到欄位資料
                //List<ToKantOrderShopeeView> shopees = new List<ToKantOrderShopeeView>();
                //shopees = MappingShopeeCol(newFile, sheetName);

                var indianaCompanies = newFile.Worksheet<ToKantOrderShopeeView>(sheetName).ToList();
                       


                #endregion
                var xxx = 0;
            }
            catch (Exception ex)
            {
                throw;
            }

            TempData["message"] = msg;
            return View("Index");
        }

        private List<ToKantOrderShopeeView> MappingShopeeCol(ExcelQueryFactory excelFile, string sheetName)
        {
            List<ToKantOrderShopeeView> shopees = new List<ToKantOrderShopeeView>();

            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.OrderNo, "訂單編號");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.OrderType, "訂單狀態");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.SubTotal, "訂單小計 (TWD)");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.Freight, "買家支付的運費");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.Amount, "訂單總金額");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.ProductInfo, "商品資訊");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.ConsigneeAddress, "收件地址");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.ConsigneeName, "收件者姓名");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.ConsigneeTel, "電話號碼");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.SendType, "寄送方式");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.PaymentTerms, "付款方式");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.ConsigneeMemo, "買家備註");
            excelFile.AddMapping<ToKantOrderShopeeView>(p => p.Memo, "備註");

            shopees = excelFile.Worksheet<ToKantOrderShopeeView>(sheetName).ToList();

            return shopees;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModels.Export
{
    public class ToKantOrderShopeeDetail
    {
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductNo { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品價格
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// 商品數量
        /// </summary>
        public int ProductQuantity { get; set; }
    }
}

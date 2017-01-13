using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModels
{
    public class DiaryChartView
    {
        [DisplayName("員工")]
        public string UserId { get; set; }
        [DisplayName("年份")]
        public int SearchYear { get; set; }
    }
}

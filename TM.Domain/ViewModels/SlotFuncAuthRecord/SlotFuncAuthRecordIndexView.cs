using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class SlotFuncAuthRecordIndexView
    {
        [DisplayName("員工編號")]
        public string SearchEmployeeId { get; set; }

        [DisplayName("員工姓名")]
        public string SearchEmployeeName { get; set; }

        [DisplayName("是否完成")]
        public string SearchIsCompleted { get; set; }

        public SlotFuncAuthRecord AddSlotFuncAuthRecord { get; set; }

        public IPagedList<SlotFuncAuthRecord> SlotFuncAuthRecords { get; set; }

        public int CurrentPage { get; set; }
    }
}

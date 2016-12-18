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
    public class DiaryIndexView
    {
        [DisplayName("工作日期")]
        public DateTime? SearchWorkDate { get; set; }

        public Diary AddDiary { get; set; }

        public IPagedList<IGrouping<DateTime,Diary>> Diaries { get; set; }

        public int CurrentPage { get; set; }
    }
}

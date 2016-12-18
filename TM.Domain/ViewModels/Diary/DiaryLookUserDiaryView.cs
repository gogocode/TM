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
    public class DiaryLookUserDiaryView
    {
        [DisplayName("工作日期")]
        public string SearchWorkDate { get; set; }

        public IPagedList<DiaryGroup> Diaries { get; set; }

        public int CurrentPage { get; set; }
    }
}

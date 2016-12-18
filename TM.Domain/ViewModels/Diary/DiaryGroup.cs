using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class DiaryGroup
    {
        public DateTime WorkDate { get; set; }

        public List<IGrouping<string,Diary>> Diaries { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain.ViewModels
{
    public class DiaryItemView
    {
        public Item AddItem { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Models
{
    public class SlotFuncAuthRecord
    {
        public SlotFuncAuthRecord() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("紀錄編號")]
        public int RecordId { get; set; }

        [DisplayName("修改權限日期")]
        public DateTime? AuthModifyDate { get; set; }

        [StringLength(10)]
        [DisplayName("員工編號")]
        public string EmployeeId { get; set; }

        [StringLength(50)]
        [DisplayName("員工姓名")]
        public string EmployeeName { get; set; }

        [UIHint("SLotAuthItem")]
        [StringLength(50)]
        [DisplayName("項目")]
        public string Item { get; set; }

        [DisplayName("內容")]
        public string Content { get; set; }

        [StringLength(1)]
        [DisplayName("是否完成")]
        public string IsCompleted { get; set; }

        [DisplayName("備註")]
        public string Comment { get; set; }

        [StringLength(10)]
        [DisplayName("建立人")]
        public string Creator { get; set; }

        [DisplayName("建立時間")]
        public DateTime? CreateDateTime { get; set; }

        [StringLength(10)]
        [DisplayName("編輯者")]
        public string Editor { get; set; }

        [DisplayName("編輯時間")]
        public DateTime? EditDateTime { get; set; }
    }
}

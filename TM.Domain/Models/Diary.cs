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
    public class Diary
    {
        public Diary()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("工作日誌編號")]
        public int DiaryId { get; set; }

        [StringLength(50)]
        [DisplayName("項目")]
        [Required(ErrorMessage = "請輸入工作項目")]
        public string Item { get; set; }

        [StringLength(200)]
        [DisplayName("內容")]
        [Required(ErrorMessage = "請輸入工作內容")]
        public string Content { get; set; }

        [DisplayName("時數")]
        public Decimal Hours { get; set; }

        public virtual User User { get; set; }
    }
}

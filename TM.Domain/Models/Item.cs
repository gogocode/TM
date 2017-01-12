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
    public class Item
    {
        public Item() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("項目編號")]
        public int ItemId { get; set; }

        [StringLength(50)]
        [DisplayName("項目名稱")]
        [Required(ErrorMessage = "請輸入項目名稱")]
        public string ItemName { get; set; }

    }
}

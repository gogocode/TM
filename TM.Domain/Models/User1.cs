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
    public class User1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(30)]
        [DisplayName("使用者名稱")]
        [Required(ErrorMessage = "請輸入使用者名稱")]
        public string UserName { get; set; }

        public string UserName1 { get; set; }

        public string UserName2 { get; set; }

        public string UserName3 { get; set; }

        public string UserName4 { get; set; }
    }
}

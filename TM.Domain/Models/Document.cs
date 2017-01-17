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
    public class Document
    {
        public Document()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("文件編號")]
        public int DocumentId { get; set; }

        [DisplayName("項目")]
        public string Item { get; set; }

        [DisplayName("檔案名稱")]
        public string FileName { get; set; }

        [DisplayName("檔案路徑")]
        public string FilePath { get; set; }

        [DisplayName("備註")]
        public string Comment { get; set; }

        [DisplayName("建立人")]
        public string Creator { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}

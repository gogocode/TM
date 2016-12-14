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
    public class Catalog
    {
        public Catalog()
        {
            SubCatalogs = new List<Catalog>();

            Users = new List<User>();
            Roles = new List<Role>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("目錄編號")]
        public int CatalogId { get; set; }

        [StringLength(30)]
        [DisplayName("目錄名稱")]
        [Required(ErrorMessage = "請輸入目錄名稱")]
        public string CatalogName { get; set; }

        [DisplayName("目錄排序")]
        [Required(ErrorMessage = "請輸入目錄排序")]
        public int CatalogOrder { get; set; }

        [StringLength(200)]
        [DisplayName("Permission")]
        [Required(ErrorMessage = "請輸入Permission")]
        public string Permission { get; set; }

        [DisplayName("是否為選單")]
        [Required(ErrorMessage = "請輸入是否為選單")]
        public bool IsMenu { get; set; }

        [StringLength(50)]
        [DisplayName("IconClass")]
        public string IconClass { get; set; }

        [DisplayName("父目錄編號")]
        [RegularExpression(@"[\S]+$", ErrorMessage = "禁止使用空白字串")]
        public int? ParentCatalogId { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("備註")]
        public string Comment { get; set; }

        [ForeignKey("ParentCatalogId")]
        public Catalog ParentCatalog { get; set; }
        public List<Catalog> SubCatalogs { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}

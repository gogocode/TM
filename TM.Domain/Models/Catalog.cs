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
        public int CatalogId { get; set; }

        [StringLength(30)]
        [DisplayName("目錄名稱")]
        [Required(ErrorMessage = "請輸入目錄名稱")]
        public string CatalogName { get; set; }

        [DisplayName("目錄排序")]
        [Required(ErrorMessage = "請輸入目錄排序")]
        public int CatalogOrder { get; set; }

        [StringLength(50)]
        [DisplayName("目錄權限碼")]
        [Required(ErrorMessage = "請輸入目錄權限碼")]
        [RegularExpression(@"[\S]+$", ErrorMessage = "禁止使用空白字串")]
        [Index("IX_CatalogCode", IsUnique = true)]
        public string CatalogCode { get; set; }

        [DisplayName("是否為Menu")]
        [Required(ErrorMessage = "請輸入是否為Menu")]
        public bool IsMenu { get; set; }

        [StringLength(50)]
        [DisplayName("Controller/Action")]
        public string MenuControllerName { get; set; }

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

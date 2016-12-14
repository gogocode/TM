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
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
            Catalogs = new List<Catalog>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [StringLength(30)]
        [DisplayName("角色英文名稱")]
        [Required(ErrorMessage = "請輸入角色英文名稱")]
        public string RoleEngName { get; set; }

        [StringLength(60)]
        [DisplayName("角色名稱")]
        [Required(ErrorMessage = "請輸入角色名稱")]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Catalog> Catalogs { get; set; }
    }
}

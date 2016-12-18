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
    public class User
    {
        public User()
        {
            Roles = new List<Role>();
            Catalogs = new List<Catalog>();
            Diarys = new List<Diary>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(30)]
        [DisplayName("使用者中文名稱")]
        [Required(ErrorMessage = "請輸入使用者中文名稱")]
        public string UserName { get; set; }

        [StringLength(10)]
        [DisplayName("員工編號")]
        [Required(ErrorMessage = "請輸入員工編號")]
        [Index("IX_EmployeeId", IsUnique = true)]
        public string EmployeeId { get; set; }

        [StringLength(20)]
        [DisplayName("使用者帳號")]
        [Required(ErrorMessage = "請輸入使用者帳號")]
        [Index("IX_Account", IsUnique = true)]
        public string Account { get; set; }

        [StringLength(50)]
        [DisplayName("使用者密碼")]
        [Required(ErrorMessage = "請輸入使用者密碼")]
        public string Password { get; set; }

        [StringLength(50)]
        [DisplayName("電子郵件")]
        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式錯誤")]
        public string Email { get; set; }

        [DisplayName("是否啟用")]
        [Required(ErrorMessage = "請輸入是否啟用")]
        public bool IsActive { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Catalog> Catalogs { get; set; }

        public virtual ICollection<Diary> Diarys { get; set; }
    }
}

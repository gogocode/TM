using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TM.Domain.Models;
using TM.Domain.Services;
using TM.Domain.ViewModels;
using TM.Web.Attribute;

namespace TM.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;

        public AccountController()
        {
            _userService = new UserService();
        }

        #region 登入
        [HttpGet]
        public ActionResult Login()

        {
            AccountLoginView vm = new AccountLoginView();
            vm.Account = "superadmin";
            vm.Password = "1234";

            return View(vm);
        }

        [HttpPost]
        public ActionResult LoginPost(AccountLoginView vm)
        {
            string msg = string.Empty;
            string roles = string.Empty;

            msg = CheckAccount(vm,ref roles);

            if (ModelState.IsValid && string.IsNullOrWhiteSpace(msg) )
            {
                //新增登入用Ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    vm.Account,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(60),
                    false,
                    roles,
                    FormsAuthentication.FormsCookiePath
                );

                //資料加密成字串
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                //將資料存入cookies中
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMsg = msg;
    
            return View("Login", vm);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect(Url.Action("Login", "Account"));
        }
        #endregion

        #region 私有方法
        private string CheckAccount(AccountLoginView vm,ref string roles)
        {
            string msg = string.Empty;

            User user = _userService.FindUser(vm);

            if(user == null)
            {
                msg = "帳號或密碼有誤";
            }
            else if(!user.IsActive)
            {
                msg = "帳號未啟動";
            }

            roles = string.Join(",",user.Roles.Select(x=>x.RoleEngName));

            return msg;
        }


        #endregion
    }
}
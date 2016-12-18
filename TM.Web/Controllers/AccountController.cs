using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TM.Domain;
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


            User user = _userService.FindUser(vm);

            msg = CheckAccount(user, ref roles);

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

                LoginState.LoginAccount = user.Account;
                LoginState.LoginUserId = user.UserId;
                LoginState.LoginUserName = user.UserName;

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

        #region 註冊
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterPost(User user)
        {
            int cnt = 0;

            if(ModelState.IsValid)
            {
                cnt = _userService.Create(user);
            }

            //註冊成功
            if(cnt > 0)
            {
                TempData["Message"] = string.Format("{0},{1}", "info", "註冊成功");
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
        #endregion

        #region 私有方法
        private string CheckAccount(User user,ref string roles)
        {
            string msg = string.Empty;

            if(user == null)
            {
                msg = "帳號或密碼有誤";
            }
            else if(!user.IsActive)
            {
                msg = "帳號未啟動";
            }
            else
            { 
                roles = string.Join(",",user.Roles.Select(x=>x.RoleEngName));
            }

            return msg;
        }


        #endregion
    }
}
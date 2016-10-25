using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Ninesky.BLL;
using Ninesky.Common;
using Ninesky.Models;
using Ninesky.Web.Areas.Member.Models;
using System.Drawing;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ninesky.Web.Areas.Member.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserService userService = new UserService();

        /// <summary>
        /// 获取AuthenticationManager（认证管理器）
        /// </summary>
        #region 属性
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        //private AuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        #endregion

        // GET: Member/User
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VerificationCode()
        {
            string verificationCode = Security.CreateVerificationText(4);
            Bitmap _img = Security.CreateVerificationImage(verificationCode, 100, 24);
            _img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            TempData["VerificationCode"] = verificationCode.ToUpper();
            return null;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 处理用户提交的注册数据
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != register.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View(register);
            }
            if (ModelState.IsValid)
            {
                if (userService.Exist(register.UserName)) ModelState.AddModelError("UserName", "用户名已存在");
                else
                {
                    User _user = new User()
                    {
                        UserName = register.UserName,
                        //默认用户组代码写这里
                        DisplayName = register.DisplayName,
                        Password = Security.Sha256(register.Password),
                        //邮箱验证与邮箱唯一性问题
                        Email = register.Email,
                        //用户状态问题
                        Status = 0,
                        RegistrationTime = System.DateTime.Now
                    };
                    _user = userService.Add(_user);
                    if (_user.UserID > 0)
                    {
                        AuthenticationManager.SignIn();
                        Content("注册成功！");
                        _user.LoginTime = System.DateTime.Now;
                        _user.LoginIP = Request.UserHostAddress;
                        userService.Update(_user);
                        var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(_identity);
                        FormsAuthentication.SetAuthCookie(_user.UserName, false);
                        return Redirect(Url.Content("~/"));
                    }
                    else
                    {
                        ModelState.AddModelError("", "注册失败！");
                    }
                }
            }
            return View(register);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="returnUrl">返回Url</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 用户登录逻辑
        /// 用ModelState.IsValid验证模型是否通过，没通过直接返回，通过则检查用户密码是否正确。用户名密码正确用CreateIdentity方法创建标识，然后用SignOut方法清空Cookies，然后用SignIn登录。
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var _user = userService.Find(loginViewModel.UserName);
                if (_user == null) ModelState.AddModelError("UserName", "用户名不存在");
                else if (_user.Password == Security.Sha256(loginViewModel.Password))
                {
                    _user.LoginTime = System.DateTime.Now;
                    _user.LoginIP = Request.UserHostAddress;
                    userService.Update(_user);
                    var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = loginViewModel.RememberMe }, _identity);
                    FormsAuthentication.SetAuthCookie(_user.UserName, false);
                    if (returnUrl == null)
                    {
                        return Redirect(Url.Content("~/"));
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else ModelState.AddModelError("Password", "密码错误");
            }
            return View(loginViewModel);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(Url.Content("~/"));
        }

        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <returns></returns>
        //public ActionResult Menu()
        //{
        //    return View();
        //}

        /// <summary>
        /// 显示用户资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View(userService.Find(User.Identity.Name));
        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify()
        {
            var _user = userService.Find(User.Identity.Name);
            if (_user == null) ModelState.AddModelError("", "用户不存在");
            else
            {
                if (TryUpdateModel(_user, new string[] { "DisplayName", "Email" }))
                {
                    if (ModelState.IsValid)
                    {
                        if (userService.Update(_user))
                            ModelState.AddModelError("", "修改成功！");
                        else
                            ModelState.AddModelError("", "无需要修改的资料");
                    }
                }
                else ModelState.AddModelError("", "更新模型数据失败");
            }
            return Redirect(Url.Content("~/"));
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel passwordViewModel = new ChangePasswordViewModel();
            return PartialView("ChangePassword", passwordViewModel);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="passwordViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            if (ModelState.IsValid)
            {
                var _user = userService.Find(passwordViewModel.UserName);
                if (_user == null) ModelState.AddModelError("", "用户不存在");
                if (_user.Password == Security.Sha256(passwordViewModel.OriginalPassword))
                {
                    _user.Password = Security.Sha256(passwordViewModel.Password);
                    if (userService.Update(_user))
                        ModelState.AddModelError("", "修改密码成功");
                    else
                        ModelState.AddModelError("", "修改密码失败");
                }
                else ModelState.AddModelError("", "原密码错误");
            }
            //return View(passwordViewModel);
            return Redirect(Url.Content("~/"));
        }
    }
}
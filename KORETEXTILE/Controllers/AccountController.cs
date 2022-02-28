using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BEEERP.Models;
using BEEERP.Models.ViewModel.Account;
using System.Collections.Generic;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Authentication;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.Notification;
using BEEERP.Models.HRModule;
using System.Data.Entity;

namespace BEEERP.Controllers
{
    [ShowNotification]
    [Authorize(Roles = "AccAdmin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        BEEContext bEEContext = new BEEContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string[] access)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext contex = new ApplicationDbContext();
                var employee = bEEContext.Employees.FirstOrDefault(x => x.Id == model.EmployeeId);
                if (employee != null)
                {
                    List<NotificationUser> notificationUsers = new List<NotificationUser>();
                    employee.LogInUserName = model.UserName;
                    bEEContext.Entry<Employee>(employee).State = System.Data.Entity.EntityState.Modified;
                    var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRoles(contex.Users.FirstOrDefault(x => x.Email == user.Email).Id, access);
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        if (access.Length > 0)
                        {
                            foreach (Module module in Enum.GetValues(typeof(Module)))
                            {
                                if (module == Module.Accounts)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Acc"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Accounts) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                    //var isExist = access.FirstOrDefault(x => x.Contains("Acc"));
                                    //if (isExist != null)
                                    //{
                                    //    var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Accounts) };
                                    //    bEEContext.UserWiseModule.Add(item);
                                    //    var notification = access.FirstOrDefault(x => x.Contains("Approver") && x.Contains("Acc"));
                                    //    if (notification != null)
                                    //    {
                                    //        foreach (var aNotifi in ApproverNotification.AccountNotification())
                                    //        {
                                    //            notificationUsers.Add(new NotificationUser(contex.Users.FirstOrDefault(x => x.UserName == model.UserName).Id, aNotifi.Item2));
                                    //        }
                                    //    }

                                    //}
                                }
                                else if (module == Module.Commercial)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Comm"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Commercial) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.Procurement)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Proc"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Procurement) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.StoreRM)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("StoreRM"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreRM) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.Production)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Prod"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Production) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.Costing)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Costing"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Costing) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.StoreFG)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("StoreFG"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreFG) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.StoreDepot)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("StoreDepot"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreDepot) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.Sales)
                                {
                                    //var isExist = access.FirstOrDefault(x => x.Contains("Sales"));
                                    //if (isExist != null)
                                    //{

                                    //   var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Sales) };
                                    //    bEEContext.UserWiseModule.Add(item);
                                    //    var notification = access.FirstOrDefault(x => x.Contains("Approver"));
                                    //    if (notification != null)
                                    //    {
                                    //        foreach (var aNotifi in ApproverNotification.AllNotification())
                                    //        {
                                    //            bEEContext.NotificationUsers.Add(new NotificationUser(contex.Users.FirstOrDefault(x => x.UserName == model.UserName).Id, aNotifi.Item2));
                                    //        }
                                    //    }
                                    //}



                                    var isExist = access.FirstOrDefault(x => x.Contains("Sales"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Sales) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.HRAdmin)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Hra"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.HRAdmin) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.FixedAsset)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("FixedAsset"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.FixedAsset) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.DataAdmin)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Data"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.DataAdmin) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.SystemAdmin)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("Sys"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.SystemAdmin) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                else if (module == Module.StoreRM)
                                {
                                    var isExist = access.FirstOrDefault(x => x.Contains("StoreRM"));
                                    if (isExist != null)
                                    {
                                        var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreRM) };
                                        bEEContext.UserWiseModule.Add(item);
                                    }
                                }
                                bEEContext.SaveChanges();
                            }
                        }

                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
                else
                {
                    return View(model);
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdateRegister(RegisterViewModel model, string[] access)
        {
            AccountController obj = new AccountController();
            var userid = bEEContext.Database.SqlQuery<string>("select id from AspNetUsers where UserName = '" + model.UserName + "'").FirstOrDefault();
            var modulename = bEEContext.Database.SqlQuery<string>("select ModuleName from UserWiseModules where UserName = '" + model.UserName + "'").ToList();
            //var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.) };
            foreach (var item in modulename)
            {
                var itemname = new UserWiseModule() { UserName = model.UserName, ModuleName = item };
                bEEContext.Entry(itemname).State = EntityState.Deleted;
                //bEEContext.UserWiseModule.Remove(itemname);
            }
            var roles = UserManager.GetRoles(userid).ToArray();

            UserManager.RemoveFromRoles(userid, roles);
            var name = model.UserName;

            ApplicationDbContext contex = new ApplicationDbContext();
            var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName };
            UserManager.AddToRoles(contex.Users.FirstOrDefault(x => x.Email == user.Email).Id, access);

            if (access.Length > 0)
            {
                foreach (Module module in Enum.GetValues(typeof(Module)))
                {
                    if (module == Module.Accounts)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Acc"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Accounts) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                        //var isExist = access.FirstOrDefault(x => x.Contains("Acc"));
                        //if (isExist != null)
                        //{
                        //    var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Accounts) };
                        //    bEEContext.UserWiseModule.Add(item);
                        //    var notification = access.FirstOrDefault(x => x.Contains("Approver") && x.Contains("Acc"));
                        //    if (notification != null)
                        //    {
                        //        foreach (var aNotifi in ApproverNotification.AccountNotification())
                        //        {
                        //            notificationUsers.Add(new NotificationUser(contex.Users.FirstOrDefault(x => x.UserName == model.UserName).Id, aNotifi.Item2));
                        //        }
                        //    }

                        //}
                    }
                    else if (module == Module.Commercial)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Comm"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Commercial) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.Procurement)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Proc"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Procurement) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.StoreRM)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("StoreRM"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreRM) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.Production)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Prod"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Production) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.Costing)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Costing"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Costing) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.StoreFG)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("StoreFG"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreFG) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.StoreDepot)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("StoreDepot"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreDepot) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.Sales)
                    {
                        //var isExist = access.FirstOrDefault(x => x.Contains("Sales"));
                        //if (isExist != null)
                        //{

                        //   var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Sales) };
                        //    bEEContext.UserWiseModule.Add(item);
                        //    var notification = access.FirstOrDefault(x => x.Contains("Approver"));
                        //    if (notification != null)
                        //    {
                        //        foreach (var aNotifi in ApproverNotification.AllNotification())
                        //        {
                        //            bEEContext.NotificationUsers.Add(new NotificationUser(contex.Users.FirstOrDefault(x => x.UserName == model.UserName).Id, aNotifi.Item2));
                        //        }
                        //    }
                        //}



                        var isExist = access.FirstOrDefault(x => x.Contains("Sales"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.Sales) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.HRAdmin)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Hra"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.HRAdmin) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.FixedAsset)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("FixedAsset"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.FixedAsset) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.DataAdmin)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Data"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.DataAdmin) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.SystemAdmin)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("Sys"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.SystemAdmin) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    else if (module == Module.StoreRM)
                    {
                        var isExist = access.FirstOrDefault(x => x.Contains("StoreRM"));
                        if (isExist != null)
                        {
                            var item = new UserWiseModule() { UserName = model.UserName, ModuleName = Enum.GetName(typeof(Module), Module.StoreRM) };
                            bEEContext.UserWiseModule.Add(item);
                        }
                    }
                    bEEContext.SaveChanges();
                }
            }

            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            //return RedirectToAction("Index", "Home");
            bEEContext.SaveChanges();
            return View("Register");
        }
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetEmployeeNameById(int employeeId)
        {
            BEEContext context = new BEEContext();
            var employee = bEEContext.Employees.FirstOrDefault(x => x.Id == employeeId);
            if (employee == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string sql = string.Format("select RoleId,AspNetRoles.Name from AspNetUserRoles inner join AspNetUsers on AspNetUserRoles.UserId = AspNetUsers.Id inner join AspNetRoles on AspNetUserRoles.RoleId = AspNetRoles.Id where AspNetUsers.UserName =  '" + employee.LogInUserName + "'");
                var employeeRoles = context.Database.SqlQuery<RoleName>(sql).ToList();
                //var employeeRoles = 
                return Json(new { EmployeeName = employee, EmployeeRoles = employeeRoles }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PermissionIndex()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Users.ToList();
            List<PermissionVModel> items = new List<PermissionVModel>();

            foreach (var item in user)
            {
                string role = "";
                item.Roles.ToList().ForEach(y => role += context.Roles.FirstOrDefault(x => x.Id == y.RoleId).Name + " ,");
                items.Add(new PermissionVModel() { UserName = item.UserName, Roles = role });
            }
            return View(items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
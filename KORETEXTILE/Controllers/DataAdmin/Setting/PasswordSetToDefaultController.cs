using BEEERP.Models;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Data_Admin.Settings
{
    [ShowNotification]
    public class PasswordSetToDefaultController : Controller
    {
        ApplicationDbContext AppDBcontext = new ApplicationDbContext();
        BEEContext context = new BEEContext();

        private ApplicationUserManager _userManager;
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

        // GET: PasswordSetToDefault
        public ActionResult SetDefaultPassword()
        {
            ViewBag.LoginUserName = LoadComboBox.LoadAllUserName();
            return View();
        }

        public ActionResult GetUserInfo(string userName)
        {
            var Employee = context.Employees.FirstOrDefault(x => x.LogInUserName == userName);
            if (Employee != null)
            {
                var Designation = "";
                if(Employee.Designation != 0)
                {
                    Designation = context.Designation.FirstOrDefault(x => x.Id == Employee.Designation).Name;
                }

                return Json( new { Message=0 , Name = Employee.Name , Id=Employee.Id, Designation= Designation }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message=2 }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> ChangeToDefault(string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                string resetToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var result = await UserManager.ResetPasswordAsync(user.Id, resetToken, "123456");
                if (result.Succeeded)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
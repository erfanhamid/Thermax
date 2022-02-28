using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Notification;
using System.Data.Entity;

namespace BEEERP.Controllers.Notification
{
    [ShowNotification]
    public class NotificationController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult ShowNotification()
        {
            string user = User.Identity.GetUserId();
            string loginName = User.Identity.GetUserName();
            var employee = context.Employees.FirstOrDefault(x => x.LogInUserName == loginName);
            if(employee!=null)
            {
                var lastNotificationDate = DateTime.Now.AddDays(-3);
                var items = (from p in context.Notification
                             join c in context.ApprovalLog on p.Id equals c.NotificationId
                             where (c.IsView == false || c.IsApproved == false) && (c.UserId == user) && (p.BranchId==employee.BranchID || p.BranchId == 0) && (p.PostingDate >= lastNotificationDate || c.IsApproved == false)
                             select p).ToList().OrderByDescending(x => x.PostingDate).ToList();
                var arrpovalLog = context.ApprovalLog.Where(x => x.UserId == user && x.IsView == false).ToList();
                foreach (var item in arrpovalLog)
                {
                    item.IsView = true;
                    context.Entry<ApprovalLog>(item).State = EntityState.Modified;
                }
                context.SaveChanges();
                return View(items);
            }
            else
            {
                return View(new List<UserNotification>());
            }
            
        }
    }
}
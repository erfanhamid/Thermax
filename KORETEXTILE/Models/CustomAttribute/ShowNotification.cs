using BEEERP.Models.Database;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Models.CustomAttribute
{
    public class ShowNotification:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BEEContext context = new BEEContext();
            string userId = HttpContext.Current.User.Identity.GetUserId();
            //filterContext.Controller.ViewBag.Notification= new List<UserNotification>();
            //filterContext.Controller.ViewBag.NotCount = 0;
            var loginName = HttpContext.Current.User.Identity.GetUserName();
            var employee = context.Employees.AsEnumerable().FirstOrDefault(x => x.LogInUserName == loginName);
            if (employee != null)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var lastNotificationDate = DateTime.Now.AddDays(-3);
                    var items = (from p in context.Notification
                                 join c in context.ApprovalLog on p.Id equals c.NotificationId
                                 where c.IsView == false && c.UserId == userId && (p.PostingDate >= lastNotificationDate || c.IsApproved == false)
                                 select p).ToList().OrderByDescending(x => x.PostingDate).ToList().Take(4).ToList();
                    filterContext.Controller.ViewBag.Notification = items;
                    filterContext.Controller.ViewBag.NotCount = items.Count();
                }
                else
                {
                    filterContext.Controller.ViewBag.Notification = new List<UserNotification>();
                    filterContext.Controller.ViewBag.NotCount = 0;
                }
            }
            else
            {
                filterContext.Controller.ViewBag.Notification = new List<UserNotification>();
                filterContext.Controller.ViewBag.NotCount = 0;
            }


        }
    }
}
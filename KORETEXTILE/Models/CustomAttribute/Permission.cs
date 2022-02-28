using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Models.CustomAttribute
{
    public class Permission:ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userId=HttpContext.Current.User.Identity.GetUserId();
            //UserHavePermission per=new UserHavePermission();
            UserHavePermission.SetUserHavePermission(userId);

        }
    }
}
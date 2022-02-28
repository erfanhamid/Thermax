using BEEERP.Models.CommonInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Models.CustomAttribute
{
    public class StoreRMModule : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserAccessAction.IsAdmin = HttpContext.Current.User.IsInRole("RMAdmin");
        }
    }
}
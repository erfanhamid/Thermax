using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BEEERP.Models.CommonInformation
{
    public static class UserHavePermission
    {
         

        public static void SetUserHavePermission(string userId)
        {

            BEEContext context = new BEEContext();
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            var currentController = currentRouteData.Values["controller"].ToString();
            var currentAction = currentRouteData.Values["action"].ToString();
            ApplicationDbContext appContext = new ApplicationDbContext();
            var user = appContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var isInRole = context.UserActionPermission.Any(y => y.ScreenName.ToLower() == currentController.ToLower() && y.Action == currentAction.ToLower() && y.Type.ToLower() == "save" && y.UserId == user.Id);
                if (isInRole)
                {
                    UserAccessAction.HavePerOnSave = true;
                }
                else
                {
                    UserAccessAction.HavePerOnSave = false;
                }
                isInRole = context.UserActionPermission.Any(y => y.ScreenName.ToLower() == currentController.ToLower() && y.Action == currentAction.ToLower() && y.Type.ToLower() == "update" && y.UserId == user.Id);
               
                if (isInRole)
                {
                    UserAccessAction.HavePerOnUpdate = true;
                }
                else
                {
                    UserAccessAction.HavePerOnUpdate = false;
                }
                isInRole = context.UserActionPermission.Any(y => y.ScreenName.ToLower() == currentController.ToLower() && y.Action == currentAction.ToLower() && y.Type.ToLower() == "delete" && y.UserId == user.Id);
               
                if (isInRole)
                {
                    UserAccessAction.HavePerOnDelete = true;
                }
                else
                {
                    UserAccessAction.HavePerOnDelete = false;
                }

                isInRole = context.UserActionPermission.Any(y => y.ScreenName.ToLower() == currentController.ToLower() && y.Action == currentAction.ToLower() && y.Type.ToLower() == "search" && y.UserId == user.Id);
                if (isInRole)
                {
                    UserAccessAction.HavePerOnSearch = true;
                }
                else
                {
                    UserAccessAction.HavePerOnSearch = false;
                }
                isInRole = context.UserActionPermission.Any(y => y.ScreenName.ToLower() == currentController.ToLower() && y.Action == currentAction.ToLower() && y.Type.ToLower() == "approve" && y.UserId == user.Id);
                if (isInRole)
                {
                    UserAccessAction.HavePerOnApprove = true;
                }
                else
                {
                    UserAccessAction.HavePerOnApprove = false;
                }
                isInRole = context.UserActionPermission.Any(y => y.ScreenName.ToLower() == currentController.ToLower() && y.Action == currentAction.ToLower() && y.Type.ToLower() == "print" && y.UserId == user.Id);
                if (isInRole)
                {
                    UserAccessAction.HavePerOnApprove = true;
                }
                else
                {
                    UserAccessAction.HavePerOnApprove = false;
                }
            }
            else
            {
                UserAccessAction.HavePerOnSave = false;
                UserAccessAction.HavePerOnUpdate = false;
                UserAccessAction.HavePerOnDelete = false;
                UserAccessAction.HavePerOnSearch = false;
                UserAccessAction.HavePerOnApprove = false;
                UserAccessAction.HavePerOnPrint = false;
            }
        }

    }
}
using BEEERP.Models.Database;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    public static class ScreenSessionData
    {
        static BEEContext context;
        public static int? GetEmployeeDepot()
        {
            context = new BEEContext();
            string userName = System.Web.HttpContext.Current.User.Identity.GetUserName();
            if (!string.IsNullOrEmpty(userName))
            {
                var employee = context.Employees.FirstOrDefault(x => x.LogInUserName ==userName );
                if (employee != null)
                {
                    
                    return employee.BranchID;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            
        }
    }
}
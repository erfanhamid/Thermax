using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Authentication
{
    public static class UserModuleManager
    {
        public static bool IsInModule(Module module)
        {
            BEEContext context = new BEEContext();
            var aModule = Enum.GetName(typeof(Module), module);
            var isHavePermission = context.UserWiseModule.FirstOrDefault(x=>x.ModuleName== aModule && x.UserName==HttpContext.Current.User.Identity.Name);
            if (isHavePermission != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
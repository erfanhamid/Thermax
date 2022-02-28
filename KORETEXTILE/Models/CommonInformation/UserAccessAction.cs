using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    public static class UserAccessAction
    {
        public static bool HavePerOnSave { set; get; }
        public static bool HavePerOnUpdate { set; get; }
        public static bool HavePerOnDelete { set; get; }
        public static bool HavePerOnSearch { set; get; }
        public static bool HavePerOnPrint { set; get; }
        public static bool HavePerOnPreview { set; get; }
        public static bool HavePerOnApprove { set; get; }
        public static bool IsAdmin { set; get; }
    }
}
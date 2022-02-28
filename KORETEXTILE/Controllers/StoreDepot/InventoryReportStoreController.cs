using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreDepot
{
    [ShowNotification]
    [Authorize(Roles = "DepAdmin,DepOperator,DepViewer,DepApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class InventoryReportStoreController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: InventoryRptForStoreDpt
        public ActionResult InventoryRptForStoreDpt()   
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class InventoryTransferController : Controller
    {
        // GET: InventoryTransfer
        public ActionResult InventoryTransfer()
        {
            return View();
        }
    }
}
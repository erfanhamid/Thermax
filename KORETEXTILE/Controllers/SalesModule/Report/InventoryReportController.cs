using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;

namespace BEEERP.Controllers.SalesModule.Report
{
    [ShowNotification]
    public class InventoryReportController : Controller
    {
        // GET: InventoryReport
        public ActionResult Search()
        {
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.ItemId = LoadComboBox.LoadItem(null);

            return View();
        }
    }
}
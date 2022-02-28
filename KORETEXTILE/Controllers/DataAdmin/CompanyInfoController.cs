using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;

namespace BEEERP.Controllers.Data_Admin
{
    [ShowNotification]
    public class CompanyInfoController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: CompanyInfo
        public ActionResult CompanyInfoView()
        {
            ViewBag.Months = LoadComboBox.LoadMonths();
            return View();
        }
    }
}
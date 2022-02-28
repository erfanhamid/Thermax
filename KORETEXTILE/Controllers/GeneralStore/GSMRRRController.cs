using System;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;
using BEEERP.Models.CustomAttribute;

namespace BEEERP.Controllers.GeneralStore
{
    [ShowNotification]
    [Permission]
    public class GSMRRRController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: GSMRRR
        public ActionResult Index()
        {

            var FactoryRM = context.Store.FirstOrDefault(x => x.Name.ToLower() == "factory rm");
            ViewBag.Store = LoadComboBox.LoadStoreRM();
            ViewBag.Supplier = LoadComboBox.LoadAllSupplier();
            //ViewBag.WOCode = LoadComboBox.LoadAllWorkOrder(null);
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadAllItem();
            ViewBag.UOM = LoadComboBox.LoadAllUOM();

            if (FactoryRM != null)
            {
                ViewBag.FactoryRM = FactoryRM.Id;
            }
            else
            {
                ViewBag.FactoryRM = 0;
            }
            return View();
        }
    }
}
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Sales.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreDepot.Report
{
    [ShowNotification]
    public class StoreDepotReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: StoreDepotReport
        public ActionResult StoreDepotReport()
        {
            
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadComboBox.LoadFGStoreByDepot(null);
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            return View();
        }
        public ActionResult GetStoreDepot(int Depot)
        {
            if (Depot != 0 && Depot.ToString() != null)
            {
                return Json(new { store = LoadComboBox.LoadFGStoreByDepot(Depot)}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetSingleDepotInventoryStatus(InventoryReportVModel model)
        {
            //Session["ReportName"] = "Finish Goods Production Summary";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.FromDate;
            param.ToDate = model.ToDate;
            //param.ToDate = model.ToDate;
            param.DepotName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.DepotID).BrnachName;

            Session["InventoryStatusParam"] = param;
            string sql = string.Format("exec SingleDepotInventoryStatus '" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'" + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.FromDate) + "'," + model.DepotID);
            var items = context.Database.SqlQuery<SingleDepotInventoryStatusReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDepotInventoryStatusReport report = new SingleDepotInventoryStatusReport();
                items.Add(report);
            }
            Session["InventoryStatusData"] = items;
            return Redirect("/CrystalReport/SingleDepotInventoryStatusRpt.aspx");
        }
        
    }
}
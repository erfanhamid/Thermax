using System;
using BEEERP.Models.CustomAttribute;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.CrystalReport.ReportFormat;

namespace BEEERP.Controllers.GeneralStore
{
    [ShowNotification]
    public class GeneralStoreReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: GeneralStoreReport
        public ActionResult GeneralStoreReportView()
        {

            ViewBag.Store = LoadComboBox.LoadGSStore();
            ViewBag.GroupId = LoadComboBox.LoadGeneralStoreGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);

            return View();
        }
        public ActionResult ShowStorewiseGSInventoryBalance(InventoryReportVModel model)
        {
            Session["ReportName"] = "StorewiseGSInventoryBalanceNewR";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            //param.ToDate = model.ToDate;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;
            //param.RootAccountType = "FG";

            Session["StorewiseGSInventoryParam"] = param;
            string sql = string.Format("exec spSingleStoreGSInventoryBalance '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "'" + "," + model.StoreId + "");
            var items = context.Database.SqlQuery<StorewiseGSInventoryReport>(sql).ToList();
            if (items.Count == 0)
            {
                StorewiseGSInventoryReport report = new StorewiseGSInventoryReport();
                items.Add(report);
            }
            Session["StorewiseGSInventoryData"] = items;
            return Redirect("/CrystalReport/StorewiseGSInventoryRpt.aspx");
        }
    }
}
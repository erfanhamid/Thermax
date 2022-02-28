using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.SalesModule.Report
{
    [ShowNotification]
    public class RetailerReportController : Controller
    {
        // GET: RetailerReport
        BEEContext context = new BEEContext();
        public ActionResult RetailerReport()
        {
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            ViewBag.Retailer = LoadComboBox.LoadAllRetailerByDealer(null);
            return View();
        }

        public ActionResult GetRetailerByDealer(int id)
        {
            return Json(LoadComboBox.LoadAllRetailerByDealer(id), JsonRequestBehavior.AllowGet);
        }
        // Report : Single Depot Retailer Contact Summary / List
        public ActionResult SingleDepotRetailer(RetailerReportVModel model)
        {
            Session["ReportName"] = "SingleDepotRetailerR";

            //ReportParmPersister param = new ReportParmPersister();
            //param.FromDate = model.From;
            //param.ToDate = model.To;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            //Session["SingleItemRMIssueDetailparam"] = param;

            string sql = string.Format("exec spSingleDepotRetailerList '" + model.DepotId + "'");
            var items = context.Database.SqlQuery<SingleDepotRetailerReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDepotRetailerReport report = new SingleDepotRetailerReport();
                items.Add(report);
            }
                Session["SingleDepotRetailerData"] = items;
                return Redirect("/CrystalReport/SingleDepotRetailerRpt.aspx");
        }
        // Report : Single Dealer All Rent Details
        public ActionResult SingleDealerAllRentDetails(RetailerReportVModel model)
        {
            Session["ReportName"] = "SingleDealerAllRentDetailsR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.ItemID = model.ItemID;
            //param.ItemGroup = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.GroupID).Name;
            //param.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == model.ItemID).Name;
            //param.StoreName = context.Store.FirstOrDefault(x => x.Id == model.StoreId).Name;

            Session["SingleDealerAllRentDetailsparam"] = param;

            string sql = string.Format("exec spSingleDealerAllRentDetails '" + model.DepotId + "'," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<SingleDealerAllRentDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDealerAllRentDetailsReport report = new SingleDealerAllRentDetailsReport();
                items.Add(report);
            }
            Session["SingleDepotRetailerData"] = items;
            return Redirect("/CrystalReport/SingleDealerAllRentDetailsRpt.aspx");
        }
        public ActionResult SingleDepotRentSummary(RetailerReportVModel model)
        {
            Session["ReportName"] = "SingleDepotAllRentSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SingleDepotRentSummaryParam"] = param;

            string sql = string.Format("exec spSingleDepotAllRentSummary" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.DepotId + "'");
            var items = context.Database.SqlQuery<SingleDepotAllRentSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDepotAllRentSummaryReport report = new SingleDepotAllRentSummaryReport();
                items.Add(report);
            }
            Session["SingleDepotRentSummaryData"] = items;
            return Redirect("/CrystalReport/NewSingleDepotRentSummary.aspx");
        }
        public ActionResult DepotWiseallRentSummary(RetailerReportVModel model)
        {
            Session["ReportName"] = "DepotWiseallRentSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["DepotWiseallRentSummaryParam"] = param;

            string sql = string.Format("exec spDepotWiseAllRentSummary" + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "'" + "," + "'" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<DepotWiseAllRentSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                DepotWiseAllRentSummaryReport report = new DepotWiseAllRentSummaryReport();
                items.Add(report);
            }
            Session["DepotWiseallRentSummaryData"] = items;
            return Redirect("/CrystalReport/DepotWiseAllRentSummaryRpt.aspx");
        }
    }
}
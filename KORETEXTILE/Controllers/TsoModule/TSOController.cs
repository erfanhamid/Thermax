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

namespace BEEERP.Controllers.TsoModule
{
    [ShowNotification]
    [Authorize(Roles = "SalAdmin,SalOperator,SalViewer,SalApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class TSOController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: TSO
        public ActionResult TSO()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            if (depot == null)
            {
                ViewBag.Disabled = "true";
                ViewBag.TSO = LoadComboBox.LoadOneUser();
            }
            else
            {
                ViewBag.Disabled = "false";
                ViewBag.TSO = LoadComboBox.LoadOneUser();
            }
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.ItemId = LoadComboBox.LoadItem(null);
            return View();
        }
        public ActionResult GetCustomerById(int custId, int depotId)
        {
            var customer = FindObjectById.GetCustByStoreAndCustId(depotId, custId);
            if (customer != null)
            {
                return Json(new { Name = customer.CustomerName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult showCustWiseSaleDetailsRep(ReportVModel model)
        {
            Session["ReportName"] = "CustomerSalesDetails";
            var items = context.Database.SqlQuery<Models.ViewModel.Sales.Report.DepotAndDealerWiseIncentiveSummaryReport>(string.Format("exec CustWiseSalesDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.Depot + "")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.Depot).BrnachName;

            Session["CustWiseSalesDetailsList"] = items;
            Session["CustWiseSalesDetailsParam"] = param;

            return Redirect("/CrystalReport/CustomerSalesDetails.aspx");
        }

        public ActionResult showCustWiseSaleSummRep(ReportVModel model)
        {
            var items = context.Database.SqlQuery<CustWiseSaleSumVModel>(string.Format("exec CustWiseSalesSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.Depot + "")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.Depot).BrnachName;

            Session["CustWiseSaleSummList"] = items;
            Session["CustWiseSaleSummParam"] = param;
            Session["CustWiseSaleSummRpt"] = "CustWiseSaleSumm";
            return Redirect("/CrystalReport/CustSaleSummary.aspx");
        }

        public ActionResult showSingleItemSaleDetails(ReportVModel model)
        {
            Session["ReportName"] = "ItemWiseSingleDepotSale";
            return Redirect("/CrystalReport/SingleItemSaleD.aspx");
        }

        public ActionResult showItemWiseSingleDepotSale(ReportVModel model)
        {
            var items = context.Database.SqlQuery<ItemWiseSingleDepotSalevModel>(string.Format("exec ItemWiseSingleDepotSale '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.Depot + "")).ToList();
            ReportParmPersister param = new ReportParmPersister();

            param.FromDate = model.From;
            param.ToDate = model.To;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.Depot).BrnachName;
            if (items == null)
            {
                items = new List<ItemWiseSingleDepotSalevModel>();
            }
            Session["ItemWiseSingleDepotSaleList"] = items;
            Session["ItemWiseSingleDepotSaleParam"] = param;
            Session["ItemWiseSingleDepotSaleRpt"] = "ItemWiseSingleDepotSale";

            return Redirect("/CrystalReport/ItemWiseSingleDeS.aspx");

        }

        public ActionResult GetDepotWiseSalesSummary(ReportVModel model)
        {
            Session["ReportName"] = "DepotWiseSalesSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["DepotWiseSalesSumryByDate"] = param;

            string sql = string.Format("exec DepotWiseSalesSummary '" + model.Depot + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<DepotWiseSalesSumryReport>(sql).ToList();
            if (items.Count == 0)
            {
                DepotWiseSalesSumryReport report = new DepotWiseSalesSumryReport();
                items.Add(report);
            }
            Session["DepotWiseSalesSumryReportData"] = items;
            return Redirect("/CrystalReport/DepotWiseSalesSumryRpt.aspx");
        }

        public ActionResult GetInvoiceWiseSaleSummary(ReportVModel model)
        {
            Session["ReportName"] = "InvoiceWiseSaleSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["InvoiceWiseSaleSummaryByDate"] = param;

            string sql = string.Format("exec SalesStatementSumInvoiceWise '" + model.Depot + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<InvoiceWiseSaleSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                InvoiceWiseSaleSummaryReport report = new InvoiceWiseSaleSummaryReport();
                items.Add(report);
            }
            Session["InvoiceWiseSaleSummaryReportData"] = items;
            return Redirect("/CrystalReport/InvoiceWiseSaleSummaryRpt.aspx");

        }

        public ActionResult GetTSOWiseSaleSummary(ReportVModel model)
        {
            Session["ReportName"] = "TSOWiseSaleSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["TSOWiseSaleSummaryByDate"] = param;

            string sql = string.Format("exec TsoWiseSalesSummary   '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.TSOId + "'");
            var items = context.Database.SqlQuery<TSOWiseSaleSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                TSOWiseSaleSummaryReport report = new TSOWiseSaleSummaryReport();
                items.Add(report);
            }
            Session["TSOWiseSaleSummaryReportData"] = items;
            return Redirect("/CrystalReport/TSOWiseSaleSummaryRpt.aspx");

        }

        public ActionResult GetTSOWiseSaleDetails(ReportVModel model)
        {
            Session["ReportName"] = "TSOWiseSaleDetailsR";

            ReportParmPersister param = new ReportParmPersister();
            var tso = context.Employees.FirstOrDefault(x => x.Id == model.TSOId);
            if (tso != null)
            {
                param.TsoName = tso.Name;
            }
            else
            {
                param.TsoName = "";
            }

            Session["TsoWiseSalesDetails"] = param;

            string sql = string.Format("exec TsoWiseSalesDetails '" + model.TSOId + "'");
            var items = context.Database.SqlQuery<TSOWiseSaleDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                TSOWiseSaleDetailsReport report = new TSOWiseSaleDetailsReport();
                items.Add(report);
            }
            Session["TSOWiseSaleDetailsReportData"] = items;
            return Redirect("/CrystalReport/TSOWiseSaleDetailsRpt.aspx");

        }
        public ActionResult GetTSOWiseCollectionDetails(ReportVModel model)
        {
            Session["ReportName"] = "TSOWiseCollectionDetailsR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            var tso = context.Employees.FirstOrDefault(x => x.Id == model.TSOId);
            if (tso != null)
            {
                param.TsoName = tso.Name;
            }
            else
            {
                param.TsoName = "";
            }

            Session["TsoWiseCollectionDetails"] = param;

            string sql = string.Format("exec TsoWiseCollectionDetails   '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.TSOId + "', '" + model.Depot + "'");
            var items = context.Database.SqlQuery<TSOWiseCollectionDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                TSOWiseCollectionDetailsReport report = new TSOWiseCollectionDetailsReport();
                items.Add(report);
            }
            Session["TSOWiseCollectionDetailsReportData"] = items;
            return Redirect("/CrystalReport/TSOWiseCollectionDetailsRpt.aspx");

        }
        public ActionResult GetTsoWiseSalesItem(ReportVModel model)
        {
            Session["ReportName"] = "TsoWiseSalesItemR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;

            Session["TsoWiseSalesItem"] = param;

            string sql = string.Format("exec TsoWiseSalesItem '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.TSOId + "'");
            var items = context.Database.SqlQuery<TsoWiseSalesItemReport>(sql).ToList();
            if (items.Count == 0)
            {
                TsoWiseSalesItemReport report = new TsoWiseSalesItemReport();
                items.Add(report);
            }
            Session["TsoWiseSalesItemReportData"] = items;
            return Redirect("/CrystalReport/TsoWiseSalesItemRpt.aspx");

        }

        public ActionResult GetTsoWiseSalesReturn(ReportVModel model)
        {
            Session["ReportName"] = "TsoWiseSalesReturnR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;

            Session["TsoWiseSalesReturn"] = param;

            string sql = string.Format("exec TsoWiseSalesReturn '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.TSOId + "'");
            var items = context.Database.SqlQuery<TsoWiseSalesItemReport>(sql).ToList();
            if (items.Count == 0)
            {
                TsoWiseSalesItemReport report = new TsoWiseSalesItemReport();
                items.Add(report);
            }
            Session["TsoWiseSalesReturnReportData"] = items;
            return Redirect("/CrystalReport/TsoWiseSalesReturnRpt.aspx");

        }

        public ActionResult GetTsoWiseSalesAndCollection(ReportVModel model)
        {
            Session["ReportName"] = "TsoWiseSalesAndCollectionR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;

            Session["TsoWiseSalesAndCollection"] = param;

            string sql = string.Format("exec TsoWiseSalesAndCollection '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.TSOId + "'");
            var items = context.Database.SqlQuery<TsoWiseSalesAndCollectionReport>(sql).ToList();
            if (items.Count == 0)
            {
                TsoWiseSalesAndCollectionReport report = new TsoWiseSalesAndCollectionReport();
                items.Add(report);
            }
            Session["TsoWiseSalesAndCollectionReportData"] = items;
            return Redirect("/CrystalReport/TsoWiseSalesAndCollectionRpt.aspx");

        }
        public ActionResult GetTsoWiseSalesStatement(ReportVModel model)
        {
            Session["ReportName"] = "TsoWiseSalesStatementR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;

            Session["TsoWiseSalesStatement"] = param;

            string sql = string.Format("exec TsoWiseSalesStatement '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', '" + model.Depot + "'");
            var items = context.Database.SqlQuery<TsoWiseSalesStatementReport>(sql).ToList();
            if (items.Count == 0)
            {
                TsoWiseSalesStatementReport report = new TsoWiseSalesStatementReport();
                items.Add(report);
            }
            Session["TsoWiseSalesStatementReportData"] = items;
            return Redirect("/CrystalReport/TsoWiseSalesStatementRpt.aspx");

        }
        public ActionResult GetTsoWiseCustomerSummary(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["TsoWiseCustomerSummary"] = param;

            string sql = string.Format("exec TsoWiseCustSummary '" + model.TSOId + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.Depot + "' ");
            var items = context.Database.SqlQuery<TsoWiseCustSummryReport>(sql).ToList();
            if (items.Count == 0)
            {
                items = new List<TsoWiseCustSummryReport>();
            }
            Session["TsoWiseCustomerSummaryRptData"] = items;
            return Redirect("/CrystalReport/TsoWiseCustomerSummaryRpt.aspx");

        }

        public ActionResult GetTsoWiseCustomerDetails(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.TsoName = context.Employees.FirstOrDefault(x => x.Id == model.TSOId).Name;
            Session["TsoWiseCustDetails"] = param;

            string sql = string.Format("exec TsoWiseCustDetails  '" + model.TSOId + "'");
            var items = context.Database.SqlQuery<TsoWiseCustDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                items = new List<TsoWiseCustDetailsReport>();
            }
            Session["TsoWiseCustDetailsRptData"] = items;
            return Redirect("/CrystalReport/TsoWiseCustDetailsRpt.aspx");
        }

    }
}
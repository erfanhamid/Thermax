using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.Models.ViewModel.Sales.Report;

namespace BEEERP.Controllers.SalesModule.Report
{
    [ShowNotification]
    //[Authorize(Roles = "SalAdmin,SalOperator,SalViewer,SalApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    //[SaleModule]
    public class CustomerReportController : Controller
    {

        BEEContext context = new BEEContext();
        // GET: SalesReport
        public ActionResult CustomerReportView()
        {
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);

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

        public ActionResult GetDealerReceiveableSummary(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec SingleDepotDealerReceiveable '"+DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate)+ "'," +model.DepotId);
            var items = context.Database.SqlQuery<SingleDepotDealerReceiveable>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDepotDealerReceiveable report = new SingleDepotDealerReceiveable();
                items.Add(report);
            }
            param.AsOnDate = model.AsOnDate;
            Session["RevParam"] = param;
            Session["RevData"] = items;
            return Redirect("/CrystalReport/SingleDepotDealerReceiveableRpt.aspx");
        }
        public ActionResult GetDealerCollectionSummary(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec SingleDepotCollectionSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.DepotId);
            var items = context.Database.SqlQuery<SingleDepotCollectionSummary>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDepotCollectionSummary report = new SingleDepotCollectionSummary();
                items.Add(report);
            }
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["CollectionParam"] = param;
            Session["CollectionData"] = items;
            return Redirect("/CrystalReport/SingleDepotDealerCollectionSummaryRpt.aspx");
        }
        public ActionResult GetDealerCollectionDetails(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec SingleDealerCollectionDetails '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.CustomerID);
            var items = context.Database.SqlQuery<SingleDealerCollectionDetailsReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDealerCollectionDetailsReport report = new SingleDealerCollectionDetailsReport();
                items.Add(report);
            }
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SingleDealerCollectionParam"] = param;
            Session["SingleDealerCollectionData"] = items;
            return Redirect("/CrystalReport/SingleDealerCollectionDetailsRpt.aspx");
        }
        public ActionResult GetDepotZoneWiseCollectionSummary(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec DepotZoneWiseCollectionSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'" );
            var items = context.Database.SqlQuery<DepotZoneWiseCollectionSummary>(sql).ToList();
            if (items.Count == 0)
            {
                DepotZoneWiseCollectionSummary report = new DepotZoneWiseCollectionSummary();
                items.Add(report);
            }
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["DepotZoneParam"] = param;
            Session["DepotZoneData"] = items;
            return Redirect("/CrystalReport/DepotZoneWiseCollectionSummaryRpt.aspx");
        }
        public ActionResult GetSingleDepotCustAccReceivableSummary(ReportVModel model)
        {
            Session["ReportName"] = "SingleDptCustAccReceivableSummaryR";

            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            Session["SingleDepotCustAccReceivableSummaryAsOnDate"] = param;

            string sql = string.Format("exec CustomerAccReceivable '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + "', '" + model.CustomerID + "', '" + model.DepotId + "'");
            var items = context.Database.SqlQuery<SingleDptCustAccRecevSmmryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleDptCustAccRecevSmmryReport report = new SingleDptCustAccRecevSmmryReport();
                items.Add(report);
            }
            Session["SingleDptCustAccRecevSmmryReportData"] = items;
            return Redirect("/CrystalReport/SingleDptCustAccReceivableSummary.aspx");
        }

        public ActionResult ShowLedgerReport(ReportVModel model)
        {
            Session["ReportName"] = "DealerLedger";
            ReportParmPersister param = new ReportParmPersister();
            var depot = ScreenSessionData.GetEmployeeDepot();
            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerID && (x.DepotId == model.DepotId || x.DepotId == depot || depot == null));
            if (customer == null)
            {
                Session["validation"] = "Customer Doesn't Exist selected Depot or You are not allowed to see.";
                return RedirectToAction("Search");
            }
            else
            {
                param.CustId = customer.Id.ToString();
                param.CustName = customer.CustomerName;
                param.Address = customer.BillTo;
                param.FromDate = DateTime.Now;
                param.ToDate = DateTime.Now;
                Session["SearchCustLedger"] = param;

                var items = context.Database.SqlQuery<CustLedgerReport>(string.Format("exec CustomerLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.CustomerID + "")).ToList();
                if (items.Count == 0)
                {
                    CustLedgerReport report = new CustLedgerReport();
                    items.Add(report);
                }
                Session["CustLedgerReportData"] = items;
                return Redirect("/CrystalReport/CustomerLedger.aspx");
            }

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
        public ActionResult ShowSinCustItemWiseSalesSummry(ReportVModel model)
        {
            Session["ReportName"] = "SingleCustItemWiseSalesSummryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerID);
            if (customer != null)
            {
                param.CustName = customer.CustomerName;
            }
            else
            {
                param.CustName = "";
            }

            Session["SingleCustItemWiseSalesSummry"] = param;

            string sql = string.Format("exec SingleCustomerItemWiseSalesSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','"+model.CustomerID+"', '" + model.Depot + "'");
            var items = context.Database.SqlQuery<SingleCustItemWiseSalesSummryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleCustItemWiseSalesSummryReport report = new SingleCustItemWiseSalesSummryReport();
                items.Add(report);
            }
            Session["SingleCustItemWiseSalesSummryReportData"] = items;
            return Redirect("/CrystalReport/SingleCustItemWiseSalesSummryRpt.aspx");

        }
        public ActionResult ShowSingleCustomerItemWiseSalesReturn(ReportVModel model)
        {
            Session["ReportName"] = "SingleCustomerItemWiseSalesReturnR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerID);
            if (customer != null)
            {
                param.CustName = customer.CustomerName;
            }
            else
            {
                param.CustName = "";
            }

            Session["SingleCustomerItemWiseSalesReturn"] = param;

            string sql = string.Format("exec SingleCustomerItemWiseSalesReturnSum '" + model.CustomerID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "', '" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<SingleCustomerItemWiseSalesReturnReport>(sql).ToList();
            if (items.Count == 0)
            {
                SingleCustomerItemWiseSalesReturnReport report = new SingleCustomerItemWiseSalesReturnReport();
                items.Add(report);
            }
            Session["SingleCustomerItemWiseSalesReturnReportData"] = items;
            return Redirect("/CrystalReport/SingleCustomerItemWiseSalesReturnRpt.aspx");

        }
        public ActionResult GetCustomerAgeingSummary(ReportVModel model)
        {
            Session["ReportName"] = "CustomerAgeingR";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            Session["CustomerAgeingSummaryParam"] = param;
            string sql = string.Format("exec CustomerAgeing  '" + model.CustomerID +" ','" + model.DepotId+ "',' "+ DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) +" '");
            var items = context.Database.SqlQuery<CustomerAgeingReport>(sql).ToList();

            if (items.Count == 0)
            {
                items = new List<CustomerAgeingReport>();
            }
            Session["CustomerAgeingSummaryData"] = items;
            return Redirect("/CrystalReport/CustomerAgeingRpt.aspx");
        }
        public ActionResult GetSingleCustomerSalesAndCollection(ReportVModel model)
        {
            //Session["ReportName"] = "SingleC";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            Session["SingleCustomerSalesAndCollectionParam"] = param;
            string sql = string.Format("exec SingleCustomerSalesAndCollection  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.CustomerID + "'");
            param.CustName = FindObjectById.CustomerSearch(model.CustomerID).CustomerName;
            param.CustomerId = model.CustomerID;
            param.FromDate = model.From;
            param.ToDate = model.To;
            var items = context.Database.SqlQuery<TsoWiseSalesAndCollectionReport>(sql).ToList();

            if (items.Count == 0)
            {
                items = new List<TsoWiseSalesAndCollectionReport>();
            }
            Session["SingleCustomerSalesAndCollectionData"] = items;
            return Redirect("/CrystalReport/SingleCustomerSalesAndCollection.aspx");
        }
        public ActionResult GetDepotWiseReceiveableSummary(ReportVModel model)
        {
          
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            Session["DepotWiseReceiveableSummaryParam"] = param;
            string sql = string.Format("exec DepotWiseReceiveableSummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) + " '");
            var items = context.Database.SqlQuery<DepotWiseReceiveableSummaryReport>(sql).ToList();

            if (items.Count == 0)
            {
                items = new List<DepotWiseReceiveableSummaryReport>();
            }
            Session["DepotWiseReceiveableSummaryData"] = items;
            return Redirect("/CrystalReport/DepotWiseReceiveableSummaryRpt.aspx");
        }
        public ActionResult GetSingleDealerAllRentDetails(ReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SingleDealerAllRentDetailsParam"] = param;
            string sql = string.Format("exec SingleDealerAllRentDetails  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.CustomerID + "'");
            var items = context.Database.SqlQuery<SingleDealerAllRentDetailsReport>(sql).ToList();

            if (items.Count == 0)
            {
                items = new List<SingleDealerAllRentDetailsReport>();
            }
            Session["SingleDealerAllRentDetailsData"] = items;
            return Redirect("/CrystalReport/SingleDealerAllRentDetailsRpt.aspx");
        }
        public ActionResult GetSingleDepotAllRentSummary(ReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SingleDepotAllRentSummaryParam"] = param;
            string sql = string.Format("exec SingleDepotAllRentSummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.DepotId + "'");
            var items = context.Database.SqlQuery<SingleDepotAllRentSummaryReport>(sql).ToList();

            if (items.Count == 0)
            {
                items = new List<SingleDepotAllRentSummaryReport>();
            }
            Session["SingleDepotAllRentSummaryData"] = items;
            return Redirect("/CrystalReport/SingleDepotAllRentSummaryRpt.aspx");
        }
        public ActionResult GetDealerContactSummary(ReportVModel model)
        {   
            string sql = string.Format("exec spDealerContactSummary");
            var items = context.Database.SqlQuery<DealerContactSummaryReport>(sql).ToList();

            if (items.Count == 0)
            {
                items = new List<DealerContactSummaryReport>();
            }
            Session["DealerContactSummaryReportData"] = items;
            return Redirect("/CrystalReport/DealerContactSummaryRpt.aspx");
        }

        public ActionResult GetDepotWiseReceivableSummary(ReportVModel model)
        {
            ReportParmPersister param = new ReportParmPersister();
            string sql = string.Format("exec DepotWiseDealerReceiveable '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) +"' ");
            var items = context.Database.SqlQuery<DepotWiseDealerReceiveableReport>(sql).ToList();
            if (items.Count == 0)
            {
                DepotWiseDealerReceiveableReport report = new DepotWiseDealerReceiveableReport();
                items.Add(report);
            }
            param.AsOnDate = model.AsOnDate;
            Session["DepotWiseDealerReceiveableParam"] = param;
            Session["DepotWiseDealerReceiveableData"] = items;
            return Redirect("/CrystalReport/DepotWiseDealerReceiveableRpt.aspx");
        }
    }

}



    

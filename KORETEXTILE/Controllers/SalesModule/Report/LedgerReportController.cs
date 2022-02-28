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
    public class LedgerReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LedgerReport
        public ActionResult Search()
        {
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.LedgerAC = LoadComboBox.LoadAllAccount();
            ReportVModel model = new ReportVModel();
            model.DepotId = ScreenSessionData.GetEmployeeDepot() ?? default(int);
            return View(model);
        }

        public ActionResult ShowLedgerReport(ReportVModel model)
        {
            Session["ReportName"] = "DealerLedger";
            ReportParmPersister param = new ReportParmPersister();
            var depot = ScreenSessionData.GetEmployeeDepot();
            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerID && (x.DepotId == model.DepotId || x.DepotId== depot || depot==null));
            if(customer==null)
            {
                Session["validation"] = "Customer Doesn't Exist selected Depot or You are not allowed to see." ;
                return RedirectToAction("Search");
            }
            else
            {
                param.CustId = customer.Id.ToString();
                param.CustName = customer.CustomerName;
                param.Address = customer.BillTo;
                param.FromDate = model.From;
                param.ToDate = model.To;
                var depo = context.Customers.FirstOrDefault(x => x.Id == customer.Id).DepotId;
                param.DepotName = context.BranchInformation.FirstOrDefault(x => x.Slno == depo).BrnachName;
                Session["SearchCustLedger"] = param;

                var items = context.Database.SqlQuery<CustLedgerReport>(string.Format("exec CustomerLedger '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'," + model.CustomerID + "")).ToList();
                if(items.Count==0)
                {
                    CustLedgerReport report = new CustLedgerReport();
                    items.Add(report);
                }
                Session["CustLedgerReportData"] = items;
                return Redirect("/CrystalReport/CustomerLedger.aspx");
            }
            
        }
        public ActionResult ShowAccountLedger()
        {
            Session["ReportName"] = "Sample";
            return Redirect("/CrystalReport/ReportViewer.aspx");
        }
        
    }
}
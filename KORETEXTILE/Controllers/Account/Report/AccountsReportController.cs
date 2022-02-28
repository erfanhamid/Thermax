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

namespace BEEERP.Controllers.Account.Report
{
    [ShowNotification]
    public class AccountsReportController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult Ledger()
        {
            //ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.LedgerAC = LoadComboBox.LoadAllAccount();
            ViewBag.Supplier = LoadComboBox.LoadAllSupplierByGroup(null);
            ViewBag.Employee = LoadComboBox.LoadAllEmployee();
            ViewBag.SupplierGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.LATR = LoadComboBox.LoadALLLATR();
            ViewBag.ILC = LoadComboBox.LoadAllILC();
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.CostCenter = LoadComboBox.LoadAllCostCenter();
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            ViewBag.Retailer = LoadComboBox.LoadAllRetailerByDealer(null);

            ReportVModel model = new ReportVModel();
            model.DepotId = ScreenSessionData.GetEmployeeDepot() ?? default(int);
            return View(model);
        }

        public ActionResult GetSupplierByGroup(int id)
        {
            if(id != 0)
            {
                return Json(LoadComboBox.LoadAllSupplierByGroup(id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public ActionResult ShowAccountLedger(ReportVModel model)
        {
            Session["AccountLedgerName"] = "AccountLedger";
            ReportParmPersister param = new ReportParmPersister();
            var ledgerAcc = context.ChartOfAccount.FirstOrDefault(x => x.Id == model.LedgerAC);
            if (ledgerAcc == null)
            {
                Session["validation"] = "Chart Of Account doesn't exist selected Depot or You are not allowed to see.";
                return RedirectToAction("Ledger");
            }
            else
            {
                
                param.FromDate = model.From;
                param.ToDate =model.To;
                param.AccountName = ledgerAcc.Name;
                param.RootAccountType = context.ChartOfAccount.FirstOrDefault(x => x.Id == model.LedgerAC).RootAccountType;
                Session.Remove("AccountLedgerparam");
                Session["AccountLedgerparam"] = param;
                string sql = string.Format("exec AccountsLedger " + model.LedgerAC + ",'" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
                List<AccountsLedgerReport> reports = new List<AccountsLedgerReport>();
                var items = context.Database.SqlQuery<AccountsLedgerReport>(sql).ToList();
                if (items.Count == 0)
                {
                    //AccountsLedgerReport report = new AccountsLedgerReport();
                    //items.Add(report);
                }
                else
                {
                    foreach(var item in items)
                    {
                        AccountsLedgerReport report = new AccountsLedgerReport();
                        if(item.rootAccountType=="as"||item.rootAccountType=="ex")
                        {
                            reports.Add(item);
                        }
                        else
                        {
                            report.Name = item.Name;
                            report.Particulars = item.Particulars;
                            report.rootAccountType = item.rootAccountType;
                            report.SubLedger = item.SubLedger;
                            report.TrDate = item.TrDate;
                            report.DocType = item.DocType;
                            report.DocID = item.DocID;
                            report.Amount = item.Amount*-1;
                            report.TrType =  item.TrType;
                            reports.Add(report);
                        }
                       
                    }
                }
                Session["AccountLedgerReportData"] = reports;
                return Redirect("/CrystalReport/AccountLedger.aspx");
            }
        }

        public ActionResult GetCustNameByDepot(int depot, int cust)
        {
            var customer = context.Customers.FirstOrDefault(x => x.DepotId == depot && x.Id == cust);
            if (customer != null)
            {
                return Json(customer.CustomerName, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
        }

        public ActionResult ShowMRLedgerReport(ReportVModel model)
        {
            Session["ReportName"] = "MRLedger";
            ReportParmPersister param = new ReportParmPersister();
            
            var employee = context.Employees.FirstOrDefault(x => x.Id == model.EmployeeID);
            if (employee == null)
            {
                Session["validationEmployee"] = "Employee Doesn't Exist.";
                return RedirectToAction("Ledger");
            }
            else
            {
                param.EmployeeID = employee.Id;
                param.EmployeeName = employee.Name;
                //param.Address = customer.BillTo;
                param.EmpDesignation = context.Designation.FirstOrDefault(x => x.Id == employee.Designation).Name;
                param.FromDate = model.From;
                param.ToDate = model.To;
                Session["SearchMRLedger"] = param;

                var items = context.Database.SqlQuery<MRLedgerReport>(string.Format("exec MoneyRequisitionLedger '"+ model.EmployeeID +"','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'")).ToList();
                if (items.Count == 0)
                {
                    MRLedgerReport report = new MRLedgerReport();
                    items.Add(report);
                }
                Session["MRLedgerReportData"] = items;
                return Redirect("/CrystalReport/MRLedger.aspx");
            }
        }

        public ActionResult Report()
        {
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.LedgerAC = LoadComboBox.LoadAllAccount();
            ReportVModel model = new ReportVModel();
            model.DepotId = ScreenSessionData.GetEmployeeDepot() ?? default(int);
            ViewBag.LedgerGroup = LoadComboBox.LoadChartofAccountGroupOnlyHasLedger();
            return View(model);
        }
        public ActionResult ShowGroupLedgerTransectionSummary(ReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["GroupLedgerTransactionSummaryparam"] = param;
            string sql = string.Format("exec GroupLedgerTransactionSummary '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "', "+model.LedgerGroup);
            var items = context.Database.SqlQuery<GroupLedgerTransactionSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                GroupLedgerTransactionSummaryReport report = new GroupLedgerTransactionSummaryReport();
                items.Add(report);
            }
            Session["GroupLedgerTransactionSummaryData"] = items;
            return Redirect("/CrystalReport/GroupLedgerTransactionSummaryRpt.aspx");
        }
        

        [HttpPost]
        public ActionResult ShowIncomeStatement(ReportVModel model)
        {
            Session["IncomeStatementName"] = "IncomeStatement";
            ReportParmPersister param = new ReportParmPersister();          
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session.Remove("IncomeStatementparam");
            Session["IncomeStatementparam"] = param;
            string sql = string.Format("exec IncomeStatement '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            // List<AccountsLedgerReport> reports = new List<AccountsLedgerReport>();
            var items = context.Database.SqlQuery<IncomeStatementReport>(sql).ToList();
            if (items.Count == 0)
            {
                IncomeStatementReport report = new IncomeStatementReport();
                items.Add(report);
            }

            Session["IncomeStatementData"] = items;
            return Redirect("/CrystalReport/IncomeStatement.aspx");
        }

        [HttpPost]
        public ActionResult ShowBalanceSheet(ReportVModel model)
        {
            Session["BalanceSheetName"] = "BalanceSheet";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
            Session.Remove("BalanceSheetParam");
            Session["BalanceSheetParam"] = param;
            string sql = string.Format("exec BalanceSheet '" + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) +"'");
            // List<AccountsLedgerReport> reports = new List<AccountsLedgerReport>();
            var items = context.Database.SqlQuery<BalanceSheetReport>(sql).ToList();
            if (items.Count == 0)
            {
                BalanceSheetReport report = new BalanceSheetReport();
                items.Add(report);
            }

            Session["BalanceSheetData"] = items;
            return Redirect("/CrystalReport/BalanceSheet.aspx");
        }

        public ActionResult ShowSupplierLedger(ReportVModel model)
        {
            Session["ReportName"] = "SupplierLedgerR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ID = model.SupplierID;
            param.SupplierName = context.Supplier.FirstOrDefault(m => m.Id == model.SupplierID).SupplierName;
            var groupid = context.Supplier.FirstOrDefault(x => x.Id == model.SupplierID).GroupID;
            param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == groupid).SgroupName;

            //var debitSum = context.SupplierBalanceCalculation 





            Session["SupplierLedgerParam"] = param;
            string sql = string.Format("exec SupplierLedger '" + model.SupplierID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<SupplierLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierLedgerReport report = new SupplierLedgerReport();
                items.Add(report);
            }
            Session["SupplierLedgerData"] = items;
            return Redirect("/CrystalReport/SupplierLedgerRpt.aspx");
        }
        public ActionResult ShowTrialBalance(ReportVModel model)
        {

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session.Remove("TrialBalanceParam");
            Session["TrialBalanceParam"] = param;
            string sql = string.Format("exec TrialBalance '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            // List<AccountsLedgerReport> reports = new List<AccountsLedgerReport>();
            var items = context.Database.SqlQuery<TrialBalanceReport>(sql).ToList();
            var a = items.Where(x => x.Name == "Sales Revenue").ToList();
            if (items.Count == 0)
            {
                TrialBalanceReport report = new TrialBalanceReport();
                items.Add(report);
            }

            Session["TrialBalanceData"] = items;
            return Redirect("/CrystalReport/TrialBalanceR.aspx");
        }
        // to show import LC ledger
        public ActionResult ShowImportLCLedger(ReportVModel model)
        {
            Session["ReportName"] = "ImportLCLedgerR";

            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ID = model.ILCID;
            //param.SupplierName = context.Supplier.FirstOrDefault(m => m.Id == model.SupplierID).SupplierName;
            //var groupid = context.Supplier.FirstOrDefault(x => x.Id == model.SupplierID).GroupID;
            //param.SupplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == groupid).SgroupName;

            Session["ILCLedgerParam"] = param;
            string sql = string.Format("exec spImportLCLedger '" + model.ILCID + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "' ");
            var items = context.Database.SqlQuery<ImportLCLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                ImportLCLedgerReport report = new ImportLCLedgerReport();
                items.Add(report);
            }
            Session["ImportLCLedgerData"] = items;
            return Redirect("/CrystalReport/ImportLCLedgerRpt.aspx");
        }
        // to show LATR Ledger 
        public ActionResult ShowLATRLedger(ReportVModel model)
        {
            Session["ReportName"] = "LATRLedgerR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.ILCId = context.LTR.FirstOrDefault(x => x.LTRID == model.LTRID).LCID;
            param.ILCNo = context.ImportLC.FirstOrDefault(x => x.ILCId == param.ILCId).ILCNO;


            Session["LATRLedgerParam"] = param;

            string sql = string.Format("exec LATRLedger '" + model.LTRID + "',  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<LATRLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                LATRLedgerReport report = new LATRLedgerReport();
                items.Add(report);
            }
            Session["LATRLedgerReportData"] = items;
            return Redirect("/CrystalReport/LATRLedgerReportRpt.aspx");
        }
        // to show Retailer Rent Ledger Ledger 
        public ActionResult RetailerRentLedger(ReportVModel model)
        {
            Session["ReportName"] = "RetailerRentLedgerR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            //param.ILCId = context.LTR.FirstOrDefault(x => x.LTRID == model.LTRID).LCID;
            //param.ILCNo = context.ImportLC.FirstOrDefault(x => x.ILCId == param.ILCId).ILCNO;


            Session["RetailerRentLedgerParam"] = param;

            string sql = string.Format("exec spRetailerRentLedger '" + model.RetailerId + "',  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<RetailerRentLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                RetailerRentLedgerReport report = new RetailerRentLedgerReport();
                items.Add(report);
            }
            Session["RetailerRentLedgerData"] = items;
            return Redirect("/CrystalReport/RetailerRentLedgerRpt.aspx");
        }
        // to show Dealer Freezer Rent Ledger Ledger 
        public ActionResult DealerRentLedger(ReportVModel model)
        {
            Session["ReportName"] = "DealerRentLedgerR";
            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerID);
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.DID = customer.Id;
            param.CustName = customer.CustomerName;
            param.DepotName = context.BranchInformation.FirstOrDefault(x => x.Slno == customer.DepotId).BrnachName;
            //param.ILCNo = context.ImportLC.FirstOrDefault(x => x.ILCId == param.ILCId).ILCNO;


            Session["DealerRentLedgerParam"] = param;

            string sql = string.Format("exec spDealerRentLedger '" + model.CustomerID + "',  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<DealerRentLedgerReport>(sql).ToList();
            if (items.Count == 0)
            {
                DealerRentLedgerReport report = new DealerRentLedgerReport();
                items.Add(report);
            }
            Session["DealerRentLedgerData"] = items;
            return Redirect("/CrystalReport/DealerRentLedgerRpt.aspx");
        }

        // to show Account History
        public ActionResult LedgerAccountHistory(ReportVModel model)
        {
            Session["ReportName"] = "LedgerAccountHistoryR";
            //var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerID);
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            param.CostCenterID = model.CostCenterID;
            param.CostCenterName = context.BranchInformation.FirstOrDefault(x => x.Slno == model.CostCenterID).BrnachName;
            param.LedgerAccountID = model.LedgerAC;
            param.AccountName = context.ChartOfAccount.FirstOrDefault(x => x.Id == model.LedgerAC).Name;

            //param.DID = customer.Id;
            //param.CustName = customer.CustomerName;
            //param.DepotName = context.BranchInformation.FirstOrDefault(x => x.Slno == customer.DepotId).BrnachName;
            //param.ILCNo = context.ImportLC.FirstOrDefault(x => x.ILCId == param.ILCId).ILCNO;


            Session["lahistoryParam"] = param;

            string sql = string.Format("exec spAccountHistory  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "','" + model.LedgerAC + "','" + model.CostCenterID + "' ");
            var items = context.Database.SqlQuery<AccountHistoryReport>(sql).ToList();
            if (items.Count == 0)
            {
                AccountHistoryReport report = new AccountHistoryReport();
                items.Add(report);
            }
            Session["lahistoryData"] = items;
            return Redirect("/CrystalReport/LedgerAccountHistoryRpt.aspx");
        }


    }
}
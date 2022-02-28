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
    public class VoucherReportsController : Controller
    {
        BEEContext context = new BEEContext(); 
        // GET: VoucherReports
        public ActionResult VoucherReports()
        {
            return View();
        }

        public ActionResult ShowReceiveVoucherReport(ReportVModel model)
        {
            Session["ReportName"] = "ReceiveVoucherR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;        
            Session["ReceiveVoucherReportParam"] = param;

            string sql = string.Format("exec ReceiveVoucherReport  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ReceiveVoucherReport>(sql).ToList();
            if (items.Count == 0)
            {
                items = new List<ReceiveVoucherReport>();
            }
            Session["ReceiveVoucherReportData"] = items;
            return Redirect("/CrystalReport/ReceiveVoucherRpt.aspx");
        }

        public ActionResult ShowFundTransferVoucherReport(ReportVModel model)
        {
            Session["ReportName"] = "FundTransferVoucherR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["FundTransferVoucherParam"] = param;

            string sql = string.Format("exec FundTransferVoucherReport  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<FundTransferVoucherReport>(sql).ToList();
            if (items.Count == 0)
            {
                items = new List<FundTransferVoucherReport>();
            }
            Session["FundTransferVoucherReportData"] = items;
            return Redirect("/CrystalReport/FundTransferVoucherRpt.aspx");
        }
        public ActionResult ShowPaymentVoucherReport(ReportVModel model)
        {
            Session["ReportName"] = "PaymentVoucherR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["PaymentVoucherParam"] = param;

            string sql = string.Format("exec PaymentVoucherReport  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<PaymentVoucherReport>(sql).ToList();
            if (items.Count == 0)
            {
                items = new List<PaymentVoucherReport>();
            }
            Session["PaymentVoucherReportData"] = items;
            return Redirect("/CrystalReport/PaymentVoucherRpt.aspx");
        }
        public ActionResult ShowAllSPV(ReportVModel model)
        {
            Session["ReportName"] = "AllSupplierPaymentVouchersR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["AllSupplierPaymentVouchersParam"] = param;

            string sql = string.Format("exec AllSupplierPaymentVouchers  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AllSupplierPaymentVouchersReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllSupplierPaymentVouchersReport report = new AllSupplierPaymentVouchersReport();
                items.Add(report);
            }
            Session["AllSupplierPaymentVouchersReportData"] = items;
            return Redirect("/CrystalReport/AllSupplierPaymentVouchersRpt.aspx");
        }
        public ActionResult ShowSupplierWisePaymentSummary(ReportVModel model)
        {
            Session["ReportName"] = "SupplierWisePaymentSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["SupplierWisePaymentSummaryParam"] = param;

            string sql = string.Format("exec SupplierWisePaymentSummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<SupplierWisePaymentSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                SupplierWisePaymentSummaryReport report = new SupplierWisePaymentSummaryReport();
                items.Add(report);
            }
            Session["SupplierWisePaymentSummaryReportData"] = items;
            return Redirect("/CrystalReport/SupplierWisePaymentSummaryRpt.aspx");
        }

        public ActionResult ShowAccountWisePaymentSummary(ReportVModel model)
        {
            Session["ReportName"] = "AccountWisePaymentSummaryR";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;
            Session["AccountWisePaymentSummaryParam"] = param;

            string sql = string.Format("exec AccountWisePaymentSummary  '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AccountWisePaymentSummaryReport>(sql).ToList();
            if (items.Count == 0)
            {
                AccountWisePaymentSummaryReport report = new AccountWisePaymentSummaryReport();
                items.Add(report);
            }
            Session["AccountWisePaymentSummaryReportData"] = items;
            return Redirect("/CrystalReport/AccountWisePaymentSummaryRpt.aspx");
        }
        
    }
}
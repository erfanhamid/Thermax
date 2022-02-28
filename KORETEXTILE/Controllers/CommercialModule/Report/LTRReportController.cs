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

namespace BEEERP.Controllers.CommercialModule.Report
{
    [ShowNotification]
    public class LTRReportController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LTRReport
        public ActionResult LTRReport()
        {
            ViewBag.LATR = LoadComboBox.LoadALLLATR();
            return View();
        }

        public ActionResult GetLTRNoById(int Id)
        {
            var ltr = context.LTR.FirstOrDefault(x => x.LTRID == Id);
            if (ltr != null)
            {
                return Json(new { lTRNo = ltr.LTRNO }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetLTRIDByNo(string No)
        {
            var ltr = context.LTR.FirstOrDefault(x => x.LTRNO == No);
            if (ltr != null)
            {
                return Json(new { lTRId = ltr.LTRID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

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

        public ActionResult BankWiseLATRABS(ReportVModel model)
        {
            Session["ReportName"] = "BankWiseLATR";
            ReportParmPersister param = new ReportParmPersister();
            param.AsOnDate = model.AsOnDate;
          

            Session["BankWiseLatr"] = param;

            string sql = string.Format("exec BankWiseLATRSummary '"  + DateTimeFormat.ConvertToDDMMYYYY(model.AsOnDate) +  "'");
            var items = context.Database.SqlQuery<BankWiseLATRABSReport>(sql).ToList();
            if (items.Count == 0)
            {
                BankWiseLATRABSReport report = new BankWiseLATRABSReport();
                items.Add(report);
            }
            Session["BankWiseLATRData"] = items;
            return Redirect("/CrystalReport/BankWiseLATRABSummaryRpt.aspx");
        }
        public ActionResult LATRPayment(ReportVModel model)
        {
            Session["ReportName"] = "LATRPayment";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To; 


            Session["LATRPaymentParam"] = param;

            string sql = string.Format("exec ShowAllLATRP '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<LATRPaymentReport>(sql).ToList();
            if (items.Count == 0)
            {
                LATRPaymentReport report = new LATRPaymentReport();
                items.Add(report);
            }
            Session["LATRPaymentData"] = items;
            return Redirect("/CrystalReport/LATRPaymentRpt.aspx");
        }
        public ActionResult LATRNew(ReportVModel model)
        {
            Session["ReportName"] = "LATRNew";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;


            Session["LATRNewParam"] = param;

            string sql = string.Format("exec ShowAllNewLATR '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AllNewLATRReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllNewLATRReport report = new AllNewLATRReport();
                items.Add(report);
            }
            Session["LATRNewData"] = items;
            return Redirect("/CrystalReport/AllNewLATRRpt.aspx");
        }
        public ActionResult AllLATRA(ReportVModel model)
        {
            Session["ReportName"] = "ALLLATRA";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;


            Session["LATRAParam"] = param;

            string sql = string.Format("exec ShowAllLATRA '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<ALLLATRAReport>(sql).ToList();
            if (items.Count == 0)
            {
                ALLLATRAReport report = new ALLLATRAReport();
                items.Add(report);
            }
            Session["LATRAData"] = items;
            return Redirect("/CrystalReport/AllLATRARpt.aspx");
        }
        public ActionResult AllLATROB(ReportVModel model)
        {
            Session["ReportName"] = "ALLLATROB";
            ReportParmPersister param = new ReportParmPersister();
            param.FromDate = model.From;
            param.ToDate = model.To;


            Session["LATROBParam"] = param;

            string sql = string.Format("exec ShowAllLATROB '" + DateTimeFormat.ConvertToDDMMYYYY(model.From) + "','" + DateTimeFormat.ConvertToDDMMYYYY(model.To) + "'");
            var items = context.Database.SqlQuery<AllLATROBReport>(sql).ToList();
            if (items.Count == 0)
            {
                AllLATROBReport report = new AllLATROBReport();
                items.Add(report);
            }
            Session["LATROBData"] = items;
            return Redirect("/CrystalReport/AllLATROBRpt.aspx");
        }
    }
}
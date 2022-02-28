using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Procurement;
using System.Data.Entity;
using BEEERP.Models.Bridge;
using BEEERP.Models.Log;
using System.IO;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class PRSController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: PRS
        public ActionResult PRS()
        {
            ViewBag.Supplier = LoadComboBox.LoadAllSupplier();
            ViewBag.Account = LoadComboBox.LoadReceiveAcc();

            return View();
        }

        public ActionResult SavePRS(PRS pRS)
        {
            string id = "";
            string yearLastTwoPart = pRS.Date.Year.ToString().Substring(2, 2).ToString();
            var noOfPRS = context.PRs.Count();
            if (noOfPRS > 0)
            {
                var lastPRS = context.PRs.ToList().LastOrDefault(x => x.PRSNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                //id = yearLastTwoPart + (Convert.ToInt32(lastPRS.PRSNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                if (lastPRS == null)
                {
                    id = yearLastTwoPart + "00001";
                }
                else
                {
                    id = yearLastTwoPart + (Convert.ToInt32(lastPRS.PRSNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                id = yearLastTwoPart + "00001";
            }
            pRS.PRSNo = Convert.ToInt32(id);
            pRS.Date= pRS.Date.Add(DateTime.Now.TimeOfDay);            
            try
            {
                context.PRs.Add(pRS);
                //Transection.
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                AccountModuleBridge.InsertUpdateFromPaymentRefundSupplier(ref context, pRS);
                SupplierBalanceCalculationBridge.InsertUpdateFromPaymentRefundFromSupplier(ref context, pRS);
                UserLog.SaveUserLog(ref context, new UserLog(pRS.PRSNo.ToString(), DateTime.Now, "PRS", ScreenAction.Save));
                context.SaveChanges();
                return Json(pRS, JsonRequestBehavior.AllowGet );
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPRSById(int prs)
        {
            var pRS = context.PRs.FirstOrDefault(x => x.PRSNo == prs);
            if (pRS != null)
            {
                return Json(pRS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdatePRS(PRS pRS)
        {        
            var prsExist = context.PRs.AsNoTracking().FirstOrDefault(x => x.PRSNo == pRS.PRSNo);
            if (prsExist != null)
            {
                pRS.Date = pRS.Date.Add(DateTime.Now.TimeOfDay);
                AccountModuleBridge.InsertUpdateFromPaymentRefundSupplier(ref context, pRS);
                SupplierBalanceCalculationBridge.InsertUpdateFromPaymentRefundFromSupplier(ref context, pRS);
                context.Entry<PRS>(pRS).State = EntityState.Modified;
                UserLog.SaveUserLog(ref context, new UserLog(pRS.PRSNo.ToString(), DateTime.Now, "PRFS", ScreenAction.Update));
                context.SaveChanges();
                return Json(pRS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeletePRSById(int id)
        {
            var prsExist = context.PRs.FirstOrDefault(x => x.PRSNo == id);
            if (prsExist != null)
            {
                context.PRs.Remove(prsExist);
                AccountModuleBridge.DeleteFromPaymentRefundSupplier(ref context, id);
                SupplierBalanceCalculationBridge.DeleteFromPaymentRefundFromSupplier(ref context, prsExist);
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "PRS", ScreenAction.Delete));
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PRSPreview(int prs)
        {
            

            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "PRFS" && p.TrnasId == prs.ToString()
                            select c).ToList().FirstOrDefault();
            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }

            string sql = string.Format("exec spPrintPRFS " + prs + "");
            var items = context.Database.SqlQuery<PRSPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                PRSPreviewReport report = new PRSPreviewReport();
                items.Add(report);
            }
            PRSPreviewReportR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public PRSPreviewReportR ShowReport(ReportParmPersister persister, List<PRSPreviewReport> data)
        {


            PRSPreviewReportR previewR = new PRSPreviewReportR();

            previewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PRSPreviewReportR.rpt");
            previewR.Load(APPPATH);
            previewR.SetDataSource(data);
            return previewR;
        }

    }
}
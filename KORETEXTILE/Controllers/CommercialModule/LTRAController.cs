using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Log;
using System.IO;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.Account
{
    //[Permission]
    //[AccountingModule]
    [ShowNotification]
    public class LTRAController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LTRA
        public ActionResult LTRA()
        {
            ViewBag.LTR = LoadComboBox.LoadLTR(null);
            ViewBag.LTRID = LoadComboBox.LoadALLLATRID();
            ViewBag.LTRNo = LoadComboBox.LoadALLLATRNO();
            //ViewBag.Type = LoadComboBox.LoadIncreseOrDecrese();
            ViewBag.Account = LoadComboBox.LoadAllAccount();
            ViewBag.ILC = LoadComboBox.LoadAllILC();
            return View();
        }

        public ActionResult LoadLCInfo(int lcId)
        {
            var lcInfo = (from lc in context.ImportLC
                          join s in context.Supplier on lc.SupplierId equals s.Id
                          where lc.ILCId == lcId
                          select new { ILCNo = lc.ILCNO, AitILCNo = lc.IALCNO, Supplier = s.SupplierName }).FirstOrDefault();
            var BankAc = (from lc in context.ImportLC
                          join p in context.ImportProformaInvoices on lc.IPIId equals p.IPIId
                          join c in context.ChartOfAccount on p.IssuingBankId equals c.Id
                          where lc.ILCId == lcId
                          select c.Name).FirstOrDefault();
            var LTR = LoadComboBox.LoadLTR(lcId);
            if (lcInfo != null)
            {
                return Json(new { lcInfo, LTR, BankAc }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult LoadLTRNoByID(int LTRID)
        {
            var LTRNo = context.LTR.FirstOrDefault(x => x.LTRID == LTRID).LTRNO;
            return Json(new { ltrno = LTRNo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadLTRInfo(int lcId, string lTRNo, string date)
        {
            var ltrInfo = (from l in context.LTR
                           join c in context.ChartOfAccount on l.ACCID equals c.Id
                           where l.LCID == lcId && l.LTRNO == lTRNo
                           select new { LTRID = l.LTRID, GL = c.Name }).FirstOrDefault();

            if (ltrInfo != null)
            {
                DateTime dateT = DateTime.Parse(date).Date;
                //decimal ob = context.LTRBalCalculation.Where(x => x.LTRID == ltrInfo.LTRID && x.Date <= dateT).ToList().Sum(y => y.Amount);
                decimal ob = context.LTRBalCalculations.Where(x => x.LTRID == ltrInfo.LTRID && x.Date <= dateT).ToList().Sum(y => y.Amount);
                return Json(new { ltrInfo, ob }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LoadLTRNo(int id)
        {
            if (id != 0 && id.ToString() != "")
            {

                var ltr = context.LTR.Where(x => x.LTRID == id).ToList();
                if (ltr.Count() > 0)
                {
                    var data = ltr.FirstOrDefault().LTRNO;

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        //public ActionResult GetAccountById(string id)
        //{
        //    if (id == "Increase")
        //    {
        //        return Json(LoadComboBox.LoadAllAccount(), JsonRequestBehavior.AllowGet);
        //    }
        //    else if(id == "Decrease")
        //    {
        //        return Json(LoadComboBox.LoadAllAccountWithoutBankAndCash(), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult SaveLtra(LTRA ltra)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "LATR Adjustment").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (ltra.Date > IsExpired.SELDate)
            {

                using (context)
                {
                    var IsExist = context.LTR.Where(x => x.LTRID == ltra.LTRID).ToList();
                    if (IsExist.Count() > 0)
                    {


                        string ltraNo = "";
                        string yearLastTwoPart = ltra.Date.Year.ToString().Substring(2, 2).ToString();
                        var noOfInvoice = context.LTRA.Count();
                        if (noOfInvoice > 0)
                        {
                            var lastInvoice = context.LTRA.ToList().LastOrDefault(x => x.LTRANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                            if (lastInvoice == null)
                            {
                                ltraNo = yearLastTwoPart + "00001";
                            }
                            else
                            {
                                ltraNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.LTRANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                            }

                        }
                        else
                        {
                            ltraNo = yearLastTwoPart + "00001";
                        }

                        ltra.LTRANo = Convert.ToInt32(ltraNo);
                        ltra.Date = ltra.Date.Add(DateTime.Now.TimeOfDay);
                        ltra.Type = "Increase";
                        context.LTRA.Add(ltra);

                        UserLog.SaveUserLog(ref context, new UserLog(ltra.LTRANo.ToString(), ltra.Date, "LATRA", ScreenAction.Save));
                        //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                        AccountModuleBridge.InsertFromLTRA(ref context, ltra);
                        LTRBalCalulationBridge.InsertFromLTRA(ref context, ltra);

                        context.SaveChanges();
                        return Json(new { ltra }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetLTRAById(int id)
        {
            var ltraItem = context.LTRA.FirstOrDefault(x => x.LTRANo == id);

            if (ltraItem != null)
            {
                string ltrNo = "";
                try
                {
                    ltrNo = context.LTR.FirstOrDefault(x => x.LTRID == ltraItem.LTRID && x.LCID == ltraItem.ILCId).LTRNO;
                }
                catch
                {

                }
                return Json(new { ltraItem, ltrNo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateLtra(LTRA ltra)
        {
            var IsExist = context.LTR.Where(x => x.LTRID == ltra.LTRID).ToList();
            if (IsExist.Count() > 0)
            {

                var ltraExist = context.LTRA.FirstOrDefault(x => x.LTRANo == ltra.LTRANo);
                if (ltraExist != null)
                {
                    context.LTRA.Remove(ltraExist);
                    ltra.Date = ltra.Date.Add(DateTime.Now.TimeOfDay);
                    ltra.Type = "Increase";
                    context.LTRA.Add(ltra);
                    UserLog.SaveUserLog(ref context, new UserLog(ltra.LTRANo.ToString(), ltra.Date, "LATRA", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                    LTRBalCalulationBridge.InsertFromLTRA(ref context, ltra);
                    AccountModuleBridge.InsertFromLTRA(ref context, ltra);
                    context.SaveChanges();
                    return Json(new { ltra }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteLtraByid(int id)
        {
            var ltraExist = context.LTRA.FirstOrDefault(x => x.LTRANo == id);
            if (ltraExist != null)
            {
                context.LTRA.Remove(ltraExist);

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), ltraExist.Date, "LATRA", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsiaExist, new List<DSIALineItem>());
                LTRBalCalulationBridge.DeleteFromLTRA(ref context, ltraExist);
                AccountModuleBridge.DeleteFromLTRA(ref context, ltraExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LTRAPrintPreview(int LTRANo)
        {
            Session["ReportName"] = "EmployeeAiTBalanceLedger";

            ReportParmPersister param = new ReportParmPersister();
            Session["LTRAPreview"] = param;

            string sql = string.Format("exec LTRAPreview '" + LTRANo + "' ");
            var items = context.Database.SqlQuery<LTRA>(sql).ToList();
            if (items.Count() == 0)
            {
                LTRA report = new LTRA();
                items.Add(report);
            }

            LTRAPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public LTRAPreviewR ShowReport(ReportParmPersister persister, List<LTRA> data)
        {
            LTRAPreviewR lTRAPreviewR = new LTRAPreviewR();

            lTRAPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\IDTSamplePreviewR.rpt");
            lTRAPreviewR.Load(APPPATH);
            lTRAPreviewR.SetDataSource(data);

            lTRAPreviewR.SetParameterValue("compName", persister.CompName);
            lTRAPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            lTRAPreviewR.SetParameterValue("compContact", persister.CompContact);
            lTRAPreviewR.SetParameterValue("compFax", persister.Fax);
            lTRAPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            lTRAPreviewR.SetParameterValue("factContact", persister.FactContact);
            return lTRAPreviewR;
        }
    }
}
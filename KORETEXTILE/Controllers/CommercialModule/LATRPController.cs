using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommercialModule;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.CommercialModule
{
    [ShowNotification]
    public class LATRPController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LATRP
        public ActionResult LATRP()
        {
            ViewBag.LTR = LoadComboBox.LoadLTR(null);
            ViewBag.LTRID = LoadComboBox.LoadALLLATRID();
            ViewBag.LTRNo = LoadComboBox.LoadALLLATRNO();
            ViewBag.ILC = LoadComboBox.LoadAllILC();
            //ViewBag.Type = LoadComboBox.LoadIncreseOrDecrese();
            //ViewBag.Account = LoadComboBox.LoadAllAccountWithoutBankAndCash();
            ViewBag.Account = LoadComboBox.LoadAllCashBankAccount();
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

        public ActionResult LoadLTRInfo( string lTRNo, string date)
        {
            var ltrInfo = (from l in context.LTR
                           join c in context.ChartOfAccount on l.ACCID equals c.Id
                           where  l.LTRNO == lTRNo
                           select new { LTRID = l.LTRID, GL = c.Name,LCID = l.LCID }).FirstOrDefault();

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
        public ActionResult LoadLTRNoByID(int LTRID)
        {
            var LTRNo = context.LTR.FirstOrDefault(x => x.LTRID == LTRID).LTRNO;
            return Json(new { ltrno = LTRNo }, JsonRequestBehavior.AllowGet);
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

        public ActionResult SaveLtrp(LATRP ltra)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "LATR Payment").ScreenID;
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
                        var noOfInvoice = context.LATRP.Count();
                        if (noOfInvoice > 0)
                        {
                            var lastInvoice = context.LATRP.ToList().LastOrDefault(x => x.LTRPNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                            if (lastInvoice == null)
                            {
                                ltraNo = yearLastTwoPart + "00001";
                            }
                            else
                            {
                                ltraNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.LTRPNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                            }

                        }
                        else
                        {
                            ltraNo = yearLastTwoPart + "00001";
                        }

                        ltra.LTRPNo = Convert.ToInt32(ltraNo);
                        ltra.Date = ltra.Date.Add(DateTime.Now.TimeOfDay);

                        context.LATRP.Add(ltra);

                        UserLog.SaveUserLog(ref context, new UserLog(ltra.LTRPNo.ToString(), ltra.Date, "LATRP", ScreenAction.Save));
                        //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                        AccountModuleBridge.InsertFromLTRP(ref context, ltra);
                        LTRBalCalulationBridge.InsertFromLTRP(ref context, ltra);

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
            
            var ltraItem = context.LATRP.FirstOrDefault(x => x.LTRPNo == id);

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

        public ActionResult UpdateLtra(LATRP ltra)
        {
            var IsExist = context.LTR.Where(x => x.LTRID == ltra.LTRID).ToList();
            if (IsExist.Count() > 0)
            {

                var ltraExist = context.LATRP.FirstOrDefault(x => x.LTRPNo == ltra.LTRPNo);
                if (ltraExist != null)
                {
                    context.LATRP.Remove(ltraExist);
                    ltra.Date = ltra.Date.Add(DateTime.Now.TimeOfDay);
                   
                    context.LATRP.Add(ltra);
                    UserLog.SaveUserLog(ref context, new UserLog(ltra.LTRPNo.ToString(), ltra.Date, "LATRP", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                    LTRBalCalulationBridge.InsertFromLTRP(ref context, ltra);
                    AccountModuleBridge.InsertFromLTRP(ref context, ltra);
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
            var ltraExist = context.LATRP.FirstOrDefault(x => x.LTRPNo == id);
            if (ltraExist != null)
            {
                context.LATRP.Remove(ltraExist);

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), ltraExist.Date, "LATRP", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsiaExist, new List<DSIALineItem>());
                LTRBalCalulationBridge.DeleteFromLTRP(ref context, ltraExist);
                AccountModuleBridge.DeleteFromLTRP(ref context, ltraExist);
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
            var items = context.Database.SqlQuery<LATRP>(sql).ToList();
            if (items.Count() == 0)
            {
                LATRP report = new LATRP();
                items.Add(report);
            }

            LTRAPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public LTRAPreviewR ShowReport(ReportParmPersister persister, List<LATRP> data)
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
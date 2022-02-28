using BEEERP.Models.AccountModule;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class SADADController : Controller
    {
        BEEContext context = new BEEContext();
        // old controller name SPVAAController
        // GET: SADAD
        public ActionResult SADAD()
        {
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.AdjustableSPVNo = LoadComboBox.LoadAdjustSPVNo(null);
            //ViewBag.PaymentAccount = LoadComboBox.LoadReceiveAcc();
            //ViewBag.GSB = LoadComboBox.LoadGSBNo(null);
            //ViewBag.GPB = LoadComboBox.LoadGPBNo(null);
            //ViewBag.GBE = LoadComboBox.LoadGBENo(null);
            ViewBag.gbpgsbfpb = LoadComboBox.LoadGBPGSBFPBB();
            ViewBag.billno = LoadComboBox.LoadGBENo(null);
            return View();
        }
        public ActionResult BillTypeSupplierId(int supplierId)
        {
            var gbpgsbfpb = LoadComboBox.LoadGBPGSBFPBB();
            var gsb = LoadComboBox.LoadGSBNo(supplierId);
            var gpb = LoadComboBox.LoadGPBNo(supplierId);
            var fpb = LoadComboBox.LoadFPBNo(supplierId);
            var ilcb = LoadComboBox.LoadILCBNo(supplierId);
            return Json(new { gsb, gpb, fpb, ilcb, gbpgsbfpb }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAdvanceDetailsBySupplierId(int supplierId)
        {
            var advanceid = LoadComboBox.LoadAdvanceID(supplierId);
            return Json(new { advanceid = advanceid }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAdvanceAmount(int id)
        {
            var paid = context.PayBillLines.Where(x => x.PBID == id).ToList().Select(x => x.PaymentAmount).DefaultIfEmpty(0).Sum();
            var advanceamt = context.PayBillInfo.FirstOrDefault(x => x.PBID == id).PaidAmt;
            var total = advanceamt - paid;
            return Json(new { amount = total, advanceamt }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult saveSAPA(SPAdvanceAdjustment sPAdvanceAdjustment, List<SPAdvanceAmountLineItem> addedItems)
        {
            try
            {
                var sPA = context.SPAdvanceAdjustments.FirstOrDefault(x => x.SPAANo == sPAdvanceAdjustment.SPAANo);
                if (sPA != null)
                {
                    var sPALineItem = context.SPAdvanceAmountLineItems.Where(x => x.SPAANo == sPA.SPAANo).ToList();
                    context.SPAdvanceAdjustments.Remove(sPA);
                    foreach (var item in sPALineItem)
                    {
                        context.SPAdvanceAmountLineItems.Remove(item);
                    }
                    sPA.update = true;
                }

                string id = "";
                string yearLastTwoPart = sPAdvanceAdjustment.TDate.Year.ToString().Substring(2, 2).ToString();
                var noOfSPV = context.PayBillInfo.Count();
                if (noOfSPV > 0)
                {
                    var lastSPV = context.SPAdvanceAdjustments.ToList().LastOrDefault(x => x.SPAANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastSPV == null)
                    {
                        id = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        id = yearLastTwoPart + (Convert.ToInt32(lastSPV.SPAANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    id = yearLastTwoPart + "00001";
                }
                sPAdvanceAdjustment.SPAANo = Convert.ToInt32(id);
                sPAdvanceAdjustment.TDate = sPAdvanceAdjustment.TDate.Add(DateTime.Now.TimeOfDay);
                var spv = context.PayBillInfo.FirstOrDefault(x => x.PBID == sPAdvanceAdjustment.SPVNo);
                sPAdvanceAdjustment.SPVNo = spv.PBID;
                context.SPAdvanceAdjustments.Add(sPAdvanceAdjustment);
                foreach (var item in addedItems)
                {
                    item.SPAANo = sPAdvanceAdjustment.SPAANo;
                    context.SPAdvanceAmountLineItems.Add(item);
                    PayBillLine pbaline = new PayBillLine();
                    pbaline.PBID = spv.PBID;
                    pbaline.BillType = item.BillType;
                    pbaline.BillNo = item.BillNo;
                    pbaline.Amount = item.AdjustedAmount;
                    pbaline.PaymentAmount = item.AdjustedAmount;
                    pbaline.Balance = 0;
                    pbaline.Remaining = 0;
                    pbaline.SPAANo = sPAdvanceAdjustment.SPAANo;
                    context.PayBillLines.Add(pbaline);

                }
                UserLog.SaveUserLog(ref context, new UserLog(sPAdvanceAdjustment.SPAANo.ToString(), DateTime.Now, "SADA", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { id = sPAdvanceAdjustment.SPAANo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateSAPA(SPAdvanceAdjustment sPAdvanceAdjustment, List<SPAdvanceAmountLineItem> addedItems)
        {
            try
            {
                var sPA = context.SPAdvanceAdjustments.FirstOrDefault(x => x.SPAANo == sPAdvanceAdjustment.SPAANo);
                if (sPA != null)
                {
                    var sPALineItem = context.SPAdvanceAmountLineItems.Where(x => x.SPAANo == sPA.SPAANo).ToList();
                    context.SPAdvanceAdjustments.Remove(sPA);
                    foreach (var item in sPALineItem)
                    {
                        context.SPAdvanceAmountLineItems.Remove(item);
                    }

                    var pbaline = context.PayBillLines.Where(x => x.SPAANo == sPAdvanceAdjustment.SPAANo && x.PBID == sPAdvanceAdjustment.SPVNo).ToList();
                    foreach (var item in pbaline)
                    {
                        context.PayBillLines.Remove(item);
                    }
                }

                sPAdvanceAdjustment.TDate = sPAdvanceAdjustment.TDate.Add(DateTime.Now.TimeOfDay);
                var spv = context.PayBillInfo.FirstOrDefault(x => x.PBID == sPAdvanceAdjustment.SPVNo);
                sPAdvanceAdjustment.SPVNo = spv.PBID;
                context.SPAdvanceAdjustments.Add(sPAdvanceAdjustment);
                foreach (var item in addedItems)
                {
                    item.SPAANo = sPAdvanceAdjustment.SPAANo;
                    context.SPAdvanceAmountLineItems.Add(item);
                    PayBillLine pbaline = new PayBillLine();
                    pbaline.PBID = spv.PBID;
                    pbaline.BillType = item.BillType;
                    pbaline.BillNo = item.BillNo;
                    pbaline.Amount = item.AdjustedAmount;
                    pbaline.PaymentAmount = item.AdjustedAmount;
                    pbaline.Balance = 0;
                    pbaline.Remaining = 0;
                    pbaline.SPAANo = sPAdvanceAdjustment.SPAANo;
                    context.PayBillLines.Add(pbaline);

                }
                UserLog.SaveUserLog(ref context, new UserLog(sPAdvanceAdjustment.SPAANo.ToString(), DateTime.Now, "SADA", ScreenAction.Update));
                context.SaveChanges();
                return Json(new { id = sPAdvanceAdjustment.SPAANo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSPAById(int id)
        {
            var sPa = context.SPAdvanceAdjustments.FirstOrDefault(x => x.SPAANo == id);
            if (sPa != null)
            {
                var paybillinfo = context.PayBillInfo.FirstOrDefault(x => x.PBID == sPa.SPVNo);
                var sPaLine = context.SPAdvanceAmountLineItems.Where(x => x.SPAANo == id).ToList();
                var payBillLine = context.PayBillLines.Where(x => x.PBID == sPa.SPVNo && x.SPAANo == sPa.SPAANo).ToList();
                foreach (var item in sPaLine)
                {
                    if (item.BillType == "GSB")
                    {
                        item.Remaining = LoadComboBox.GSBDueAmountByGbeNo(item.BillNo);
                    }
                    else if (item.BillType == "FPB")
                    {
                        item.Remaining = LoadComboBox.FPBDueAmountByGbeNo(item.BillNo);
                    }
                    else if (item.BillType == "GPB")

                    {
                        item.Remaining = LoadComboBox.GPBDueAmountByGpbNo(item.BillNo);
                    }
                    else
                    {

                    }

                }
                return Json(new { sPa, sPaLine, paybillinfo, payBillLine }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteSPAById(int id)
        {
            try
            {
                var sPA = context.SPAdvanceAdjustments.FirstOrDefault(x => x.SPAANo == id);
                if (sPA != null)
                {
                    var sPALineItem = context.SPAdvanceAmountLineItems.Where(x => x.SPAANo == sPA.SPAANo).ToList();
                    context.SPAdvanceAdjustments.Remove(sPA);
                    foreach (var item in sPALineItem)
                    {
                        context.SPAdvanceAmountLineItems.Remove(item);
                    }
                    //var pbaline = context.PayBillLines.Where(x => x.SPAANo == id && x.PBID == id).ToList();
                    var pbaline = context.PayBillLines.Where(x => x.SPAANo == id).ToList();
                    foreach (var item in pbaline)
                    {
                        context.PayBillLines.Remove(item);
                    }
                }

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "SAAD", ScreenAction.Delete));
                context.SaveChanges();
                return Json(new { message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
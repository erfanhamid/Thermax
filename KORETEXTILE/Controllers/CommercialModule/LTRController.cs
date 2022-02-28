using BEEERP.Models.Bridge;
using BEEERP.Models.CommercialModule;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.CommercialModule
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class LTRController : Controller
    {

        BEEContext context = new BEEContext();
        // GET: LTR
        public ActionResult LTREntry()
        {

            ViewBag.ACC = LoadComboBox.LoadAccounts();
            ViewBag.ILC = LoadComboBox.LoadAllILC();
            return View();
        }

        public ActionResult SaveLTR(LTR LTR)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "New LATR").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (LTR.Date > IsExpired.SELDate)
            {

                var IsExist = context.LTR.Where(x => x.LTRNO == LTR.LTRNO).ToList();
                if (IsExist.Count() > 0)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string LTRID = "";
                    string yearLastTwoPart = LTR.Date.Year.ToString().Substring(2, 2).ToString();
                    var noOfLTRID = context.LTR.Count();

                    if (noOfLTRID > 0)
                    {
                        var lastLTR = context.LTR.ToList().LastOrDefault(x => x.LTRID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastLTR == null)
                        {
                            LTRID = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            LTRID = yearLastTwoPart + (Convert.ToInt32(lastLTR.LTRID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }

                    }
                    else
                    {
                        LTRID = yearLastTwoPart + "00001";
                    }

                    LTR.LTRID = Convert.ToInt32(LTRID);
                    LTR.DocType = "LATR";
                    var data = (from lc in context.ImportLC
                                join pi in context.ImportProformaInvoices on lc.IPIId equals pi.IPIId
                                select new { bankid = pi.IssuingBankId, lcid = lc.ILCId }).ToList();

                    LTR.ACCID = data.FirstOrDefault(x => x.lcid == LTR.LCID).bankid;

                    try
                    {
                        context.LTR.Add(LTR);
                        UserLog.SaveUserLog(ref context, new UserLog(LTR.LTRID.ToString(), LTR.Date, "LATR", ScreenAction.Save));
                        LTRBalCalulationBridge.InsertUpdateFromLTR(ref context, LTR);
                        AccountModuleBridge.InsertFromLTR(ref context, LTR);
                        LcBalanceCalculationBridge.InsertFromLTR(ref context, LTR);
                        context.SaveChanges();
                        return Json(LTR.LTRID, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateLTR(LTR LTR)
        {
            
            var IsExist = context.LTR.FirstOrDefault(x => x.LTRID == LTR.LTRID);
            if (IsExist != null)
            {
                context.LTR.Remove(IsExist);
            }

            var ltrno = context.LTR.Where(x => x.LTRNO == LTR.LTRNO).ToList();
            if (ltrno.Count != 0)
            {
                try
                {
                    LTR.DocType = "LATR";
                    var data = (from lc in context.ImportLC
                                join pi in context.ImportProformaInvoices on lc.IPIId equals pi.IPIId
                                select new { bankid = pi.IssuingBankId, lcid = lc.ILCId }).ToList();

                    LTR.ACCID = data.FirstOrDefault(x => x.lcid == LTR.LCID).bankid;
                    context.LTR.Add(LTR);
                    LTRBalCalulationBridge.InsertUpdateFromLTR(ref context, LTR);
                    LcBalanceCalculationBridge.InsertFromLTR(ref context, LTR);
                    AccountModuleBridge.InsertFromLTR(ref context, LTR);
                    UserLog.SaveUserLog(ref context, new UserLog(LTR.LTRID.ToString(), LTR.Date, "LATR", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(LTR.LTRID, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }

            else
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
            
            
        }


        public ActionResult DeleteLTR(LTR LTR)
        {
            var LTRExist = context.LTR.FirstOrDefault(x => x.LTRID == LTR.LTRID);
            if (LTRExist != null)
            {
                context.LTR.Remove(LTRExist);
                //var sifdFgLineItem = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList();
              
                UserLog.SaveUserLog(ref context, new UserLog(LTR.LTRID.ToString(), LTR.Date, "LATR", ScreenAction.Delete));
                LTRBalCalulationBridge.DeleteFromLTRBal(ref context, LTR);
                AccountModuleBridge.DeleteFromLTR(ref context, LTR);
                LcBalanceCalculationBridge.DeleteFromLTR(ref context, LTR);
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLTRByID(int id)
        {
            if(id > 0 || id.ToString() != "")
            {
                
                var data = context.LTR.FirstOrDefault(x => x.LTRID == id && x.DocType == "LATR");
                if(data != null)
                {
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

        public ActionResult GetLTRByNo(string id)
        {
            if (id != null && id.ToString() != "")
            {
                var data = context.LTR.FirstOrDefault(x => x.LTRNO == id && x.DocType == "LATR");
                if (data != null)
                {
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


        public ActionResult GetLCById(int id)
        {
            if (id > 0 || id.ToString() != "")
            {
                ImportLC ILC = new ImportLC();
                var data = context.ImportLC.FirstOrDefault(x => x.ILCId == id);
                if(data != null)
                {
                    ILC.ILCNO = data.ILCNO;
                    ILC.IALCNO = data.IALCNO;
                   // ILC.NameOfSupplier = context.Supplier.FirstOrDefault(s => s.Id == data.SupplierId).SupplierName;
                }
                else
                {
                    ILC.ILCNO = "";
                    ILC.IALCNO = "";
                    //ILC.NameOfSupplier = "";
                }
               

                return Json(ILC, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetLCByNo(int id)
        {
            if (id > 0 || id.ToString() != "")
            {
                ImportLC ILC = new ImportLC();
                var data = context.ImportLC.FirstOrDefault(x => x.ILCId == id);
                if(data != null)
                {
                    ILC.ILCId = data.ILCId;
                    ILC.IALCNO = data.IALCNO;
                    //ILC.NameOfSupplier = context.Supplier.FirstOrDefault(s => s.Id == data.SupplierId).SupplierName;
                }
                else
                {
                    ILC.ILCId = 0;
                    ILC.IALCNO = "";
                    //ILC.NameOfSupplier = "";
                }
                

                return Json(ILC, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
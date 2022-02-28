using BEEERP.Models.AccountModule;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers
{
    [ShowNotification]
    public class LCPCController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LCPC
        public ActionResult LCPC()
        {
            ViewBag.ILCNo = LoadComboBox.LoadALLILCIdAndNo();
            return View();
        }
        //public ActionResult GetILC(int ilcid)
        //{
        //    //var items = LoadComboBox.GetILCInfo(ilcid);
        //    //var ilcTotCost = context.ILCBalCalculation.Where(x => x.ILCID == ilcid).Sum(x => x.Amount);
        //    //var ilcTotCost = context.ILCBalCalculation.GroupBy(x => x.ILCID == ilcid).Select(n => n.Sum(m => m.Amount));
        //    //var lctype = (from lc in context.ImportProformaInvoices
        //    //            join pi in context.ImportLC on lc.IPIId equals pi.IPIId
        //    //            where pi.ILCId == ilcid
        //    //            select lc).ToList().FirstOrDefault();
        //    //var ialcNo = context.ImportLC.FirstOrDefault(x => x.ILCId == ilcid).IALCNO;
        //    //return Json(new { lcType = lctype.LCType, altlcNo = ialcNo,totcost = ilcTotCost}, JsonRequestBehavior.AllowGet);
        //    //return Json(new { lcType = lctype.LCType, ialcNo = ialcNo, totcost = ilcTotCost, lcitems = items }, JsonRequestBehavior.AllowGet);

        //    var items = LoadComboBox.GetILCInfo(ilcid);
        //    var ilcTotCost = context.ILCBalCalculation.ToList().Where(x => x.ILCID == ilcid).Sum(x => x.Amount);
        //    var lctype = (from lc in context.ImportProformaInvoices
        //                  join pi in context.ImportLC on lc.IPIId equals pi.IPIId
        //                  where pi.ILCId == ilcid
        //                  select lc).ToList().FirstOrDefault();
        //    var ialcNo = context.ImportLC.FirstOrDefault(x => x.ILCId == ilcid).IALCNO;
        //    return Json(new { lcType = lctype.LCType, ilcNo = ialcNo, totcost = ilcTotCost, lcitems = items }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult SaveLCFC(tblLCCosting lcfc, List<tblLCCostingLine> addedItems)
        {
            string lcfcNo = "";
            string yearLastTwoPart = lcfc.LCPCDate.Year.ToString().Substring(2, 2).ToString();
            var noOflcfc = context.tblLCCosting.Count();

            if (noOflcfc > 0)
            {
                var lastlcfc = context.tblLCCosting.ToList().LastOrDefault(x => x.LCPCNo.ToString().Substring(0, 2) == yearLastTwoPart);
                if (lastlcfc == null)
                {
                    lcfcNo = yearLastTwoPart + "00001";
                }
                else
                {
                    lcfcNo = yearLastTwoPart + (Convert.ToInt32(lastlcfc.LCPCNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                lcfcNo = yearLastTwoPart + "00001";
            }
            //lcfc.Type="Partial"
            lcfc.LCPCNo = Convert.ToInt32(lcfcNo);
            addedItems.ForEach(x =>
            {
                x.LCPCNo = lcfc.LCPCNo;
                context.tblLCCostingLine.Add(x);
            });
            context.tblLCCosting.Add(lcfc);

            var items = context.tblLCCostingQtBalance.Where(x => x.DocType == "LCFC" && x.DocNo == lcfc.LCPCNo && x.ILCID == lcfc.ILCID).ToList();
            foreach (var item in items)
            {
                context.tblLCCostingQtBalance.Remove(item);

            }
            foreach (var item in addedItems)
            {
                context.tblLCCostingQtBalance.Add(new tblLCCostingQtBalance()
                {
                    ILCID = lcfc.ILCID,
                    ItemID = item.ItemID,
                    Date = lcfc.LCPCDate,
                    DocType = "LCFC",
                    DocNo = item.LCPCNo,
                    Qty = item.ItemQty,
                });
            }
            AccountModuleBridge.InsertUpdateFromLCPC(ref context, lcfc);
            LcBalanceCalculationBridge.InsertUpdateFromLCPC(ref context, lcfc);
            //InventoryTransactionBridge.InsertFromRMSalesEntry(ref context, rms, addedItems);

            UserLog.SaveUserLog(ref context, new UserLog(lcfc.LCPCNo.ToString(), DateTime.Now, "LCFC", ScreenAction.Save));
            context.SaveChanges();
            return Json(new { LCPCNo = lcfc.LCPCNo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateLCFC(tblLCCosting lcfc, List<tblLCCostingLine> addedItems)
        {
            using (context)
            {
                var lcfcExist = context.tblLCCosting.FirstOrDefault(x => x.LCPCNo == lcfc.LCPCNo);
                if (lcfcExist != null)
                {
                    context.tblLCCosting.Remove(lcfcExist);

                    context.tblLCCostingLine.Where(x => x.LCPCNo == lcfc.LCPCNo).ToList().ForEach(x =>
                    {
                        context.tblLCCostingLine.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.LCPCNo = lcfc.LCPCNo;
                        context.tblLCCostingLine.Add(x);
                    });

                    lcfc.LCPCDate = lcfc.LCPCDate.Add(DateTime.Now.TimeOfDay);
                    context.tblLCCosting.Add(lcfc);

                    var isexist = context.tblLCCostingQtBalance.Where(x => x.DocType == "LCFC" && x.DocNo == lcfc.LCPCNo);
                    if (isexist != null)
                    {
                        foreach (var item in isexist)
                        {
                            context.tblLCCostingQtBalance.Remove(item);

                        }
                    }
                    else
                    {
                        foreach (var item in addedItems)
                        {
                            context.tblLCCostingQtBalance.Add(new tblLCCostingQtBalance()
                            {
                                ILCID = lcfc.ILCID,
                                ItemID = item.ItemID,
                                Date = lcfc.LCPCDate,
                                DocType = "LCFC",
                                DocNo = item.LCPCNo,
                                Qty = item.ItemQty,
                            });
                        }
                    }

                    UserLog.SaveUserLog(ref context, new UserLog(lcfc.LCPCNo.ToString(), DateTime.Now, "LCFC", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { LCPCNo = lcfc.LCPCNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult GetLCFCById(int id)
        {
            var lcfc = context.tblLCCosting.FirstOrDefault(x => x.LCPCNo == id);

            if (lcfc != null)
            {
                var lcfcLineItem = context.tblLCCostingLine.Where(x => x.LCPCNo == id).ToList().Select(x =>
                {
                    var item = context.tblLCCostingLine.FirstOrDefault(y => y.LCPCNo == x.LCPCNo && y.ItemID == x.ItemID);
                    x.LCQty = item.LCQty;
                    x.PrvQty = item.PrvQty;
                    x.ItemQty = item.ItemQty;
                    x.ItemRate = item.ItemRate;
                    x.ItemValue = item.ItemValue;
                    x.ItemName = FindObjectById.GetChartOfInventoryById(x.ItemID).Name;
                    var uom = FindObjectById.GetChartOfInventoryById(x.ItemID).UoMID;
                    x.Unit = context.UOM.FirstOrDefault(z => z.Id == uom).Name;
                    var gid = FindObjectById.GetChartOfInventoryById(x.ItemID).ParentId;
                    x.Group = FindObjectById.GetChartOfInventoryById(gid).Name;
                    return x;
                }).ToList();
                return Json(new { lcfc, lcfcLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteLCFC(tblLCCosting lcfc, List<tblLCCostingLine> addedItems)
        {
            using (context)
            {
                var lcfcExist = context.tblLCCosting.FirstOrDefault(x => x.LCPCNo == lcfc.LCPCNo);
                if (lcfcExist != null)
                {
                    context.tblLCCosting.Remove(lcfcExist);

                    context.tblLCCostingLine.Where(x => x.LCPCNo == lcfc.LCPCNo).ToList().ForEach(x =>
                    {
                        context.tblLCCostingLine.Remove(x);
                    });

                    var isexist = context.tblLCCostingQtBalance.Where(x => x.DocType == "LCPC" && x.DocNo == lcfc.LCPCNo);
                    if (isexist != null)
                    {
                        foreach (var item in isexist)
                        {
                            context.tblLCCostingQtBalance.Remove(item);

                        }
                    }

                    AccountModuleBridge.DeleteFromLCPC(ref context, lcfc);
                    LcBalanceCalculationBridge.DeleteFromLCFC(ref context, lcfc);
                    UserLog.SaveUserLog(ref context, new UserLog(lcfc.LCPCNo.ToString(), DateTime.Now, "LCFC", ScreenAction.Delete));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { LCPCNo = lcfc.LCPCNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
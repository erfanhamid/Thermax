using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.Store_FG;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.Store_FG
{
    [ShowNotification]
    [StoreFGModule]
    public class IssueRMToFGController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: IssueRMToFG
        public ActionResult IssueRMToFG()   
        {
            ViewBag.RMStore = LoadComboBox.LoadAllRMStore();
            var floor = context.Store.Where(x => x.Name.ToLower().Replace(" ", string.Empty) == "factoryrm").ToList();
            ViewBag.FloorId = floor.FirstOrDefault().Id;
            ViewBag.FgItem = LoadComboBox.LoadAllFGItem();
            ViewBag.FGStore = LoadComboBox.LoadAllFGStore();
            ViewBag.RMItem = LoadComboBox.LoadAllRMItem();
            return View();
        }

        public ActionResult SaveIRF(IssueRMToFG irf, List<IssueRMToFGLineItem> addedItems)
        {
            string iRTFNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.IssueRMToFG.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.IssueRMToFG.ToList().LastOrDefault(x => x.IRTFNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                iRTFNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.IRTFNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            }
            else
            {
                iRTFNo = yearLastTwoPart + "00001";
            }

            irf.IRTFNo = Convert.ToInt32(iRTFNo);
            irf.Date = irf.Date.Add(DateTime.Now.TimeOfDay);
            addedItems.ForEach(x =>
            {
                x.IRTFNo = irf.IRTFNo;
                context.IssueRMToFGLineItem.Add(x);
            });

            context.IssueRMToFG.Add(irf);

            UserLog.SaveUserLog(ref context, new UserLog(irf.IRTFNo.ToString(), DateTime.Now, "RmToFg", ScreenAction.Save));
            InventoryTransactionBridge.InsertFromIssueRmToFg(ref context, irf, addedItems);

            context.SaveChanges();
            return Json(new { irf }, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult GetIRTFById(int id)
        {
            var irfItem = context.IssueRMToFG.FirstOrDefault(x => x.IRTFNo == id);

            if (irfItem != null)
            {
                var irfLineItem = context.IssueRMToFGLineItem.Where(x => x.IRTFNo == id).ToList().Select(x =>
                {
                    var rmItem = FindObjectById.GetChartOfInventoryById(x.RMItemId);
                    x.RMItemName = rmItem.Name;
                    x.PacSizeIssue = rmItem.PacSize;
                    var fgItem = FindObjectById.GetChartOfInventoryById(x.FGItemId);
                    x.FGItemName = fgItem.Name;
                    x.PacSizeTo = fgItem.PacSize;
                    return x;
                }).ToList();

                return Json(new { irfItem, irfLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateIRF(IssueRMToFG irf, List<IssueRMToFGLineItem> addedItems)
        {
            var irfExist = context.IssueRMToFG.FirstOrDefault(x => x.IRTFNo == irf.IRTFNo);
            if (irfExist != null)
            {
                context.IssueRMToFG.Remove(irfExist);

                context.IssueRMToFGLineItem.Where(x => x.IRTFNo == irf.IRTFNo).ToList().ForEach(x =>
                {
                    context.IssueRMToFGLineItem.Remove(x);
                });

                addedItems.ForEach(x =>
                {
                    x.IRTFNo = irf.IRTFNo;
                    context.IssueRMToFGLineItem.Add(x);
                });

                irf.Date = irf.Date.Add(DateTime.Now.TimeOfDay);
                context.IssueRMToFG.Add(irf);
                UserLog.SaveUserLog(ref context, new UserLog(irf.IRTFNo.ToString(), DateTime.Now, "RmToFg", ScreenAction.Update));
                InventoryTransactionBridge.InsertFromIssueRmToFg(ref context, irf, addedItems);

                context.SaveChanges();
                return Json(irf, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteIRFById(int id)
        {
            var irfExist = context.IssueRMToFG.FirstOrDefault(x => x.IRTFNo == id);
            if (irfExist != null)
            {
                context.IssueRMToFG.Remove(irfExist);
                var irfLineItem = context.IssueRMToFGLineItem.Where(x => x.IRTFNo == id).ToList();
                context.IssueRMToFGLineItem.Where(x => x.IRTFNo == id).ToList().ForEach(x =>
                {
                    context.IssueRMToFGLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "RmToFg", ScreenAction.Delete));
                InventoryTransactionBridge.DeleteFromIssueRmToFg(ref context, irfExist);

                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
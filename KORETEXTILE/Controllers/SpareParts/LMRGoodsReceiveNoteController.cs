using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.SpareParts;
using BEEERP.Models.SalesModule;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class LMRGoodsReceiveNoteController : Controller
    {
   
        BEEContext context = new BEEContext();
        // GET: LMRGoodsReceiveNote
        public ActionResult LMRGoodsReceiveNote()
        {

            ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.Supplier = LoadComboBox.LoadAllSupplier();
            //ViewBag.WOCode = LoadComboBox.LoadAllWorkOrder(null);
            ViewBag.Group = LoadComboBox.LoadAllSPGroup();
            ViewBag.Item = LoadComboBox.LoadAllItem();
            ViewBag.UOM = LoadComboBox.LoadAllUOM();
            
            return View();
        }
        //public ActionResult GetWOCodeBySupplierId(int supplierId, bool isSearch)
        //{
        //    if (isSearch == true)
        //    {
        //        return Json(LoadComboBox.LoadAllWorkOrderSearch(supplierId), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(LoadComboBox.LoadAllWorkOrder(supplierId), JsonRequestBehavior.AllowGet);
        //    }


        //}

        public ActionResult GetWorderByWONo(int id)
        {
            var workOrder = context.WorkOrder.FirstOrDefault(x => x.WONo == id);

            if (workOrder != null)
            {
                var workOrderLI = context.WorkOrderLineItem.Where(x => x.WONo == id).ToList().Select(x =>
                {
                    var item = context.WorkOrderLineItem.FirstOrDefault(y => y.WONo == x.WONo && y.ItemID == x.ItemID);
                    var singleItem = FindObjectById.GetChartOfInventoryById(item.ItemID);
                    x.ItemName = singleItem.Name;
                    x.GroupName = GetItemNameById(item.GroupID).Name;
                    x.GroupID = item.GroupID;
                    x.ItemQty = item.ItemQty;
                    x.UnitCost = item.UnitCost;
                    x.UnitId = singleItem.UoMID;
                    x.Unit = GetUOMNameById(x.UnitId);
                    x.RevQty = GetTotalRcvQty(id, x.ItemID);
                    x.AvaQty = x.ItemQty - x.RevQty;
                    //x.PackSize = GetItemNameById(item.ItemID).PacSize;
                    return x;
                }).ToList();
                return Json(new { workOrder, workOrderLI }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ChartOfInventory GetItemNameById(int id)
        {
            var item = context.ChartOfInventory.ToList().FirstOrDefault(x => x.Id == id);
            return item;
        }

        public string GetUOMNameById(int id)
        {
            var item = context.UOM.ToList().FirstOrDefault(x => x.Id == id);
            return item.Name;
        }

        public decimal GetTotalRcvQty(int woNo, int Item)
        {
            //var goodRcvNote = context.GoodsReceiveNoteLineItem.ToList().Where(x => x.ItemID == Item);
            var goodRcvNote = (from G in context.GoodsReceiveNote
                               join GL in context.GoodsReceiveNoteLineItem on G.GRNNo equals GL.GRNNo
                               where (G.WONo == woNo) && (GL.ItemID == Item)
                               select GL.Qty).ToList();
            if (goodRcvNote != null)
            {
                decimal sum = goodRcvNote.Sum();
                if (sum == 0)
                {
                    return 0;
                }
                else
                {
                    return sum;
                }
            }
            else
            {
                return 0;
            }
        }


        public ActionResult GetItemAndGroupName(int item, int group)
        {
            string ItemName = GetItemNameById(item).Name;
            string GroupName = GetItemNameById(group).Name;
            //decimal CostRt = GetItemNameById(item).clmStandardCost;
            //string PackSize = GetItemNameById(item).PacSize;
            int UomId = GetItemNameById(item).UoMID;
            string Unit = GetUOMNameById(UomId);
            return Json(new { Item = ItemName, Group = GroupName, Unit = Unit, /*PackSize = PackSize*/ }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveGRN(LMRGoodsReceiveNote grn, List<LMRGoodsReceiveNoteLineItem> addedItems1)
        {
            var dataCheck = true;
            addedItems1.ForEach(x =>
            {
                if (x.ItemGrpID == 0 || x.ItemID == 0 || x.Qty == 0 || x.CostRt == 0 || x.CostVal == 0 || x.UOMID == 0)
                {
                    dataCheck = false;
                }
            });
            if (dataCheck == false)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            using (context)
            {
                string grnNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.LMRGoodsReceiveNote.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.LMRGoodsReceiveNote.ToList().LastOrDefault(x => x.GRNNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        grnNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        grnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GRNNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                    // grnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GRNNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    grnNo = yearLastTwoPart + "00001";
                }

                grn.GRNNo = Convert.ToInt32(grnNo);
                grn.GRNDate = grn.GRNDate.Add(DateTime.Now.TimeOfDay);
                addedItems1.ForEach(x =>
                {
                    x.GRNNo = grn.GRNNo;
                    context.LMRGoodsReceiveNoteLineItem.Add(x);
                });
                grn.Type = "WO";
                context.LMRGoodsReceiveNote.Add(grn);
                //AccountModuleBridge.InsertUpdateFromGRN(ref context, grn, addedItems1);
                //InventoryTransactionBridge.InsertFromGRNEntry(ref context, grn, addedItems1);
                UserLog.SaveUserLog(ref context, new UserLog(grn.GRNNo.ToString(), DateTime.Now, "GRN", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { grnNo = grn.GRNNo }, JsonRequestBehavior.AllowGet);
         

            }
        }
    }
}
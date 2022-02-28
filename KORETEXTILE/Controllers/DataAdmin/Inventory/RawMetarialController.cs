using BEEERP.Controllers.Inventory;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreRM.Transaction
{
    [ShowNotification]
    [Authorize(Roles = "DataAdmin,DataOperator,DataViewer,DataApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    [Permission]
    public class RawMetarialController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: RawMetarial
        public ActionResult RawMetarial()
        {
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.GroupOrItem = LoadComboBox.LoadGroupOrItem();
            ViewBag.UOM = LoadComboBox.LoadAllUOM();
            var items = new List<ChartOnInvList>();
            return View();
        }
        [HttpPost]
        public ActionResult SaveRawMetarial(ChartOfInventory chartOfInv)
        {

            var invenExist = context.ChartOfInventory.FirstOrDefault(x => x.ItemCode == chartOfInv.ItemCode);

            chartOfInv.Id = generate.GenerateInventoryId(context);
            chartOfInv.rootAccountType = "RM";

            if (invenExist == null || invenExist.ItemCode == null)
            {
                context.ChartOfInventory.Add(chartOfInv);
                UserLog.SaveUserLog(ref context, new UserLog(chartOfInv.Id.ToString(), DateTime.Now, "Chart of Inventory (RM)", ScreenAction.Save));
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllInventoryItems()
        {
            var items = new List<ChartOnInvList>();
            context.ChartOfInventory.ToList().Where(x => x.rootAccountType == "RM").ToList().ForEach(x =>
            {
                items.Add(new ChartOnInvList
                {
                    Id = x.Id,
                    Name = x.Name,
                    GroupName = SaleCommonInfo.GetGroupName(x.parentId),
                    ItemCode = x.ItemCode,
                    GroupOrItem = x.type,
                    UoMName = SaleCommonInfo.GetUoMName(x.UoMID),
                    //TradePrice = x.RetailPrice,
                    //CartonCapacity = x.clmCartonCapacity,
                    //StandardCost = Convert.ToDecimal(x.clmStandardCost),
                });

            });
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemById(int id)
        {
            return Json(FindObjectById.GetChartOfInventoryById(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateInventory(ChartOfInventory chartOfInv)
        {
            chartOfInv.rootAccountType = "RM";
            context.Entry<ChartOfInventory>(chartOfInv).State = EntityState.Modified;
            UserLog.SaveUserLog(ref context, new UserLog(chartOfInv.Id.ToString(), DateTime.Now, "Chart of Inventory (RM)", ScreenAction.Update));
            context.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteItemById(int id)
        {
            var isExist = context.InventoryTransaction.FirstOrDefault(x => x.CIID == id);
            if (isExist == null)
            {
                try
                {
                    var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
                    context.ChartOfInventory.Remove(item);
                    UserLog.SaveUserLog(ref context, new UserLog(item.Id.ToString(), DateTime.Now, "Chart of Inventory (RM)", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InvOpeningBalanceRM()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            if (depot == null)
            {
                ViewBag.Disabled = "true";
            }
            else
            {
                ViewBag.Disabled = "false";
            }
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.ItemGroup = LoadComboBox.LoadItemGroupRM();
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadComboBox.LoadStore(depot);
            ViewBag.DepotId = depot;
            return View();
        }
        public ActionResult CheckItemValidity(int itemId, int storeId)
        {
            var transaction = context.InventoryTransaction.FirstOrDefault(x => x.CIID == itemId && x.DocType == "RMOB" && x.StoreID == storeId);
            if (transaction == null)
            {
                var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == itemId);
                return Json(new {  Message = 1, ItemName = item.Name, ItemCode = item.ItemCode }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { StandardCost = 0, Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SaveInvRMOb(List<InventoryTransaction> transaction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ChartOfInvRMOB chartOfInvRMOB = new ChartOfInvRMOB();


                    chartOfInvRMOB.DepotID = transaction.FirstOrDefault().DepotID;
                    chartOfInvRMOB.StoreID = transaction.FirstOrDefault().StoreID;
                    var lastRow = context.ChartOfInvRMOB.ToList().LastOrDefault();
                    if (lastRow == null)
                    {
                        chartOfInvRMOB.RMOBNO = 1;
                    }
                    else
                    {
                        chartOfInvRMOB.RMOBNO = lastRow.RMOBNO + 1;
                    }

                    context.ChartOfInvRMOB.Add(chartOfInvRMOB);
                    decimal standardCost = 0;
                    transaction.ForEach(x =>
                    {
                        var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.CIID);
                        ChartOfInvRMOBLineItem chartOfInvRMOBLineItem = new ChartOfInvRMOBLineItem();

                        chartOfInvRMOBLineItem.OBQT = Convert.ToInt32(x.Qty);
                        chartOfInvRMOBLineItem.RMOBNO = chartOfInvRMOB.RMOBNO;
                        chartOfInvRMOBLineItem.STCost = x.Rate;
                        chartOfInvRMOBLineItem.Value = x.Amount;
                        chartOfInvRMOBLineItem.UnitID = item.UoMID;
                        chartOfInvRMOBLineItem.ItemID = item.Id;
                        chartOfInvRMOBLineItem.ItemGrpID = item.parentId;
                        x.DocID = chartOfInvRMOB.RMOBNO;
                        x.TRType = 1;
                        standardCost += x.Amount;
                        //x.Rate = Convert.ToDecimal(item.clmStandardCost);
                        //x.Amount = Convert.ToDecimal(x.Qty) * item.clmStandardCost;
                        x.DocType = "RMOB";
                        context.InventoryTransaction.Add(x);
                        context.ChartOfInvRMOBLineItem.Add(chartOfInvRMOBLineItem);
                    });
                    AccountModuleBridge.InsertFromRMOB(ref context, chartOfInvRMOB, standardCost);
                    UserLog.SaveUserLog(ref context, new UserLog(chartOfInvRMOB.RMOBNO.ToString(), DateTime.Now, "RMOB", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(chartOfInvRMOB.RMOBNO, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteRMItemsById(int id)
        {
            try
            {
                bool isExist = false;
                var rmItem = context.ChartOfInvRMOB.FirstOrDefault(x => x.RMOBNO == id);
                var rmLineItem = context.ChartOfInvRMOBLineItem.Where(x => x.RMOBNO == id).ToList();
                var transection = context.Transection.Where(x => x.DocType == "RMOB" && x.DocID == id).ToList();
                var invTransection = context.InventoryTransaction.Where(x => x.DocType == "RMOB" && x.CIID == id).ToList();
                rmLineItem.ForEach(x => {
                    int item = context.InventoryTransaction.Count(y => y.CIID == x.ItemID && y.DocType != "RMOB" && y.DocID == id);
                    if (item > 0)
                    {
                        isExist = true;
                    }
                });
                context.ChartOfInvRMOB.Remove(rmItem);
                if (isExist == false)
                {
                    rmLineItem.ForEach(x => {
                        context.ChartOfInvRMOBLineItem.Remove(x);
                    });
                    transection.ForEach(x => {
                        context.Transection.Remove(x);
                    });
                    invTransection.ForEach(x => {
                        context.InventoryTransaction.Remove(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(rmItem.RMOBNO.ToString(), DateTime.Now, "(RMOB)", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetInvRMOpenById(int id)
        {
            var rmItem = context.ChartOfInvRMOB.FirstOrDefault(x => x.RMOBNO == id);

            if (rmItem != null)
            {
                var rmLineItem = context.ChartOfInvRMOBLineItem.Where(x => x.RMOBNO == id).ToList().Select(x =>
                {
                    var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                    x.ItemName = item.Name;
                    x.ItemCode = item.ItemCode;
                    x.GroupId = item.parentId;
                    //x.PacSize = item.PacSize;
                    return x;
                }).ToList();
                return Json(new { rmItem, rmLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateInvRMOB(List<InventoryTransaction> transaction, int docId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ChartOfInvRMOB chartOfInvRMOB = new ChartOfInvRMOB();


                    chartOfInvRMOB.DepotID = transaction.FirstOrDefault().DepotID;
                    chartOfInvRMOB.StoreID = transaction.FirstOrDefault().StoreID;
                    chartOfInvRMOB.RMOBNO = docId;
                    var isExist = context.ChartOfInvRMOB.AsNoTracking().FirstOrDefault(x => x.RMOBNO == docId);
                    if (isExist != null)
                    {
                        context.Entry<ChartOfInvRMOB>(chartOfInvRMOB).State = EntityState.Modified;
                        
                        context.ChartOfInvRMOBLineItem.Where(x => x.RMOBNO == chartOfInvRMOB.RMOBNO).ToList().ForEach(x => context.ChartOfInvRMOBLineItem.Remove(x));
                        decimal standardCost = 0;
                        transaction.ForEach(x =>
                        {
                            var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.CIID);
                            ChartOfInvRMOBLineItem chartOfInvRMOBLineItem = new ChartOfInvRMOBLineItem();

                            chartOfInvRMOBLineItem.OBQT = Convert.ToInt32(x.Qty);
                            chartOfInvRMOBLineItem.RMOBNO = chartOfInvRMOB.RMOBNO;
                            chartOfInvRMOBLineItem.STCost = x.Rate;
                            chartOfInvRMOBLineItem.Value = x.Amount;
                            chartOfInvRMOBLineItem.UnitID = item.UoMID;
                            chartOfInvRMOBLineItem.ItemID = item.Id;
                            chartOfInvRMOBLineItem.ItemGrpID = item.parentId;
                            x.DocID = chartOfInvRMOB.RMOBNO;
                            x.TRType = 1;
                            standardCost += x.Amount;
                            //x.Rate = Convert.ToDecimal(item.clmStandardCost);
                            //x.Amount = Convert.ToDecimal(x.Qty) * item.clmStandardCost;
                            x.DocType = "RMOB";
                            context.InventoryTransaction.Add(x);
                            context.ChartOfInvRMOBLineItem.Add(chartOfInvRMOBLineItem);
                        });
                        AccountModuleBridge.InsertFromRMOB(ref context, chartOfInvRMOB, standardCost);
                        UserLog.SaveUserLog(ref context, new UserLog(chartOfInvRMOB.RMOBNO.ToString(), DateTime.Now, "RMOB", ScreenAction.Update));
                        context.SaveChanges();
                        return Json(chartOfInvRMOB.RMOBNO, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(2, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadRmItem(groupId), JsonRequestBehavior.AllowGet);
        }
    }
   
}
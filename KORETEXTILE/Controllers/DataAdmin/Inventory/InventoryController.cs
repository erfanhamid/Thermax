using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.SalesModule;
using System.Data.Entity;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.Bridge;
using BEEERP.Models.Log;
using BEEERP.Models.StoreRM.GRN;

namespace BEEERP.Controllers.Inventory
{
    [ShowNotification]
    [Authorize(Roles = "DataAdmin,DataOperator,DataViewer,DataApprover,SysAdmin,SysViewer,SysOperator,SysApprover,SalAdmin,SalViewer,SalApprover,SalOperator,InvAdmin")]
    [Permission]
    public class InventoryController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();

        // GET: Inventory
        public ActionResult Inventory()
        {
            ViewBag.Group = LoadComboBox.LoadItemGroup();
            ViewBag.GroupOrItem = LoadComboBox.LoadGroupOrItem();
            ViewBag.UOM = LoadComboBox.LoadAllUOM();
            ViewBag.Country = LoadComboBox.LoadAllCountry();

            var items = new List<ChartOnInvList>();


            return View();
        }

        [HttpPost]
        public ActionResult SaveInventory(ChartOfInventory chartOfInv)
        {

            var invenExist = context.ChartOfInventory.FirstOrDefault(x => x.ItemCode == chartOfInv.ItemCode);

            chartOfInv.Id = generate.GenerateInventoryId(context);
            chartOfInv.rootAccountType = "FG";

            if (invenExist == null || invenExist.ItemCode == null)
            {
                context.ChartOfInventory.Add(chartOfInv);
                
                UserLog.SaveUserLog(ref context, new UserLog(chartOfInv.Id.ToString(), DateTime.Now, "Chart of Inventory (FG)",ScreenAction.Save));
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
            var cI = context.ChartOfInventory.ToList().Where(x => x.rootAccountType == "RM").ToList();
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
                    //CountryId = x.CountryId,
                    //CountryName = context.Countries.FirstOrDefault(c => c.ID == x.CountryId).Name
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
            chartOfInv.rootAccountType = "FG";
            context.Entry<ChartOfInventory>(chartOfInv).State = EntityState.Modified;
            UserLog.SaveUserLog(ref context, new UserLog(chartOfInv.Id.ToString(), DateTime.Now, "Chart of Inventory (FG)", ScreenAction.Update));
            context.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteItemById(int id)
        {
            var isExist = context.InventoryTransaction.FirstOrDefault(x => x.CIID == id);
            if(isExist==null)
            {
                try
                {
                    var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
                    context.ChartOfInventory.Remove(item);
                    UserLog.SaveUserLog(ref context, new UserLog(item.Id.ToString(), DateTime.Now, "Chart of Inventory (FG)", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                catch(Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InvOpeningBalance()
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
            ViewBag.ItemGroup = LoadComboBox.LoadItemGroup();
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadComboBox.LoadStore(depot);
            ViewBag.DepotId = depot;
            return View();
        }
        public ActionResult CheckItemValidity(int itemId,int storeId)
        {
            var transaction = context.InventoryTransaction.FirstOrDefault(x => x.CIID == itemId && x.DocType == "FGOB"&&x.StoreID==storeId);
            if(transaction==null)
            {
                var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == itemId);
                return Json(new {Message=1,ItemName=item.Name,ItemCode=item.ItemCode }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {StandardCost =0, Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SaveInvOb(List<InventoryTransaction>  transaction)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ChartOfInvFGOB chartOfInvFGOB = new ChartOfInvFGOB();


                    chartOfInvFGOB.DepotID = transaction.FirstOrDefault().DepotID;
                    chartOfInvFGOB.StoreID = transaction.FirstOrDefault().StoreID;
                    var lastRow= context.ChartOfInvFGOB.ToList().LastOrDefault();
                    if (lastRow==null)
                    {
                        chartOfInvFGOB.FGOBNO = 1;
                    }
                    else
                    {
                        chartOfInvFGOB.FGOBNO =lastRow.FGOBNO+ 1;
                    }
                        
                    context.ChartOfInvFGOB.Add(chartOfInvFGOB);
                    decimal standardCost = 0;
                    transaction.ForEach(x =>
                    {
                        var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.CIID);
                        ChartOfInvFGOBLineItem chartOfInvObLineItem = new ChartOfInvFGOBLineItem();
                        RemainingStock r = new RemainingStock();

                        chartOfInvObLineItem.OBQT = Convert.ToInt32(x.Qty);
                        chartOfInvObLineItem.FGOBNO = chartOfInvFGOB.FGOBNO;
                        chartOfInvObLineItem.STCost = x.Rate;
                        chartOfInvObLineItem.Value = x.Amount;
                        chartOfInvObLineItem.UnitID = item.UoMID;
                        chartOfInvObLineItem.ItemID = item.Id;
                        chartOfInvObLineItem.ItemGrpID = item.parentId;
                        x.DocID = chartOfInvFGOB.FGOBNO;
                        x.TRType = 1;
                        standardCost += x.Amount;
                        //x.Rate = Convert.ToDecimal(item.clmStandardCost);
                        //x.Amount = Convert.ToDecimal(x.Qty) * item.clmStandardCost;
                        x.DocType = "FGOB";
                        r.ItemId = x.CIID;
                        r.Qty = (decimal)x.Qty;
                        r.LCRNNo = x.DocID;
                        r.UnitCost = x.Rate;
                        r.DocType = x.DocType;
                        r.DocNo = x.DocID;
                        r.StoreId = x.StoreID;
                        context.InventoryTransaction.Add(x);
                        context.RemainingStock.Add(r);
                        context.ChartOfInvFGOBLineItem.Add(chartOfInvObLineItem);
                    });
                    AccountModuleBridge.InsertFromFGOB(ref context, chartOfInvFGOB, standardCost);
                    UserLog.SaveUserLog(ref context, new UserLog(chartOfInvFGOB.FGOBNO.ToString(), DateTime.Now, "(FGOB)", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(chartOfInvFGOB.FGOBNO, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteFGItemsById(int id)
        {
            try
            {
                bool isExist = false;
                var fgItem = context.ChartOfInvFGOB.FirstOrDefault(x => x.FGOBNO == id);
                var fgLineItem = context.ChartOfInvFGOBLineItem.Where(x => x.FGOBNO == id).ToList();
                var transection = context.Transection.Where(x => x.DocType == "FGOB" && x.DocID == id).ToList();
                var invTransection = context.InventoryTransaction.Where(x => x.DocType == "FGOB" && x.CIID == id).ToList();
                var RemainingStock = context.RemainingStock.Where(x => x.DocType == "FGOB" && x.ItemId == id).ToList();
                fgLineItem.ForEach(x => {
                    int item = context.InventoryTransaction.Count(y => y.CIID == x.ItemID && y.DocType != "FGOB" && y.DocID == id);
                    if(item>0)
                    {
                        isExist = true;
                    }
                });
                context.ChartOfInvFGOB.Remove(fgItem);
                if(isExist==false)
                {
                    fgLineItem.ForEach(x => {
                        context.ChartOfInvFGOBLineItem.Remove(x);
                    });
                    transection.ForEach(x => {
                        context.Transection.Remove(x);
                    });
                    invTransection.ForEach(x => {
                        context.InventoryTransaction.Remove(x);
                    });
                    RemainingStock.ForEach(x => {
                        context.RemainingStock.Remove(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(fgItem.FGOBNO.ToString(), DateTime.Now, "(FGOB)", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch(Exception ex)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
           
            
        }
        public ActionResult GetInvFgOpenById(int id)
        {
            var fgItem = context.ChartOfInvFGOB.FirstOrDefault(x => x.FGOBNO == id);
            
            if(fgItem!=null)
            {
                var fgLineItem = context.ChartOfInvFGOBLineItem.Where(x => x.FGOBNO == id).ToList().Select(x =>
                {
                    var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                    x.ItemName = item.Name;
                    x.ItemCode = item.ItemCode;
                    x.GroupId = item.parentId;
                    //x.PacSize = item.PacSize;
                    return x;
                }).ToList();
                return Json(new { fgItem, fgLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateInvFGOB(List<InventoryTransaction> transaction,int docId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ChartOfInvFGOB chartOfInvFGOB = new ChartOfInvFGOB();


                    chartOfInvFGOB.DepotID = transaction.FirstOrDefault().DepotID;
                    chartOfInvFGOB.StoreID = transaction.FirstOrDefault().StoreID;
                    chartOfInvFGOB.FGOBNO = docId;
                    var isExist = context.ChartOfInvFGOB.AsNoTracking().FirstOrDefault(x => x.FGOBNO == docId);
                    if(isExist!=null)
                    {
                        context.Entry<ChartOfInvFGOB>(chartOfInvFGOB).State = EntityState.Modified;
                        //context.ChartOfInvFGOB.Add(chartOfInvFGOB);
                        context.ChartOfInvFGOBLineItem.Where(x => x.FGOBNO == chartOfInvFGOB.FGOBNO).ToList().ForEach(x => context.ChartOfInvFGOBLineItem.Remove(x));
                        decimal standardCost = 0;
                        transaction.ForEach(x =>
                        {
                            var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.CIID);
                            ChartOfInvFGOBLineItem chartOfInvObLineItem = new ChartOfInvFGOBLineItem();

                            chartOfInvObLineItem.OBQT = Convert.ToInt32(x.Qty);
                            chartOfInvObLineItem.FGOBNO = chartOfInvFGOB.FGOBNO;
                            chartOfInvObLineItem.STCost = x.Rate;
                            chartOfInvObLineItem.Value = x.Amount;
                            chartOfInvObLineItem.UnitID = item.UoMID;
                            chartOfInvObLineItem.ItemID = item.Id;
                            chartOfInvObLineItem.ItemGrpID = item.parentId;
                            x.DocID = chartOfInvFGOB.FGOBNO;
                            x.TRType = 1;
                            standardCost += x.Amount;
                            //x.Rate = Convert.ToDecimal(item.clmStandardCost);
                            //x.Amount = Convert.ToDecimal(x.Qty) * item.clmStandardCost;
                            x.DocType = "FGOB";
                            context.InventoryTransaction.Add(x);
                            context.ChartOfInvFGOBLineItem.Add(chartOfInvObLineItem);
                        });
                        AccountModuleBridge.InsertFromFGOB(ref context, chartOfInvFGOB, standardCost);
                        UserLog.SaveUserLog(ref context, new UserLog(chartOfInvFGOB.FGOBNO.ToString(), DateTime.Now, "(FGOB)", ScreenAction.Update));
                        context.SaveChanges();
                        return Json(chartOfInvFGOB.FGOBNO, JsonRequestBehavior.AllowGet);
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
            return Json(LoadComboBox.LoadFGItem(groupId), JsonRequestBehavior.AllowGet);
        }
    }
    

    public class ChartOnInvList
    {
        public string ItemCode { set; get; }
        public string Name { set; get; }
        public string GroupName { set; get; }
        public string GroupOrItem { get; set; }
        public int Id { get; set; }
        public string UoMName { set; get; }
        public decimal TradePrice { set; get; }  
        public int CartonCapacity { set; get; }
        public decimal StandardCost { set; get; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
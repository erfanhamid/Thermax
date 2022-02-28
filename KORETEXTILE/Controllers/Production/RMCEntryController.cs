using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.ProductionModule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Production
{
    [ShowNotification]
    // old controller name :RawMaterialConsumtionBatchController
    public class RMCEntryController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: RMCEntry
        public ActionResult RMCEntry()
        {
            //string RMCNo = "";
            //string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            //if (context.RMCEntry.Count() > 0)
            //{
            //    var lastRMC = context.RMCEntry.ToList().LastOrDefault(x => x.RMCNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
            //    if (lastRMC == null)
            //    {
            //        RMCNo = yearLastTwoPart + "00001";
            //    }
            //    else
            //    {
            //        RMCNo = yearLastTwoPart + (Convert.ToInt32(lastRMC.RMCNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            //    }
            //}
            //else
            //{
            //    RMCNo = yearLastTwoPart + "00001";
            //}
            //ViewBag.RMCNo = RMCNo;
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.GroupId = LoadComboBox.LoadItemGroupByBatchNo(null);
            ViewBag.LoadBatch = LoadComboBox.LoadBatch();
            ViewBag.Store = LoadComboBox.LoadProductionStoreForRM();
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            return View();
        }
        public ActionResult UOMOnItemChange(int id)
        {
            var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                var uom = context.UOM.FirstOrDefault(x => x.Id == item.UoMID);
                return Json(new { Messsage = 1, Uom = uom.Name }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGroupByBatch(string batchNo)
        {
            var items = LoadComboBox.LoadItemGroupByBatchNo(batchNo);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemByBatchID(string batchName)
        {
            return Json(LoadComboBox.LoadAllRMItem(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMovingAvgCost(int id, int storeid)
        {
            var cost = LoadComboBox.GetMovingAvg(id, storeid);
            if (cost != null)
            {
                if (cost.clmStandardCost > 0)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveRMC(RMCEntry rmcEntry, List<RMCLineItem> rmcLineItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "RM Consumption").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (rmcEntry.RMCDate > IsExpired.SELDate)
            {
                string RMCNo = "";
                string yearLastTwoPart = rmcEntry.RMCDate.Year.ToString().Substring(2, 2).ToString();
                if (context.RMCEntry.Count() > 0)
                {
                    var lastRMC = context.RMCEntry.ToList().LastOrDefault(x => x.RMCNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastRMC == null)
                    {
                        RMCNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        RMCNo = yearLastTwoPart + (Convert.ToInt32(lastRMC.RMCNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    RMCNo = yearLastTwoPart + "00001";
                }
                rmcEntry.RMCNo = Convert.ToInt32(RMCNo);
                foreach (var item in rmcLineItems)
                {
                    rmcEntry.RMCTotalQty += item.ItemQty;
                    rmcEntry.RMCTotalValue += item.TotalStanCost;
                }
                var existsRMCNo = context.RMCEntry.FirstOrDefault(m => m.RMCNo == rmcEntry.RMCNo);
                if (existsRMCNo == null)
                {
                    try
                    {
                        //rmcEntry.RMCDate = rmcEntry.RMCDate.Add(DateTime.Now.TimeOfDay);
                        context.RMCEntry.Add(rmcEntry);
                        rmcLineItems.ForEach(x =>
                        {
                            var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                            var cost = LoadComboBox.GetRMCRate(x.ItemID, rmcEntry.RMCDate.ToString("yyyy-MM-dd"));
                            if (cost != null)
                            {
                                x.UnitStanCost = Convert.ToDecimal(cost);
                                x.RmcRate = x.UnitStanCost;
                                x.RmcValue = x.UnitStanCost * x.ItemQty;
                            }
                            else
                            {
                                x.UnitStanCost = 0;
                                x.RmcRate = x.UnitStanCost;
                                x.RmcValue = x.UnitStanCost * x.ItemQty;
                            }

                            x.TotalStanCost = Math.Round(x.ItemQty * x.UnitStanCost, 2);
                            //CommonInfo.AllocateItem(ref context, x.ItemID, "RMCBatch", x.ItemQty, rmcEntry.RMCNo, rmcEntry.StoreId, false);
                        });
                        rmcLineItems.ForEach(x => { x.RMCNo = rmcEntry.RMCNo; context.RMCLineItem.Add(x); });

                        AccountModuleBridge.InsertUpdateFromRMCBatch(ref context, rmcEntry, rmcLineItems);
                        InventoryTransactionBridge.InsertFromRMCEntry(ref context, rmcLineItems, rmcEntry);
                        BatchBalanceCalculationBridge.InsertUpdateFromRMCBatch(ref context, rmcEntry, rmcLineItems);
                        UserLog.SaveUserLog(ref context, new UserLog(rmcEntry.RMCNo.ToString(), rmcEntry.RMCDate, "RMC", ScreenAction.Save));
                        context.SaveChanges();
                        return Json(rmcEntry.RMCNo, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetRMPByRMCNo(int rmcNo)
        {
            if (rmcNo == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rmc = context.RMCEntry.FirstOrDefault(x => x.RMCNo == rmcNo);
                if (rmc == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var rmcLineItem = context.RMCLineItem.Where(x => x.RMCNo == rmcNo).ToList();
                    rmcLineItem.ForEach(x =>
                    {
                        var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
                        x.ItemName = context.ChartOfInventory.FirstOrDefault(m => m.Id == x.ItemID).Name;
                        //x.PacSize = item.PacSize;
                    });

                    return Json(new { Message = 1, rmcEntry = rmc, rmcLineItem = rmcLineItem }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult GetItemRemainQty(int itemId, int storeId, string date)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            DateTime rmcdate = Convert.ToDateTime(date);
            var cost = LoadComboBox.GetRMCRate(itemId, rmcdate.ToString("yyyy-MM-dd"));
            string sql = ("exec spGetRMSItemQty " + itemId + ", '" + DateTimeFormat.ConvertToDDMMYYYY(rmcdate) + "'," + storeId + "");
            List<RMCItemQty> Data = context.Database.SqlQuery<RMCItemQty>(sql).ToList();
            var RemQty = Data[0];
            var qty = (decimal)RemQty.BalanceQty;
            return Json(new { RemQty = qty , Item = item, Rate = cost }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateRMCEntry(RMCEntry rmcConsumption, List<RMCLineItem> addedItems)
        {
            try
            {
                //InventoryTransactionBridge.DeleteFromRMCEntry(ref context, rmcConsumption.RMCNo);
                //context.SaveChanges();
                foreach (var item in addedItems)
                {
                    rmcConsumption.RMCTotalQty += item.ItemQty;
                    rmcConsumption.RMCTotalValue += item.TotalStanCost;
                }
                var isExist = context.RMCEntry.AsNoTracking().FirstOrDefault(x => x.RMCNo == rmcConsumption.RMCNo);
                if (isExist == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var prevItem = context.RMCLineItem.Where(x => x.RMCNo == rmcConsumption.RMCNo).ToList();
                    prevItem.ForEach(x =>
                    {
                        context.RMCLineItem.Remove(x);
                    });
                    //rmcConsumption.RMCDate = isExist.RMCDate;
                    context.Entry<RMCEntry>(rmcConsumption).State = EntityState.Modified;
                    addedItems.ForEach(x => {
                        var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                        var cost = LoadComboBox.GetRMCRate(x.ItemID, rmcConsumption.RMCDate.ToString("yyyy-MM-dd"));
                        //var cost = x.RmcRate;
                        if (cost != null)
                        {
                            x.UnitStanCost = Convert.ToDecimal(cost);
                            x.RmcRate = x.UnitStanCost;
                            x.RmcValue = x.UnitStanCost * x.ItemQty;
                        }
                        else
                        {
                            x.UnitStanCost = 0;
                            x.RmcRate = x.UnitStanCost;
                            x.RmcValue = x.UnitStanCost * x.ItemQty;
                        }
                        //x.UnitStanCost = Convert.ToDecimal(item.clmStandardCost);
                        x.TotalStanCost = Math.Round(x.ItemQty * x.UnitStanCost, 2);
                        //CommonInfo.AllocateItem(ref context, x.ItemID, "RMCBatch", x.ItemQty, rmcConsumption.RMCNo, rmcConsumption.StoreId, true);
                    });
                    addedItems.ForEach(x => { x.RMCNo = rmcConsumption.RMCNo; context.RMCLineItem.Add(x); });
                    //InventoryTransactionBridge.InsertFromRMCEntry(ref context, addedItems, rmcConsumption);
                    InventoryTransactionBridge.InsertUpdateFromRMCEntry(ref context, addedItems, rmcConsumption);
                    AccountModuleBridge.InsertUpdateFromRMCBatch(ref context, rmcConsumption, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(rmcConsumption.RMCNo.ToString(), rmcConsumption.RMCDate, "RMC", ScreenAction.Update));
                    BatchBalanceCalculationBridge.InsertUpdateFromRMCBatch(ref context, rmcConsumption, addedItems);
                    context.SaveChanges();
                    //return Json(rmcConsumption.RMCNo, JsonRequestBehavior.AllowGet);
                    //return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                return Json(rmcConsumption.RMCNo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteRMC(RMCEntry rmcConsumption, List<RMCLineItem> addedItems)
        {


            var isExist = context.RMCEntry.AsNoTracking().FirstOrDefault(x => x.RMCNo == rmcConsumption.RMCNo);
            if (isExist == null)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var prevItem = context.RMCLineItem.Where(x => x.RMCNo == rmcConsumption.RMCNo).ToList();
                prevItem.ForEach(x =>
                {
                    context.RMCLineItem.Remove(x);
                });
                context.Entry<RMCEntry>(isExist).State = EntityState.Deleted;
                //context.RMCEntry.Remove(isExist);
                InventoryTransactionBridge.DeleteFromRMCEntry(ref context, rmcConsumption.RMCNo);
                BatchBalanceCalculationBridge.DeleteFromRMCBatch(ref context, rmcConsumption);
                UserLog.SaveUserLog(ref context, new UserLog(rmcConsumption.RMCNo.ToString(), DateTime.Now, "Raw Material Consumption", ScreenAction.Delete));
                context.SaveChanges();
            }
            return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
        }

        public FileResult GetRMCPreview(int RMCNo)
        {
            Session["reportName"] = "Row Material Consumtion Details For ID" + RMCNo;

            //ReportParmPersister param = new ReportParmPersister();
            //var data = (from e in context.Employees.AsEnumerable()
            //            join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //            where u.ScreenName == "Raw Material Consumption" && u.Action == "Save" && u.TrnasId == RMCNo.ToString()
            //            select e.Name).ToList();
            //if(data.Count > 0)
            //{
            //    param.PostedBy = data.FirstOrDefault().ToString();
            //}
            //param.PostedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "Raw Material Consumption" && u.Action == "Save" && u.TrnasId == RMCNo.ToString()
            //                  select e.Name).FirstOrDefault();
            string sql = string.Format("exec PreviewRMC " + RMCNo);
            var items = context.Database.SqlQuery<PreviewRMC>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewRMC report = new PreviewRMC();
                items.Add(report);
            }

            PreviewRMCR rd = ShowReport(items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PreviewRMCR ShowReport(List<PreviewRMC> data)
        {
            PreviewRMCR RMC = new PreviewRMCR();

            RMC.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewRMCR.rpt");
            RMC.Load(APPPATH);
            RMC.SetDataSource(data);
            //RMC.SetParameterValue("postedBy", persister.PostedBy);
            //RMC.SetParameterValue("compName", persister.CompName);
            //RMC.SetParameterValue("compAddress", persister.CompAddress);
            //RMC.SetParameterValue("reportName", Session["reportName"]);
            return RMC;
        }
    }
}
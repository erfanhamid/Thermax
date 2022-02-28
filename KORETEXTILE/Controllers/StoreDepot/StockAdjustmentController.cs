using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.StoreDepot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreDepot
{
    [ShowNotification]
    public class StockAdjustmentController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: StockAdjustment
        public ActionResult StockAdjustment()
        {
            string saNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.StockAdjustment.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.StockAdjustment.ToList().LastOrDefault(x => x.SANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastInvoice == null)
                {
                    saNo = yearLastTwoPart + "00001";
                }
                else
                {
                    saNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.SANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                saNo = yearLastTwoPart + "00001";
            }
            ViewBag.SANO = saNo;
            //var depot = LoadComboBox.LoadAllDepot();
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.Group = LoadItemGroup();
            ViewBag.Item = LoadItem(null);
            ViewBag.Store = LoadComboBox.LoadAllFGStoreByDepot(null);
            ViewBag.Type = LoadComboBox.LoadTransectionType();

            return View();
        }
        public static SelectList LoadItemGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'g' and rootAccountType = 'FG' ";
            List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
            Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString()+" - " + x.ItemName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadItem(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (group == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'l' and parentId = "+group+"";
                List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
                Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() +" - " + x.ItemName); });
                //context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.parentId == group).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name + " - " + x.PacSize); });
                return new SelectList(items, "Key", "Value");
            }

        }
        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadItem(groupId), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetItemRemainQty(int itemId, int storeId, string date)
        {
            DateTime tDate = Convert.ToDateTime(date);
            decimal RemQty = SaleCommonInfo.GetRemainItemQtyByDate(itemId, storeId, tDate.Date);
            //string sql = "select uomid as UOM from chartofInv where  id = " + itemId + "";
            //List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
            var data = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == itemId);
            var uom = data.UOM;
            var PCPerCtn = data.clmCartonCapacity;
            var RemCtn = RemQty / PCPerCtn;
            //string Uom = context.UOM.FirstOrDefault(x => x.Id == item).Name;
            return Json(new { RemQty, uom, PCPerCtn , RemCtn}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSA(StockAdjustment sa, List<StockAdjustmentLineItem> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "FG Stock Adjustment").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (sa.Date > IsExpired.SELDate)
            {
                string saNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.StockAdjustment.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.StockAdjustment.ToList().LastOrDefault(x => x.SANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        saNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        saNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.SANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    saNo = yearLastTwoPart + "00001";
                }

                sa.SANo = Convert.ToInt32(saNo);
                sa.Date = sa.Date.Add(DateTime.Now.TimeOfDay);
                decimal TotalAmount = 0;

                addedItems.ForEach(x =>
                {
                    x.SANo = sa.SANo;
                    string sql = "select clmStandardCost as clmStandardCost from chartofInv where  id = " + x.ItemId + "";
                    List<ItemInformation> item = context.Database.SqlQuery<ItemInformation>(sql).ToList();
                //var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemId);
                x.UnitCost = Convert.ToDecimal(item.FirstOrDefault().clmStandardCost);
                    x.Value = x.UnitCost * x.Qty;
                    TotalAmount += x.Value;
                    context.StockAdjustmentLineItems.Add(x);
                });
                sa.TotalAmount = TotalAmount;
                context.StockAdjustment.Add(sa);

                UserLog.SaveUserLog(ref context, new UserLog(sa.SANo.ToString(), sa.Date, "FGSA", ScreenAction.Save));
                AccountModuleBridge.InsertFromFGSA(ref context, sa);
                InventoryTransactionBridge.InsertFromStockAdjustment(ref context, sa, addedItems);

                context.SaveChanges();
                return Json(new { sa }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSAById(int id)
        {
            var saE = context.StockAdjustment.FirstOrDefault(x => x.SANo == id);

            if (saE != null)
            {
                var Depot = context.Store.FirstOrDefault(x => x.Id == saE.Store).DepotID;
                var saLineE = context.StockAdjustmentLineItems.Where(x => x.SANo == id).ToList();
                saLineE.ForEach(x =>
                {
                    string sql = "select Name as ItemName,parentId as GroupId, uomid as UOM  from chartofInv where  id = " + x.ItemId + "";
                    List<ItemInformation> item = context.Database.SqlQuery<ItemInformation>(sql).ToList();
                    var chartOfInv = item.FirstOrDefault();
                    x.ItemName = chartOfInv.ItemName;
                    x.GroupId = chartOfInv.GroupId;
                    x.UOM = context.UOM.FirstOrDefault(y => y.Id == chartOfInv.UOM).Name;

                });
                return Json(new { saE, saLineE, Depot }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSA(StockAdjustment sa, List<StockAdjustmentLineItem> addedItems)
        {
            var saExist = context.StockAdjustment.FirstOrDefault(x => x.SANo == sa.SANo);
            if (saExist != null)
            {
                context.StockAdjustment.Remove(saExist);

                context.StockAdjustmentLineItems.Where(x => x.SANo == sa.SANo).ToList().ForEach(x => {
                    context.StockAdjustmentLineItems.Remove(x);
                });
                decimal TotalAmount = 0;
                addedItems.ForEach(x =>
                {
                    x.SANo = sa.SANo;
                    string sql = "select clmStandardCost as clmStandardCost from chartofInv where  id = " + x.ItemId + "";
                    List<ItemInformation> data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
                    var item = data.FirstOrDefault();
                    x.UnitCost = Convert.ToDecimal(item.clmStandardCost);
                    x.Value = x.UnitCost * x.Qty;
                    TotalAmount += x.Value;
                    context.StockAdjustmentLineItems.Add(x);
                });
                sa.TotalAmount = TotalAmount;
                sa.Date = sa.Date.Add(DateTime.Now.TimeOfDay);

                context.StockAdjustment.Add(sa);
                UserLog.SaveUserLog(ref context, new UserLog(sa.SANo.ToString(), sa.Date, "FGSA", ScreenAction.Update));
                AccountModuleBridge.InsertFromFGSA(ref context, sa);
                InventoryTransactionBridge.InsertFromStockAdjustment(ref context, sa, addedItems);
                context.SaveChanges();
                return Json(new { sa }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSAByid(int id)
        {
            var saExist = context.StockAdjustment.FirstOrDefault(x => x.SANo == id);
            if (saExist != null)
            {
                context.StockAdjustment.Remove(saExist);
                var saLineItem = context.StockAdjustmentLineItems.Where(x => x.SANo == id).ToList();
                saLineItem.ForEach(x =>
                {
                    context.StockAdjustmentLineItems.Remove(x);
                });

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), saExist.Date, "FGSA", ScreenAction.Delete));
                InventoryTransactionBridge.DeleteFromStockAdjustment(ref context, saExist);
                AccountModuleBridge.DeleteFromFGSA(ref context, saExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetStockADjustmentPreview(int sano)
        {
            Session["ReportName"] = "StockADjustmentPreviewR";

            ReportParmPersister param = new ReportParmPersister();

            var saE = context.StockAdjustment.FirstOrDefault(x => x.SANo == sano);
            if (saE != null)
            {
                param.PostedBy = (from e in context.Employees.ToList()
                                  join
                                  u in context.UserLog.ToList() on e.LogInUserName equals u.LogInName
                                  where u.ScreenName == "FGSA" && u.Action == "Save" && u.TrnasId == saE.SANo.ToString()
                                  select e.Name).FirstOrDefault();
            }
            else
            {
                param.PostedBy = "";
            }

            Session["StockADjustmentPreviewParam"] = param;

            string sql = string.Format("exec FGStockAdjustment '" + sano + "'");
            var items = context.Database.SqlQuery<StockADjustmentPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                StockADjustmentPreviewReport report = new StockADjustmentPreviewReport();
                items.Add(report);
            }

            StockADjustmentPreviewReportR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public StockADjustmentPreviewReportR ShowReport(ReportParmPersister persister, List<StockADjustmentPreviewReport> data)
        {
            StockADjustmentPreviewReportR saR = new StockADjustmentPreviewReportR();

            saR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\StockADjustmentPreviewReportR.rpt");
            saR.Load(APPPATH);
            saR.SetDataSource(data);

            saR.SetParameterValue("postedBy", persister.PostedBy);
            saR.SetParameterValue("compName", persister.CompName);
            saR.SetParameterValue("compAddress", persister.CompAddress);
            //saR.SetParameterValue("compContact", persister.CompContact);
            //saR.SetParameterValue("compFax", persister.Fax);
            //saR.SetParameterValue("factAddress", persister.FactAddress);
            //saR.SetParameterValue("factContact", persister.FactContact);

            return saR;
        }
    }
    public class ItemInformation
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int GroupId { get; set; }
        public decimal UnitCost { get; set; }
        public int UOM { get; set; }
        public decimal clmStandardCost { get; set; }
    }
}
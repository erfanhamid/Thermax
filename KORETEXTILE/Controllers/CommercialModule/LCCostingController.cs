using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommercialModule;
using BEEERP.Models.CommercialModule.Import;
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
    public class LCCostingController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LCPartialCosting
        [ShowNotification]
        [Permission]
        [Authorize(Roles = "RMAdmin,RMOperator,RMViewer,RMApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
        public ActionResult LCPCEntry()
        {

            return View();
        }

        public ActionResult GetILCByID(int id)
        {
            var ILCItem = context.ImportLC.FirstOrDefault(X => X.ILCId == id);
            if (ILCItem != null)
            {
                string sql = string.Format("exec CalculateLcSubmittedBillAmount " + id + "");
                var Total = context.Database.SqlQuery<decimal>(sql);

                var ILCNo = ILCItem.ILCNO;
                var TotalCostAllocated = (from lcl in context.LCCostingLines
                                          join l in context.LCCostings on lcl.LCPCNo equals l.LCPCNo
                                          where (l.ILCID == id)
                                          select lcl.ItemValue).ToList().DefaultIfEmpty(0).Sum();

                //var totalSubmittedCost = (from bl in context.ILCBLine
                //                          join b in context.ILCB on bl.ILCBNo equals b.ILCBNo
                //                          where b.ILCID == id
                //                          select bl.Amount).ToList().DefaultIfEmpty(0).Sum();
                var totalSubmittedCost = Total;
                List<LCItemInfo> lCItemInfos = new List<LCItemInfo>();

                var lcline = context.ImportLCLineItem.Where(x => x.ILCID == id).ToList();
                var chartOfInv = context.ChartOfInventory.ToList();
                lcline.ForEach(x =>
                {
                    LCItemInfo lc = new LCItemInfo();
                    lc.ItemId = x.ItemId;
                    lc.ItemName = chartOfInv.FirstOrDefault(y => y.Id == x.ItemId).Name;
                    lc.ItemGroupId = chartOfInv.FirstOrDefault(y => y.Id == x.ItemId).parentId;
                    if (lc.ItemGroupId != 0)
                    {
                        lc.ItemGroup = chartOfInv.FirstOrDefault(y => y.Id == lc.ItemGroupId).Name;
                    }
                    lc.UnitId = chartOfInv.FirstOrDefault(y => y.Id == x.ItemId).UoMID;
                    if (lc.UnitId != 0)
                    {
                        lc.UnitName = context.UOM.FirstOrDefault(y => y.Id == lc.UnitId).Name;
                    }

                    lc.LCQty = x.Qty;
                    lc.CostedQty = (from l in context.LCCostings
                                    join lcl in context.LCCostingLines on l.LCPCNo equals lcl.LCPCNo
                                    where (l.ILCID == id) && (lcl.ItemID == lc.ItemId)
                                    select lcl.ItemQty).ToList().Sum();


                    lCItemInfos.Add(lc);
                });

                return Json(new { ILCNo = ILCNo, Item = lCItemInfos, TotalCost = TotalCostAllocated, TotalSubmittedBill = totalSubmittedCost }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult SaveLCPC(LCCosting LCPC, List<LCCostingLine> addedItems2, bool IsClosed)
        {
            using (context)
            {
                string LCPCNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfLCPC = context.LCCostings.Count();

                if (noOfLCPC > 0)
                {
                    var lastLCPC = context.LCCostings.ToList().LastOrDefault(x => x.LCPCNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    LCPCNo = yearLastTwoPart + (Convert.ToInt32(lastLCPC.LCPCNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    LCPCNo = yearLastTwoPart + "00001";
                }

                LCPC.LCPCNo = Convert.ToInt32(LCPCNo);
                LCPC.LCPCDate = LCPC.LCPCDate.Add(DateTime.Now.TimeOfDay);
                if (LCPC.ILCID != 0 && LCPC.ILCID > 0)
                {
                    if (IsClosed != false)
                    {
                        ImportLC ILC = context.ImportLC.FirstOrDefault(x => x.ILCId == LCPC.ILCID);
                        ILC.IsClosed = IsClosed;
                        context.SaveChanges();
                    }
                }


                var CheckLCPCID = context.LCCostings.FirstOrDefault(x => x.LCPCNo == LCPC.LCPCNo);

                if (CheckLCPCID == null)
                {
                    addedItems2.ForEach(x =>
                    {
                        x.LCPCNo = LCPC.LCPCNo;
                        context.LCCostingLines.Add(x);
                    });
                    LCPC.Type = "LCPC";
                    context.LCCostings.Add(LCPC);

                    UserLog.SaveUserLog(ref context, new UserLog(LCPC.LCPCNo.ToString(), DateTime.Now, "LCPC", ScreenAction.Save));
                    AccountModuleBridge.InsertFromLCPC(ref context, LCPC, addedItems2, LCPC.LCPCDate);
                    LcBalanceCalculationBridge.InsertUdpateFromILC(ref context, LCPC);
                    //InventoryTransactionBridge.InsertFromGRNEntry(ref context, LCPC, addedItems2);
                    context.SaveChanges();
                    return Json(new { LCPCNo = LCPC.LCPCNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }
        }
        public ActionResult GetItemInfoById(int itemId)
        {

            var item = context.ChartOfInventory.FirstOrDefault(m => m.Id == itemId);

            //if (item != null)
            //{
            //    var carton = item.clmCartonCapacity;
            //    var packSize = item.PacSize;
            //    return Json(new { carton = carton, packSize = packSize }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            return Json(0, JsonRequestBehavior.AllowGet);
            //}

        }
        public ActionResult GetAutoCost(int lcid, int itemId)
        {
            string sql = string.Format("exec CalculateLcCosting " + lcid + ","+itemId+"");
            var Rate = context.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            return Json(new { rate = Rate }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLCPCById(int id)
        {

            var LC = context.LCCostings.FirstOrDefault(x => x.LCPCNo == id);
            List<LCItemInfo> LcItemInfos = new List<LCItemInfo>();

            if (LC != null)
            {
                var ILCID = LC.ILCID;
                var ChartOfInv = context.ChartOfInventory.ToList();
                var LCItem = context.LCCostingLines.Where(x => x.LCPCNo == id).ToList();
                var Date = LC.LCPCDate;
                var ILCNo = context.ImportLC.FirstOrDefault(x => x.ILCId == LC.ILCID);
                LCItem.ForEach(x =>
                {
                    LCItemInfo L = new LCItemInfo();
                    L.ItemId = x.ItemID;
                    L.ItemName = ChartOfInv.FirstOrDefault(y => y.Id == x.ItemID).Name;
                    L.ItemGroupId = ChartOfInv.FirstOrDefault(y => y.Id == x.ItemID).parentId;
                    if (L.ItemGroupId != 0)
                    {
                        L.ItemGroup = ChartOfInv.FirstOrDefault(y => y.Id == L.ItemGroupId).Name;
                    }
                    L.UnitId = ChartOfInv.FirstOrDefault(y => y.Id == x.ItemID).UoMID;
                    if (L.UnitId != 0)
                    {
                        L.UnitName = context.UOM.FirstOrDefault(y => y.Id == L.UnitId).Name;
                    }
                    L.LCQty = x.LCQty;
                    L.ItemQty = x.ItemQty;
                    L.ItemValue = x.ItemValue;
                    L.ItemRate = x.ItemRate;
                    //L.CostedQty = (from l in context.LCCostings
                    //                join lcl in context.LCCostingLines on l.LCPCNo equals lcl.LCPCNo
                    //                where (lcl.LCPCNo == id) && (lcl.ItemID == L.ItemId)
                    //                select lcl.ItemQty).ToList().Sum();

                    L.CostedQty = L.LCQty - L.ItemQty;
                    LcItemInfos.Add(L);
                });

                return Json(new { ILCID = ILCID, Item = LcItemInfos, Date = Date, ILCNo = ILCNo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult UpdateLCLine(LCCosting LCPC, List<LCCostingLine> addedItems2)
        {
            using (context)
            {
                var IsExist = context.LCCostings.FirstOrDefault(x => x.LCPCNo == LCPC.LCPCNo);
                if (IsExist != null)
                {
                    context.LCCostings.Remove(IsExist);
                    context.LCCostingLines.Where(x => x.LCPCNo == LCPC.LCPCNo).ToList().ForEach(x =>
                     {
                         context.LCCostingLines.Remove(x);
                     });

                }

                LCPC.LCPCDate = LCPC.LCPCDate.Add(DateTime.Now.TimeOfDay);
                LCPC.CostingTotal = LCPC.CostingTotal;
                var CheckLCPCID = context.LCCostings.FirstOrDefault(x => x.LCPCNo == LCPC.LCPCNo);

                addedItems2.ForEach(x =>
                {
                    x.LCPCNo = LCPC.LCPCNo;
                    context.LCCostingLines.Add(x);
                });
                LCPC.Type = "LCPC";
                context.LCCostings.Add(LCPC);

                UserLog.SaveUserLog(ref context, new UserLog(LCPC.LCPCNo.ToString(), DateTime.Now, "LCPC", ScreenAction.Update));
                AccountModuleBridge.InsertFromLCPC(ref context, LCPC, addedItems2, IsExist.LCPCDate);
                context.SaveChanges();
                return Json(new { LCPCNo = LCPC.LCPCNo }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult DeleteLC(int id)
        {
            var LCExist = context.LCCostings.FirstOrDefault(x => x.LCPCNo == id);
            if (LCExist != null)
            {
                context.LCCostings.Remove(LCExist);
                //var sifdFgLineItem = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList();
                context.LCCostingLines.Where(x => x.LCPCNo == id).ToList().ForEach(x =>
                {
                    context.LCCostingLines.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "LCPC", ScreenAction.Delete));
                LcBalanceCalculationBridge.InsertUdpateFromILC(ref context, LCExist);
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                AccountModuleBridge.DeleteFromLCPC(ref context, id);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public sealed class LCItemInfo
        {
            public string ItemGroup { get; set; }
            public int ItemGroupId { get; set; }
            public string ItemName { get; set; }
            public int ItemId { get; set; }
            public string UnitName { get; set; }
            public int UnitId { get; set; }
            public decimal LCQty { get; set; }
            public decimal CostedQty { get; set; }
            public decimal ItemRate { get; set; }
            public decimal ItemValue { get; set; }
            public decimal ItemQty { get; set; }

        }

        public ActionResult GetLCCostingPreview(int LCPCNo)
        {
            Session["ReportName"] = "LCCostingPreview";

            ReportParmPersister param = new ReportParmPersister();

            var lcCosting = context.LCCostings.FirstOrDefault(x => x.LCPCNo == LCPCNo);
            var importLc = context.ImportLC.FirstOrDefault(x => x.ILCId == lcCosting.ILCID);

            param.ILCId = lcCosting.ILCID;
            param.ILCNo = importLc.ILCNO;
            param.IALCNo = importLc.IALCNO;
            param.SupplierName = context.Supplier.FirstOrDefault(x => x.Id == importLc.SupplierId).SupplierName;
            param.LCPCNo = LCPCNo;
            var loginName = context.UserLog.FirstOrDefault(x => x.TrnasId == LCPCNo.ToString() && x.ScreenName == "LCPC" && x.Action == "Save").LogInName;
            param.PostedBy = context.Employees.FirstOrDefault(x => x.LogInUserName == loginName).Name;

            Session["LCCostingPreview"] = param;

            string sql = string.Format("exec LCCostingPreview '" + LCPCNo + "'");
            var items = context.Database.SqlQuery<LCCostingPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                LCCostingPreviewReport report = new LCCostingPreviewReport();
                items.Add(report);
            }

            LCCostingPreviewReportR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public LCCostingPreviewReportR ShowReport(ReportParmPersister persister, List<LCCostingPreviewReport> data)
        {
            LCCostingPreviewReportR lcCostingR = new LCCostingPreviewReportR();

            lcCostingR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\LCCostingPreviewReportR.rpt");
            lcCostingR.Load(APPPATH);
            lcCostingR.SetDataSource(data);

            lcCostingR.SetParameterValue("CompanyName", persister.CompName);
            lcCostingR.SetParameterValue("ILCId", persister.ILCId);
            lcCostingR.SetParameterValue("ILCNo", persister.ILCNo);
            lcCostingR.SetParameterValue("IALCNo", persister.IALCNo);
            lcCostingR.SetParameterValue("SupplierName", persister.SupplierName);
            lcCostingR.SetParameterValue("LCPCNo", persister.LCPCNo);
            lcCostingR.SetParameterValue("CompanyAddress", persister.CompAddress);
            lcCostingR.SetParameterValue("PostedBy", persister.PostedBy);
            
            return lcCostingR;
        }


    }
}
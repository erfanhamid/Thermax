using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.StoreRM.GRN;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreRM
{
    [ShowNotification]
    [Permission]
    [Authorize(Roles = "RMAdmin,RMOperator,RMViewer,RMApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    [StoreRMModule]
    public class LCRNCostingController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: LCRNCosting
        public ActionResult LCRNEntry()
        {
            var count = context.GoodsReceiveNote.Count();
            if (count == 0)
            {
                ViewBag.LCRNNo = 1;
            }
            else
            {
                ViewBag.LCRNNo = context.GoodsReceiveNote.ToList().Max(m => m.GRNNo) + 1;
            }

            ViewBag.Item = LoadComboBox.LoadAllItem();
            ViewBag.lcs = LoadComboBox.LoadLC(0);
            ViewBag.StoreId = LoadComboBox.LoadAllStore();
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Supplier = LoadComboBox.SupplierFromLC();
            ViewBag.Item = LoadComboBox.LoadLCPCItems(0);
            ViewBag.UOM = LoadComboBox.LoadAllUOM();
            ViewBag.LcCosting = LoadComboBox.LoadAllUOM();
            ViewBag.lcpcNumbers = LoadComboBox.LoadLCPCNo(0);
            return View();
        }
        public ActionResult GetLCBySupplierId(int splierId)
        {
            var lc = LoadComboBox.LoadLC(splierId);
            return Json(new { lc = lc }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLCPCNoByLCNo(int lcno)
        {
            var lcpcnumbers = LoadComboBox.LoadLCPCNo(lcno);
            return Json(new { lcpcnumbers = lcpcnumbers }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLCCostingItems(int lcpcno)
        {
            var lcItem = LoadComboBox.LoadLCPCItems(lcpcno);
            return Json(new { lcItem = lcItem }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemInfo(int itemId, int itemLCId)
        {
            var quantity = context.ImportLCLineItem.FirstOrDefault(m => m.ILCID == itemLCId && m.ItemId == itemId).Qty;
            var item = context.ChartOfInventory.FirstOrDefault(m => m.Id == itemId);
            var lcItemReceived = context.GoodsReceiveNoteLineItem.Where(m => m.GRNNo == context.GoodsReceiveNote.FirstOrDefault(n => n.WONo == itemLCId).GRNNo && m.ItemID == itemId).ToList();
            decimal lcItemReceivedAmount = 0;
            var UoM = FindObjectById.GetUomById(item.UoMID).Name;
            lcItemReceived.ForEach(m => { lcItemReceivedAmount += m.Qty; });
            return Json(new { UoM = UoM, item = item, quantity = quantity, lcItemReceivedAmount = lcItemReceivedAmount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveLCRN(GoodsReceiveNote lcrn, List<GoodsReceiveNoteLineItem> addedItems)
        {
            try
            {
                string grnNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.GoodsReceiveNote.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.GoodsReceiveNote.ToList().LastOrDefault(x => x.GRNNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    grnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GRNNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    grnNo = yearLastTwoPart + "00001";
                }
                lcrn.GRNNo = Convert.ToInt32(grnNo);
                lcrn.GRNDate = lcrn.GRNDate.Add(DateTime.Now.TimeOfDay);
                lcrn.Type = "LC";

                decimal totalCost = 0;
                addedItems.ForEach(m => { m.GRNNo = lcrn.GRNNo; totalCost += m.CostVal; context.GoodsReceiveNoteLineItem.Add(m); });
                //lcrn.TotalCost = totalCost;
                context.GoodsReceiveNote.Add(lcrn);
                UserLog.SaveUserLog(ref context, new UserLog(lcrn.GRNNo.ToString(), DateTime.Now, "LCRN", ScreenAction.Save));
                InventoryTransactionBridge.InsertFromGRNEntry(ref context, lcrn, addedItems);
                
                //List<RemainingStock> remainingstocks = new List<RemainingStock>();
                addedItems.ForEach(m =>
                {
                    RemainingStock remainingStock = new RemainingStock();
                    remainingStock.Qty = m.Qty;
                    remainingStock.LCRNNo = m.GRNNo;
                    remainingStock.ItemId = m.ItemID;
                    remainingStock.UnitCost = m.CostRt;
                    remainingStock.DocType = "LCRN";
                    remainingStock.DocNo =lcrn.GRNNo;
                    remainingStock.StoreId = lcrn.CompanyID;
                    //remainingstocks.Add(remainingStock);
                    context.RemainingStock.Add(remainingStock);
                });
                
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetLcrn(int lcrnNo)
        {
            try
            {
                var prevLcrn = context.GoodsReceiveNote.FirstOrDefault(m => m.GRNNo == lcrnNo);
                if (prevLcrn == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var lcrnlineitem = context.GoodsReceiveNoteLineItem.Where(m => m.GRNNo == prevLcrn.GRNNo).ToList();
                    lcrnlineitem.ForEach(m =>
                    {
                        var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == m.ItemID);
                        //m.ItemName = item.ItemCode + "-" + item.Name + "-" + item.PacSize;
                        //m.PackSize = item.PacSize;
                        m.UoM = FindObjectById.GetUomById(item.UoMID).Name;

                    });
                    return Json(new { Message = 1, lcrn = prevLcrn, lcrnlineitem = lcrnlineitem }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult UpdateLCRN(GoodsReceiveNote lcrn, List<GoodsReceiveNoteLineItem> addedItems)
        {
            try
            {
                var prevLCRN = context.GoodsReceiveNote.FirstOrDefault(m => m.GRNNo == lcrn.GRNNo);
                if (prevLCRN == null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.GoodsReceiveNote.Remove(prevLCRN);
                    var prevLineitemList = context.GoodsReceiveNoteLineItem.Where(m => m.GRNNo == lcrn.GRNNo).ToList();
                    prevLineitemList.ForEach(m => { context.GoodsReceiveNoteLineItem.Remove(m); });
                    lcrn.GRNDate = lcrn.GRNDate.Add(DateTime.Now.TimeOfDay);
                    lcrn.Type = "LC";
                    decimal totalCost = 0;
                    addedItems.ForEach(m => { m.GRNNo = lcrn.GRNNo; totalCost += m.CostVal; context.GoodsReceiveNoteLineItem.Add(m); });
                    //lcrn.TotalCost = totalCost;
                    context.GoodsReceiveNote.Add(lcrn);
                    UserLog.SaveUserLog(ref context, new UserLog(lcrn.GRNNo.ToString(), DateTime.Now, "LCRN", ScreenAction.Update));
                    InventoryTransactionBridge.InsertFromGRNEntry(ref context, lcrn, addedItems);
                    var remstock = context.RemainingStock.FirstOrDefault(m => m.LCRNNo == lcrn.GRNNo);
                    List<RemainingStock> stockitems = new List<RemainingStock>();
                    addedItems.ForEach(n =>
                    {
                        stockitems = context.RemainingStock.Where(k => k.ItemId == n.ItemID).ToList();
                    });
                    stockitems.ForEach(b =>
                    {
                        context.RemainingStock.Remove(b);
                    });
                    addedItems.ForEach(m =>
                    {
                        RemainingStock remainingStock = new RemainingStock();
                        remainingStock.Qty = m.Qty;
                        remainingStock.LCRNNo = m.GRNNo;
                        remainingStock.ItemId = m.ItemID;
                        remainingStock.UnitCost = m.CostRt;
                        remainingStock.DocType = "LCRN";
                        //remainingStock.DocNo = Convert.ToUInt16(lcrn.Lcpcno);
                        remainingStock.StoreId = lcrn.CompanyID;
                        context.RemainingStock.Add(remainingStock);
                        
                    });
                    context.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteLCRN(int lcrnNo)
        {
            try
            {
                var lcrn = context.GoodsReceiveNote.FirstOrDefault(m => m.GRNNo == lcrnNo);
                if (lcrn == null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.GoodsReceiveNote.Remove(lcrn);
                    var prevLineitemList = context.GoodsReceiveNoteLineItem.Where(m => m.GRNNo == lcrn.GRNNo).ToList();
                    prevLineitemList.ForEach(m => { context.GoodsReceiveNoteLineItem.Remove(m); });
                    UserLog.SaveUserLog(ref context, new UserLog(lcrn.GRNNo.ToString(), DateTime.Now, "LCRN", ScreenAction.Delete));
                    InventoryTransactionBridge.InsertFromGRNEntry(ref context, lcrn, new List<GoodsReceiveNoteLineItem>());
                    var remstock = context.RemainingStock.FirstOrDefault(m => m.LCRNNo == lcrn.GRNNo);
                    List<RemainingStock> stockitems = new List<RemainingStock>();
                    stockitems = context.RemainingStock.Where(k => k.LCRNNo == lcrnNo).ToList();
                    stockitems.ForEach(b =>
                    {
                        context.RemainingStock.Remove(b);
                    });
                    context.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
        }
        public FileResult GetLCRNSamplePreview(int LCRNNo)
        {
            Session["ReportName"] = "LCRNSamplePreviewR";

            ReportParmPersister param = new ReportParmPersister();

            var lcrn = context.GoodsReceiveNote.FirstOrDefault(x => x.GRNNo == LCRNNo);
            if (lcrn != null)
            {
                param.ChalanNo = lcrn.ChallanNo;
                param.GRNNo = lcrn.GRNNo;
                param.GRNDate = lcrn.GRNDate;
                param.RefNo = lcrn.RefNo;
                if (param.RefNo == null) {
                    param.RefNo = "";
                }
                param.Description = lcrn.Descriptions;
                if (param.Description == null)
                {
                    param.Description = "";
                }
                param.LCNo = lcrn.WONo;
            }
            else
            {
                param.GRNNo = 0;
                param.RefNo = "";
                param.Description = "";
                param.LCNo = 0;
            }
            param.SupplierName = context.Supplier.FirstOrDefault(m => m.Id == lcrn.SupplierID).SupplierName;
            param.LCDate = context.ImportLC.FirstOrDefault(m => m.ILCId == lcrn.WONo).ILCDate;
            param.ShiftFrom = context.Store.FirstOrDefault(x => x.Id == lcrn.CompanyID).Name;
            param.PostedBy = (from e in context.Employees.AsEnumerable()
                              join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                              where u.ScreenName == "LCRN" && u.Action == "Save" && u.TrnasId == lcrn.GRNNo.ToString()
                              select e.Name).FirstOrDefault();

            Session["LCRNSamplePreview"] = param;

            string sql = string.Format("exec PreviewLcrn '" + LCRNNo + "'");
            var items = context.Database.SqlQuery<LCRNSamplePreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                LCRNSamplePreviewReport report = new LCRNSamplePreviewReport();
                items.Add(report);
            }

            LCRNSamplePreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }
        public LCRNSamplePreviewR ShowReport(ReportParmPersister persister, List<LCRNSamplePreviewReport> data)
        {
            LCRNSamplePreviewR lcrnSamplePreviewR = new LCRNSamplePreviewR();

            lcrnSamplePreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\LCRNSamplePreviewR.rpt");
            lcrnSamplePreviewR.Load(APPPATH);
            lcrnSamplePreviewR.SetDataSource(data);

            lcrnSamplePreviewR.SetParameterValue("SupplierName", persister.SupplierName);
            lcrnSamplePreviewR.SetParameterValue("LCNo", persister.LCNo);
            lcrnSamplePreviewR.SetParameterValue("LCDate", persister.LCDate.ToString("dd-MM-yyyy"));
            lcrnSamplePreviewR.SetParameterValue("rfNo", persister.RefNo);
            lcrnSamplePreviewR.SetParameterValue("Description", persister.Description);

            lcrnSamplePreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            lcrnSamplePreviewR.SetParameterValue("GRNNum", persister.GRNNo);
            lcrnSamplePreviewR.SetParameterValue("chalanNo", persister.ChalanNo);
            lcrnSamplePreviewR.SetParameterValue("GRNDate", persister.GRNDate.ToString("dd-MM-yyyy"));
            lcrnSamplePreviewR.SetParameterValue("postedBy", persister.PostedBy);
            lcrnSamplePreviewR.SetParameterValue("compName", persister.CompName);
            lcrnSamplePreviewR.SetParameterValue("compAddress", persister.CompAddress);
            lcrnSamplePreviewR.SetParameterValue("compContact", persister.CompContact);
            lcrnSamplePreviewR.SetParameterValue("compFax", persister.Fax);
            lcrnSamplePreviewR.SetParameterValue("factAddress", persister.FactAddress);
            lcrnSamplePreviewR.SetParameterValue("factContact", persister.FactContact);
            return lcrnSamplePreviewR;
        }
    }
}
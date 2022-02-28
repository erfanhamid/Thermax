using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.StoreRM.GRN;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class MaterialReceiveController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MaterialReceive
        public ActionResult MaterialReceive()
        {
            ViewBag.Item = LoadComboBox.LoadAllItem();
            ViewBag.lcs = LoadComboBox.LoadLC(0);
            ViewBag.StoreId = LoadComboBox.LoadAllStore();
            ViewBag.Group = LoadComboBox.LoadAllSupplierGroup();
            //ViewBag.Supplier = LoadComboBox.SupplierFromLCByGroup(null);
            ViewBag.Supplier = LoadComboBox.SupplierFromType(null);
            ViewBag.Comp = LoadComboBox.LoadAllCompanyID();
            ViewBag.TypeId = LoadComboBox.LoadAllTypeId();


            ViewBag.Supplier = LoadComboBox.LoadAllImportSupplier();
            //ViewBag.Item = LoadComboBox.LoadLCItems(null);
            ViewBag.UOM = LoadComboBox.LoadAllUOM();
            var FactoryRM = context.Store.FirstOrDefault(x => x.Name.ToLower() == "factory rm");
            if (FactoryRM != null)
            {
                ViewBag.FactoryRM = FactoryRM.Id;
            }
            else
            {
                ViewBag.FactoryRM = 0;
            }
            return View();
        }

        public ActionResult GetSupplierByTypeID(int id)
        {
            return Json(LoadComboBox.SupplierFromType(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplierIdByGroup(int id)
        {
            var supplier = LoadComboBox.SupplierFromLCByGroup(id);
            return Json(new { supplier = supplier }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLCBySupplierId(int splierId, int typeId)
        {
            var lc = LoadComboBox.LoadLCMR(splierId, typeId);
            return Json(new { lc = lc }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLCItems(int lcId, int typeId)
        {
            //List <lcwoitems> ;

            if (typeId == 1)
            {
                var lcwoitems = LoadComboBox.LoadLCItems(lcId);
                return Json(new { lcItem = lcwoitems }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var lcwoitems = LoadComboBox.LoadWOItems(lcId);
                return Json(new { lcItem = lcwoitems }, JsonRequestBehavior.AllowGet);
            }



            //return Json(new { lcItem = lcwoitems }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemInfo(int itemId, int itemLCId)
        {
            var quantity = context.ImportLCLineItem.FirstOrDefault(m => m.ILCID == itemLCId && m.ItemId == itemId).Qty;
            var item = context.ChartOfInventory.FirstOrDefault(m => m.Id == itemId);
            //var any = context.GoodsReceiveNote.FirstOrDefault(n => n.WONo == itemLCId).GRNNo;
            //var lcItemReceived = context.GoodsReceiveNoteLineItem.Where(m => m.GRNNo == context.GoodsReceiveNote.FirstOrDefault(n => n.WONo == itemLCId).GRNNo && m.ItemID == itemId).ToList();
            var lcItemReceived = GetTotalRcvQty(itemLCId, itemId);
            //decimal lcItemReceivedAmount = 0;
            var UoM = FindObjectById.GetUomById(item.UoMID).Name;
            //lcItemReceived.ForEach(m => { lcItemReceivedAmount += m.Qty; });
            return Json(new { UoM = UoM, item = item, quantity = quantity, lcItemReceivedAmount = lcItemReceived }, JsonRequestBehavior.AllowGet);
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



        public ActionResult SaveLCRN(GoodsReceiveNote lcrn, List<GoodsReceiveNoteLineItem> addedItems)
        {
            try
            {
                string grnNo = "";
                string yearLastTwoPart = lcrn.GRNDate.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.GoodsReceiveNote.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.GoodsReceiveNote.ToList().LastOrDefault(x => x.GRNNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        grnNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        grnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GRNNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    grnNo = yearLastTwoPart + "00001";
                }
                lcrn.GRNNo = Convert.ToInt32(grnNo);
                lcrn.GRNDate = lcrn.GRNDate.Add(DateTime.Now.TimeOfDay);
                lcrn.Type = "LC";

                decimal totalCost = 0;
                addedItems.ForEach(m => {
                    m.GRNNo = lcrn.GRNNo;
                    totalCost += m.CostVal;
                    context.GoodsReceiveNoteLineItem.Add(m);
                });
                //lcrn.TotalCost = totalCost;

                if (lcrn.TypeId == 1)
                {
                    lcrn.Type = "LC";
                }
                else
                {
                    lcrn.Type = "WO";
                }



                context.GoodsReceiveNote.Add(lcrn);
                //AccountModuleBridge.InsertUpdateFromLCRN(ref context, lcrn, addedItems);
                //InventoryTransactionBridge.InsertFromLCRNEntry(ref context, lcrn, addedItems);
                //UserLog.SaveUserLog(ref context, new UserLog(lcrn.GRNNo.ToString(), DateTime.Now, "LCRN", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { lcrnNo = lcrn.GRNNo }, JsonRequestBehavior.AllowGet);
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
                var prevLcrn = context.GoodsReceiveNote.FirstOrDefault(m => m.GRNNo == lcrnNo && m.Type == "LC");
                if (prevLcrn == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    prevLcrn.GroupID = context.Supplier.FirstOrDefault(x => x.Id == prevLcrn.SupplierID).GroupID;
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
                    //UserLog.SaveUserLog(ref context, new UserLog(lcrn.GRNNo.ToString(), DateTime.Now, "LCRN", ScreenAction.Update));
                    //AccountModuleBridge.InsertUpdateFromLCRN(ref context, lcrn, addedItems);
                    //InventoryTransactionBridge.InsertFromLCRNEntry(ref context, lcrn, addedItems);
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
                    //UserLog.SaveUserLog(ref context, new UserLog(lcrn.GRNNo.ToString(), DateTime.Now, "LCRN", ScreenAction.Delete));
                    //AccountModuleBridge.DeleteFromLCRNEntry(ref context, lcrn);
                    //InventoryTransactionBridge.DeleteFromLCRNEntry(ref context, lcrn);
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
            //Session["ReportName"] = "LCRNSamplePreviewR";

            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "LCRN" && p.TrnasId == LCRNNo.ToString()
                            select c).ToList().FirstOrDefault();

            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }

            //var lcrn = context.GoodsReceiveNote.FirstOrDefault(x => x.GRNNo == LCRNNo);
            //if (lcrn != null)
            //{
            //    param.ChalanNo = lcrn.ChallanNo;
            //    param.GRNNo = lcrn.GRNNo;
            //    param.GRNDate = lcrn.GRNDate;
            //}
            //else
            //{
            //    param.GRNNo = 0;
            //}
            //param.SupplierName = context.Supplier.FirstOrDefault(m => m.Id == lcrn.SupplierID).SupplierName;
            //param.RefNo = lcrn.RefNo;
            //param.Description = lcrn.Descriptions;
            //param.LCNo = lcrn.WONo;
            //param.LCDate = context.ImportLC.FirstOrDefault(m => m.ILCId == lcrn.WONo).ILCDate;
            //param.ShiftFrom = context.Store.FirstOrDefault(x => x.Id == lcrn.StoreID).Name;
            //param.PostedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "LCRN" && u.Action == "Save" && u.TrnasId == lcrn.GRNNo.ToString()
            //                  select e.Name).FirstOrDefault();

            //Session["LCRNSamplePreview"] = param;

            string sql = string.Format("exec spPrintLCRN '" + LCRNNo + "'");
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
            lcrnSamplePreviewR.SetParameterValue("postedBy", persister.PostedBy);
            return lcrnSamplePreviewR;
        }
    }
}
   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;
using BEEERP.Models.SpareParts;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.SpareParts
{
    [ShowNotification]
    public class MRRGoodsReceiveNoteController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: MRRGoodsReceiveNote
        public ActionResult MRRGoodsReceiveNote()
        {
            ViewBag.Item = LoadComboBox.LoadAllItem();
            ViewBag.lcs = LoadComboBox.LoadLC(0);
            ViewBag.StoreId = LoadComboBox.LoadAllStore();
            ViewBag.QCNo = LoadComboBox.LoadAllQCNo();
            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.Type = LoadComboBox.LoadAllTypeId();
            ViewBag.BoxID = LoadComboBox.LoadAllSPBox(null);
            ViewBag.Group = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.SupplierFromLCByGroup(null);


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
        public ActionResult GetSupplierIdByGroup(int id)
        {
            var supplier = LoadComboBox.SupplierFromLCByGroup(id);
            return Json(new { supplier = supplier }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLCBySupplierId(int splierId)
        {
            var lc = LoadComboBox.LoadLC(splierId);
            return Json(new { lc = lc }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLCItems(int lcId)
        {
            var lcItem = LoadComboBox.LoadLCItems(lcId);
            return Json(new { lcItem = lcItem }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBoxByBatchID(int id)
        {
            return Json(LoadComboBox.LoadAllSPBox(id), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetItemInfo(int itemId, int itemLCId)
        //{
        //    var quantity = context.ImportLCLineItem.FirstOrDefault(m => m.ILCID == itemLCId && m.ItemId == itemId).Qty;
        //    var item = context.ChartOfInventory.FirstOrDefault(m => m.Id == itemId);
        //    //var lcItemReceived = GetTotalRcvQty(itemLCId, itemId);
        //    var UoM = FindObjectById.GetUomById(item.UoMID).Name;
        //    return Json(new { UoM = UoM, item = item, quantity = quantity, lcItemReceivedAmount = lcItemReceived }, JsonRequestBehavior.AllowGet);
        //}

        //public decimal GetTotalRcvQty(int woNo, int Item)
        //{
        //    var goodRcvNote = (from G in context.MRRGoodsReceiveNote
        //                       join GL in context.MRRGoodsReceiveNoteLineItem on G.LCRNNo equals GL.LCRNNo
        //                       where (G.WONo == woNo) && (GL.ItemID == Item)
        //                       select GL.QcQty).ToList();
        //    if (goodRcvNote != null)
        //    {
        //        decimal sum = goodRcvNote.Sum();
        //        if (sum == 0)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return sum;
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}



        public ActionResult SaveMRRGoodsReceiveNote(MRRGoodsReceiveNote lcrn, List<MRRGoodsReceiveNoteLineItem> addedItems1)
        {
            try
            {
                string grnNo = "";
                string yearLastTwoPart = lcrn.Date.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.MRRGoodsReceiveNote.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.MRRGoodsReceiveNote.ToList().LastOrDefault(x => x.LCRNNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        grnNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        grnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.LCRNNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    grnNo = yearLastTwoPart + "00001";
                }
                lcrn.LCRNNo = Convert.ToInt32(grnNo);
                lcrn.Date = lcrn.Date.Add(DateTime.Now.TimeOfDay);
                lcrn.Type = "LC";

                decimal totalCost = 0;
                addedItems1.ForEach(m =>
                {
                    m.LCRNNo = lcrn.LCRNNo;
                    totalCost += m.CostVal;
                    context.MRRGoodsReceiveNoteLineItem.Add(m);
                });
                //lcrn.TotalCost = totalCost;
                context.MRRGoodsReceiveNote.Add(lcrn);
                //AccountModuleBridge.InsertUpdateFromLCRN(ref context, lcrn, addedItems);
                SPInventoryTransactionBridge.InsertFromMRR(ref context, lcrn, addedItems1);
                //UserLog.SaveUserLog(ref context, new UserLog(lcrn.GRNNo.ToString(), DateTime.Now, "LCRN", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { lcrnNo = lcrn.LCRNNo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetMRRGoodsReceiveNote(int lcrnNo)
        {
            try
            {
                var prevLcrn = context.MRRGoodsReceiveNote.FirstOrDefault(m => m.LCRNNo == lcrnNo && m.Type == "LC");
                if (prevLcrn == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //prevLcrn.GroupID = context.Supplier.FirstOrDefault(x => x.Id == prevLcrn.SupplierID).GroupID;
                    var lcrnlineitem = context.MRRGoodsReceiveNoteLineItem.Where(m => m.LCRNNo == prevLcrn.LCRNNo).ToList();
                    lcrnlineitem.ForEach(m =>
                    {
                        var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == m.ItemID);
                        m.ItemName = item.ItemCode + "-" + item.Name ;
                        //m.PackSize = item.PacSize;
                        // m.UoM = FindObjectById.GetUomById(item.UoMID).Name;
                        var GroupId = FindObjectById.GetGroupNameById(item.Id).parentId;
                        m.GroupName = context.ChartOfInventory.FirstOrDefault(x => x.Id == GroupId).Name;
                        var store = context.Store.Where(x => x.Id == m.StoreId).SingleOrDefault();
                        m.StoreId = store.Id;
                        m.StoreName = store.Name;

                        var box = context.SPBox.Where(x => x.BoxID == m.BoxID).SingleOrDefault();
                        m.BoxID = box.BoxID;
                        m.BoxName = box.BoxNo;
                    });
                    return Json(new { Message = 1, lcrn = prevLcrn, lcrnlineitem = lcrnlineitem }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetQcByQcNo(int qcNo)
        {
          
            //var prevLcrn = context.QualityControl.FirstOrDefault(m => m.QcNo == qcNo /*&& m.Type == "LC"*/);
            //if (prevLcrn == null)
            //{
            //    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            var lcrnlineitem = context.QualityControlLineItem.Where(m => m.QcNo == qcNo).ToList();
            lcrnlineitem.ForEach(m =>
            {
                var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == m.ItemID);
                m.ItemName = item.ItemCode + "-" + item.Name ;
                //m.PackSize = item.PacSize;
                //m.UoM = FindObjectById.GetUomById(item.UoMID).Name;
                var GroupId = FindObjectById.GetGroupNameById(item.Id).parentId;
                m.GroupName = context.ChartOfInventory.FirstOrDefault(x => x.Id == GroupId).Name;

            });



            var mrNo = context.QualityControl.FirstOrDefault(x => x.QcNo == qcNo).LCRNNo;
            var compId = context.QualityControl.FirstOrDefault(x => x.QcNo == qcNo).CompanyID;
            var tp = context.GoodsReceiveNote.FirstOrDefault(x => x.GRNNo == mrNo).Type;

            int sptype;

            if (tp == "LC")
            {
                 sptype = 1;
            }
            else
            {
                 sptype = 2;
            }




            return Json(new { Message = 1, sptp = sptype,CompID =compId,  lcrnlineitem = lcrnlineitem }, JsonRequestBehavior.AllowGet);
            //}
        }
       
  
        public ActionResult UpdateMRRGoodsReceiveNote(MRRGoodsReceiveNote lcrn, List<MRRGoodsReceiveNoteLineItem> addedItems1)
        {
            try
            {
                var prevLCRN = context.MRRGoodsReceiveNote.FirstOrDefault(m => m.LCRNNo == lcrn.LCRNNo);
                if (prevLCRN == null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.MRRGoodsReceiveNote.Remove(prevLCRN);
                    var prevLineitemList = context.MRRGoodsReceiveNoteLineItem.Where(m => m.LCRNNo == lcrn.LCRNNo).ToList();
                    prevLineitemList.ForEach(m => { context.MRRGoodsReceiveNoteLineItem.Remove(m); });
                    lcrn.Date = lcrn.Date.Add(DateTime.Now.TimeOfDay);
                    lcrn.Type = "LC";
                    //decimal totalCost = 0;
                    addedItems1.ForEach(m => { m.LCRNNo = lcrn.LCRNNo; /*totalCost += m.CostVal;*/ context.MRRGoodsReceiveNoteLineItem.Add(m); });
                    //lcrn.TotalCost = totalCost;
                    context.MRRGoodsReceiveNote.Add(lcrn);
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
        public ActionResult DeleteMRRGoodsReceiveNote(int lcrnNo)
        {
            try
            {
                var lcrn = context.MRRGoodsReceiveNote.FirstOrDefault(m => m.LCRNNo == lcrnNo);
                if (lcrn == null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.MRRGoodsReceiveNote.Remove(lcrn);
                    var prevLineitemList = context.MRRGoodsReceiveNoteLineItem.Where(m => m.LCRNNo == lcrn.LCRNNo).ToList();
                    prevLineitemList.ForEach(m => { context.MRRGoodsReceiveNoteLineItem.Remove(m); });
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
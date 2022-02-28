using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Procurement;
using BEEERP.Models.Log;
using BEEERP.Models.ViewModel.Procurement;
using System.IO;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using Microsoft.Ajax.Utilities;

namespace BEEERP.Controllers.Procurement
{
    [ShowNotification]
    //[Authorize]
    //[ProcurementModule]
    public class GPBController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult GPB()
        {
            return View();
        }
        public ActionResult GetGRNInfo(int grnNo)
        {
            var isAlredyExist = context.PurchaseLineGRN.FirstOrDefault(x => x.GRNNo == grnNo);
            if (isAlredyExist != null)
            {
                return Json(new { Message = 2, GRNo = grnNo, GPBNo = isAlredyExist.BillNo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var isGrnExist = context.GoodsReceiveNote.Any(x => x.GRNNo == grnNo);
                if (isGrnExist)
                {
                    var grnDetails = (from gl in context.GoodsReceiveNoteLineItem
                                      join g in context.GoodsReceiveNote on gl.GRNNo equals g.GRNNo
                                      where gl.GRNNo == grnNo
                                      select new { g, gl }).ToList();

                    List<GoodsReceiveNoteLineItem> grnLineL = new List<GoodsReceiveNoteLineItem>();
                    grnDetails.ForEach(x =>
                    {
                        GoodsReceiveNoteLineItem grnLine = new GoodsReceiveNoteLineItem();
                        grnLine.GRNNo = x.gl.GRNNo;
                        grnLine.ItemID = x.gl.ItemID;
                        grnLine.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.gl.ItemID).Name;
                        grnLine.UOMID = x.gl.UOMID;
                        grnLine.Unit = context.UOM.FirstOrDefault(y => y.Id == x.gl.UOMID).Name;
                        grnLine.Qty = x.gl.Qty;
                        grnLine.CostRt = x.gl.CostRt;
                        grnLine.CostVal = x.gl.CostVal;
                        WorkOrderLineItem workOrderLineItem = context.WorkOrderLineItem.FirstOrDefault(w => w.WONo == x.g.WONo && w.ItemID == x.gl.ItemID);
                        if (workOrderLineItem != null)
                        {
                            grnLine.VatPer = workOrderLineItem.VatPerc;
                            grnLine.AitPer = workOrderLineItem.AitPerc;
                        }
                        else
                        {
                            grnLine.VatPer = 0;
                            grnLine.AitPer = 0;
                        }
                        //grnLine.BusinessUnit = x.gl.BusinessUnit;
                        //grnLine.BusinessUnitName = context.BussinessUnit.FirstOrDefault(b => b.ID == x.gl.BusinessUnit).Name;
                        //grnLine.JobNo = x.gl.JobNo;
                        grnLineL.Add(grnLine);
                    });
                    int WONO = grnDetails.FirstOrDefault().g.WONo;
                    int supplierId = grnDetails.FirstOrDefault().g.SupplierID;
                    var supplier = context.Supplier.FirstOrDefault(x => x.Id == supplierId);
                    string supplierName = supplier.SupplierName;
                    string supplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == supplier.GroupID).SgroupName;
                    int workorderid = 0;
                    int workorderno = 0;
                    var workOrder = context.WorkOrder.Where(w => w.WONo == WONO).FirstOrDefault();
                    if (workOrder != null)
                    {
                        workorderid = workOrder.WONo;
                        //workorderno = workOrder.WOCode;
                    }
                    return Json(new { Message = 1, grnLineL, supplierId, supplierName, supplierGroup, WorkOrderId = workorderid, WorkOrderCode = workorderno }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 3 }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public ActionResult GetGPBDetails(int billNo)
        {
            var purchaseentryline = (from pel in context.PurchaseLineItem
                                     join pe in context.PurchaseEntry on pel.BillNo equals pe.BillNo
                                     where pe.BillNo == billNo
                                     select new { pel }).ToList();
            var purchaseentry = (from pe in context.PurchaseEntry
                                 join pel in context.PurchaseLineItem on pe.BillNo equals pel.BillNo
                                 join s in context.Supplier on pe.SupplierId equals s.Id
                                 join sg in context.SupplierGroups on s.GroupID equals sg.SgroupId
                                 join gr in context.GoodsReceiveNote on pel.GRNNo equals gr.GRNNo
                                 join grl in context.GoodsReceiveNoteLineItem on pel.GRNNo equals grl.GRNNo
                                 join wo in context.WorkOrder on gr.WONo equals wo.WONo
                                 where pe.BillNo == billNo
                                 select new { pe, pel, PDate = pe.PDate, PurRef = pe.PurRef, PurDescription = pe.PurDescription, supplierName = s.SupplierName, supplierGroup = sg.SgroupName, workorderid = wo.WONo/*, workorderno = wo.WOCode*/ }).ToList();

            if (purchaseentryline.Count > 0 && purchaseentry.Count > 0)
            {
                purchaseentryline.ForEach(x =>
                {
                    int wono = Convert.ToInt32(purchaseentry[0].workorderid);
                    int itemid = Convert.ToInt32(x.pel.ItemId);
                    x.pel.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.pel.ItemId).Name;
                    //x.pel.BusinessUnitName = context.BussinessUnit.FirstOrDefault(b => b.ID == x.pel.BusinessUnit).Name;
                    x.pel.Unit = context.UOM.FirstOrDefault(y => y.Id == x.pel.UOMId).Name;
                    GoodsReceiveNoteLineItem receiveNoteLineItem = context.GoodsReceiveNoteLineItem.FirstOrDefault(l => l.GRNNo == x.pel.GRNNo && l.ItemID == itemid);
                    if (receiveNoteLineItem != null)
                    {
                        WorkOrderLineItem workOrderLineItem = context.WorkOrderLineItem.FirstOrDefault(w => w.WONo == wono && w.ItemID == itemid);
                        if (workOrderLineItem != null)
                        {
                            x.pel.Vat = Convert.ToDouble(receiveNoteLineItem.CostVal) * (workOrderLineItem.VatPerc / 100);
                            x.pel.Ait = Convert.ToDouble(receiveNoteLineItem.CostVal) * (workOrderLineItem.AitPerc / 100);
                            x.pel.VDA = Convert.ToDouble(receiveNoteLineItem.CostVal) * (workOrderLineItem.VDAPerc / 100);
                        }
                    }
                    else
                    {
                        x.pel.Vat = 0;
                        x.pel.Ait = 0;
                        x.pel.VDA = 0;
                    }
                });
                return Json(new { Message = 1, purchaseentry, purchaseentryline }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveGPB(PurchaseEntry purchaseEntry, List<PurchaseLineItems> addedItems)
        {
            string billNo = "";
            string yearLastTwoPart = purchaseEntry.PDate.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.PurchaseEntry.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.PurchaseEntry.ToList().LastOrDefault(x => x.BillNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastInvoice == null)
                {
                    billNo = yearLastTwoPart + "00001";
                }
                else
                {
                    billNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.BillNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                billNo = yearLastTwoPart + "00001";
            }

            purchaseEntry.BillNo = Convert.ToInt32(billNo);
            purchaseEntry.PDate = purchaseEntry.PDate.Add(DateTime.Now.TimeOfDay);

            addedItems.ForEach(x =>
            {
                x.BillNo = purchaseEntry.BillNo;
                x.GroupId = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemId).ParentId;
                context.PurchaseLineItem.Add(x);


            });
            var purchaseLineGrn = addedItems.DistinctBy(x => x.GRNNo).ToList();
            purchaseLineGrn.ForEach(x =>
            {
                PurchaseLineGRN plg = new PurchaseLineGRN();
                plg.BillNo = x.BillNo;
                plg.GRNNo = x.GRNNo;
                context.PurchaseLineGRN.Add(plg);
            });
            context.PurchaseEntry.Add(purchaseEntry);

            //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);

            AccountModuleBridge.InsertUpdateFromGoodPurchaseBill(ref context, purchaseEntry);
            SupplierBalanceCalculationBridge.InsertUpdateFromGoodPurchaseBill(ref context, purchaseEntry);

            UserLog.SaveUserLog(ref context, new UserLog(purchaseEntry.BillNo.ToString(), DateTime.Now, "GPB", ScreenAction.Save));
            int i = context.SaveChanges();
            if (i > 0)
                return Json(new { purchaseEntry.BillNo, Message = 1 }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { purchaseEntry.BillNo, Message = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGPB(PurchaseEntry purchaseEntry, List<PurchaseLineItems> addedItems)
        {
            if (purchaseEntry.BillNo > 0)
            {
                var existPurchaseEntry = context.PurchaseEntry.FirstOrDefault(x => x.BillNo == purchaseEntry.BillNo);
                var purchaselineitems = context.PurchaseLineItem.Where(x => x.BillNo == purchaseEntry.BillNo);
                var purchaselinegrnlist = context.PurchaseLineGRN.Where(x => x.BillNo == purchaseEntry.BillNo);
                context.PurchaseEntry.Remove(existPurchaseEntry);
                context.PurchaseLineItem.RemoveRange(purchaselineitems);
                context.PurchaseLineGRN.RemoveRange(purchaselinegrnlist);
                purchaseEntry.PDate = purchaseEntry.PDate.Add(DateTime.Now.TimeOfDay);
                addedItems.ForEach(x =>
                {
                    x.BillNo = purchaseEntry.BillNo;
                    x.GroupId = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemId).ParentId;
                    context.PurchaseLineItem.Add(x);


                });
                var purchaseLineGrn = addedItems.DistinctBy(x => x.GRNNo).ToList();
                purchaseLineGrn.ForEach(x =>
                {
                    PurchaseLineGRN plg = new PurchaseLineGRN();
                    plg.BillNo = x.BillNo;
                    plg.GRNNo = x.GRNNo;
                    context.PurchaseLineGRN.Add(plg);
                });


                context.PurchaseEntry.Add(purchaseEntry);

                UserLog.SaveUserLog(ref context, new UserLog(purchaseEntry.BillNo.ToString(), DateTime.Now, "GPB", ScreenAction.Update));
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);

                AccountModuleBridge.InsertUpdateFromGoodPurchaseBill(ref context, purchaseEntry);
                SupplierBalanceCalculationBridge.InsertUpdateFromGoodPurchaseBill(ref context, purchaseEntry);
                int i = context.SaveChanges();
                if (i > 0)
                {
                    return Json(new { purchaseEntry.BillNo, Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { purchaseEntry.BillNo, Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(new { purchaseEntry.BillNo, Message = 0 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteGPB(int id)
        {
            var gpbExist = context.PurchaseEntry.FirstOrDefault(x => x.BillNo == id);
            if (gpbExist != null)
            {
                //var grn = context.PurchaseLineGRN.FirstOrDefault(x => x.BillNo == id);
                //context.PurchaseLineGRN.Remove(grn);
                context.PurchaseEntry.Remove(gpbExist);
                context.PurchaseLineItem.Where(x => x.BillNo == id).ToList().ForEach(x =>
                {
                    context.PurchaseLineItem.Remove(x);
                });
                context.PurchaseLineGRN.Where(x => x.BillNo == id).ToList().ForEach(x =>
                {
                    context.PurchaseLineGRN.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "GPB", ScreenAction.Delete));
                SupplierBalanceCalculationBridge.DeleteFromGPB(ref context, gpbExist);
                AccountModuleBridge.DeleteFromGPB(ref context, id);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PrintBill(int billNo)
        {
            if (billNo > 0)
            {
                ReportParmPersister param = new ReportParmPersister();
                var postedBy= (from p in context.UserLog
                               join c in context.Employees on p.LogInName equals c.LogInUserName
                               where p.ScreenName == "GPB" && p.TrnasId == billNo.ToString()
                               select c).ToList().FirstOrDefault();
                if (postedBy != null)
                {
                    param.PostedBy = postedBy.Name;
                }
                else
                {
                    param.PostedBy = "";
                }

                //param.PostedBy = (from p in context.UserLog
                //                  join c in context.Employees on p.LogInName equals c.LogInUserName
                //                  where p.ScreenName == "GPB" && p.TrnasId == billNo.ToString()
                //                  select c).ToList().FirstOrDefault().Name;

                string sql = string.Format("exec GoodsPurchaseBillPreview " + billNo + "");
                var items = context.Database.SqlQuery<GoodsPurchaseBillPreview>(sql).ToList();
                if (items.Count == 0)
                {
                    GoodsPurchaseBillPreview report = new GoodsPurchaseBillPreview();
                    items.Add(report);
                }
                GoodsPurchaseBillPreviewR rd = ShowReport(param, items);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                return new FileStreamResult(stream, "application/pdf");
            }
            else
                return Json("Invalid Bill No", JsonRequestBehavior.AllowGet);
        }
        public GoodsPurchaseBillPreviewR ShowReport(ReportParmPersister persister, List<GoodsPurchaseBillPreview> data)
        {
            GoodsPurchaseBillPreviewR GoodsPurchaseBillPreview = new GoodsPurchaseBillPreviewR();
            GoodsPurchaseBillPreview.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\GoodsPurchaseBillPreviewR.rpt");
            GoodsPurchaseBillPreview.Load(APPPATH);
            GoodsPurchaseBillPreview.SetDataSource(data);
            GoodsPurchaseBillPreview.SetParameterValue("postedBy", persister.PostedBy);
            //GoodsPurchaseBillPreview.SetParameterValue("compName", persister.CompName);
            //GoodsPurchaseBillPreview.SetParameterValue("compAddress", persister.CompAddress);
            //if (persister.Fax != "" || persister.Fax != null)
            //    GoodsPurchaseBillPreview.SetParameterValue("compContact", "Phone : " + persister.CompContact + " Fax : " + persister.Fax);
            //else
            //    GoodsPurchaseBillPreview.SetParameterValue("compContact", "Phone : " + persister.CompContact);
            //if (persister.FactAddress != "" || persister.FactAddress != null)
            //    GoodsPurchaseBillPreview.SetParameterValue("factoryAddress", "Factory : " + persister.FactAddress + " Phone : " + persister.FactContact);
            //else
            //    GoodsPurchaseBillPreview.SetParameterValue("factoryAddress", " ");
            return GoodsPurchaseBillPreview;
        }
    }
}
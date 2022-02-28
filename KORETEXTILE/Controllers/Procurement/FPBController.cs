using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Procurement;
using BEEERP.Models.StoreRM.GRN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using BEEERP.Models.Bridge;
using System.Collections;
using BEEERP.Models;
using BEEERP.Models.Log;
using BEEERP.CrystalReport.ReportFormat;
using System.IO;
using BEEERP.CrystalReport.ReportRdlc;

namespace BEEERP.Controllers.Procurement
{
    [ShowNotification]
    public class FPBController : Controller
    {
        
        BEEContext context = new BEEContext();
        
        public ActionResult FPB()
        {
            BEEContext context = new BEEContext();
            ViewBag.DebitAccount = LoadComboBox.LoadDebitAccount();

            return View();
        }
        public ActionResult GetFRNInfo(int id)
        {
            int frnNo = id;
            var isAlredyExist = context.FPBLineFRN.FirstOrDefault(x => x.FRNNo == frnNo);
            if (isAlredyExist != null)
            {
                return Json(new { Message = 2, FRNo = frnNo, FPBNo = isAlredyExist.BillNo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var isFrnExist = context.GoodsReceiveNote.Any(x => x.GRNNo == frnNo);
                
                if (isFrnExist)
                {
                    var frnDetails = (from gl in context.GoodsReceiveNoteLineItem
                                      join g in context.GoodsReceiveNote on gl.GRNNo equals g.GRNNo
                                      where gl.GRNNo == frnNo && g.Type=="FWO"
                                      select new { g, gl }).ToList();

                    List<GoodsReceiveNoteLineItem> frnLineL = new List<GoodsReceiveNoteLineItem>();
                    frnDetails.ForEach(x =>
                    {
                        GoodsReceiveNoteLineItem frnLine = new GoodsReceiveNoteLineItem();
                        frnLine.GRNNo = x.gl.GRNNo;
                        frnLine.ItemID = x.gl.ItemID;
                        frnLine.ItemName = context.FreezerModelDescription.FirstOrDefault(z => z.ID == frnLine.ItemID).Description;
                        frnLine.UOMID = x.gl.UOMID;
                        frnLine.Unit = context.UOM.FirstOrDefault(y => y.Id == x.gl.UOMID).Name;
                        frnLine.Qty = x.gl.Qty;
                        frnLine.CostRt = x.gl.CostRt;
                        frnLine.CostVal = x.gl.CostVal;
                        frnLineL.Add(frnLine);
                    });
                    int WONO = frnDetails.FirstOrDefault().g.WONo;
                    int supplierId = frnDetails.FirstOrDefault().g.SupplierID;
                    var supplier = context.Supplier.FirstOrDefault(x => x.Id == supplierId);
                    string supplierName = supplier.SupplierName;
                    string supplierGroup = context.SupplierGroups.FirstOrDefault(x => x.SgroupId == supplier.GroupID).SgroupName;
                    int workorderid = 0;
                    int workorderno = 0;
                    var workOrder = context.FreezerWorkOrder.Where(w => w.WONo == WONO).FirstOrDefault();
                    if (workOrder != null)
                    {
                        workorderid = workOrder.WONo;
                        workorderno = workOrder.WOCode;
                    }
                    return Json(new { Message = 1, frnLineL, supplierId, supplierName, supplierGroup, WorkOrderId = workorderid, WorkOrderCode = workorderno }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 3 }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public ActionResult SaveFPB(FPB fpb, List<FPBLine> addedItems)
        {
            string billNo = "";
            string yearLastTwoPart = fpb.PDate.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.FPB.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.FPB.ToList().LastOrDefault(x => x.BillNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
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

            fpb.BillNo = Convert.ToInt32(billNo);
            fpb.PDate = fpb.PDate.Add(DateTime.Now.TimeOfDay);

            addedItems.ForEach(x =>
            {
                x.Vat = 0;
                x.Ait = 0;
                x.BillNo = fpb.BillNo;
                x.GroupId = 0;
                context.FPBLine.Add(x);
            });
            var fpbLineFrn = addedItems.DistinctBy(x => x.FRNNo).ToList();
            fpbLineFrn.ForEach(x =>
            {
                FPBLineFRN flf = new FPBLineFRN();
                flf.BillNo = x.BillNo;
                flf.FRNNo = x.FRNNo;
                context.FPBLineFRN.Add(flf);
            });

            
            context.FPB.Add(fpb);

            UserLog.SaveUserLog(ref context, new UserLog(fpb.BillNo.ToString(), DateTime.Now, "FPB", ScreenAction.Save));
            AccountModuleBridge.InsertUpdateFromFreezerPurchaseBill(ref context, fpb);
            SupplierBalanceCalculationBridge.InsertUpdateFromFreezerPurchaseBill(ref context, fpb);
            int i = context.SaveChanges();
            if (i > 0)
                return Json(new { fpb.BillNo, Message = 1 }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { fpb.BillNo, Message = 0 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetFPBDetails(int id)
        {
            int billNo = id;
            var fpbLine = (from pel in context.FPBLine
                                     join pe in context.FPB on pel.BillNo equals pe.BillNo
                                     where pe.BillNo == billNo
                                     select new { pel }).ToList();
            var fpb = (from pe in context.FPB
                                 join pel in context.FPBLine on pe.BillNo equals pel.BillNo
                                 join s in context.Supplier on pe.SupplierId equals s.Id
                                 join sg in context.SupplierGroups on s.GroupID equals sg.SgroupId
                                 join gr in context.GoodsReceiveNote on pel.FRNNo equals gr.GRNNo
                                 join grl in context.GoodsReceiveNoteLineItem on pel.FRNNo equals grl.GRNNo
                                 join wo in context.FreezerWorkOrder on gr.WONo equals wo.WONo
                                 where pe.BillNo == billNo
                                 select new { pe, pel, PDate = pe.PDate, PurRef = pe.PurRef, DebitAcc=pe.DebtAccID, PurDescription = pe.PurDescription, supplierName = s.SupplierName, supplierGroup = sg.SgroupName, workorderid = wo.WONo, workorderno = wo.WOCode }).ToList();
            
                fpbLine.ForEach(x =>
                {
                    int wono = Convert.ToInt32(fpb[0].workorderid);
                    int itemid = Convert.ToInt32(x.pel.ItemId);
                    x.pel.ItemName = context.FreezerModelDescription.FirstOrDefault(z => z.ID == itemid).Description;
                    x.pel.Unit = context.UOM.FirstOrDefault(y => y.Id == x.pel.UOMId).Name;
                    
                });
            if (fpb.Count==0)
            {
                return Json(new { Message = 0, fpb, fpbLine }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = 1, fpb, fpbLine }, JsonRequestBehavior.AllowGet);
            }
                
        }


        public ActionResult DeleteFPB(int id)
        {
            var fpbExist = context.FPB.FirstOrDefault(x => x.BillNo == id);
            if (fpbExist != null)
            {
                //var grn = context.PurchaseLineGRN.FirstOrDefault(x => x.BillNo == id);
                //context.PurchaseLineGRN.Remove(grn);
                context.FPB.Remove(fpbExist);
                context.FPBLine.Where(x => x.BillNo == id).ToList().ForEach(x =>
                {
                    context.FPBLine.Remove(x);
                });
                context.FPBLineFRN.Where(x => x.BillNo == id).ToList().ForEach(x =>
                {
                    context.FPBLineFRN.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "FPB", ScreenAction.Delete));
                SupplierBalanceCalculationBridge.DeleteFromFPB(ref context, fpbExist);
                AccountModuleBridge.DeleteFromFPB(ref context, id);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateFPB(FPB fpb, List<FPBLine> addedItems)
        {
            if (fpb.BillNo > 0)
            {
                var existfpb = context.FPB.FirstOrDefault(x => x.BillNo == fpb.BillNo);
                var fpblineitems = context.FPBLine.Where(x => x.BillNo == fpb.BillNo);
                var fpblinefrnlist = context.FPBLineFRN.Where(x => x.BillNo == fpb.BillNo);
                context.FPB.Remove(existfpb);
                context.FPBLine.RemoveRange(fpblineitems);
                context.FPBLineFRN.RemoveRange(fpblinefrnlist);
                fpb.PDate = fpb.PDate.Add(DateTime.Now.TimeOfDay);
                addedItems.ForEach(x =>
                {
                    x.BillNo = fpb.BillNo;
                    x.GroupId = LoadComboBox.GetItemInfofpb().FirstOrDefault(y => y.Id == x.ItemId).ParentId;
                    context.FPBLine.Add(x);


                });
                var frnLinefrn = addedItems.DistinctBy(x => x.FRNNo).ToList();
                frnLinefrn.ForEach(x =>
                {
                    FPBLineFRN plg = new FPBLineFRN();
                    plg.BillNo = x.BillNo;
                    plg.FRNNo = x.FRNNo;
                    context.FPBLineFRN.Add(plg);
                });


                context.FPB.Add(fpb);

                UserLog.SaveUserLog(ref context, new UserLog(fpb.BillNo.ToString(), DateTime.Now, "FPB", ScreenAction.Update));
                //InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);

                AccountModuleBridge.InsertUpdateFromFreezerPurchaseBill(ref context, fpb);
                SupplierBalanceCalculationBridge.InsertUpdateFromFreezerPurchaseBill(ref context, fpb);
                int i = context.SaveChanges();
                if (i > 0)
                {
                    return Json(new { fpb.BillNo, Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { fpb.BillNo, Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(new { fpb.BillNo, Message = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintBill(int FPBNo)
        {
            if (FPBNo > 0)
            {
                ReportParmPersister param = new ReportParmPersister();
                var postedBy = (from p in context.UserLog
                                join c in context.Employees on p.LogInName equals c.LogInUserName
                                where p.ScreenName == "FPB" && p.TrnasId == FPBNo.ToString()
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

                string sql = string.Format("exec spFPBPreview " + FPBNo + "");
                var items = context.Database.SqlQuery<FreezerPurchaseBillPreview>(sql).ToList();
                if (items.Count == 0)
                {
                    FreezerPurchaseBillPreview report = new FreezerPurchaseBillPreview();
                    items.Add(report);
                }
                FreezerPurchaseBillPreviewR rd = ShowReport(param, items);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                return new FileStreamResult(stream, "application/pdf");
            }
            else
                return Json("Invalid Bill No", JsonRequestBehavior.AllowGet);
        }
        public FreezerPurchaseBillPreviewR ShowReport(ReportParmPersister persister, List<FreezerPurchaseBillPreview> data)
        {
            FreezerPurchaseBillPreviewR FreezerPurchaseBillPreview = new FreezerPurchaseBillPreviewR();
            FreezerPurchaseBillPreview.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\FreezerPurchaseBillPreviewR.rpt");
            FreezerPurchaseBillPreview.Load(APPPATH);
            FreezerPurchaseBillPreview.SetDataSource(data);
            FreezerPurchaseBillPreview.SetParameterValue("postedBy", persister.PostedBy);
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
            return FreezerPurchaseBillPreview;
        }
    }
}
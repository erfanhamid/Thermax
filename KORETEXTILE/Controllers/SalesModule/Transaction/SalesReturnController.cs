using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.CrystalReport.ReportFormat;
using System.IO;
using BEEERP.Models.Notification;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace BEEERP.Controllers.SalesModule.Transaction
{
    [ShowNotification]
    [SaleModule]
    [Permission]
    public class SalesReturnController : Controller
    {
        //BEEContext context = new BEEContext();
        //// GET: SalesReturn
        //public ActionResult SalesReturn(int rvno = 0, string type = "")
        //{
        //    // ViewBag.Item = LoadInvoiceItem(null);

        //    //if(rvno != 0)
        //    //{
        //    //    ViewBag.Id = rvno;
        //    //}
        //    return View();
        //}

        //public ActionResult GetInvoiceItemById(int id) {

        //    var sales = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == id);
        //    if (sales != null)
        //    {
        //        //List<SalesEntryNewLineItem> salelineItem = new List<SalesEntryNewLineItem>();
        //        var salesLineItem = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == id).ToList();
        //        //salesLineItem.ForEach(x =>
        //        //{
        //        //    int rtnQty = 0;
        //        //    var returnQty = (from s in context.SalesReturnEntrie join 
        //        //                     sl in context.SalesReturnLineItem on s.SRNo equals sl.SRNo where(s.InvoiceNo == x.InvoiceNo) && (sl.ItemID == x.ItemID)
        //        //                     select sl).ToList();
        //        //    if(returnQty != null)
        //        //    {
        //        //        rtnQty = returnQty.Sum(y => y.ReturnQty);
        //        //    }
        //        //    else
        //        //    {
        //        //        rtnQty = 0;
        //        //    }
        //        //    if (rtnQty < x.Qty)
        //        //    {
        //        //        x.Qty = rtnQty;
        //        //        salelineItem.Add(x);
        //        //    }

        //        //});
        //        //return Json(LoadInvoiceItem(salelineItem), JsonRequestBehavior.AllowGet);
        //        //    return Json(LoadInvoiceItem(salesLineItem), JsonRequestBehavior.AllowGet);
        //        //}
        //        //else
        //        //{
        //        //    return Json(0, JsonRequestBehavior.AllowGet);
        //        //}
        //    }

        //    public ActionResult GetInvoiceById(int id, int invoice) {

        //        var salesLineItem = context.SalesEntryNewLineItem.FirstOrDefault(x => x.InvoiceNo == invoice && x.ItemID == id);
        //        double rtnQty = 0;
        //        double rtnOfferQty = 0;
        //        var returnQty = (from s in context.SalesReturnEntrie join
        //                         sl in context.SalesReturnLineItem on s.SRNo equals sl.SRNo where (s.InvoiceNo == invoice) && (sl.ItemID == id)
        //                         select sl).ToList();
        //        if (returnQty != null)
        //        {
        //            rtnQty = returnQty.Sum(y => y.ReturnQty);
        //            rtnOfferQty = (double)returnQty.Sum(y => y.RtnOfferQty);
        //        }
        //        else
        //        {
        //            rtnQty = 0;
        //            rtnOfferQty = 0;
        //        }
        //        if (rtnQty <= salesLineItem.Qty)
        //        {
        //            salesLineItem.Qty = salesLineItem.Qty - rtnQty;
        //        }

        //        if (rtnOfferQty <= salesLineItem.OfferQty)
        //        {
        //            salesLineItem.OfferQty = salesLineItem.OfferQty - rtnOfferQty;
        //        }

        //        return Json(salesLineItem, JsonRequestBehavior.AllowGet);
        //    }

        //    //public ActionResult SaveSalesReturnOrder(SalesReturnEntry salesReturn, List<SalesReturnLineItem> addedItems)
        //    //{
        //    //    using (context)
        //    //    {
        //    //        var SRNoCount = context.SalesReturnEntrie.Count();
        //    //        var lastSRNo = 0;
        //    //        if (SRNoCount > 0)
        //    //        {
        //    //            lastSRNo = context.SalesReturnEntrie.Max(x => x.SRNo);
        //    //            lastSRNo += 1;
        //    //        }
        //    //        else
        //    //        {
        //    //            lastSRNo = 1;
        //    //        }

        //    //        salesReturn.SRNo = lastSRNo;
        //    //        salesReturn.IsPrevious = false;
        //    //        var sales = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == salesReturn.InvoiceNo);
        //    //        salesReturn.CustomerId = sales.CustomerID;
        //    //        salesReturn.TSO = context.Customers.FirstOrDefault(x => x.Id == sales.CustomerID).TSOId;
        //    //        salesReturn.BranchId = sales.BranchId;
        //    //        var salesLineItem = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == salesReturn.InvoiceNo).FirstOrDefault();
        //    //        salesReturn.Store = salesLineItem.StoreID;
        //    //        salesReturn.SRDate = salesReturn.SRDate.Add(DateTime.Now.TimeOfDay);

        //    //        UserNotification notification = new UserNotification();
        //    //        notification.UserId = User.Identity.GetUserId();
        //    //        notification.Type = "SalesReturnEntry";
        //    //        notification.TransactionNo = lastSRNo.ToString();
        //    //        notification.NotificationHead = "Sales Return";
        //    //        notification.NotificationDetails = "This Sales Return Order is Pending.";
        //    //        notification.PostingDate = DateTime.Now;
        //    //        notification.BranchId = salesReturn.BranchId;

        //    //        decimal standardCost = (decimal)0.0;

        //    //        addedItems.ForEach(x =>
        //    //        {
        //    //            x.SRNo = salesReturn.SRNo;
        //    //            context.SalesReturnLineItem.Add(x);
        //    //            var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
        //    //            standardCost +=Convert.ToDecimal(item.clmStandardCost * (x.OfferQty + x.ReturnQty));
        //    //        });

        //    //        salesReturn.TotalCOGS = standardCost;
        //    //        context.SalesReturnEntrie.Add(salesReturn);

        //    //        UserLog.SaveUserLog(ref context, new UserLog(salesReturn.SRNo.ToString(), DateTime.Now, "SalesReturnEntry", ScreenAction.Save));
        //    //        context.Notification.Add(notification);

        //    //        //InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, salesReturn, addedItems);
        //    //        //AccountModuleBridge.InsertFromSalesReturn(ref context,salesReturn);
        //    //        //CustomerCalculationBridge.InsertFromSalesReturnEntry(ref context, salesReturn);
        //    //        context.SaveChanges();
        //    //        return Json(new { srNo = salesReturn.SRNo }, JsonRequestBehavior.AllowGet);
        //    //    }
        //    //}

        //    //public ActionResult SaveSalesReturn(SalesReturnEntry salesReturn, List<SalesReturnLineItem> addedItems)
        //    //{

        //    //    var receiveV = context.SalesReturnEntrie.AsNoTracking().FirstOrDefault(x => x.SRNo == salesReturn.SRNo);

        //    //    if (receiveV != null)
        //    //    {
        //    //        var notificationE = context.Notification.FirstOrDefault(x => x.Type == "SalesReturnEntry" && x.TransactionNo == receiveV.SRNo.ToString());
        //    //        if (notificationE != null)
        //    //        {
        //    //            notificationE.NotificationDetails = "This Sales Return is approved.";
        //    //            notificationE.UserId = User.Identity.GetUserId();
        //    //            context.Entry<UserNotification>(notificationE).State = EntityState.Modified;

        //    //            context.ApprovalLog.Where(x => x.NotificationId == notificationE.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });

        //    //            var salesReturnLineItemExist = context.SalesReturnLineItem.Where(x => x.SRNo == salesReturn.SRNo).ToList();
        //    //            salesReturnLineItemExist.ForEach(x =>
        //    //            {
        //    //                context.SalesReturnLineItem.Remove(x);
        //    //            });

        //    //            decimal standardCost = (decimal)0.0;
        //    //            addedItems.ForEach(x =>
        //    //            {
        //    //                x.SRNo = salesReturn.SRNo;
        //    //                context.SalesReturnLineItem.Add(x);
        //    //                var item = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID);
        //    //                standardCost += Convert.ToDecimal(item.clmStandardCost * (x.OfferQty + x.ReturnQty));

        //    //            });

        //    //            salesReturn.TotalCOGS = standardCost;
        //    //            salesReturn.SRDate = salesReturn.SRDate.Add(DateTime.Now.TimeOfDay);
        //    //            context.Entry<SalesReturnEntry>(salesReturn).State = EntityState.Modified;
        //    //        }
        //    //        salesReturn.SRNo = salesReturn.SRNo;
        //    //        salesReturn.IsPrevious = false;
        //    //        var sales = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == salesReturn.InvoiceNo);
        //    //        salesReturn.CustomerId = sales.CustomerID;
        //    //        salesReturn.TSO = context.Customers.FirstOrDefault(x => x.Id == sales.CustomerID).TSOId;
        //    //        salesReturn.BranchId = sales.BranchId;
        //    //        var salesLineItem = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == salesReturn.InvoiceNo).FirstOrDefault();
        //    //        salesReturn.Store = salesLineItem.StoreID;
        //    //        salesReturn.SRDate = salesReturn.SRDate.Add(DateTime.Now.TimeOfDay);
        //    //        CustomerCalculationBridge.InsertFromSalesReturnEntry(ref context, salesReturn);
        //    //        addedItems.ForEach(x =>
        //    //        {
        //    //            x.ClmCostPrice = LoadComboBox.GetItemUnitCostReturn(x.ItemID, "SalesReturn", Convert.ToDecimal(x.ReturnQty), salesReturn.InvoiceNo, salesReturn.Store, false);
        //    //        });
        //    //        InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, salesReturn, addedItems);
        //    //        AccountModuleBridge.InsertFromSalesReturn(ref context, salesReturn);

        //    //        //addedItems.ForEach(x =>
        //    //        //{
        //    //        //    x.SRNo = salesReturn.SRNo;
        //    //        //    x.ClmCostPrice = LoadComboBox.GetItemUnitCostReturn(x.ItemID, "SalesReturn", Convert.ToDecimal(x.ReturnQty), salesReturn.InvoiceNo, salesReturn.Store, false);
        //    //        //    context.SalesReturnLineItem.Add(x);
        //    //        //});
        //    //        UserLog.SaveUserLog(ref context, new UserLog(salesReturn.SRNo.ToString(), DateTime.Now, "SalesReturn", ScreenAction.Approve));
        //    //        context.SaveChanges();
        //    //        return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
        //    //    }

        //    //    else
        //    //    {
        //    //        return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
        //    //    }

        //    //salesReturn.TotalCOGS = standardCost;
        //    //    context.SalesReturnEntrie.Add(salesReturn);

        //    //    UserLog.SaveUserLog(ref context, new UserLog(salesReturn.SRNo.ToString(), DateTime.Now, "SalesReturn", ScreenAction.Save));
        //    //    var updateNotif = context.Notification.FirstOrDefault(x => x.TransactionNo == salesReturn.SRNo.ToString() && x.Type == "SalesReturnEntry");
        //    //    context.ApprovalLog.Where(x => x.NotificationId == updateNotif.Id).ToList().ForEach(x => { x.IsApproved = true; context.Entry<ApprovalLog>(x).State = EntityState.Modified; });
        //    //    InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, salesReturn, addedItems);
        //    //    AccountModuleBridge.InsertFromSalesReturn(ref context, salesReturn);
        //    //    CustomerCalculationBridge.InsertFromSalesReturnEntry(ref context, salesReturn);
        //    //    context.SaveChanges();
        //    //    return Json(new { srNo = salesReturn.SRNo }, JsonRequestBehavior.AllowGet);

        //}

        //public ActionResult GetSalesReturnById(int id)
        //{
        //    var srItem = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == id);
        //    double rtnQty = 0;
        //    double rtnOfferQty = 0;
        //    if (srItem != null)
        //    {
        //        if (srItem.IsPrevious == false)
        //        {
        //            var srLineItem = context.SalesReturnLineItem.Where(x => x.SRNo == id).ToList();
        //            srLineItem.ForEach(x =>
        //            {
        //                var item = context.SalesReturnLineItem.FirstOrDefault(y => y.SRNo == x.SRNo && y.ItemID == x.ItemID);
        //                var salesLineItem = context.SalesEntryNewLineItem.FirstOrDefault(z => z.InvoiceNo == srItem.InvoiceNo && z.ItemID == x.ItemID);
        //                var chartOfInv = context.ChartOfInventory.ToList().FirstOrDefault(z => z.Id == x.ItemID);
        //                var returnQty = (from s in context.SalesReturnEntrie
        //                                 join
        //                                 sl in context.SalesReturnLineItem on s.SRNo equals sl.SRNo
        //                                 where (s.InvoiceNo == srItem.InvoiceNo) && (sl.ItemID == x.ItemID)
        //                                 select sl).ToList();
        //                if (returnQty != null)
        //                {
        //                    rtnQty = returnQty.Sum(y => y.ReturnQty);
        //                    rtnOfferQty = (double)returnQty.Sum(y => y.RtnOfferQty);
        //                }
        //                else
        //                {
        //                    rtnQty = 0;
        //                    rtnOfferQty = 0;
        //                }
        //                x.ItemName = chartOfInv.Name;
        //                x.SalesQty = salesLineItem.Qty - (rtnQty - item.ReturnQty);
        //                x.OfferQty = salesLineItem.OfferQty - (rtnOfferQty - (double)item.RtnOfferQty);
        //            });
        //            return Json(new { srItem, srLineItem }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        return Json("0", JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult UpdateSalesReturn(SalesReturnEntry salesReturn, List<SalesReturnLineItem> addedItems)
        //{
        //    var srExist = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == salesReturn.SRNo);
        //    if (srExist != null)
        //    {
        //        context.SalesReturnEntrie.Remove(srExist);

        //        context.SalesReturnLineItem.Where(x => x.SRNo == salesReturn.SRNo).ToList().ForEach(x => {
        //            context.SalesReturnLineItem.Remove(x);
        //        });

        //        var sales = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == salesReturn.InvoiceNo);
        //        salesReturn.CustomerId = sales.CustomerID;
        //        salesReturn.TSO = context.Customers.FirstOrDefault(x => x.Id == sales.CustomerID).TSOId;
        //        salesReturn.BranchId = sales.BranchId;
        //        var salesLineItem = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == salesReturn.InvoiceNo).FirstOrDefault();
        //        salesReturn.Store = salesLineItem.StoreID;
        //        salesReturn.SRDate = salesReturn.SRDate.Add(DateTime.Now.TimeOfDay);

        //        addedItems.ForEach(x =>
        //        {
        //            x.SRNo = salesReturn.SRNo;
        //            x.ClmCostPrice = LoadComboBox.GetItemUnitCostReturn(x.ItemID, "SalesReturn", Convert.ToDecimal(x.ReturnQty), salesReturn.InvoiceNo, salesReturn.Store, true);
        //            context.SalesReturnLineItem.Add(x);
        //        });

        //        context.SalesReturnEntrie.Add(salesReturn);
        //        UserLog.SaveUserLog(ref context, new UserLog(salesReturn.SRNo.ToString(), DateTime.Now, "SalesReturn", ScreenAction.Update));
        //        InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, salesReturn, addedItems);
        //        CustomerCalculationBridge.InsertFromSalesReturnEntry(ref context, salesReturn);
        //        context.SaveChanges();
        //        return Json(new { srNo = salesReturn.SRNo }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult DeleteSalesReturnByid(int id) {

        //    var srExist = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == id);
        //    if (srExist != null)
        //    {
        //        context.SalesReturnEntrie.Remove(srExist);
        //        var srLineItem = context.SalesReturnLineItem.Where(x => x.SRNo == id).ToList();
        //        context.SalesReturnLineItem.Where(x => x.SRNo == id).ToList().ForEach(x =>
        //        {
        //            context.SalesReturnLineItem.Remove(x);
        //        });
        //        var SalesR = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == id);
        //        var Stock = context.RemainingStock.Where(x => x.DocNo == SalesR.InvoiceNo && x.DocType == "SalesReturn").ToList();
        //        Stock.ForEach(x => {
        //            context.RemainingStock.Remove(x);
        //        });
        //        UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "SalesReturn", ScreenAction.Delete));
        //        InventoryTransactionBridge.InsertFromSalesRetrunEntry(ref context, srExist, new List<SalesReturnLineItem>());
        //        //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
        //        context.SaveChanges();
        //        return Json(id, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(0, JsonRequestBehavior.AllowGet);

        //}

        //public static SelectList LoadInvoiceItem(List<SalesEntryNewLineItem> sl = null)    
        //{
        //    if (sl == null)
        //    {
        //        Dictionary<string, string> items = new Dictionary<string, string>();
        //        items.Add("", "--- Select Item ---");
        //        return new SelectList(items, "Key", "Value");
        //    }
        //    else
        //    {
        //        BEEContext context = new BEEContext();
        //        Dictionary<string, string> items = new Dictionary<string, string>();
        //        items.Add("", "--- Select Item ---");
        //        sl.ForEach(x => { items.Add(x.ItemID.ToString(), FindObjectById.GetChartOfInventoryById(x.ItemID).Id + " - " + FindObjectById.GetChartOfInventoryById(x.ItemID).Name + " - " + FindObjectById.GetChartOfInventoryById(x.ItemID).PacSize); });
        //        return new SelectList(items, "Key", "Value");
        //    }

        //}
        //public FileResult SalesReturnPreview(int SRNo)
        //{
        //    Session["ReportName"] = "GrfPreviewR";

        //    ReportParmPersister param = new ReportParmPersister();
        //    var salesReturn = context.SalesReturnEntrie.FirstOrDefault(x => x.SRNo == SRNo);
        //    if (salesReturn != null)
        //    {
        //        if (salesReturn.IsPrevious)
        //        {
        //            param.PostedBy = (from e in context.Employees.ToList()
        //                              join
        //                              u in context.UserLog.ToList() on e.LogInUserName equals u.LogInName
        //                              where u.ScreenName == "SalesReturnPrevious" && u.Action == "Save" && u.TrnasId == salesReturn.SRNo.ToString()
        //                              select e.Name).FirstOrDefault();
        //        }
        //        else {
        //            param.PostedBy = (from e in context.Employees.ToList()
        //                              join
        //                              u in context.UserLog.ToList() on e.LogInUserName equals u.LogInName
        //                              where u.ScreenName == "SalesReturn" && u.Action == "Save" && u.TrnasId == salesReturn.SRNo.ToString()
        //                              select e.Name).FirstOrDefault();
        //        }


        //        param.SalesReturnNo = salesReturn.SRNo;
        //        param.SalesReturnDate = salesReturn.SRDate;
        //        param.TsoId = salesReturn.TSO;
        //        param.TsoName = context.Employees.FirstOrDefault(x => x.Id == salesReturn.TSO).Name;
        //        param.CustId = salesReturn.CustomerId.ToString();
        //        param.CustName = context.Customers.FirstOrDefault(x => x.Id == salesReturn.CustomerId).CustomerName;
        //        param.InvoiceNo = salesReturn.InvoiceNo;
        //    }
        //    else
        //    {
        //        param.SalesReturnNo = 0;
        //    }


        //    param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == salesReturn.BranchId).BrnachName;
        //    //string salesReturnPreview = "SalesReturn";


        //    Session["GrfPreview"] = param;
        //    string sql = string.Format("exec PreviewSalesReturn '" + salesReturn.SRNo + "'");
        //    var items = context.Database.SqlQuery<PreviewSalesRetrun>(sql).ToList();
        //    if (items.Count == 0)
        //    {
        //        PreviewSalesRetrun report = new PreviewSalesRetrun();
        //        items.Add(report);
        //    }
        //    SalesReturnPreview rd = ShowReport(param, items);
        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    rd.Close();
        //    return new FileStreamResult(stream, "application/pdf");
        //}

        //public SalesReturnPreview ShowReport(ReportParmPersister persister, List<PreviewSalesRetrun> data)
        //{
        //    SalesReturnPreview grfPreviewR = new SalesReturnPreview();

        //    grfPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
        //    String APPPATH = Server.MapPath(@"\CrystalReport\SalesReturnPreview.rpt");
        //    grfPreviewR.Load(APPPATH);
        //    grfPreviewR.SetDataSource(data);
        //    grfPreviewR.SetParameterValue("tsoId", persister.TsoId);
        //    grfPreviewR.SetParameterValue("TsoName", persister.TsoName);
        //    grfPreviewR.SetParameterValue("CustomerId", persister.CustId); 
        //    grfPreviewR.SetParameterValue("CustomerName", persister.CustName);
        //    grfPreviewR.SetParameterValue("InvoiceNo", persister.InvoiceNo);
        //    grfPreviewR.SetParameterValue("SalesRetrunNo", persister.SalesReturnNo);
        //    grfPreviewR.SetParameterValue("SalesReturnDate", persister.SalesReturnDate.ToString("dd-MM-yyyy"));
        //    grfPreviewR.SetParameterValue("salesCenter", persister.BranchName);
        //    grfPreviewR.SetParameterValue("postedBy", persister.PostedBy);


        //    grfPreviewR.SetParameterValue("compName", persister.CompName);
        //    grfPreviewR.SetParameterValue("compAddress", persister.CompAddress);
        //    grfPreviewR.SetParameterValue("compContact", persister.CompContact);
        //    grfPreviewR.SetParameterValue("compFax", persister.Fax);
        //    grfPreviewR.SetParameterValue("factAddress", persister.FactAddress);
        //    grfPreviewR.SetParameterValue("factContact", persister.FactContact);
        //    return grfPreviewR;
        //}
    }  
 }

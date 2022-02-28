using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CommercialModule.Import;
using System.Data.Entity;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.CommercialModule.Import
{
    [ShowNotification]
    [Permission]
    [AccountingModule]
    public class ImportLCController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ImportLC
        public ActionResult ImportLC()
        {
            var importLCs = context.ImportLC.Count();
            if (importLCs == 0)
            {
                ViewBag.ImportLCId = 1;
            }
            else
            {
                ViewBag.ImportLCId = context.ImportLC.Max(b => b.ILCId) + 1;
            }
            ViewBag.supplier = LoadComboBox.LoadSupplierForLC();
            ViewBag.ps = LoadComboBox.LoadPartialShipment();
            ViewBag.pi = LoadComboBox.LoadPI(0);
            
            return View();
        }
        public ActionResult GetIPIByIPIId(int IpiId)
        {
            
            if (IpiId == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var ipi = context.ImportProformaInvoices.FirstOrDefault(m => m.IPIId == IpiId);
               
                if (ipi == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ipilineItems = context.ImportProformaInvoiceLineItems.Where(m => m.IPIId == ipi.IPIId).ToList();
                    ipilineItems.ForEach
                    (x => { var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == x.ItemId); x.ItemName = item.Name;
                        x.GroupName = context.ChartOfInventory.FirstOrDefault(m => m.Id == item.parentId).Name;
                        x.GroupId = context.ChartOfInventory.FirstOrDefault(m => m.Id == item.parentId).Id; });
                    return Json(new { Message = 1, ipi = ipi, ipilineItems = ipilineItems }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult GetIPIBySupplierId(int supplierId)
        {
            return Json(LoadComboBox.LoadPI(supplierId), JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaveLC(ImportLC importLC, List<ImportLCLineItem> addedItems)
        {
            var importLCs = context.ImportLC.Count();
            if (importLCs == 0)
            {
                importLC.ILCId = 1;
            }
            else
            {
                importLC.ILCId = context.ImportLC.Max(b => b.ILCId) + 1;
            }
            importLC.ILCDate = importLC.ILCDate.Add(DateTime.Now.TimeOfDay);

            var LCExits = context.ImportLC.FirstOrDefault(x => x.ILCId == importLC.ILCId);
            if (LCExits == null)
            {
                //try
                //{
                    context.ImportLC.Add(importLC);
                    addedItems.ForEach(x => { x.ILCID = importLC.ILCId; context.ImportLCLineItem.Add(x); });
                    //UserLog.SaveUserLog(ref context, new UserLog(importLC.ILCId.ToString(), DateTime.Now, "ImportLC", ScreenAction.Save));
                    //LcBalanceCalculationBridge.InsertUpdateFromLCImport(ref context, importLC);
                    //AccountModuleBridge.InsertFromLC(ref context, importLC);

                    context.SaveChanges();
                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });

                //}
                //catch (Exception ex)
                //{
                //    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                //}

            }
            else
            {

                return Json(new { Message = 2, JsonRequestBehavior.AllowGet });

            }
        }
        public ActionResult GetLCByLCId(ImportLC importLC)
        {
           
            if (importLC.ILCId == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var lc = context.ImportLC.FirstOrDefault(m => m.ILCId ==importLC.ILCId);
                
                
                if (lc == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //var NameOfSupplier=context.Supplier.FirstOrDefault(n=>n.SupplierName)
                    lc.PINo = context.ImportProformaInvoices.FirstOrDefault(k => k.IPIId == lc.IPIId).IPINO;
                    //lc.NameOfSupplier = context.Supplier.FirstOrDefault(m => m.Id == lc.SupplierId).SupplierName;
                    var lclineItems = context.ImportLCLineItem.Where(m => m.ILCID == lc.ILCId).ToList();
                    lclineItems.ForEach
                    (x => { var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == x.ItemId); x.ItemName = item.Name;});
                    return Json(new { Message = 1, lc = lc, lclineItems = lclineItems }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }
        public ActionResult UpdateLC(ImportLC importLC, List<ImportLCLineItem> addedItems)
        {
            try
            {
                var isExist = context.ImportLC.AsNoTracking().FirstOrDefault(x => x.ILCId == importLC.ILCId);
                if (isExist == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var prevLC = context.ImportLCLineItem.Where(x => x.ILCID == importLC.ILCId).ToList();
                    prevLC.ForEach(x =>
                    {
                        context.ImportLCLineItem.Remove(x);
                    });
                    context.Entry<ImportLC>(importLC).State = EntityState.Modified;
                    addedItems.ForEach(x => { x.ILCID = importLC.ILCId; context.ImportLCLineItem.Add(x); });
                    UserLog.SaveUserLog(ref context, new UserLog(importLC.ILCId.ToString(), DateTime.Now, "ImportLC", ScreenAction.Update));
                    //LcBalanceCalculationBridge.InsertUpdateFromLCImport(ref context, importLC);
                    //AccountModuleBridge.InsertFromLC(ref context, importLC);
                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteLC(int lcId)
        {
            try
            {
                var prevLC = context.ImportLC.FirstOrDefault(x => x.ILCId == lcId);
                var lcInLCPayment = context.ILCP.FirstOrDefault(m => m.clmILCID == lcId);
                var lcInLTR = context.LTR.FirstOrDefault(m => m.LCID == lcId);
                var lcInLTRA = context.LTRA.FirstOrDefault(m => m.ILCId == lcId);
                if (prevLC==null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else if (lcInLCPayment!=null|| lcInLTR != null|| lcInLTRA != null)
                {
                    return Json(new { Message = 3 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var liLineItems = context.ImportLCLineItem.Where(m => m.ILCID == lcId).ToList();
                    context.ImportLC.Remove(prevLC);
                    liLineItems.ForEach(m => { context.ImportLCLineItem.Remove(m); });
                    UserLog.SaveUserLog(ref context, new UserLog(lcId.ToString(), DateTime.Now, "ImportLC", ScreenAction.Delete));
                   // LcBalanceCalculationBridge.DeleteFromLCImport(ref context, prevLC);
                    //AccountModuleBridge.DeleteFromLC(ref context, prevLC);
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public FileResult PrintLC(int ILCId)
        {
            
            Session["ReportName"] = "LcPreview";

            ReportParmPersister param = new ReportParmPersister();
            var lc = context.ImportLC.FirstOrDefault(x => x.ILCId == ILCId);
            param.ILCId = lc.ILCId;
            param.IALCNo = lc.IALCNO;
            param.ILCNo = lc.ILCNO;
            param.SupplierName = context.Supplier.FirstOrDefault(x=>x.Id==lc.SupplierId).SupplierName;
            var loginName=context.UserLog.FirstOrDefault(x => x.TrnasId == ILCId.ToString() && x.ScreenName == "ImportLC"&&x.Action=="Save").LogInName;
            param.PostedBy = context.Employees.FirstOrDefault(x => x.LogInUserName == loginName).Name;
            Session["LcPreview"] = param;

            string sql = "exec PreviewLCEntry " + ILCId + " ";
            var items = context.Database.SqlQuery<PreviewLcEntry>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewLcEntry report = new PreviewLcEntry();
                items.Add(report);
            }

            LCEntryPreview rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }
      

        public LCEntryPreview ShowReport(ReportParmPersister persister, List<PreviewLcEntry> data)
        {
            LCEntryPreview ILCBPreviewR = new LCEntryPreview();

            ILCBPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ILCBPreviewR.rpt");
            ILCBPreviewR.Load(APPPATH);
            ILCBPreviewR.SetDataSource(data);
            ILCBPreviewR.SetParameterValue("CompName", persister.CompName);
            ILCBPreviewR.SetParameterValue("CompAddress", persister.CompAddress);
            ILCBPreviewR.SetParameterValue("LcNo", persister.ILCNo);
            ILCBPreviewR.SetParameterValue("AlcNo", persister.IALCNo);
            ILCBPreviewR.SetParameterValue("SupplierName", persister.SupplierName);
            ILCBPreviewR.SetParameterValue("LCId", persister.ILCId.ToString());
            ILCBPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            return ILCBPreviewR;
        }
    }
}
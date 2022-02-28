using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;

namespace BEEERP.Controllers.CommercialModule.Import
{
    [ShowNotification]
    [Permission]
    [AccountingModule]
    public class ImportProformaInvoiceController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: ImportProformaInvoice
        public ActionResult ImportProformaInvoice()
        {
            var importPis = context.ImportProformaInvoices.Count();
            if(importPis==0)
            {
                ViewBag.numberOfIPI = 1;
            }
            else
            {
                ViewBag.numberOfIPI = context.ImportProformaInvoices.Max(b => b.IPIId) + 1;
            }
            ViewBag.MOP = LoadComboBox.LoadMOP();
            ViewBag.Incoterms = LoadComboBox.LoadIncoterms();
            ViewBag.LcType = LoadComboBox.LoadLCType();
            ViewBag.SupplierId = LoadComboBox.LoadAllImportSupplier();
            ViewBag.Ports = LoadComboBox.LoadPort();
            ViewBag.Company = LoadComboBox.LoadAllCompanyID();
            ViewBag.BeneficiaryBanks = LoadComboBox.LoadBeneficiaryBanks();
            ViewBag.IssuingBanks = LoadComboBox.LoadIssuingBanks();
            ViewBag.Currency = LoadComboBox.LoadCurrency();
            ViewBag.Customer = LoadComboBox.LoadAllCustomers();
            ViewBag.Group = LoadComboBox.LoadItemGroupfa();
            ViewBag.Items = LoadComboBox.LoadItem(null);
            ViewBag.Group = LoadComboBox.LoadAllSPGroup();
            //ViewBag.Items = LoadComboBox.LoadRmItem(null);
           // ViewBag.purchaseOrder = LoadComboBox.LoadPurchaseOrderBySupplier(0);
            ViewBag.Transhipment = LoadComboBox.LoadTranShipment();
            ViewBag.PartialShipment = LoadComboBox.LoadPartialShipment();
            return View();
        }
        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
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
        public ActionResult SaveIPI(ImportProformaInvoice importProformaInvoice,List<ImportProformaInvoiceLineItems> addedItems)
        {
            var importPis = context.ImportProformaInvoices.Count();
            if (importPis == 0)
            {
                importProformaInvoice.IPIId= 1;
            }
            else
            {
                importProformaInvoice.IPIId = context.ImportProformaInvoices.Max(b => b.IPIId) + 1;
            }
            importProformaInvoice.IPIDate = importProformaInvoice.IPIDate.Add(DateTime.Now.TimeOfDay);
            
            var IPIExits = context.ImportProformaInvoices.FirstOrDefault(x => x.IPIId == importProformaInvoice.IPIId);
            if (IPIExits == null)
            {
                //try
                //{
                    context.ImportProformaInvoices.Add(importProformaInvoice);
                    addedItems.ForEach(x => { x.IPIId = importProformaInvoice.IPIId; context.ImportProformaInvoiceLineItems.Add(x); });
                    //UserLog.SaveUserLog(ref context,new UserLog(importProformaInvoice.IPIId.ToString(),DateTime.Now, "ImportProformaInvoice",ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });

                //}
                //    catch (Exception ex)
                //{
                //    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                //}

        }
            else
            {

                return Json(new { Message = 2, JsonRequestBehavior.AllowGet });

            }
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
                    (x => { var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == x.ItemId); x.ItemName = item.Name;x.GroupName =context.ChartOfInventory.FirstOrDefault(m=>m.Id==item.parentId).Name; x.GroupId = context.ChartOfInventory.FirstOrDefault(m => m.Id == item.parentId).Id; });
                    return Json(new { Message = 1, ipi = ipi, ipilineItems = ipilineItems }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult GetIPIByIPINo(int IpiNo)
        {
            if (IpiNo == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var ipi = context.ImportProformaInvoices.FirstOrDefault(m => m.IPINO == IpiNo.ToString());
                if (ipi == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ipilineItems = context.ImportProformaInvoiceLineItems.Where(m => m.IPIId == ipi.IPIId).ToList();
                    ipilineItems.ForEach
                    (x => { var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == x.ItemId); x.ItemName = item.Name; x.GroupName = context.ChartOfInventory.FirstOrDefault(m => m.Id == item.parentId).Name; x.GroupId = context.ChartOfInventory.FirstOrDefault(m => m.Id == item.parentId).Id; });
                    return Json(new { Message = 1, ipi = ipi, ipilineItems = ipilineItems }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult UpdateIPI(ImportProformaInvoice importProformaInvoice, List<ImportProformaInvoiceLineItems> addedItems)
        {
            try
            {
                var isExist = context.ImportProformaInvoices.AsNoTracking().FirstOrDefault(x => x.IPIId == importProformaInvoice.IPIId);
                if (isExist == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var prevIPI = context.ImportProformaInvoiceLineItems.Where(x => x.IPIId == importProformaInvoice.IPIId).ToList();
                    prevIPI.ForEach(x =>
                    {
                        context.ImportProformaInvoiceLineItems.Remove(x);
                    });
                    context.Entry<ImportProformaInvoice>(importProformaInvoice).State = EntityState.Modified;
                    addedItems.ForEach(x => { x.IPIId = importProformaInvoice.IPIId; context.ImportProformaInvoiceLineItems.Add(x); });
                    UserLog.SaveUserLog(ref context, new UserLog(importProformaInvoice.IPIId.ToString(), DateTime.Now, "ImportProformaInvoice", ScreenAction.Update));
                    var importLC = context.ImportLC.FirstOrDefault(x => x.IPIId == importProformaInvoice.IPIId);
                    if (importLC!=null)
                    {
                        UpdateImportLCLine(importProformaInvoice, addedItems);
                    }

                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public bool UpdateImportLCLine(ImportProformaInvoice importProformaInvoice, List<ImportProformaInvoiceLineItems> addedItems)
        {
            var importLC = context.ImportLC.FirstOrDefault(x => x.IPIId == importProformaInvoice.IPIId);
            var prevLC = context.ImportLCLineItem.Where(x => x.ILCID == importLC.ILCId).ToList();
            prevLC.ForEach(x =>
            {
                context.ImportLCLineItem.Remove(x);
            });

            addedItems.ForEach(x => {
                ImportLCLineItem impli = new ImportLCLineItem();
                impli.ILCID = importLC.ILCId;
                impli.ItemId = x.ItemId;
                impli.ItemName = x.ItemName;
                impli.Qty = x.Qty;
                impli.Rate = x.Rate;
                impli.Value = x.Value;
                impli.HSCode = x.HSCode;
                impli.PackingNote = x.PackingNote;
                context.ImportLCLineItem.Add(impli);
            });
            return true;
        }
        public ActionResult DeleteIPI(int ipinvoiceId)
        {
            try
            {
                var prevIPI = context.ImportProformaInvoices.FirstOrDefault(m=>m.IPIId==ipinvoiceId);
                var ipiInLC = context.ImportLC.FirstOrDefault(m => m.IPIId == ipinvoiceId);
                if (prevIPI==null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else if (ipiInLC!=null)
                {
                    return Json(new { Message = 3 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ipiLineItems = context.ImportProformaInvoiceLineItems.Where(m => m.IPIId == ipinvoiceId).ToList();
                    context.ImportProformaInvoices.Remove(prevIPI);
                    ipiLineItems.ForEach(m=> { context.ImportProformaInvoiceLineItems.Remove(m); });
                    UserLog.SaveUserLog(ref context, new UserLog(ipinvoiceId.ToString(), DateTime.Now, "ImportProformaInvoice", ScreenAction.Delete));
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult GetPuechaseOderNoBySupplierId(int sid)
        //{
        //    var suplier = context.Supplier.FirstOrDefault(x => x.Id == sid);

        //    return Json(new { purchaseOrders=LoadComboBox.LoadPurchaseOrderBySupplier(sid), suplier }, JsonRequestBehavior.AllowGet);
        //}
        

        public FileResult GetIPIPreview(int IPIId)
        {
            Session["ReportName"] = "IPIPreview";
            ReportParmPersister param = new ReportParmPersister();
            Session["IPIPreview"] = param;

            string sql = string.Format("exec PreviewIPI '" + IPIId + "'");
            var items = context.Database.SqlQuery<IPIPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                IPIPreviewReport report = new IPIPreviewReport();
                items.Add(report);
            }

            IPIPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public IPIPreviewR ShowReport(ReportParmPersister persister, List<IPIPreviewReport> data)
        {
            IPIPreviewR IPIPreviewR = new IPIPreviewR();

            IPIPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\IPIPreviewR.rpt");
            IPIPreviewR.Load(APPPATH);
            IPIPreviewR.SetDataSource(data);

            IPIPreviewR.SetParameterValue("compName", persister.CompName);
            IPIPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            IPIPreviewR.SetParameterValue("compContact", persister.CompContact);
            IPIPreviewR.SetParameterValue("compFax", persister.Fax);
            IPIPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            IPIPreviewR.SetParameterValue("factContact", persister.FactContact);
            return IPIPreviewR;
        }
    }
}
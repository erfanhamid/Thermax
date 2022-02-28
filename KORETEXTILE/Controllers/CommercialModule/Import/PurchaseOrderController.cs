using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommercialModule.Import;
using BEEERP.Models.Log;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.CommercialModule.Import
{
    [ShowNotification]
    public class PurchaseOrderController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: PurchaseOrder
        public ActionResult PurchaseOrder()
        {
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadRmItem(null);
            ViewBag.UoM = LoadComboBox.LoadAllUOM();

            return View();
        }

        public ActionResult SavePurchaseOrder(PurchaseOrder purchaseOrder, List<PurchaseOrderLineItem> addedItems)
        {
            string poNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.PurchaseOrder.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.PurchaseOrder.ToList().LastOrDefault(x => x.PONo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                poNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.PONo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            }
            else
            {
                poNo = yearLastTwoPart + "00001";
            }

            purchaseOrder.PONo = Convert.ToInt32(poNo);
            purchaseOrder.PODate = purchaseOrder.PODate.Add(DateTime.Now.TimeOfDay);

            addedItems.ForEach(x =>
            {
                x.PONo = purchaseOrder.PONo;
                context.PurchaseOrderLineItem.Add(x);
            });

            context.PurchaseOrder.Add(purchaseOrder);

            UserLog.SaveUserLog(ref context, new UserLog(purchaseOrder.PONo.ToString(), DateTime.Now, "PurchaseOrder", ScreenAction.Save));

            //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
            context.SaveChanges();
            return Json(purchaseOrder, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPurchaseOrderById(int id)
        {
            var purchaseOrder = context.PurchaseOrder.FirstOrDefault(x => x.PONo == id);

            if (purchaseOrder != null)
            {
                var chartOfInv = context.ChartOfInventory.ToList();
                var supplierGroup = context.Supplier.FirstOrDefault(x => x.Id == purchaseOrder.SupplierID);
                purchaseOrder.SupplierGroup = supplierGroup.GroupID;

                var purchaseOrderLineItem = context.PurchaseOrderLineItem.Where(x => x.PONo == id).ToList().Select(x =>
                {
                    var item = context.PurchaseOrderLineItem.FirstOrDefault(y => y.PONo == x.PONo && y.ItemID == x.ItemID);
                    var itemE = chartOfInv.FirstOrDefault(y => y.Id == item.ItemID);
                    x.ItemName = itemE.Name;
                    x.GroupName = chartOfInv.FirstOrDefault(y => y.Id == item.GroupID).Name;
                    x.Unit = context.UOM.FirstOrDefault(z => z.Id == itemE.UoMID).Name;
                    x.ItemID = item.ItemID;
                    x.GroupID = item.GroupID;
                    x.ItemQty = item.ItemQty;
                    x.UnitCost = item.UnitCost;
                    x.TotalCost = item.TotalCost;
                    x.VatPerc = item.VatPerc;
                    x.Specification = item.Specification;
                    return x;
                }).ToList();
                return Json(new { purchaseOrder, purchaseOrderLineItem }, JsonRequestBehavior.AllowGet);        
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdatePurchaseOrder(PurchaseOrder purchaseOrder, List<PurchaseOrderLineItem> addedItems)
        {
            var poExist = context.PurchaseOrder.FirstOrDefault(x => x.PONo == purchaseOrder.PONo);
            if (poExist != null)
            {
                context.PurchaseOrder.Remove(poExist);

                context.PurchaseOrderLineItem.Where(x => x.PONo == purchaseOrder.PONo).ToList().ForEach(x =>
                {
                    context.PurchaseOrderLineItem.Remove(x);
                });

                addedItems.ForEach(x =>
                {
                    x.PONo = purchaseOrder.PONo;
                    context.PurchaseOrderLineItem.Add(x);
                });

                purchaseOrder.PODate = purchaseOrder.PODate.Add(DateTime.Now.TimeOfDay);

                context.PurchaseOrder.Add(purchaseOrder);
                UserLog.SaveUserLog(ref context, new UserLog(purchaseOrder.PONo.ToString(), DateTime.Now, "PurchaseOrder", ScreenAction.Update));
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                context.SaveChanges();

                return Json(purchaseOrder, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeletePurchaseOrder(int id)
        {
            var poExist = context.PurchaseOrder.FirstOrDefault(x => x.PONo == id);
            if (poExist != null)
            {
                context.PurchaseOrder.Remove(poExist);
                context.PurchaseOrderLineItem.Where(x => x.PONo == id).ToList().ForEach(x =>
                {
                    context.PurchaseOrderLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "PurchaseOrder", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }

        public FileResult GetPurchaseOrderPreview(int PONo)
        {
            Session["ReportName"] = "PurchaseOrderPreview";

            ReportParmPersister param = new ReportParmPersister();
            var loginName = context.UserLog.FirstOrDefault(x => x.TrnasId == PONo.ToString() && x.ScreenName == "ILCB" && x.Action == "Save").LogInName;
            param.PostedBy = context.Employees.FirstOrDefault(x => x.LogInUserName == loginName).Name;


            Session["PurchaseOrderPreview"] = param;

            string sql = string.Format("exec PreviewPurchaseOrder '" + PONo + "'");
            var items = context.Database.SqlQuery<PurchaseOrderPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                PurchaseOrderPreviewReport report = new PurchaseOrderPreviewReport();
                items.Add(report);
            }

            PurchaseOrderPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PurchaseOrderPreviewR ShowReport(ReportParmPersister persister, List<PurchaseOrderPreviewReport> data)
        {
            PurchaseOrderPreviewR PurchaseOrderPreviewR = new PurchaseOrderPreviewR();

            PurchaseOrderPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PurchaseOrderPreviewR.rpt");
            PurchaseOrderPreviewR.Load(APPPATH);
            PurchaseOrderPreviewR.SetDataSource(data);

            PurchaseOrderPreviewR.SetParameterValue("CompanyName", persister.CompName);
            //PurchaseOrderPreviewR.SetParameterValue("PostedBy", persister.PostedBy);

            //fgrpPreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            //fgrpPreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            //fgrpPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            //fgrpPreviewR.SetParameterValue("ifgsNo", persister.IFGSNo);
            //fgrpPreviewR.SetParameterValue("ifgsDate", persister.IFGSDate.ToString("dd-MM-yyyy"));
            //fgrpPreviewR.SetParameterValue("salesCenter", persister.BranchName);
            //fgrpPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            //fgrpPreviewR.SetParameterValue("compName", persister.CompName);
            //fgrpPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            //fgrpPreviewR.SetParameterValue("compContact", persister.CompContact);
            //fgrpPreviewR.SetParameterValue("compFax", persister.Fax);
            //fgrpPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //fgrpPreviewR.SetParameterValue("factContact", persister.FactContact);
            return PurchaseOrderPreviewR;
        }
    }
}
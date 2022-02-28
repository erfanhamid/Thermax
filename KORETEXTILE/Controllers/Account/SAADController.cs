using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.AccountModule;
using BEEERP.Models.Database;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class SAADController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SAAD
        public ActionResult SAAD()
        {
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.account = LoadComboBox.LoadAllAccount();
            //ViewBag.supplier = LoadComboBox.LoadAllSupplier();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.gbpgsbfpb = LoadComboBox.LoadGBPGSBFPBB();
            ViewBag.billno = LoadComboBox.LoadGBENo(null);
            return View();
        }
        public ActionResult GetSupplierByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadSupplier(groupId), JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveSAAD(SupplierAcAdjustment supplierAcAdjustment)
        {
            try
            {
                string id = "";
                string yearLastTwoPart = supplierAcAdjustment.Date.Year.ToString().Substring(2, 2).ToString();
                var noOfSupplierAcAds = context.SupplierAcAdjustments.Count();
                if (noOfSupplierAcAds > 0)
                {
                    var lastSAAD = context.SupplierAcAdjustments.ToList().LastOrDefault(x => x.SAADNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    //id = yearLastTwoPart + (Convert.ToInt32(lastSAAD.SAADNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    if (lastSAAD==null)
                    {
                        id = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        id = yearLastTwoPart + (Convert.ToInt32(lastSAAD.SAADNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    id = yearLastTwoPart + "00001";
                }
                supplierAcAdjustment.SAADNo = Convert.ToInt32(id);
                supplierAcAdjustment.Date = supplierAcAdjustment.Date.Add(DateTime.Now.TimeOfDay);
                context.SupplierAcAdjustments.Add(supplierAcAdjustment);

                AccountModuleBridge.InsertUpdateFromSAAD(ref context, supplierAcAdjustment);
                SupplierBalanceCalculationBridge.InsertUpdateFromSAAD(ref context, supplierAcAdjustment);
                context.SaveChanges();

                return Json(new { Message = 1,saadno = supplierAcAdjustment.SAADNo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetSAAD(int saadNo)
        {
            var saad = context.SupplierAcAdjustments.FirstOrDefault(m=>m.SAADNo==saadNo);
            if (saad==null)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var supplierInformation = GetDueDetailsByBillType(saad.SupplierId);
                return Json(new { Message = 1, saad= saad,supplierInformation }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDueDetailsByBillType(int supplierId)
        {
            var gbpgsbfpb = LoadComboBox.LoadGBPGSBFPBB();
            var gsb = LoadComboBox.LoadGSBNo(supplierId);
            var gpb = LoadComboBox.LoadGPBNo(supplierId);
            var fpb = LoadComboBox.LoadFPBNo(supplierId);
            var ilcb = LoadComboBox.LoadILCBNo(supplierId);


            //var gbe = LoadComboBox.LoadGBENo(supplierId);

            //var ilcb = LoadComboBox.GetDueInvoice(supplierId);
            //SelectList dpNo = LoadComboBox.LoadDirectPurchaseNo(supplierId);
            //decimal obAmount = (decimal)context.Supplier.FirstOrDefault(x => x.Id == supplierId).OB;
            //decimal obPaid = (from pi in context.PayBillInfo
            //                  join pl in context.PayBillLines on pi.PBID equals pl.PBID
            //                  where pi.SupplierId == supplierId && pl.BillType == "OB"
            //                  select pl.Amount).DefaultIfEmpty(0).Sum();
            //decimal obDueAmount = obAmount - obPaid;

            return Json(new { gsb, gpb, ilcb, gbpgsbfpb, fpb }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSAAD(SupplierAcAdjustment supplierAcAdjustment)
        {
            try
            {
                var prevSaad = context.SupplierAcAdjustments.FirstOrDefault(m=>m.SAADNo==supplierAcAdjustment.SAADNo);
                if(prevSaad==null){
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    supplierAcAdjustment.Date = supplierAcAdjustment.Date.Add(DateTime.Now.TimeOfDay);
                    context.SupplierAcAdjustments.Remove(prevSaad);
                    context.SupplierAcAdjustments.Add(supplierAcAdjustment);
                    AccountModuleBridge.InsertUpdateFromSAAD(ref context, supplierAcAdjustment);
                    SupplierBalanceCalculationBridge.InsertUpdateFromSAAD(ref context, supplierAcAdjustment);
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteSAAD(SupplierAcAdjustment supplierAcAdjustment)
        {
            try
            {
                var prevSaad = context.SupplierAcAdjustments.FirstOrDefault(m => m.SAADNo == supplierAcAdjustment.SAADNo);
                if (prevSaad == null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    supplierAcAdjustment.Date = supplierAcAdjustment.Date.Add(DateTime.Now.TimeOfDay);
                    context.SupplierAcAdjustments.Remove(prevSaad);
                    AccountModuleBridge.DeleteFromSupplierAccountAdjestment(ref context, supplierAcAdjustment);
                    SupplierBalanceCalculationBridge.DeleteFromSupplierAccountAdjustment(ref context, supplierAcAdjustment);
                    context.SaveChanges();
                    return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SAADPrintPreview(int saad)
        {


            ReportParmPersister param = new ReportParmPersister();
            var postedBy = (from p in context.UserLog
                            join c in context.Employees on p.LogInName equals c.LogInUserName
                            where p.ScreenName == "SAAD" && p.TrnasId == saad.ToString()
                            select c).ToList().FirstOrDefault();
            if (postedBy != null)
            {
                param.PostedBy = postedBy.Name;
            }
            else
            {
                param.PostedBy = "";
            }

            string sql = string.Format("exec spPrintSAAD " + saad + "");
            var items = context.Database.SqlQuery<SAADPrintPreview>(sql).ToList();
            if (items.Count == 0)
            {
                SAADPrintPreview report = new SAADPrintPreview();
                items.Add(report);
            }
            SAADPrintPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public SAADPrintPreviewR ShowReport(ReportParmPersister persister, List<SAADPrintPreview> data)
        {


            SAADPrintPreviewR previewR = new SAADPrintPreviewR();

            previewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SAADPrintPreviewR.rpt");
            previewR.Load(APPPATH);
            previewR.SetDataSource(data);
            return previewR;
        }
    }
}
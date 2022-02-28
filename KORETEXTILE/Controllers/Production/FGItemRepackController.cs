using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.ProductionModule;
using BEEERP.Models.Log;
using BEEERP.Models.Data_Admin;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.Production
{
    [ShowNotification]
    public class FGItemRepackController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FGItemRepack
        public ActionResult FGItemRepack()  
        {
            ViewBag.Store = LoadComboBox.LoadAllStore(); 
            var floor = context.Store.Where(x => x.Name.ToLower().Replace(" ", string.Empty) == "floorfg").ToList();
            ViewBag.FloorId = floor.FirstOrDefault().Id;
            ViewBag.ItemInput = LoadComboBox.LoadAllFGItem();
            ViewBag.Batch = LoadComboBox.LoadBatch();
            ViewBag.ItemByBatch = LoadComboBox.LoadItemByBatchNo(null);
            return View();
        }

        public ActionResult LoadItemByBatch(string batchNo)
        {
            return Json(LoadComboBox.LoadItemByBatchNo(batchNo),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemDetails(int itemId, string batchNo)
        {
            var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == itemId);
            var batch = context.BatchLineItem.Where(x => x.BatchNo == batchNo.Trim() && x.ItemID == itemId).FirstOrDefault();

            return Json(new { Item = item , Batch = batch.Qty}, JsonRequestBehavior.AllowGet);  
        }

        public ActionResult SaveFGIR(FGItemRepack fgir, List<FGItemRepackLineItem> addedItems)
        {
            string fgirNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.FGItemRepack.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.FGItemRepack.ToList().LastOrDefault(x => x.FGIRNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                fgirNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.FGIRNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            }
            else
            {
                fgirNo = yearLastTwoPart + "00001";
            }

            fgir.FGIRNo = Convert.ToInt32(fgirNo);
            fgir.Date = fgir.Date.Add(DateTime.Now.TimeOfDay);
            addedItems.ForEach(x =>
            {
                x.FGIRNo = fgir.FGIRNo;
                context.FGItemRepackLineItem.Add(x);
            });

            context.FGItemRepack.Add(fgir);

            UserLog.SaveUserLog(ref context, new UserLog(fgir.FGIRNo.ToString(), DateTime.Now, "FGIR", ScreenAction.Save));
            InventoryTransactionBridge.InsertFromFGItemRepack(ref context, fgir, addedItems);

            context.SaveChanges();
            return Json(new { fgir }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFGIRById(int id)
        {
            var fgirItem = context.FGItemRepack.FirstOrDefault(x => x.FGIRNo == id);

            if (fgirItem != null)
            {
                var fgirLineItem = context.FGItemRepackLineItem.Where(x => x.FGIRNo == id).ToList().Select(x =>
                {
                    x.ItemNameOut = GetItemNameById(x.ItemIdOut).Name;
                    //x.PacSizeOut = GetItemNameById(x.ItemIdOut).PacSize;
                    x.ItemNameIn = GetItemNameById(x.ItemIdIn).Name;
                    //x.PacSizeIn = GetItemNameById(x.ItemIdIn).PacSize;

                    return x;
                }).ToList();

                return Json(new { fgirItem, fgirLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateFGIR(FGItemRepack fgir, List<FGItemRepackLineItem> addedItems)
        {
            var fgirExist = context.FGItemRepack.FirstOrDefault(x => x.FGIRNo == fgir.FGIRNo);
            if (fgirExist != null)
            {
                context.FGItemRepack.Remove(fgirExist);

                context.FGItemRepackLineItem.Where(x => x.FGIRNo == fgir.FGIRNo).ToList().ForEach(x =>
                {
                    context.FGItemRepackLineItem.Remove(x);
                });

                addedItems.ForEach(x =>
                {
                    x.FGIRNo = fgir.FGIRNo;
                    context.FGItemRepackLineItem.Add(x);
                });

                fgir.Date = fgir.Date.Add(DateTime.Now.TimeOfDay);
                context.FGItemRepack.Add(fgir);
                UserLog.SaveUserLog(ref context, new UserLog(fgir.FGIRNo.ToString(), DateTime.Now, "FGIR", ScreenAction.Update));
                InventoryTransactionBridge.InsertFromFGItemRepack(ref context, fgir, addedItems);

                context.SaveChanges();
                return Json(fgir, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteFGIRById(int id)
        {
            var fgirExist = context.FGItemRepack.FirstOrDefault(x => x.FGIRNo == id);
            if (fgirExist != null)
            {
                context.FGItemRepack.Remove(fgirExist);
                var fgirLineItem = context.FGItemRepackLineItem.Where(x => x.FGIRNo == id).ToList();
                context.FGItemRepackLineItem.Where(x => x.FGIRNo == id).ToList().ForEach(x =>
                {
                    context.FGItemRepackLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "FGIR", ScreenAction.Delete));
                InventoryTransactionBridge.DeleteFromFGItemRepack(ref context, fgirExist);

                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetFGIRPreview(int FGIRNo)
        //{
        //    Session["ReportName"] = "EmployeeAiTBalanceLedger";

        //    ReportParmPersister param = new ReportParmPersister();
        //    Session["LTRAPreview"] = param;

        //    string sql = string.Format("exec LTRAPreview '" + LTRANo + "' ");
        //    var items = context.Database.SqlQuery<LTRA>(sql).ToList();
        //    if (items.Count() == 0)
        //    {
        //        LTRA report = new LTRA();
        //        items.Add(report);
        //    }

        //    LTRAPreviewR rd = ShowReport(param, items);
        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    rd.Close();
        //    return new FileStreamResult(stream, "application/pdf");
        //}

        //public LTRAPreviewR ShowReport(ReportParmPersister persister, List<LTRA> data)
        //{
        //    LTRAPreviewR lTRAPreviewR = new LTRAPreviewR();

        //    lTRAPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
        //    String APPPATH = Server.MapPath(@"\CrystalReport\IDTSamplePreviewR.rpt");
        //    lTRAPreviewR.Load(APPPATH);
        //    lTRAPreviewR.SetDataSource(data);

        //    lTRAPreviewR.SetParameterValue("compName", persister.CompName);
        //    lTRAPreviewR.SetParameterValue("compAddress", persister.CompAddress);
        //    lTRAPreviewR.SetParameterValue("compContact", persister.CompContact);
        //    lTRAPreviewR.SetParameterValue("compFax", persister.Fax);
        //    lTRAPreviewR.SetParameterValue("factAddress", persister.FactAddress);
        //    lTRAPreviewR.SetParameterValue("factContact", persister.FactContact);
        //    return lTRAPreviewR;
        //}

        public ChartOfInventory GetItemNameById(int id)
        {
            var item = context.ChartOfInventory.ToList().FirstOrDefault(x => x.Id == id);
            return item;
        }
    }
}
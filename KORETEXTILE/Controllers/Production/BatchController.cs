using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.ProductionModule;
using BEEERP.Models.CommonInformation;
using System.Data.Entity;
using BEEERP.Models.Bridge;
using BEEERP.Models.Log;
using System.IO;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;

namespace BEEERP.Controllers.ProductModule
{
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    //[ProductionModule]
    public class BatchController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Batch
        public ActionResult Batch() 
        {
            ViewBag.Batch = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            int count = context.Batch.Count();
            int countListPlusOne = 0;
            if (count!=0)
            {
                countListPlusOne = context.Batch.ToList().Max(x => x.ID) + 1;
            }
            else
            {
                countListPlusOne = 1;
            }

            ViewBag.countListPlusOne = countListPlusOne;
            return View();
        }
        
        public ActionResult SaveBatch(Batch batch,List<BatchLineItem> addedItems)
        {
            int count = context.Batch.Count();
            if (count != 0)
            {
                batch.ID = context.Batch.ToList().Max(x => x.ID) + 1;
            }
            else
            {
                batch.ID = 1;
            }
            //var itemCount = addedItems.Count;
            batch.BatchDate = batch.BatchDate.Add(DateTime.Now.TimeOfDay);
            var batchExits = context.Batch.FirstOrDefault(x => x.BatchNo.ToLower() == batch.BatchNo.ToLower());
            if (batchExits == null)
            {
                try
                {
                    context.Batch.Add(batch);
                    addedItems.ForEach(x => { x.BatchNo = batch.BatchNo; context.BatchLineItem.Add(x); });
                    UserLog.SaveUserLog(ref context,new UserLog( batch.BatchNo.ToString(), DateTime.Now, "Batch", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });

                }
                catch (Exception ex)
                {
                    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                }

            }
            else
            {

                return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
            }
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
                return Json(new { Messsage = 1, Uom = uom.Name, UnitCost = item.clmStandardCost }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetBatchByBatchId(int batchId)
        {
            if (batchId == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var batch = context.Batch.FirstOrDefault(m => m.ID == batchId);
                
                if(batch==null)
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var batclineItems = context.BatchLineItem.Where(m => m.BatchNo == batch.BatchNo).ToList();
                    batclineItems.ForEach
                    (x => { var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == x.ItemID);x.ItemName = item.Name;x.BatchDate = batch.BatchDate.ToString("dd-MM-yyyy");x.groupId = item.parentId;x.PacSize = item.PacSize;  });
                    return Json(new { Message = 1, batch = batch, batchLineItem = batclineItems}, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult UpdateBatch(Batch batch, List<BatchLineItem> addedItems)
        {
            try
            {
                var isExist = context.Batch.AsNoTracking().FirstOrDefault(x => x.ID ==batch.ID);
                if (isExist == null)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var prevItem = context.BatchLineItem.Where(x => x.BatchNo == batch.BatchNo).ToList();
                    prevItem.ForEach(x =>
                    {
                        context.BatchLineItem.Remove(x);
                    });
                    context.Entry<Batch>(batch).State = EntityState.Modified;
                    addedItems.ForEach(x => { x.BatchNo = batch.BatchNo; context.BatchLineItem.Add(x); });
                    UserLog.SaveUserLog(ref context, new UserLog(batch.BatchNo.ToString(), DateTime.Now, "Batch", ScreenAction.Update));
                    context.SaveChanges();
                }
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult GetBatchPreview(int ID)
        {
            Session["ReportName"] = "BatchPreviewR";

            ReportParmPersister param = new ReportParmPersister();

            var batch = context.Batch.FirstOrDefault(x => x.ID == ID);
            if (batch != null)
            {
                param.BatchID = batch.ID;
                param.BatchNo = batch.BatchNo;
                param.AsOnDate = batch.BatchDate;
            }
            else
            {
                param.SampleNo = 0;
            }
            param.PostedBy = (from e in context.Employees.AsEnumerable()
                              join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                              where u.ScreenName == "Batch" && u.Action == "Save" && u.TrnasId == batch.ID.ToString()
                              select e.Name).FirstOrDefault();

            Session["BatchPreview"] = param;

            string sql = string.Format("exec BatchPreview '" + ID + "'");
            var items = context.Database.SqlQuery<BatchPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                BatchPreviewReport report = new BatchPreviewReport();
                items.Add(report);
            }

            BatchPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public BatchPreviewR ShowReport(ReportParmPersister persister, List<BatchPreviewReport> data)
        {
            BatchPreviewR batchPreviewR = new BatchPreviewR();

            batchPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\BatchPreviewR.rpt");
            batchPreviewR.Load(APPPATH);
            batchPreviewR.SetDataSource(data);

            batchPreviewR.SetParameterValue("bID", persister.BatchID);
            batchPreviewR.SetParameterValue("batchNo", persister.BatchNo);
            batchPreviewR.SetParameterValue("batchDate", persister.AsOnDate.ToString("dd-MM-yyyy"));
            batchPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            batchPreviewR.SetParameterValue("compName", persister.CompName);
            batchPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            batchPreviewR.SetParameterValue("compContact", persister.CompContact);
            batchPreviewR.SetParameterValue("compFax", persister.Fax);
            batchPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            batchPreviewR.SetParameterValue("factContact", persister.FactContact);
            return batchPreviewR;
        }

    }
}
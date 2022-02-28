using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.ProductionModule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Production
{
    [ShowNotification]
    //[Permission]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class FGProductionController : Controller
    {
        BEEContext context = new BEEContext();

        public ActionResult FgProduction()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            if (depot == null)
            {
                ViewBag.Disabled = "true";
            }
            else
            {
                ViewBag.Disabled = "false";
            }
            string FGPNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            if (context.RMCEntry.Count() > 0)
            {
                var lastRMC = context.FGProduction.ToList().LastOrDefault(x => x.FGPNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastRMC == null)
                {
                    FGPNo = yearLastTwoPart + "00001";
                }
                else
                {
                    FGPNo = yearLastTwoPart + (Convert.ToInt32(lastRMC.FGPNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                FGPNo = yearLastTwoPart + "00001";
            }
            ViewBag.FGPNo = FGPNo;

            ViewBag.Item = LoadComboBox.LoadItemByBatchNo(null);
            ViewBag.GroupId = LoadComboBox.LoadItemGroupByBatchNo(null);
            ViewBag.LoadItemByGroup = LoadComboBox.LoadItemByGroup(null);
            ViewBag.LoadBatch = LoadComboBox.LoadBatch();
            ViewBag.Store = LoadComboBox.LoadProductionStore();
            return View();
        }
        public ActionResult UOMOnItemChange(int id)
        {
            //var getTargetQty = context.BatchLineItem.FirstOrDefault(x => x.BatchNo == batch && x.ItemID == id);
            //int batchId = context.Batch.FirstOrDefault(x => x.BatchNo == batch).ID;
            //decimal recQty = (from fg in context.FGProduction
            //              join fgline in context.FGProductionLineItem on fg.FGPNo equals fgline.FGPNo
            //              where (fgline.ItemID == id) && (fg.BatchID == batchId)
            //              select fgline.Qty).DefaultIfEmpty(0).Sum();
            //var item=context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
            var item = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == id);
            if (item!=null)
            {
                var uom = context.UOM.FirstOrDefault(x => x.Id == item.UoMID);
                return Json(new { Messsage = 1, Uom = uom.Name,UnitCost=item.clmStandardCost, PCPerCtn = item.clmCartonCapacity}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetGroupByBatch(string batchNo)
        {
            //var items = LoadComboBox.LoadItemGroupByBatchNo(batchNo);
            //return Json(new { items, item = LoadComboBox.LoadItemByBatchNo(batchNo) }, JsonRequestBehavior.AllowGet);
            return Json(LoadComboBox.LoadAllFGGroup(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemByGroup(int id)
        {
            return Json(LoadComboBox.LoadItemByGroup(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveFGProduction(FGProduction fGProduction,List<FGProductionLineItem> fGProductionLineItem )
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Finished Goods Production").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (fGProduction.FGPDate > IsExpired.SELDate)
            {
                try
                {
                    string FGPNo = "";
                    string yearLastTwoPart = fGProduction.FGPDate.Year.ToString().Substring(2, 2).ToString();
                    if (context.RMCEntry.Count() > 0)
                    {
                        var lastRMC = context.FGProduction.ToList().LastOrDefault(x => x.FGPNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastRMC == null)
                        {
                            FGPNo = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            FGPNo = yearLastTwoPart + (Convert.ToInt32(lastRMC.FGPNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        FGPNo = yearLastTwoPart + "00001";
                    }

                    fGProduction.FGPNo = Convert.ToInt32(FGPNo);
                    context.FGProduction.Add(fGProduction);
                    fGProductionLineItem.ForEach(x =>
                    {
                        x.FGPNo = fGProduction.FGPNo; context.FGProductionLineItem.Add(x);
                    });
                    InventoryTransactionBridge.InsertFromFGProduction(ref context, fGProductionLineItem, fGProduction);
                    //AccountModuleBridge.InsertUpdateFromFGP(ref context, fGProduction, fGProductionLineItem);
                    //UserLog.SaveUserLog(ref context, new UserLog(fGProduction.FGPNo.ToString(), fGProduction.FGPDate, "FGP", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, FGNo = fGProduction.FGPNo }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetFGPByFgpNo(int fgpNo)
        {
            if (fgpNo == 0)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var fgp = context.FGProduction.FirstOrDefault(x => x.FGPNo == fgpNo);
                if (fgp == null)
                {
                    return Json(new { Message =0}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    var fgpLineItem = context.FGProductionLineItem.Where(x => x.FGPNo == fgpNo).ToList();
                    fgpLineItem.ForEach(x =>
                    {
                        var item = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID);
                        x.ItemName =item.Name;
                        x.PCPerCtn = Convert.ToDecimal(item.clmCartonCapacity);
                        x.GroupId = item.ParentId;
                        
                    });
                    return Json(new { Message = 1, production = fgp, lineItem = fgpLineItem },JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult UpdateFGProduction(FGProduction fGProduction, List<FGProductionLineItem> fGProductionLineItem)
        {

            try
            {
                //var item = context.FGProduction.ToList().LastOrDefault();
                var isExist = context.FGProduction.AsNoTracking().FirstOrDefault(x => x.FGPNo==fGProduction.FGPNo);
                if(isExist==null)
                {
                    return Json(new { Message = 2}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var prevItem = context.FGProductionLineItem.Where(x => x.FGPNo == fGProduction.FGPNo).ToList();
                    prevItem.ForEach(x =>
                    {
                        context.FGProductionLineItem.Remove(x);
                    });
                }
                context.Entry<FGProduction>(fGProduction).State = EntityState.Modified;
                fGProductionLineItem.ForEach(x => {
                    x.FGPNo = fGProduction.FGPNo;
                    context.FGProductionLineItem.Add(x);
                });
                InventoryTransactionBridge.InsertFromFGProduction(ref context, fGProductionLineItem,fGProduction);
                AccountModuleBridge.InsertUpdateFromFGP(ref context, fGProduction, fGProductionLineItem);
                UserLog.SaveUserLog(ref context, new UserLog(fGProduction.FGPNo.ToString(), fGProduction.FGPDate, "FGP", ScreenAction.Update));
                context.SaveChanges();
                return Json(new { Message = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        public FileResult GetRMCPreview(int FGPNo)
        {
           // Session["reportName"] = "Row Material Consumtion Details For ID" + RMCNo;

            //ReportParmPersister param = new ReportParmPersister();
            //var data = (from e in context.Employees.AsEnumerable()
            //            join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //            where u.ScreenName == "Raw Material Consumption" && u.Action == "Save" && u.TrnasId == RMCNo.ToString()
            //            select e.Name).ToList();
            //if(data.Count > 0)
            //{
            //    param.PostedBy = data.FirstOrDefault().ToString();
            //}
            //param.PostedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "Raw Material Consumption" && u.Action == "Save" && u.TrnasId == RMCNo.ToString()
            //                  select e.Name).FirstOrDefault();
            string sql = string.Format("exec PreviewFGP " + FGPNo);
            var items = context.Database.SqlQuery<PreviewFGP>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewFGP report = new PreviewFGP();
                items.Add(report);
            }

            PreviewFGPR rd = ShowReport(items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PreviewFGPR ShowReport(List<PreviewFGP> data)
        {
            PreviewFGPR RMC = new PreviewFGPR();

            RMC.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewFGPR.rpt");
            RMC.Load(APPPATH);
            RMC.SetDataSource(data);
            //RMC.SetParameterValue("postedBy", persister.PostedBy);
            //RMC.SetParameterValue("compName", persister.CompName);
            //RMC.SetParameterValue("compAddress", persister.CompAddress);
            //RMC.SetParameterValue("reportName", Session["reportName"]);
            return RMC;
        }

    }
}
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Store_FG;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Store_FG
{
    [ShowNotification]
   // [Authorize(Roles = "FGAdmin,FGOperator,FGViewer,FGApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class FGRPController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FGRP
        public ActionResult FGRPEntry()
        {
            List<int> items =
     (from c in context.IssueFGStore
      where !(from o in context.FGRP
              select o.IFGFNo)
             .Contains(c.clmIFGSID)
      select c.clmIFGSID).ToList();

            ViewBag.FGRPNo = items;
            return View();
        }
        public ActionResult GetItemByIFGSNo(int ifgsNo)
        {
            var item = context.IssueFGStoreLineItem.Where(x => x.clmIFGSNo == ifgsNo).ToList();
            
            if(item.Count>0)
            {
                var allItem = (from i in item
                              join c in LoadComboBox.GetItemInfo() on i.clmItemID equals c.Id
                              join g in LoadComboBox.GetItemInfo() on c.ParentId equals g.Id
                              join ifgs in context.IssueFGStore on i.clmIFGSNo equals ifgs.clmIFGSID
                              select new {GroupName=g.Name,Qty=i.clmQty,ItemName=c.Name, PackSize = "", IfgsDate = ifgs.clmIFGSDate }).ToList();
                return Json(new { Item = allItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {Message=0 }, JsonRequestBehavior.AllowGet);

            }
           
        }
        public ActionResult ReceiveIFGS(int ifgsNo, DateTime date)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "FG Receive").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (date > IsExpired.SELDate)
            {
                var ifgsEntry = context.FGRP.FirstOrDefault(x => x.IFGFNo == ifgsNo);
                if (ifgsEntry == null)
                {
                    var ifgf = context.IssueFGStore.FirstOrDefault(x => x.clmIFGSID == ifgsNo);
                    var ifgfLineItem = context.IssueFGStoreLineItem.Where(x => x.clmIFGSNo == ifgsNo).ToList();
                    FGRP fgrp = new FGRP();
                    List<FGRPLineItem> fGRPLineItem = new List<FGRPLineItem>();


                    var lastFgrp = context.FGRP.ToList().LastOrDefault();
                    if (lastFgrp == null)
                    {
                        fgrp.FGRPNo = 1;
                    }
                    else
                    {
                        fgrp.FGRPNo = lastFgrp.FGRPNo + 1;
                    }
                    fgrp.FGRPDate = date;
                    fgrp.IFGFNo = ifgf.clmIFGSID;
                    fgrp.IFGSDate = ifgf.clmIFGSDate;
                    fgrp.TotalCost = ifgf.clmCostTotal;
                    fgrp.TotalQT = Convert.ToDecimal(ifgf.clmIFGSTotal);
                    fgrp.IDP = 1;
                    fgrp.YearPart = 1;
                    ifgf.IssueFrom = context.Store.FirstOrDefault(x => x.Name == "Floor FG").Id;
                    ifgf.IssueTo = context.Store.FirstOrDefault(x => x.Name == "Factory FG").Id;
                    ifgfLineItem.ForEach(x =>
                    {
                        var item = new FGRPLineItem() { FGRPNo = fgrp.FGRPNo, ItemGrpID = x.clmItemGrpID, ItemID = x.clmItemID, FGRPQty = Convert.ToDecimal(x.clmQty), CostRt = x.clmCostRt, CostVal = x.clmCostVal };
                        fGRPLineItem.Add(item);
                        context.FGRPLineItem.Add(item);
                    });
                    context.FGRP.Add(fgrp);
                    InventoryTransactionBridge.InsertFromFGRPEntry(ref context, fGRPLineItem, fgrp, ifgf.IssueFrom, ifgf.IssueTo);
                    UserLog.SaveUserLog(ref context, new UserLog(fgrp.FGRPNo.ToString(), DateTime.Now, "FGRP", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, id = fgrp.FGRPNo }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult UpdateIFGS(int ifgsNo, DateTime date, int fgrpNo)
        {
            var ifgsEntry = context.FGRP.FirstOrDefault(x => x.IFGFNo == ifgsNo);
            if (ifgsEntry != null)
            {
                context.FGRP.Remove(ifgsEntry);
                var ifgf = context.IssueFGStore.FirstOrDefault(x => x.clmIFGSID == ifgsNo);
                var ifgfLineItem = context.IssueFGStoreLineItem.Where(x => x.clmIFGSNo == ifgsNo).ToList();
                FGRP fgrp = new FGRP();
                List<FGRPLineItem> fGRPLineItem = new List<FGRPLineItem>();
                fgrp.FGRPNo = fgrpNo;
                fgrp.FGRPDate = date;
                fgrp.IFGFNo = ifgf.clmIFGSID;
                fgrp.IFGSDate = ifgf.clmIFGSDate;
                fgrp.TotalCost = ifgf.clmCostTotal;
                fgrp.IDP = 1;
                fgrp.YearPart = 1;
                ifgf.IssueFrom = context.Store.FirstOrDefault(x => x.Name == "Floor FG").Id;
                ifgf.IssueTo = context.Store.FirstOrDefault(x => x.Name == "Factory FG").Id;
                fgrp.TotalQT = Convert.ToDecimal(ifgf.clmIFGSTotal);
                var fgrpLine = context.FGRPLineItem.Where(x => x.FGRPNo == fgrp.FGRPNo).ToList();
                if(fgrpLine.Count > 0)
                {
                    fgrpLine.ForEach(x => {
                        context.FGRPLineItem.Remove(x);

                    });
                }
                ifgfLineItem.ForEach(x => {
                    var item = new FGRPLineItem() { FGRPNo = fgrp.FGRPNo, ItemGrpID = x.clmItemGrpID, ItemID = x.clmItemID, FGRPQty = Convert.ToDecimal(x.clmQty), CostRt = x.clmCostRt, CostVal = x.clmCostVal };
                    fGRPLineItem.Add(item);
                    context.FGRPLineItem.Add(item);
                });
                context.FGRP.Add(fgrp);
                InventoryTransactionBridge.InsertFromFGRPEntry(ref context, fGRPLineItem, fgrp, ifgf.IssueFrom, ifgf.IssueTo);
                UserLog.SaveUserLog(ref context, new UserLog(fgrp.FGRPNo.ToString(), DateTime.Now, "FGRP", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { Message = 1, id = fgrp.FGRPNo }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetPreviousFGRP(int fgrpNo)
        {
            var fgrp = context.FGRP.FirstOrDefault(x => x.FGRPNo == fgrpNo);
            try
            {
                if (fgrp != null)
                {
                    var fgrpLineItem = (from i in context.FGRPLineItem.Where(x => x.FGRPNo == fgrpNo).ToList()
                                        join c in LoadComboBox.GetItemInfo() on i.ItemID equals c.Id
                                        join g in LoadComboBox.GetItemInfo() on c.ParentId equals g.Id
                                        select new { ItemId = i.ItemID, GroupName = g.Name, Qty = Convert.ToDecimal(i.FGRPQty), ItemName = c.Name, PackSize = "" }).ToList();
                    return Json(new { fgrp, fgrpLineItem, Message = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public FileResult GetFGRPPreview(int fgrpNo)
        {
            Session["ReportName"] = "FGRPPreviewR";

            ReportParmPersister param = new ReportParmPersister();

            var sample = context.FGRP.FirstOrDefault(x => x.FGRPNo == fgrpNo);
            if (sample != null)
            {
                param.SampleNo = sample.FGRPNo;
                param.SampleDate = sample.FGRPDate;
                param.IFGSNo = sample.IFGFNo;
                param.IFGSDate = sample.IFGSDate;

            }
            else
            {
                param.SampleNo = 0;
            }
            var issue = context.IssueFGStore.FirstOrDefault(x => x.clmIFGSID == sample.IFGFNo);
            //param.ShiftFrom = context.Store.FirstOrDefault(x => x.Id == issue.IssueFrom).Name;
            //param.ShiftTo = context.Store.FirstOrDefault(x => x.Id == issue.IssueTo).Name;
            param.ShiftFrom = FindObjectById.GetStoreById(issue.IssueFrom).Name;
            param.ShiftTo = FindObjectById.GetStoreById(issue.IssueTo).Name;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == issue.IssueTo).BrnachName;
            param.PostedBy = (from e in context.Employees.AsEnumerable()
                              join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                              where u.ScreenName == "FGRP" && u.Action == "Save" && u.TrnasId == sample.FGRPNo.ToString()
                              select e.Name).FirstOrDefault();


            Session["FGRPPreview"] = param;

            string sql = string.Format("exec FGRPPreview '" + fgrpNo + "'");
            var items = context.Database.SqlQuery<FGRPPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                FGRPPreviewReport report = new FGRPPreviewReport();
                items.Add(report);
            }

            FGRPPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public FGRPPreviewR ShowReport(ReportParmPersister persister, List<FGRPPreviewReport> data)
        {
            FGRPPreviewR fgrpPreviewR = new FGRPPreviewR();

            fgrpPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\FGRPPreviewR.rpt");
            fgrpPreviewR.Load(APPPATH);
            fgrpPreviewR.SetDataSource(data);

            fgrpPreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            fgrpPreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            fgrpPreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            fgrpPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            fgrpPreviewR.SetParameterValue("ifgsNo", persister.IFGSNo);
            fgrpPreviewR.SetParameterValue("ifgsDate", persister.IFGSDate.ToString("dd-MM-yyyy"));
            fgrpPreviewR.SetParameterValue("salesCenter", persister.BranchName);
            fgrpPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            fgrpPreviewR.SetParameterValue("compName", persister.CompName);
            fgrpPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            fgrpPreviewR.SetParameterValue("compContact", persister.CompContact);
            fgrpPreviewR.SetParameterValue("compFax", persister.Fax);
            fgrpPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            fgrpPreviewR.SetParameterValue("factContact", persister.FactContact);
            return fgrpPreviewR;
        }
    }
}
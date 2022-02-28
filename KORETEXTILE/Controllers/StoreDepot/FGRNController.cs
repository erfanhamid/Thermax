using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.StoreDepot;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using Rotativa;
using BEEERP.Models.ViewModel.Sales.Report;
using BEEERP.Models.ViewModel.StoreDepot;
using CrystalDecisions.Shared;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.StoreDepot
{
    [ShowNotification]
    [Permission]
    [Authorize(Roles = "DepAdmin,DepOperator,DepViewer,DepApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    [StoreFGModule]
    public class FGRNController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FGRN
        public ActionResult FGRNEntry()     
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.IssueTo = LoadComboBox.LoadAllFGStoreByDepot(depot);
            ViewBag.IssueFrom = LoadComboBox.LoadAllFGStore();
            ViewBag.Group = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadAllItem();
            return View();
        }

        public ActionResult GetStoreByDepotId(int depotId)
        {
            return Json(LoadComboBox.LoadAllFGRevStoreByDepot(depotId), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetSIFDByChallanId(int chalanNo)
        //{
        //    var depot = ScreenSessionData.GetEmployeeDepot();
        //    var sifdItem = context.SIFD.FirstOrDefault(x => x.ChallanNo == chalanNo);
        //    var storeExist = (from s in context.SIFD
        //                      join st in context.Store on s.NewStoreID equals st.Id
        //                      join d in context.BranchInformation on st.DepotID equals d.Slno
        //                      where (d.Slno == depot) && (s.ChallanNo == chalanNo)
        //                      select s).FirstOrDefault();
        //    if (storeExist != null)
        //    {
        //        if (sifdItem != null)
        //        {
        //            var sifdLineItem = context.SIFDLineItem.Where(x => x.SIFDNo == sifdItem.SIFDNo).ToList().Select(x =>
        //            {
        //                var item = context.SIFDLineItem.FirstOrDefault(y => y.Id == x.Id);
        //                x.ItemName = GetItemNameById(item.ItemID).Name;
        //                x.GroupName = GetItemNameById(item.ItemGrpID).Name;
        //                x.ItemID = item.ItemID;
        //                x.ItemGrpID = item.ItemGrpID;
        //                x.ShiftQty = item.ShiftQty;
        //                x.CostVal = item.CostVal;
        //                x.CostRt = item.CostRt;
        //                x.CtnQty = GetItemNameById(item.ItemID).clmCartonCapacity;
        //                x.ReceivedQty = GetTotalRcvQty(item.ItemID, sifdItem.ChallanNo);
        //                x.PackSize = GetItemNameById(item.ItemID).PacSize;
        //                return x;
        //            }).ToList();
        //            return Json(new { sifdItem, sifdLineItem, Messege = 1 }, JsonRequestBehavior.AllowGet); 

        //        }
        //        else
        //        {
        //            return Json(new { Messege = 0} , JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else if (sifdItem == null)
        //    {
        //        return Json(new { Messege = 3 }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { Messege = 2 }, JsonRequestBehavior.AllowGet);
        //    }

        //}

        public ActionResult GetItemAndGroupName(int item, int group)
        {
            var aitem = FindObjectById.GetChartOfInventoryById(item);
            string ItemName = aitem.Name;
            string GroupName = GetItemNameById(group).Name;
            decimal? CostRt = aitem.clmStandardCost;
            string PackSize = aitem.PacSize;
            return Json(new { Item = ItemName, Group = GroupName, Cost = CostRt, PackSize = PackSize }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveGRF(GRF fgrn, List<GRFLineItem> addedItems1)    
        {
            using (context)
            {
                string fgrnNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.GRF.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.GRF.ToList().LastOrDefault(x => x.GRFNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    fgrnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GRFNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    fgrnNo = yearLastTwoPart + "00001";
                }

                fgrn.GRFNo = Convert.ToInt32(fgrnNo);
                fgrn.ReceiveDate = fgrn.ReceiveDate.Add(DateTime.Now.TimeOfDay);
                //fgrn.ChallanDate = fgrn.ChallanDate.Add(TimeSpan);
                addedItems1.ForEach(x =>
                {
                    double retailPrice = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID).RetailPrice;
                    x.UnitRetailPrice = (decimal)retailPrice;
                    decimal cost = fgrn.TotalCost;
                    x.GRFNo = fgrn.GRFNo;
                    fgrn.TotalCost = cost + (x.Qty * x.Rate);
                    context.GRFLineItem.Add(x);
                });

                context.GRF.Add(fgrn);

                UserLog.SaveUserLog(ref context, new UserLog(fgrn.GRFNo.ToString(), DateTime.Now, "FGRN", ScreenAction.Save));

                InventoryTransactionBridge.InsertFromFGRMEntry(ref context, fgrn, addedItems1);
                context.SaveChanges();
                return Json(new { FgrnNo = fgrn.GRFNo }, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetFGRNById(int id) 
        //{
        //    var grfItem = context.GRF.FirstOrDefault(x => x.GRFNo == id);
        //    int sifdId = FindObjectById.GetSifdById(Convert.ToInt32(grfItem.ChallanNo)).SIFDNo;
        //    int shiftFrom = FindObjectById.GetSifdById(Convert.ToInt32(grfItem.ChallanNo)).CurrentStoreID;
        //    DateTime chalanDate = FindObjectById.GetSifdById(Convert.ToInt32(grfItem.ChallanNo)).Date;
        //    if (grfItem != null)
        //    {
        //        var grfLineItem = context.GRFLineItem.Where(x => x.GRFNo == id).ToList().Select(x =>
        //        {
        //            var item = context.GRFLineItem.FirstOrDefault(y => y.GRFNo == x.GRFNo && y.ItemID == x.ItemID);
        //            x.ItemName = GetItemNameById(item.ItemID).Name;
        //            x.GroupName = GetItemNameById(item.GroupID).Name;
        //            x.ItemID = item.ItemID;
        //            x.GroupID = item.GroupID;
        //            x.Qty = item.Qty;
        //            x.Cost = item.Cost;
        //            x.Rate = item.Rate;
        //            x.PackSize = GetItemNameById(item.ItemID).PacSize;
        //            return x;
        //        }).ToList();
        //        return Json(new { grfItem, grfLineItem, sifdId, shiftFrom, chalanDate }, JsonRequestBehavior.AllowGet);    
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult DeleteGrfByid(int id)   
        {
            var grfExist = context.GRF.FirstOrDefault(x => x.GRFNo == id);
            if (grfExist != null)
            {
                context.GRF.Remove(grfExist);
                var grfLineItem = context.GRFLineItem.Where(x => x.GRFNo == id).ToList(); 
                context.GRFLineItem.Where(x => x.GRFNo == id).ToList().ForEach(x =>
                {
                    context.GRFLineItem.Remove(x);
                });

                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "FGRN", ScreenAction.Delete));
                InventoryTransactionBridge.InsertFromFGRMEntry(ref context, grfExist, new List<GRFLineItem>());
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ChartOfInventory GetItemNameById(int id)
        {
            var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == id);
            return item;
        }

        public decimal GetTotalRcvQty(int Item, int challan)
        {
            //var goodRcvNote = context.GoodsReceiveNoteLineItem.ToList().Where(x => x.ItemID == Item);
            var grf = (from G in context.GRF    
                               join GL in context.GRFLineItem on G.GRFNo equals GL.GRFNo
                               where ((GL.ItemID == Item) && (G.ChallanNo == challan.ToString()))
                               select GL.Qty).ToList();
            if (grf != null)
            {
                decimal sum = grf.Sum();
                if (sum == 0)
                {
                    return 0;
                }
                else
                {
                    return sum;
                }
            }
            else
            {
                return 0;
            }
        }
        
        public FileResult GetFgrnPreview(int grfNo)   
        {
            Session["ReportName"] = "GrfPreviewR";

            ReportParmPersister param = new ReportParmPersister();
            var grf = context.GRF.FirstOrDefault(x => x.GRFNo == grfNo);    
            if (grf != null)
            {
                param.ChalanNo = Convert.ToInt32(grf.ChallanNo);
                param.SampleNo = grf.GRFNo;
                param.SampleDate = grf.ReceiveDate;
            }
            else
            {
                param.SampleNo =0;
            }

            param.ShiftFrom = context.Store.FirstOrDefault(x => x.Id == grf.IssueFrom).Name;
            param.ShiftTo = context.Store.FirstOrDefault(x => x.Id == grf.IssueTo).Name;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == grf.DepotID).BrnachName;
            param.PostedBy = (from e in context.Employees.ToList()
                              join
                              u in context.UserLog.ToList() on e.LogInUserName equals u.LogInName
                              where u.ScreenName == "FGRN" && u.Action == "Save" && u.TrnasId == grf.GRFNo.ToString()
                              select e.Name).FirstOrDefault();

            Session["GrfPreview"] = param;

            string sql = string.Format("exec PreviewFgrn '" + grfNo + "'");
            var items = context.Database.SqlQuery<GRFSampleReport>(sql).ToList();  
            if (items.Count == 0)
            {
                GRFSampleReport report = new GRFSampleReport();   
                items.Add(report);
            }
            GrfPreviewR rd = ShowReport(param,items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");
        }

        public GrfPreviewR ShowReport(ReportParmPersister persister, List<GRFSampleReport> data)
        {
            GrfPreviewR grfPreviewR = new GrfPreviewR();

            grfPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\GrfPreviewR.rpt");
            grfPreviewR.Load(APPPATH);         
            grfPreviewR.SetDataSource(data);

            grfPreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            grfPreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            grfPreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            grfPreviewR.SetParameterValue("challanNo", persister.ChalanNo);
            grfPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            grfPreviewR.SetParameterValue("salesCenter", persister.BranchName);
            grfPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            grfPreviewR.SetParameterValue("compName", persister.CompName);
            grfPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            grfPreviewR.SetParameterValue("compContact", persister.CompContact);
            grfPreviewR.SetParameterValue("compFax", persister.Fax);
            grfPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            grfPreviewR.SetParameterValue("factContact", persister.FactContact);
            return grfPreviewR;
        }

    }
}
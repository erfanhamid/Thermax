using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.StoreRM.IRM;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.Models.SalesModule;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.StoreRM
{
    [ShowNotification]
    [Permission]
    //[Authorize(Roles = "RMAdmin,RMOperator,RMViewer,RMApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class IRMController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: IRM
        public ActionResult IRMEntry()      
        {
            var factryRMId = context.Store.FirstOrDefault(x => x.Name.ToLower().Replace(" ", string.Empty) == "factoryrm");
            var floorRmId = context.Store.FirstOrDefault(x => x.Name.ToLower().Replace(" ", string.Empty) == "floorrm");
            //var floorRmId = context.Store.FirstOrDefault(x => x.Name.ToLower().Replace(" ", string.Empty) == "storerm");
            if (factryRMId != null && floorRmId != null)
            {
                ViewBag.IssueFrom = LoadComboBox.LoadSingleStore(factryRMId.Id);
                ViewBag.IssueTo = LoadComboBox.LoadSingleStore(floorRmId.Id);
            }
            else
            {
                ViewBag.IssueFrom = LoadComboBox.LoadSingleStore(null);
                ViewBag.IssueTo = LoadComboBox.LoadSingleStore(null);
            }
           
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadRmItem(null);
            ViewBag.UoM = LoadComboBox.LoadAllUOM();
            return View();
        }

        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadRmItem(groupId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemAndGroupName(int itemId, int groupId)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            var group = FindObjectById.GetChartOfInventoryById(groupId);
            string PackSize = item.PacSize;
            string unit = FindObjectById.GetUomById(item.UoMID).Name;
            return Json(new { ItemName = item.Name, GroupName = group.Name, Unit = unit, PackSize = PackSize }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveIRM(IRM irm, List<IRMLineItem> addedItems)
        {
            using (context)
            {
                string irmNo = "";  
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.IRM.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.IRM.ToList().LastOrDefault(x => x.IRMID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                   
                    if (lastInvoice == null)
                    {
                        irmNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        irmNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.IRMID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    irmNo = yearLastTwoPart + "00001";
                }

                irm.IRMID = Convert.ToInt32(irmNo);
                irm.IRMDate = irm.IRMDate.Add(DateTime.Now.TimeOfDay);
                addedItems.ForEach(x =>
                {
                    decimal cost = irm.TotalCost;
                    x.IRMID = irm.IRMID;
                    irm.TotalCost = cost + (x.Qty * x.Rate);
                    context.IRMLine.Add(x);
                });
                context.IRM.Add(irm);
                UserLog.SaveUserLog(ref context, new UserLog(irm.IRMID.ToString(), DateTime.Now, "IRM", ScreenAction.Save));
                InventoryTransactionBridge.InsertFromIRMEntry(ref context, addedItems, irm);
                context.SaveChanges();
                return Json(new { irm = irm.IRMID }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetIrmById(int id)  
        {
            var irmItem = context.IRM.FirstOrDefault(x => x.IRMID == id);

            if (irmItem != null)
            {
                //var irmLineItem = context.IRMLine.Where(x => x.IRMID == id).ToList().Select(x =>  
                //{
                //    var item = context.IRMLine.FirstOrDefault(y => y.IRMID == x.IRMID);
                //    var interItem = GetItemNameById(item.ItemID);
                //    x.ItemName = interItem.Name;
                //    x.GroupName = GetItemNameById(item.GroupID).Name;
                //    x.ItemID = item.ItemID;
                //    x.GroupID = item.GroupID;
                //    x.Qty = item.Qty;
                //    x.Value = item.Value;
                //    x.Rate = item.Rate;
                //    x.UomID = item.UomID;
                //    x.StockQty = item.StockQty;
                //    x.PackSize = interItem.PacSize;
                //    x.UoMName = FindObjectById.GetUomById(FindObjectById.GetChartOfInventoryById(item.ItemID).UoMID).Name;
                //    return x;
                //}).ToList();
                var irmLineItem = context.IRMLine.Where(x => x.IRMID == id).ToList();
                irmLineItem.ForEach(m=>
                {
                    var item = context.ChartOfInventory.FirstOrDefault(n => n.Id == m.ItemID).Name;
                    m.ItemName = item;
                    m.GroupName = GetItemNameById(m.GroupID).Name;
                    //m.ItemID = item.ItemID;
                    //m.GroupID = item.GroupID;
                    //m.Qty = item.Qty;
                    //x.Value = item.Value;
                    //x.Rate = item.Rate;
                    //x.UomID = item.UomID;
                    //x.StockQty = item.StockQty;
                    //x.PackSize = interItem.PacSize;
                    m.UoMName = FindObjectById.GetUomById(FindObjectById.GetChartOfInventoryById(m.ItemID).UoMID).Name;

                });

                return Json(new { irmItem, irmLineItem }, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateIRM(IRM irm, List<IRMLineItem> addedItems)
        {
            using (context)
            {
                var irmExist = context.IRM.FirstOrDefault(x => x.IRMID == irm.IRMID);
                if (irmExist != null)
                {
                    context.IRM.Remove(irmExist);

                    context.IRMLine.Where(x => x.IRMID == irm.IRMID).ToList().ForEach(x => {
                        context.IRMLine.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        decimal cost = irm.TotalCost;
                        x.IRMID = irm.IRMID;
                        irm.TotalCost = cost + (x.Qty * x.Rate);
                        context.IRMLine.Add(x);
                    });
                    irm.IRMDate = irm.IRMDate.Add(DateTime.Now.TimeOfDay);
                    context.IRM.Add(irm);
                    UserLog.SaveUserLog(ref context, new UserLog(irm.IRMID.ToString(), DateTime.Now, "IRM", ScreenAction.Update));
                    InventoryTransactionBridge.InsertFromIRMEntry(ref context, addedItems, irm);
                    context.SaveChanges();
                    return Json(new { irmNo = irm.IRMID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }

        }

        public ActionResult DeleteIRMByid(int id)   
        {
            var irmExist = context.IRM.FirstOrDefault(x => x.IRMID == id);
            if (irmExist != null)
            {
                context.IRM.Remove(irmExist);

                var item = context.IRMLine.Where(x => x.IRMID == id).ToList();
                item.ForEach(x =>
                {
                    context.IRMLine.Remove(x);
                });
                
            
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "IRM", ScreenAction.Delete));
                InventoryTransactionBridge.InsertFromIRMEntry(ref context, new List<IRMLineItem>(), irmExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ChartOfInventory GetItemNameById(int id)
        {
            var item = context.ChartOfInventory.ToList().FirstOrDefault(x => x.Id == id);
            
            return item;
        }
        public ActionResult Print(int IRMNo)
        {
            if (IRMNo > 0)
            {
                ReportParmPersister param = new ReportParmPersister();
                var postedBy = (from p in context.UserLog
                                join c in context.Employees on p.LogInName equals c.LogInUserName
                                where p.ScreenName == "IRM" && p.TrnasId == IRMNo.ToString()
                                select c).ToList().FirstOrDefault();
                if (postedBy != null)
                {
                    param.PostedBy = postedBy.Name;
                }
                else
                {
                    param.PostedBy = "";
                }

                //param.PostedBy = (from p in context.UserLog
                //                  join c in context.Employees on p.LogInName equals c.LogInUserName
                //                  where p.ScreenName == "GPB" && p.TrnasId == billNo.ToString()
                //                  select c).ToList().FirstOrDefault().Name;

                string sql = string.Format("exec spPrintIRMP " + IRMNo + "");
                var items = context.Database.SqlQuery<IRMPreview>(sql).ToList();
                if (items.Count == 0)
                {
                    IRMPreview report = new IRMPreview();
                    items.Add(report);
                }
                IRMPreviewR rd = ShowReport(param, items);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                return new FileStreamResult(stream, "application/pdf");
            }
            else
                return Json("Invalid Bill No", JsonRequestBehavior.AllowGet);
        }
        public IRMPreviewR ShowReport(ReportParmPersister persister, List<IRMPreview> data)
        {
            IRMPreviewR IRMPreview = new IRMPreviewR();
            IRMPreview.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\IRMPreviewR.rpt");
            IRMPreview.Load(APPPATH);
            IRMPreview.SetDataSource(data);
            IRMPreview.SetParameterValue("postedBy", persister.PostedBy);
            return IRMPreview;
        }
    }
}
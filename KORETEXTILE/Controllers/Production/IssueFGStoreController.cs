using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.Database;
using BEEERP.Models.ProductionModule;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.Models.SalesModule;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.CrystalReport.ReportFormat;
using System.IO;

namespace BEEERP.Controllers.Production
{
    //[Permission]
    [ShowNotification]
    //[Authorize(Roles = "ProAdmin,ProOperator,ProViewer,ProApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class IssueFGStoreController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: IssueFGStore
        public ActionResult IssueFGStore()  
        {
            string ifgsNo = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.IssueFGStore.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.IssueFGStore.ToList().LastOrDefault(x => x.clmIFGSID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                
                if (lastInvoice == null)
                {
                    ifgsNo = yearLastTwoPart + "00001";
                }
                else
                {
                    ifgsNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.clmIFGSID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                ifgsNo = yearLastTwoPart + "00001";
            }
            ViewBag.IFGSNo = ifgsNo;
            ViewBag.Group = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.UoM = LoadComboBox.LoadAllUOM();
            ViewBag.IssueFrom = LoadComboBox.GetIssuFrom();
            ViewBag.IssueTo = LoadComboBox.GetIssuTo();
            return View();
        }

        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemName(int id)
        {
            var item = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == id);
            return Json(new { Name = item.Name, PackSize = item.PacSize }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveIssueFGToStore( IssueFGStore issuFGStore, List<IssueFGStoreLineItem> addedItems)    
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Issue Floor FG to Factory FG").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (issuFGStore.clmIFGSDate > IsExpired.SELDate)
            {
                using (context)
                {
                    string ifgsNo = "";
                    string yearLastTwoPart = issuFGStore.clmIFGSDate.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = context.IssueFGStore.Count();
                    if (noOfInvoice > 0)
                    {
                        var lastInvoice = context.IssueFGStore.ToList().LastOrDefault(x => x.clmIFGSID.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastInvoice == null)
                        {
                            ifgsNo = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            ifgsNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.clmIFGSID.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        ifgsNo = yearLastTwoPart + "00001";
                    }
                    decimal cost = 0;
                    issuFGStore.clmIFGSID = Convert.ToInt32(ifgsNo);
                    issuFGStore.clmIFGSDate = issuFGStore.clmIFGSDate.Add(DateTime.Now.TimeOfDay);
                    issuFGStore.clmGRQNO = "1";
                    addedItems.ForEach(x =>
                    {
                        //decimal cost = issuFGStore.clmCostTotal;
                        x.clmIFGSNo = issuFGStore.clmIFGSID;
                        cost += (x.clmQty * x.clmCostRt);
                        context.IssueFGStoreLineItem.Add(x);
                    });
                    issuFGStore.clmCostTotal = cost;
                    issuFGStore.clmIFGSDate = issuFGStore.clmIFGSDate.Add(DateTime.Now.TimeOfDay);
                    context.IssueFGStore.Add(issuFGStore);

                    UserLog.SaveUserLog(ref context, new UserLog(issuFGStore.clmIFGSID.ToString(), issuFGStore.clmIFGSDate, "IFGS", ScreenAction.Save));
                    // InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFGStore, addedItems);
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { ifgsNo = issuFGStore.clmIFGSID }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
            
            
        }

        public ActionResult GetResultId(int id)
        {
            var ifgsItem = context.IssueFGStore.FirstOrDefault(x => x.clmIFGSID == id);

            if (ifgsItem != null)
            {
                var ifgsLineItem = context.IssueFGStoreLineItem.Where(x => x.clmIFGSNo == id).ToList().Select(x =>
                {
                    //var item = context.IssueFGStoreLineItem.FirstOrDefault(y => y.clmItemID == x.clmItemID);
                    x.ItemName = GetItemNameById(x.clmItemID);
                    //x.AvailableQty = GetAvailableQty(x.clmItemID, , IDate);
                    x.PcsPerCtn = (int)FindObjectById.GetChartOfInventoryById(x.clmItemID).clmCartonCapacity;
                    x.PackSize = FindObjectById.GetChartOfInventoryById(x.clmItemID).PacSize;
                    return x;
                }).ToList();
                return Json(new { ifgsItem, ifgsLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateIssueFGToStore(IssueFGStore issuFGStore, List<IssueFGStoreLineItem> addedItems)   
        {
            using (context)
            {
                var ifgsExist = context.IssueFGStore.FirstOrDefault(x => x.clmIFGSID == issuFGStore.clmIFGSID);
                if (ifgsExist != null)
                {
                    context.IssueFGStore.Remove(ifgsExist);

                    context.IssueFGStoreLineItem.Where(x => x.clmIFGSNo == issuFGStore.clmIFGSID).ToList().ForEach(x => {
                        context.IssueFGStoreLineItem.Remove(x);
                    });
                    decimal cost = 0;
                    addedItems.ForEach(x =>
                    {
                        //decimal cost = issuFGStore.clmCostTotal;
                        x.clmIFGSNo = issuFGStore.clmIFGSID;
                        cost += (x.clmQty * x.clmCostRt);
                        context.IssueFGStoreLineItem.Add(x);
                    });
                    issuFGStore.clmGRQNO = "1";
                    issuFGStore.clmIFGSDate = issuFGStore.clmIFGSDate.Add(DateTime.Now.TimeOfDay);
                    context.IssueFGStore.Add(issuFGStore);
                    UserLog.SaveUserLog(ref context, new UserLog(issuFGStore.clmIFGSID.ToString(), issuFGStore.clmIFGSDate, "IFGS", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context,  issuFGStore, addedItems);
                    context.SaveChanges();
                    return Json(new { ifgsNo = issuFGStore.clmIFGSID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }


        }

        public ActionResult GetItemRemainQty(int itemId, int storeId, string Date)
        {
            var IDate = Convert.ToDateTime(Date);
            var item = FindObjectById.GetChartOfInventoryById(itemId);
           
            //return Json(new { RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId, null), Price = item.RetailPrice, SCost = item.clmStandardCost, UoM = FindObjectById.GetUomById(item.UoMID).Id, Vat = item.VatPerc, Discount = item.DisPerc, PacSize = item.PacSize, cartoncapacity = item.clmCartonCapacity }, JsonRequestBehavior.AllowGet);
            return Json(new { RemQty = GetAvailableQty(itemId, storeId, IDate), Price = item.RetailPrice, SCost = item.clmStandardCost, UoM = FindObjectById.GetUomById(item.UoMID).Id,   }, JsonRequestBehavior.AllowGet);
        }
        public decimal GetAvailableQty( int itemId, int storeId,DateTime IDate)
        {
            var str = string.Format("exec getItemAvailableQty "  + storeId + "," + itemId);
            var Qty = context.Database.SqlQuery<RemaingQty>(str).ToList();
            return Qty.FirstOrDefault().AvalailableQty;
        }
        public ActionResult DeleteIFGSByid(int id)  
        {
            IssueFGStore ifgsExist = context.IssueFGStore.FirstOrDefault(x => x.clmIFGSID == id);
            if (ifgsExist != null)
            {
                context.IssueFGStore.Remove(ifgsExist);
                var issuFgLineItem = context.IssueFGStoreLineItem.Where(x => x.clmIFGSNo == id).ToList();
                context.IssueFGStoreLineItem.Where(x => x.clmIFGSNo == id).ToList().ForEach(x =>
                {
                    context.IssueFGStoreLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "IFGS", ScreenAction.Delete));
                //InventoryTransactionBridge.DeleteFromIFGSEntry(ref context, ifgsExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public string GetItemNameById(int id)   
        {
            var item = LoadComboBox.GetItemInfo().ToList().FirstOrDefault(x => x.Id == id);
            return item.Id + " - " + item.Name + " - " + item.PacSize; 
        }

        public Dictionary<string,string> GetIssuFrom()
        {
            var floorFg = context.Store.FirstOrDefault(x => x.Name.ToLower() == "floor fg");
            if (floorFg != null)
            {
                var items = new Dictionary<string, string>();
                items.Add(floorFg.Id.ToString(), floorFg.Name);
                return items;
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }

        public Dictionary<string,string> GetIssueTo()
        {
            var factoryFg = context.Store.FirstOrDefault(x => x.Name.ToLower() == "factory fg");
            if (factoryFg != null)
            {
                var items = new Dictionary<string, string>();
                items.Add(factoryFg.Id.ToString(), factoryFg.Name);
                return items;
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }

        public FileResult GetIFGSPreview(int clmIFGSID)
        {
            Session["ReportName"] = "Shift Finish Goods Inventory";

            ReportParmPersister param = new ReportParmPersister();

            var sample = context.IssueFGStore.FirstOrDefault(x => x.clmIFGSID == clmIFGSID);
            if (sample != null)
            {
                param.SampleNo = sample.clmIFGSID;
                param.SampleDate = sample.clmIFGSDate;
            }
            else
            {
                param.SampleNo = 0;
            }
            var issFrom = context.Store.FirstOrDefault(x => x.Id == 24);
            param.ShiftFrom = issFrom.Name;
            param.ShiftTo = context.Store.FirstOrDefault(x => x.Id == 26).Name;          
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == issFrom.DepotID).BrnachName;
            //param.PostedBy = (from e in context.Employees.AsEnumerable()
            //                  join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "IFGS" && u.Action == "Save" && u.TrnasId == sample.clmIFGSID.ToString()
            //                  select e.Name).FirstOrDefault();

            string sql = string.Format("exec IFGSPreview '" + clmIFGSID + "'");
            var items = context.Database.SqlQuery<IFGSPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                IFGSPreviewReport report = new IFGSPreviewReport();
                items.Add(report);
            }

            IFGSPreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public IFGSPreviewR ShowReport(ReportParmPersister persister, List<IFGSPreviewReport> data)
        {
            IFGSPreviewR ifgsPreviewR = new IFGSPreviewR();

            ifgsPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\IFGSPreviewR.rpt");
            ifgsPreviewR.Load(APPPATH);
            ifgsPreviewR.SetDataSource(data);

            ifgsPreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            ifgsPreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            ifgsPreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            ifgsPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            ifgsPreviewR.SetParameterValue("salesCenter", persister.BranchName);
            //ifgsPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            ifgsPreviewR.SetParameterValue("compName", persister.CompName);
            ifgsPreviewR.SetParameterValue("compAddress", persister.CompAddress);
            ifgsPreviewR.SetParameterValue("compContact", persister.CompContact);
            //ifgsPreviewR.SetParameterValue("compFax", persister.Fax);
            //ifgsPreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //ifgsPreviewR.SetParameterValue("factContact", persister.FactContact);
            return ifgsPreviewR;
        }

    }
    public class RemaingQty
    {
        public decimal AvalailableQty { get; set; }
    }
}
using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.Store_FG;
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.Store_FG
{
    [ShowNotification]
    [Authorize(Roles = "FGAdmin,FGOperator,FGViewer,FGApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class ShiftInventoryController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: ShiftInventory
        public ActionResult ShiftInventory()    
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
            //var depot = context.BranchInformation.FirstOrDefault(x => x.BrnachName.ToLower() == "al-madina");
            ViewBag.Store = LoadComboBox.LoadSingleStore(context.SysSet.FirstOrDefault().FactoryFG);
            ViewBag.StoreFG = LoadComboBox.LoadAllFGRevStoreByDepot(depot);
            ViewBag.Group = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadFGItem(null);
            ViewBag.Challan = generate.GenerateChallamNo(context);


            var factoryFg = context.Store.FirstOrDefault(x => x.Name.ToLower() == "factory fg");
            if (factoryFg != null)
            {
                ViewBag.ShiftFrom = factoryFg.Id;
            }
            return View();
        }
        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemDetails(int itemId, int storeId, DateTime date)  
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            return Json(new { RemQty = SaleCommonInfo.GetRemainItemQtyByDate(itemId, storeId, date), CTNCapacity = FindObjectById.GetChartOfInventoryById(itemId).clmCartonCapacity, Price = FindObjectById.GetChartOfInventoryById(itemId).RetailPrice, SCost = FindObjectById.GetChartOfInventoryById(itemId).clmStandardCost, UoM = FindObjectById.GetUomById(FindObjectById.GetChartOfInventoryById(itemId).UoMID).Name }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemAndGroupName(int itemId, int groupId ) 
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            var group = FindObjectById.GetChartOfInventoryById(groupId);
            //string PackSize = item.;
            //string unit = FindObjectById.GetUomById(item.UoMID).Name;
            return Json(new { ItemName = item.Name, GroupName = group.Name }, JsonRequestBehavior.AllowGet);    
        }

        public ActionResult SaveSIFD(SIFD sifd, List<SIFDLineItem> addedItems)  
        {
            using (context)
            {
                string sifdNo = "";
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.SIFD.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.SIFD.ToList().LastOrDefault(x => x.SIFDNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    sifdNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.SIFDNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    sifdNo = yearLastTwoPart + "00001";
                }

                sifd.SIFDNo = Convert.ToInt32(sifdNo);
                sifd.Date = sifd.Date.Add(DateTime.Now.TimeOfDay);
                var deotId = ScreenSessionData.GetEmployeeDepot();
                var depot = context.BranchInformation.FirstOrDefault(x => x.Slno == deotId);
                sifd.Depot = depot.Slno;

                addedItems.ForEach(x =>
                {
                    double retailPrice = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID).RetailPrice;
                    x.UnitRetailPrice = (decimal)retailPrice;
                    decimal cost = sifd.TotalCost;
                    x.SIFDNo = sifd.SIFDNo;
                    sifd.TotalCost = cost + (x.ShiftQty * x.CostRt);
                    context.SIFDLineItem.Add(x);
                });

                context.SIFD.Add(sifd);

                UserLog.SaveUserLog(ref context, new UserLog(sifd.SIFDNo.ToString(), DateTime.Now, "SIFG", ScreenAction.Save));

                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                context.SaveChanges();
                return Json(new { sifd = sifd.SIFDNo, ChallanNo = sifd.ChallanNo }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSIFDById(int id) 
        {
            var sifdItem = context.SIFD.FirstOrDefault(x => x.SIFDNo == id);

            if (sifdItem != null)
            {
                var sifdLineItem = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList().Select(x =>
                {
                    var item = context.SIFDLineItem.FirstOrDefault(y => y.Id == x.Id);
                    x.ItemName = GetItemNameById(item.ItemID).Name;
                    x.GroupName = GetItemNameById(item.ItemGrpID).Name;
                    x.ItemID = item.ItemID;
                    x.ItemGrpID = item.ItemGrpID;
                    x.ShiftQty = item.ShiftQty;
                    x.CtnQty = GetItemNameById(item.ItemID).clmCartonCapacity;
                    x.CostVal = item.CostVal;
                    x.CostRt = item.CostRt;
                    x.PackSize = GetItemNameById(item.ItemID).PacSize;
                    return x;
                }).ToList();
                return Json(new { sifdItem, sifdLineItem }, JsonRequestBehavior.AllowGet);  
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSIFD(SIFD sIFD, List<SIFDLineItem> addedItems) 
        {
            using (context)
            {
                var sifdExist = context.SIFD.FirstOrDefault(x => x.SIFDNo == sIFD.SIFDNo); 
                if (sifdExist != null)
                {
                    context.SIFD.Remove(sifdExist);

                    context.SIFDLineItem.Where(x => x.SIFDNo == sIFD.SIFDNo).ToList().ForEach(x => {
                        context.SIFDLineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        double retailPrice = context.ChartOfInventory.FirstOrDefault(y => y.Id == x.ItemID).RetailPrice;
                        x.UnitRetailPrice = (decimal)retailPrice;
                        decimal cost = sIFD.TotalCost;
                        x.SIFDNo = sIFD.SIFDNo;
                        sIFD.TotalCost = cost + (x.ShiftQty * x.CostRt);
                        context.SIFDLineItem.Add(x);
                    });
                    sIFD.Date = sIFD.Date.Add(DateTime.Now.TimeOfDay);
                    var deotId = ScreenSessionData.GetEmployeeDepot();
                    var depot = context.BranchInformation.FirstOrDefault(x => x.Slno == deotId);
                    sIFD.Depot = depot.Slno;
                    context.SIFD.Add(sIFD);

                    UserLog.SaveUserLog(ref context, new UserLog(sIFD.SIFDNo.ToString(), DateTime.Now, "SIFD", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { sifdNo = sIFD.SIFDNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
           
        }

        public ActionResult DeleteSIFDByid(int id)  
        {
            var sifgdExist = context.SIFD.FirstOrDefault(x => x.SIFDNo == id);
            if (sifgdExist != null)
            {
                context.SIFD.Remove(sifgdExist);
                //var sifdFgLineItem = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList();
                context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList().ForEach(x =>
                {
                    context.SIFDLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "SIFD", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
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

        public FileResult GetShiftFGPreview(int sifdNo)
        {
            Session["ReportName"] = "ShiftFGPreviewR";

            ReportParmPersister param = new ReportParmPersister();

            var sample = context.SIFD.FirstOrDefault(x => x.SIFDNo == sifdNo);
            if (sample != null)
            {
                param.ChalanNo = sample.ChallanNo;
                param.SampleNo = sample.SIFDNo;
                param.SampleDate = sample.Date;
            }
            else
            {
                param.SampleNo = 0;
            }
            param.ShiftFrom = context.Store.FirstOrDefault(x => x.Id == sample.CurrentStoreID).Name;
            param.ShiftTo = context.Store.FirstOrDefault(x => x.Id == sample.NewStoreID).Name;
            param.BranchName = context.BranchInformation.FirstOrDefault(x => x.Slno == sample.Depot).BrnachName;
            param.PostedBy = (from e in context.Employees.AsEnumerable()
                              join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                              where u.ScreenName == "SIFG" && u.Action == "Save" && u.TrnasId == sample.SIFDNo.ToString()
                              select e.Name).FirstOrDefault();

            Session["ShiftFGPreview"] = param;

            string sql = string.Format("exec ShiftFGPreview '" + sifdNo + "'");
            var items = context.Database.SqlQuery<ShiftFGPreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                ShiftFGPreviewReport report = new ShiftFGPreviewReport();
                items.Add(report);
            }

            ShiftFGPreviewR rd = ShowReport(param, items);
            rd.SetParameterValue("compName", param.CompName);
            rd.SetParameterValue("compAddress", param.CompAddress);
            rd.SetParameterValue("compContact", param.CompContact);
            rd.SetParameterValue("compFax", param.Fax);
            rd.SetParameterValue("factAddress", param.FactAddress);
            rd.SetParameterValue("factContact", param.FactContact);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public ShiftFGPreviewR ShowReport(ReportParmPersister persister, List<ShiftFGPreviewReport> data)
        {
            ShiftFGPreviewR shiftFGPreviewR = new ShiftFGPreviewR();

            shiftFGPreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\ShiftFGPreviewR.rpt");
            shiftFGPreviewR.Load(APPPATH);
            shiftFGPreviewR.SetDataSource(data);

            shiftFGPreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            shiftFGPreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            shiftFGPreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            shiftFGPreviewR.SetParameterValue("challanNo", persister.ChalanNo);
            shiftFGPreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            shiftFGPreviewR.SetParameterValue("salesCenter", persister.BranchName);
            shiftFGPreviewR.SetParameterValue("postedBy", persister.PostedBy);
            return shiftFGPreviewR;
        }
    }
}
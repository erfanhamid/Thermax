using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Store_FG;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreDepot
{
    [ShowNotification]
    [Authorize(Roles = "FGAdmin,FGOperator,FGViewer,FGApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    public class InterDepotTranController : Controller
    {
        BEEContext context = new BEEContext();
        GenerateId generate = new GenerateId();
        // GET: ShiftInventory
        public ActionResult DepotTransfer()
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
            ViewBag.Store = LoadComboBox.LoadAllFGStore();
            ViewBag.StoreFG = LoadComboBox.LoadAllFGStore();
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
            //return Json(new { RemQty = SaleCommonInfo.GetRemainItemQtyByDate(itemId, storeId, date), CTNCapacity = FindObjectById.GetChartOfInventoryById(itemId).clmCartonCapacity, Price = FindObjectById.GetChartOfInventoryById(itemId).RetailPrice, SCost = FindObjectById.GetChartOfInventoryById(itemId).clmStandardCost, UoM = FindObjectById.GetUomById(FindObjectById.GetChartOfInventoryById(itemId).UoMID).Name, Vat = FindObjectById.GetChartOfInventoryById(itemId).VatPerc, Discount = FindObjectById.GetChartOfInventoryById(itemId).DisPerc }, JsonRequestBehavior.AllowGet);
            return Json(new { RemQty = SaleCommonInfo.GetRemainItemQtyByDate(itemId, storeId, date), CTNCapacity = FindObjectById.GetChartOfInventoryById(itemId).clmCartonCapacity, Price = FindObjectById.GetChartOfInventoryById(itemId).RetailPrice, SCost = FindObjectById.GetChartOfInventoryById(itemId).clmStandardCost, UoM = FindObjectById.GetUomById(FindObjectById.GetChartOfInventoryById(itemId).UoMID).Name }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemAndGroupName(int itemId, int groupId)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            var group = FindObjectById.GetChartOfInventoryById(groupId);
            //string PackSize = item.PacSize;
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
                var depot = context.BranchInformation.FirstOrDefault(x => x.Slno==deotId);
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

                    context.SIFDLineItem.Where(x => x.SIFDNo == sIFD.SIFDNo).ToList().ForEach(x =>
                    {
                        context.SIFDLineItem.Remove(x);
                    });

                    sIFD.Date = sIFD.Date.Add(DateTime.Now.TimeOfDay);
                    var deotId = ScreenSessionData.GetEmployeeDepot();
                    var depot = context.BranchInformation.FirstOrDefault(x => x.Slno == deotId);
                    sIFD.Depot = depot.Slno;

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

        public FileResult GetIDTSamplePreview(int sifdNo)
        {
            Session["ReportName"] = "IDTSamplePreviewR";

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

            Session["IDTSamplePreview"] = param;

            string sql = string.Format("exec IDTSamplePreview '" + sifdNo + "'");
            var items = context.Database.SqlQuery<IDTSamplePreviewReport>(sql).ToList();
            if (items.Count == 0)
            {
                IDTSamplePreviewReport report = new IDTSamplePreviewReport();
                items.Add(report);
            }

            IDTSamplePreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public IDTSamplePreviewR ShowReport(ReportParmPersister persister, List<IDTSamplePreviewReport> data)
        {
            IDTSamplePreviewR idtSamplePreviewR = new IDTSamplePreviewR();

            idtSamplePreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\IDTSamplePreviewR.rpt");
            idtSamplePreviewR.Load(APPPATH);
            idtSamplePreviewR.SetDataSource(data);

            idtSamplePreviewR.SetParameterValue("shiftFrom", persister.ShiftFrom);
            idtSamplePreviewR.SetParameterValue("shiftTo", persister.ShiftTo);
            idtSamplePreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            idtSamplePreviewR.SetParameterValue("challanNo", persister.ChalanNo);
            idtSamplePreviewR.SetParameterValue("sampleDate", persister.SampleDate.ToString("dd-MM-yyyy"));
            idtSamplePreviewR.SetParameterValue("salesCenter", persister.BranchName);
            idtSamplePreviewR.SetParameterValue("postedBy", persister.PostedBy);
            idtSamplePreviewR.SetParameterValue("compName", persister.CompName);
            idtSamplePreviewR.SetParameterValue("compAddress", persister.CompAddress);
            idtSamplePreviewR.SetParameterValue("compContact", persister.CompContact);
            idtSamplePreviewR.SetParameterValue("compFax", persister.Fax);
            idtSamplePreviewR.SetParameterValue("factAddress", persister.FactAddress);
            idtSamplePreviewR.SetParameterValue("factContact", persister.FactContact);
            return idtSamplePreviewR; 
        }
    }
}
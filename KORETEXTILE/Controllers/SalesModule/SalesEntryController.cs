using BEEERP.Controllers.StoreDepot;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BEEERP.Controllers.SalesModule
{
    [ShowNotification]
    public class SalesEntryController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SalesEntry
        public ActionResult SalesEntry()
        {
            string invoice = "";
            string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            var noOfInvoice = context.SalesEntryNew.Count();
            if (noOfInvoice > 0)
            {
                var lastInvoice = context.SalesEntryNew.ToList().LastOrDefault(x => x.InvoiceNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                if (lastInvoice == null)
                {
                    invoice = yearLastTwoPart + "00001";
                }
                else
                {
                    invoice = yearLastTwoPart + (Convert.ToInt32(lastInvoice.InvoiceNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                invoice = yearLastTwoPart + "00001";
            }
            ViewBag.Invoice = invoice;
            var DepotID = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = DepotID;
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            //ViewBag.Group =LoadItemGroup();
            ViewBag.Item = LoadAllItem();
            ViewBag.Store = LoadComboBox.LoadStore(null);
            //ViewBag.SR = LoadComboBox.LoadSRByDepot(null);
            ViewBag.Commission = context.Commission.FirstOrDefault().CommissionPercentage;
            return View();
        }


        public static SelectList LoadItemGroup()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select Group ---");
            string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'g' and rootAccountType = 'FG' ";
            List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
            Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() + " - " + x.ItemName); });
            return new SelectList(items, "Key", "Value");
        }
        public static SelectList LoadItem(int? group)
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (group == null)
            {
                items.Add("", "--- Select Item ---");
                return new SelectList(items, "Key", "Value");
            }
            else
            {
                items.Add("", "--- Select Item ---");
                string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'l' and parentId = " + group + "";
                List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
                Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() + " - " + x.ItemName); });
                //context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.parentId == group).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name + " - " + x.PacSize); });
                return new SelectList(items, "Key", "Value");
            }

        }
        public static SelectList LoadAllItem()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
           
                items.Add("", "--- Select Item ---");
                string sql = "select id as ItemID, Name as ItemName from chartofInv where type = 'l' and rootAccountType = 'FG'";
                List<ItemInformation> Data = context.Database.SqlQuery<ItemInformation>(sql).ToList();
                Data.ForEach(x => { items.Add(x.ItemID.ToString(), x.ItemID.ToString() + " - " + x.ItemName); });
                //context.ChartOfInventory.Where(x => x.type.ToLower() == "l" && x.parentId == group).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.ItemCode + " - " + x.Name + " - " + x.PacSize); });
                return new SelectList(items, "Key", "Value");
            

        }

        public ActionResult GetItemByGroup(int group)
        {
            if(group != 0 && group.ToString() != null)
            {
                return Json(LoadItem(group), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }
       
        public ActionResult GetStoreByDepot(int Depot)
        {
            if (Depot != 0 && Depot.ToString() != null)
            {
                return Json(new {store= LoadComboBox.LoadStore(Depot)}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        
        public ActionResult GetSalesManInfoId(int id, int depot)
        {
            if (id != 0 && depot != 0)
            {
                var empInfo = context.Employees.FirstOrDefault(x => x.Id == id && x.BranchID == depot);
                if (empInfo != null)
                {
                    

                    return Json(empInfo.Name , JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetItemRemainQty(int itemId, int storeId)
        {
            decimal RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId, null);
            var data = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == itemId);
            var uom = data.UOM;
            var PCPerCtn = data.clmCartonCapacity;
            var RemCtn = RemQty / PCPerCtn;
            var Price = data.RetailPrice;
            var Cogs = data.clmStandardCost;
            return Json(new { RemQty, uom, PCPerCtn, RemCtn,Price,Cogs }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveSale(SalesEntryNew Sales, List<SalesEntryNewLineItem> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Sales Invoice").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (Sales.SalesDate > IsExpired.SELDate)
            {
                if (Sales != null && addedItems.Count > 0)
                {
                    string invoice = "";
                    string yearLastTwoPart = Sales.SalesDate.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = context.SalesEntryNew.Count();
                    if (noOfInvoice > 0)
                    {
                        var lastInvoice = context.SalesEntryNew.ToList().LastOrDefault(x => x.InvoiceNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastInvoice == null)
                        {
                            invoice = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            invoice = yearLastTwoPart + (Convert.ToInt32(lastInvoice.InvoiceNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        invoice = yearLastTwoPart + "00001";
                    }
                    Sales.InvoiceNo = Convert.ToInt32(invoice);
                    context.SalesEntryNew.Add(Sales);
                    foreach (var s in addedItems)
                    {
                        s.InvoiceNo = Sales.InvoiceNo;
                        context.SalesEntryNewLineItem.Add(s);
                    }
                    AccountModuleBridge.InsertFromSales(ref context, Sales);
                    CustomerCalculationBridge.InsertFromSales(ref context, Sales, addedItems);
                    InventoryTransactionBridge.InsertUpdateFromSales(ref context, Sales, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(Sales.InvoiceNo.ToString(), Sales.SalesDate, "Sales", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(Sales.InvoiceNo, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult GetSalesById(int id)
        {
            
            var sales = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == id);
            if(sales != null)
            {
                var salesLine = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == id).ToList();
                salesLine.ForEach(x => {
                    x.GroupId = LoadComboBox.GetItemInfo().FirstOrDefault(i => i.Id == x.ItemID).ParentId;
                    x.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(i => i.Id == x.ItemID).Name;
                    x.StoreName = context.Store.FirstOrDefault(y => y.Id == x.StoreID).Name;
                    x.COGOValue = 0;

                });
                return Json(new { sales = sales, line = salesLine }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
        }

        public ActionResult UpdateSale(SalesEntryNew Sales, List<SalesEntryNewLineItem> addedItems)
        {
            if (Sales != null && addedItems.Count > 0)
            {
                var IsExist = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == Sales.InvoiceNo);
                if(IsExist != null)
                {
                    context.SalesEntryNew.Remove(IsExist);
                    var lineExist = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == Sales.InvoiceNo).ToList();
                    if(lineExist.Count > 0)
                    {
                        foreach(var a in lineExist)
                        {
                            context.SalesEntryNewLineItem.Remove(a);
                        }
                    }
                }
                context.SalesEntryNew.Add(Sales);
                foreach (var s in addedItems)
                {
                    s.InvoiceNo = Sales.InvoiceNo;
                    context.SalesEntryNewLineItem.Add(s);
                }
                UserLog.SaveUserLog(ref context, new UserLog(Sales.InvoiceNo.ToString(), Sales.SalesDate, "Sales", ScreenAction.Update));
                AccountModuleBridge.InsertFromSales(ref context, Sales);
                CustomerCalculationBridge.InsertFromSales(ref context, Sales, addedItems);
                InventoryTransactionBridge.InsertUpdateFromSales(ref context, Sales, addedItems);
                context.SaveChanges();
                return Json(Sales.InvoiceNo, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteSalesByid(int id)
        {
            var SalesExist = context.SalesEntryNew.FirstOrDefault(x => x.InvoiceNo == id);
            if(SalesExist != null)
            {
                context.SalesEntryNew.Remove(SalesExist);
                var line = context.SalesEntryNewLineItem.Where(x => x.InvoiceNo == id).ToList();
                if(line.Count > 0)
                {
                    foreach (var l in line)
                    {
                        context.SalesEntryNewLineItem.Remove(l);
                    }
                }
                AccountModuleBridge.DeleteFromSales(ref context, SalesExist,null);
                CustomerCalculationBridge.DeleteFromSales(ref context, SalesExist, line);
                InventoryTransactionBridge.DeleteFromSales(ref context, SalesExist);
                UserLog.SaveUserLog(ref context, new UserLog(SalesExist.InvoiceNo.ToString(), SalesExist.SalesDate, "Sales", ScreenAction.Delete));
                context.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public FileResult GetSalesPreview(int InvoiceNo)
        {

            string sql = string.Format("exec PreviewSalesInvoice " + InvoiceNo);
            var items = context.Database.SqlQuery<PreviewSalesInvoice>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewSalesInvoice report = new PreviewSalesInvoice();
                items.Add(report);
            }

            PreviewSalesInvoiceR rd = ShowReport(items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PreviewSalesInvoiceR ShowReport(List<PreviewSalesInvoice> data)
        {
            PreviewSalesInvoiceR sales = new PreviewSalesInvoiceR();

            sales.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewSalesInvoiceR.rpt");
            sales.Load(APPPATH);
            sales.SetDataSource(data);
            //var comp = context.Database.SqlQuery<CompanyNameOne>("exec CompInfo").ToList();
            //RMC.SetParameterValue("postedBy", persister.PostedBy);
            //DPR.SetParameterValue("compName", comp.FirstOrDefault().CompName);
            //DPR.SetParameterValue("compAddress", comp.FirstOrDefault().CompAddress);
            //RMC.SetParameterValue("reportName", Session["reportName"]);
            return sales;
        }

    }
}
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.StoreDepot;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.StoreDepot
{
    //[Permission]
    //[StoreFGModule]
    [ShowNotification]
    public class DSIAController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: DSIA
        public ActionResult DSIA()  
        {
            //string dsiaNo = "";
            //string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
            //var noOfInvoice = context.DSIA.Count();
            //if (noOfInvoice > 0)
            //{
            //    var lastInvoice = context.DSIA.ToList().LastOrDefault(x => x.DSIANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
            //    dsiaNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.DSIANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
            //}
            //else
            //{
            //    dsiaNo = yearLastTwoPart + "00001";
            //}

            //ViewBag.DsiaNo = dsiaNo;
            var depot = LoadComboBox.LoadAllDepot();
            ViewBag.DepotId = depot;
            if (depot == null)
            {
                
                ViewBag.Store = LoadComboBox.LoadAllFGStoreByDepot(null);
            }
            else
            {
                
                ViewBag.Store = LoadComboBox.LoadAllFGStoreByDepot(null);
            }

            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Group = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.Type = LoadDamageOrSample();
            //ViewBag.Category = LoadCategory();
            return View();
        }

        public ActionResult GetStoreByDepotId(int id)
        {
            var store = LoadComboBox.LoadStore(id);
           // var tso = LoadComboBox.LoadEmployeeByDepot(id);
            return Json(new { Store = store}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Savedsia(DSIA dsia, List<DSIALineItem> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Demage Sample Inventory Adjustment").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (dsia.DSIADate > IsExpired.SELDate)
            {
                using (context)
                {
                    string dsiaNo = "";
                    string yearLastTwoPart = dsia.DSIADate.Year.ToString().Substring(2, 2).ToString();
                    var noOfInvoice = context.DSIA.Count();
                    if (noOfInvoice > 0)
                    {
                        var lastInvoice = context.DSIA.ToList().LastOrDefault(x => x.DSIANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        
                        if (lastInvoice == null)
                        {
                            dsiaNo = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            dsiaNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.DSIANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        dsiaNo = yearLastTwoPart + "00001";
                    }

                    dsia.DSIANo = Convert.ToInt32(dsiaNo);
                    dsia.DSIADate = dsia.DSIADate.Add(DateTime.Now.TimeOfDay);
                    addedItems.ForEach(x =>
                    {
                        x.DSIANo = dsia.DSIANo;
                    //x.ClmCostPrice = LoadComboBox.GetItemUnitCost(x.ItemID,"DSIA",x.AdjQTY, dsia.DSIANo, dsia.StoreID,false);
                    x.COGSValue = x.COGSRate * x.AdjQTY;

                        context.DSIALineItem.Add(x);
                    });

                    context.DSIA.Add(dsia);

                    UserLog.SaveUserLog(ref context, new UserLog(dsia.DSIANo.ToString(), dsia.DSIADate, "DSIA", ScreenAction.Save));
                    InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                    AccountModuleBridge.InsertUpdateFromDSIA(ref context, dsia, addedItems);
                    context.SaveChanges();
                    return Json(new { dsia }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(3, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetItemRemainQty(int itemId, int storeId)
        {
            var item = FindObjectById.GetChartOfInventoryById(itemId);
            //return Json(new { RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId, null), Price = item.RetailPrice, SCost = item.clmStandardCost, UoM = FindObjectById.GetUomById(item.UoMID).Id, Vat = item.VatPerc, Discount = item.DisPerc, PacSize = item.PacSize, cartoncapacity = item.clmCartonCapacity }, JsonRequestBehavior.AllowGet);
            return Json(new { RemQty = SaleCommonInfo.GetRemainItemQty(itemId, storeId, null), Price = item.RetailPrice, SCost = item.clmStandardCost, UoM = FindObjectById.GetUomById(item.UoMID).Name, PacSize = item.PacSize, cartoncapacity = item.clmCartonCapacity }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDSIAById(int id) {

            var dsiaItem = context.DSIA.FirstOrDefault(x => x.DSIANo == id);
            if (dsiaItem != null)
            {
                var Depot = context.Store.FirstOrDefault(x => x.Id == dsiaItem.StoreID).DepotID;
                var dsiaLineItem = context.DSIALineItem.Where(x => x.DSIANo == id).ToList().Select(x =>  
                {
                    var item = context.DSIALineItem.FirstOrDefault(y => y.DSIANo == x.DSIANo && y.ItemID == x.ItemID);
                    var chartOfInv = LoadComboBox.GetItemInfo().FirstOrDefault(z => z.Id == x.ItemID);   
                    x.ItemName = GetItemNameById(item.ItemID);
                    x.ItemID = item.ItemID;
                    x.AdjQTY = item.AdjQTY;
                    x.GroupId = chartOfInv.ParentId;
                    x.UnitPerCtn =(int)chartOfInv.clmCartonCapacity;
                    return x;
                }).ToList();
                return Json(new { dsiaItem, dsiaLineItem , Depot}, JsonRequestBehavior.AllowGet);  
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateDsia(DSIA dsia, List<DSIALineItem> addedItems) {

            using (context)
            {
                var dsiaExist = context.DSIA.FirstOrDefault(x => x.DSIANo == dsia.DSIANo);
                if (dsiaExist != null)
                {
                    context.DSIA.Remove(dsiaExist);

                    context.DSIALineItem.Where(x => x.DSIANo == dsia.DSIANo).ToList().ForEach(x => {
                        context.DSIALineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.DSIANo = dsia.DSIANo;
                        x.COGSValue = x.COGSRate * x.AdjQTY;
                        // x.ClmCostPrice = LoadComboBox.GetItemUnitCost(x.ItemID, "DSIA", x.AdjQTY, dsia.DSIANo, dsia.StoreID,true);
                        context.DSIALineItem.Add(x);
                    });

                    dsia.DSIADate = dsia.DSIADate.Add(DateTime.Now.TimeOfDay);
                    context.DSIA.Add(dsia);
                    UserLog.SaveUserLog(ref context, new UserLog(dsia.DSIANo.ToString(), dsia.DSIADate, "DSIA", ScreenAction.Update));
                    InventoryTransactionBridge.InsertFromSampleEntry(ref context, dsia, addedItems);
                    AccountModuleBridge.InsertUpdateFromDSIA(ref context, dsia, addedItems);
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { dsia }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteDsiaByid(int id)
        {
            var dsiaExist = context.DSIA.FirstOrDefault(x => x.DSIANo == id);
            if (dsiaExist != null)
            {
                context.DSIA.Remove(dsiaExist);
                var dsiaLineItem = context.DSIALineItem.Where(x => x.DSIANo == id).ToList();
                context.DSIALineItem.Where(x => x.DSIANo == id).ToList().ForEach(x =>
                {
                    context.DSIALineItem.Remove(x);
                });
        
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), dsiaExist.DSIADate, "DSIA", ScreenAction.Delete));
                InventoryTransactionBridge.DeleteSampleEntry(ref context, dsiaExist);
                AccountModuleBridge.DeleteFromDSIA(ref context, dsiaExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public FileResult GetSamplePreview(int dsiaNo)
        {
            Session["ReportName"] = "SamplePreviewR";

            ReportParmPersister param = new ReportParmPersister();

            var sample = context.DSIA.FirstOrDefault(x => x.DSIANo == dsiaNo);
            if (sample != null)
            {
                //param.SampleNo = sample.DSIANo;
                if (sample.Type == "SIA")
                {
                    param.SampleType = "Sample";
                }
                else if(sample.Type == "DIA")
                {
                    param.SampleType = "Damage";
                }
                else if( sample.Type == "Offer")
                {
                    param.SampleType = "Offer";
                }
                else
                {
                    param.SampleType = "Dumping Adjustment";
                }
               // param.SampleType = sample.Category;
                param.SampleDate = sample.DSIADate;
            }
            else
            {
                param.SampleNo = 0;
                param.SampleType = "";
            }

            //var tso = context.Employees.FirstOrDefault(x => x.Id == sample.TSoId);
            //if (tso != null)
            //{
            //    param.TsoId = tso.Id;
            //    param.TsoName = tso.Name;
            //}
            //else
            //{
            //    param.TsoId = 0;
            //    param.TsoName = "";
            //}
            //param.BranchName = (from b in context.BranchInformation.AsEnumerable()
            //                    join s in context.Store.AsEnumerable() on b.Slno equals s.DepotID
            //                    where s.Id == sample.StoreID
            //                    select b.BrnachName).FirstOrDefault();
            //param.PostedBy = (from e in context.Employees.ToList()
            //                  join
            //                  u in context.UserLog.ToList() on e.LogInUserName equals u.LogInName
            //                  where u.ScreenName == "DSIA" && u.Action == "Save" && u.TrnasId == sample.DSIANo.ToString()
            //                  select e.Name).FirstOrDefault();

            Session["SamplePreview"] = param;

            string sql = string.Format("exec PreviewSample '" + dsiaNo + "'");
            var items = context.Database.SqlQuery<PreviewSampleReport>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewSampleReport report = new PreviewSampleReport();
                items.Add(report);
            }
            SamplePreviewR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public SamplePreviewR ShowReport(ReportParmPersister persister, List<PreviewSampleReport> data)
        {
            SamplePreviewR samplePreviewR = new SamplePreviewR();

            samplePreviewR.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\SamplePreviewR.rpt");
            samplePreviewR.Load(APPPATH);
            samplePreviewR.SetDataSource(data);

            //samplePreviewR.SetParameterValue("tsoId", persister.TsoId);
            //samplePreviewR.SetParameterValue("tsoName", persister.TsoName);
            //samplePreviewR.SetParameterValue("sampleNo", persister.SampleNo);
            samplePreviewR.SetParameterValue("sampleType", persister.SampleType);
            //samplePreviewR.SetParameterValue("sampleDate", persister.SampleDate);
            //samplePreviewR.SetParameterValue("depotName", persister.BranchName);
            //samplePreviewR.SetParameterValue("postedBy", persister.PostedBy);
            //samplePreviewR.SetParameterValue("compName", persister.CompName);
            //samplePreviewR.SetParameterValue("compAddress", persister.CompAddress);
            //samplePreviewR.SetParameterValue("compContact", persister.CompContact);
            //samplePreviewR.SetParameterValue("compFax", persister.Fax);
            //samplePreviewR.SetParameterValue("factAddress", persister.FactAddress);
            //samplePreviewR.SetParameterValue("factContact", persister.FactContact);
            return samplePreviewR;
        }

        public string GetItemNameById(int id)
        {
            var item = LoadComboBox.GetItemInfo().FirstOrDefault(x => x.Id == id);
            return item.Id + " - " + item.Name;
        }
        
        public SelectList LoadDamageOrSample()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();

            items.Add("", "--- Select One ---");
            items.Add("DIA", "Damage");
            items.Add("SIA", "Sample");
            items.Add("Offer", "Offer");
            items.Add("DSA", "Dumping Sample");

            return new SelectList(items, "Key", "Value");
        }

        //public SelectList LoadCategory()    
        //{
        //    BEEContext context = new BEEContext();
        //    Dictionary<string, string> items = new Dictionary<string, string>();

        //    items.Add("", "--- Select One ---");
        //    items.Add("Normal", "Normal");
        //    items.Add("Special", "Special");

        //    return new SelectList(items, "Key", "Value");
        //}
    }
}
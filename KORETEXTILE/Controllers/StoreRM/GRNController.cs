using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.StoreRM.GRN;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;

namespace BEEERP.Controllers.StoreRM
{
    [ShowNotification]
    [Permission]
    //[Authorize(Roles = "RMAdmin,RMOperator,RMViewer,RMApprover,SysAdmin,SysViewer,SysOperator,SysApprover")]
    [StoreRMModule]
    public class GRNController : Controller
    {
        BEEContext context = new BEEContext();
        //bool isSearch;

        // GET: GRN
        public ActionResult GRNEntry()    
        {
            //var FactoryRM = context.Store.FirstOrDefault(x => x.Name.ToLower() == "factory rm");
            ViewBag.Store = LoadComboBox.LoadAllStore();
            ViewBag.Supplier = LoadComboBox.LoadAllSupplier();
           // ViewBag.WOCode = LoadComboBox.LoadAllWorkOrder(null);
            ViewBag.Group = LoadComboBox.LoadAllSPGroup();
            ViewBag.Item = LoadComboBox.LoadAllItem();   
            ViewBag.UOM = LoadComboBox.LoadAllUOM();

            //if (FactoryRM != null)
            //{
            //    ViewBag.FactoryRM = FactoryRM.Id;
            //}
            //else
            //{
            //    ViewBag.FactoryRM = 0;
            //}
            
            return View();
        }

        //public ActionResult GetWOCodeBySupplierId(int supplierId,bool isSearch)
        //{
        //    if (isSearch==true)
        //    {
        //        return Json(LoadComboBox.LoadAllWorkOrderSearch(supplierId), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(LoadComboBox.LoadAllWorkOrder(supplierId), JsonRequestBehavior.AllowGet);
        //    }
            
            
        //}

        public ActionResult GetWorderByWONo(int id) 
        {
            var workOrder = context.WorkOrder.FirstOrDefault(x => x.WONo == id);
            //var goodRevN = context.GoodsReceiveNote.FirstOrDefault(x => x.WONo == id);
            //int grnNo = goodRevN.GRNNo;

            if (workOrder != null)
            {
                var workOrderLI = context.WorkOrderLineItem.Where(x => x.WONo == id).ToList().Select(x =>
                {
                    var item = context.WorkOrderLineItem.FirstOrDefault(y => y.WONo == x.WONo && y.ItemID == x.ItemID);
                    var singleItem = FindObjectById.GetChartOfInventoryById(item.ItemID);
                    x.ItemName = singleItem.Name;
                    x.GroupName = GetItemNameById(item.GroupID).Name;
                    x.GroupID = item.GroupID;
                    x.ItemQty = item.ItemQty;
                    x.UnitCost = item.UnitCost;
                    x.UnitId = singleItem.UoMID;
                    x.Unit = GetUOMNameById(x.UnitId);
                    x.RevQty = GetTotalRcvQty(id, x.ItemID);
                    x.AvaQty = x.ItemQty - x.RevQty;
                    //x.PackSize = GetItemNameById(item.ItemID).PacSize;
                    return x;
                }).ToList();
                return Json(new { workOrder, workOrderLI }, JsonRequestBehavior.AllowGet); 
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ChartOfInventory GetItemNameById(int id)
        {
            var item = context.ChartOfInventory.ToList().FirstOrDefault(x => x.Id == id);
            return item;
        }

        public string GetUOMNameById(int id)    
        {
            var item = context.UOM.ToList().FirstOrDefault(x => x.Id == id);
            return item.Name;
        }

        public decimal GetTotalRcvQty(int woNo, int Item)
        {
            //var goodRcvNote = context.GoodsReceiveNoteLineItem.ToList().Where(x => x.ItemID == Item);
            var goodRcvNote = (from G in context.GoodsReceiveNote
                               join GL in context.GoodsReceiveNoteLineItem on G.GRNNo equals GL.GRNNo
                               where (G.WONo == woNo) && (GL.ItemID == Item)
                               select GL.Qty).ToList();
            if (goodRcvNote != null)
            {
                decimal sum = goodRcvNote.Sum();
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

        //public ActionResult GetItemByGroupId(int groupId)
        //{
        //    return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetItemAndGroupName(int item, int group)
        {
            string ItemName = GetItemNameById(item).Name;
            string GroupName = GetItemNameById(group).Name;
            //decimal CostRt = GetItemNameById(item).clmStandardCost;
            //string PackSize = GetItemNameById(item).PacSize;
            int UomId = GetItemNameById(item).UoMID;
            string Unit = GetUOMNameById(UomId);
            return Json(new { Item = ItemName, Group = GroupName,  Unit = Unit, /*PackSize = PackSize*/ }, JsonRequestBehavior.AllowGet);   
        }

        public ActionResult SaveGRN(GoodsReceiveNote grn, List<GoodsReceiveNoteLineItem> addedItems1)
        {
            var dataCheck = true;
            addedItems1.ForEach(x =>
            {
                if (x.ItemGrpID==0||x.ItemID==0||x.Qty==0||x.CostRt==0||x.CostVal==0||x.UOMID==0)
                {
                    dataCheck = false;
                }
            });
            if (dataCheck==false)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            using (context)
            {
                string grnNo = "";  
                string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.GoodsReceiveNote.Count();
                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.GoodsReceiveNote.ToList().LastOrDefault(x => x.GRNNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        grnNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        grnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GRNNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                   // grnNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.GRNNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                }
                else
                {
                    grnNo = yearLastTwoPart + "00001";
                }

                grn.GRNNo = Convert.ToInt32(grnNo);
                grn.GRNDate = grn.GRNDate.Add(DateTime.Now.TimeOfDay);
                //var checkChallan = context.GoodsReceiveNote.FirstOrDefault(x => x.ChallanNo == grn.ChallanNo);

                //if (checkChallan == null)
                //{
                    addedItems1.ForEach(x =>
                    {
                       // decimal cost = grn.TotalCost;
                        x.GRNNo = grn.GRNNo;
                       // grn.TotalCost = cost + (x.Qty * x.CostRt);
                        context.GoodsReceiveNoteLineItem.Add(x);
                    });
                    grn.Type = "WO";
                    context.GoodsReceiveNote.Add(grn);
                    //AccountModuleBridge.InsertUpdateFromGRN(ref context, grn, addedItems1);
                    //InventoryTransactionBridge.InsertFromGRNEntry(ref context, grn, addedItems1);
                    UserLog.SaveUserLog(ref context, new UserLog(grn.GRNNo.ToString(), DateTime.Now, "GRN", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { grnNo = grn.GRNNo }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(0, JsonRequestBehavior.AllowGet);
                //}
                
            }
        }


        public ActionResult UpdateGRN(GoodsReceiveNote grn, List<GoodsReceiveNoteLineItem> addedItems1)
        {
            using (context)
            {


                int id = grn.GRNNo;
                var grnItem = context.GoodsReceiveNote.FirstOrDefault(x => x.GRNNo == id);
                if (grnItem != null)
                {
                    context.GoodsReceiveNote.Remove(grnItem);
                    context.GoodsReceiveNoteLineItem.Where(x => x.GRNNo == id).ToList().ForEach(x =>
                    {
                        context.GoodsReceiveNoteLineItem.Remove(x);
                    });

                    grn.GRNNo = Convert.ToInt32(id);
                    grn.GRNDate = grn.GRNDate.Add(DateTime.Now.TimeOfDay);

                    addedItems1.ForEach(x =>
                    {
                       // decimal cost = grn.TotalCost;
                        x.GRNNo = grn.GRNNo;
                       // grn.TotalCost = cost + (x.Qty * x.CostRt);
                        context.GoodsReceiveNoteLineItem.Add(x);
                    });
                    grn.Type = "WO";
                    context.GoodsReceiveNote.Add(grn);

                    UserLog.SaveUserLog(ref context, new UserLog(grn.GRNNo.ToString(), DateTime.Now, "GRN", ScreenAction.Update));
                    //AccountModuleBridge.InsertUpdateFromGRN(ref context, grn, addedItems1);
                    //InventoryTransactionBridge.InsertFromGRNEntry(ref context, grn, addedItems1);
                    context.SaveChanges();
                    return Json(new { grnNo=grn.GRNNo}, JsonRequestBehavior.AllowGet);
                    

                    //UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "GRN", ScreenAction.Delete));

                    //InventoryTransactionBridge.DeleteFromGRNEntry(ref context, id);
                    //context.SaveChanges();
                    //return Json(id, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }


                

            }
        }

        public ActionResult GetGRNById(int id)  
        {
            
            var grnItem = context.GoodsReceiveNote.FirstOrDefault(x => x.GRNNo == id&&x.Type=="WO");
            
            //int shiftFrom = FindObjectById.GetSifdById(Convert.ToInt32(grfItem.ChallanNo)).CurrentStoreID;
            // DateTime chalanDate = FindObjectById.GetSifdById(Convert.ToInt32(grfItem.ChallanNo)).Date;
            if (grnItem != null)
            {
                int woNo = FindObjectById.GetGRNById(Convert.ToInt32(grnItem.GRNNo)).WONo;
                //int woCode = FindObjectById.GetWOById(woNo).WOCode;
                var grnLineItem = context.GoodsReceiveNoteLineItem.Where(x => x.GRNNo == id).ToList().Select(x =>
                {
                    var item = context.GoodsReceiveNoteLineItem.FirstOrDefault(y => y.GRNNo == x.GRNNo && y.ItemID == x.ItemID);
                    x.ItemName = GetItemNameById(item.ItemID).Name;
                    x.GroupName = GetItemNameById(item.ItemGrpID).Name;
                    x.ItemID = item.ItemID;
                    x.ItemGrpID = item.ItemGrpID;
                    x.Qty = item.Qty;
                    x.CostVal = item.CostVal;
                    x.CostRt = item.CostRt;
                    x.Unit = GetUOMNameById(item.UOMID);
                    //x.PackSize = GetItemNameById(item.ItemID).PacSize;
                    return x;
                }).ToList();
                return Json(new { grnItem, grnLineItem, WoNo = woNo/*, WoCode = woCode*/ }, JsonRequestBehavior.AllowGet);    
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteGRNById(int id)  
        {
            
            var grnItem = context.GoodsReceiveNote.FirstOrDefault(x => x.GRNNo == id);
            if (grnItem != null)
            {
                context.GoodsReceiveNote.Remove(grnItem);
                context.GoodsReceiveNoteLineItem.Where(x => x.GRNNo == id).ToList().ForEach(x =>
                {
                    context.GoodsReceiveNoteLineItem.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "GRN", ScreenAction.Delete));

                //AccountModuleBridge.DeleteFromGRN(ref context, id);
                //InventoryTransactionBridge.DeleteFromGRNEntry(ref context,id);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PrintBill(int GRNNo)
        {
            if (GRNNo > 0)
            {
                ReportParmPersister param = new ReportParmPersister();
                var postedBy = (from p in context.UserLog
                                join c in context.Employees on p.LogInName equals c.LogInUserName
                                where p.ScreenName == "GRN" && p.TrnasId == GRNNo.ToString()
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

                string sql = string.Format("exec spPrintGRN "+ GRNNo+ "");
                var items = context.Database.SqlQuery<GRNPrint>(sql).ToList();
                if (items.Count == 0)
                {
                    GRNPrint report = new GRNPrint();
                    items.Add(report);
                }
                GRNprintR rd = ShowReport(param, items);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                return new FileStreamResult(stream, "application/pdf");
            }
            else
                return Json("Invalid Bill No", JsonRequestBehavior.AllowGet);
        }
        public GRNprintR ShowReport(ReportParmPersister persister, List<GRNPrint> data)
        {
            GRNprintR grnprint = new GRNprintR();
            grnprint.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\GRNprintR.rpt");
            grnprint.Load(APPPATH);
            grnprint.SetDataSource(data);
            grnprint.SetParameterValue("postedBy", persister.PostedBy);
            //GoodsPurchaseBillPreview.SetParameterValue("compName", persister.CompName);
            //GoodsPurchaseBillPreview.SetParameterValue("compAddress", persister.CompAddress);
            //if (persister.Fax != "" || persister.Fax != null)
            //    GoodsPurchaseBillPreview.SetParameterValue("compContact", "Phone : " + persister.CompContact + " Fax : " + persister.Fax);
            //else
            //    GoodsPurchaseBillPreview.SetParameterValue("compContact", "Phone : " + persister.CompContact);
            //if (persister.FactAddress != "" || persister.FactAddress != null)
            //    GoodsPurchaseBillPreview.SetParameterValue("factoryAddress", "Factory : " + persister.FactAddress + " Phone : " + persister.FactContact);
            //else
            //    GoodsPurchaseBillPreview.SetParameterValue("factoryAddress", " ");
            return grnprint;
        }
    }
}
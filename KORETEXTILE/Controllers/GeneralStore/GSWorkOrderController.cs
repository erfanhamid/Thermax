using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.Procurement;
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using BEEERP.Models.StoreRM;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.IO;
using System.Security.Principal;

namespace BEEERP.Controllers.GeneralStore
{
    [ShowNotification]
    public class GSWorkOrderController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: GSWorkOrder
        public ActionResult GSWorkOrderView()
        {
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.Group = LoadComboBox.LoadGSItemGroup();
            ViewBag.Item = LoadComboBox.LoadRmItem(null);
            ViewBag.UoM = LoadComboBox.LoadAllUOM();
            //ViewBag.Department = LoadComboBox.LoadWorkOrderDepartment();
            return View();
        }

        public ActionResult GetSupplierByGroupId(int sGroupId)
        {
            return Json(LoadComboBox.LoadSupplier(sGroupId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSupplier(int sId)
        {
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == sId);
            return Json(new { Supplier = supplier }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemByGroupId(int groupId)
        {
            return Json(LoadComboBox.LoadItem(groupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadFGGroup()
        {
            return Json(LoadComboBox.LoadItemGroup(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadFAGroup()
        {
            return Json(LoadComboBox.LoadItemGroupfa(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadRMGroup()
        {
            return Json(LoadComboBox.LoadItemGroupRM(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUoM(int itemId)
        {
            var item = context.ChartOfInventory.FirstOrDefault(x => x.Id == itemId);

            return Json(new { Item = item }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveWorkOrder(WorkOrder workOrder, List<WorkOrderLineItem> addedItems)
        {

            using (context)
            {
                string woNo = "";
                string yearLastTwoPart = workOrder.WODate.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.WorkOrder.Count();

                if (noOfInvoice > 0)
                {
                    //var lastInvoice = context.WorkOrder.Select(x=>x.WONo).Max().ToString().Substring(0,2);
                    //var lastInvoice = context.WorkOrder.ToList().LastOrDefault(x => x.WONo.ToString().Substring(0, 1).ToString() == yearLastTwoPart);
                    var lastInvoice = context.WorkOrder.ToList().LastOrDefault(x => x.WONo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        woNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        woNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.WONo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    woNo = yearLastTwoPart + "00001";
                }


                string yearPart = workOrder.WODate.Year.ToString().Substring(2, 2);
                //string supplieGroup = workOrder.SupplierGroup.ToString().PadLeft(2, '0');
                //var maxSl1 = context.WorkOrder.Where(x => x.WOCode.ToString().Substring(0, 2) == yearPart && x.WOCode.ToString().Substring(2, 2) == supplieGroup).Count();
                //var maxSl = 0;
                //if (maxSl1 > 0)
                //{
                //    maxSl = context.WorkOrder.Where(x => x.WOCode.ToString().Substring(0, 2) == yearPart && x.WOCode.ToString().Substring(2, 2) == supplieGroup).ToList().Max(x => x.WOCode);
                //    maxSl += 1;
                //}
                //else
                //{
                //    maxSl = Convert.ToInt32(yearPart + supplieGroup + "00001");
                //}
                //workOrder.WOCode = maxSl;

                workOrder.WONo = Convert.ToInt32(woNo);
                //workOrder.WODate = workOrder.WODate.Add(DateTime.Now.TimeOfDay);
                workOrder.DeliveryDate = workOrder.DeliveryDate;

                addedItems.ForEach(x =>
                {
                    x.WONo = workOrder.WONo;
                    context.WorkOrderLineItem.Add(x);
                });

                context.WorkOrder.Add(workOrder);

                UserLog.SaveUserLog(ref context, new UserLog(workOrder.WONo.ToString(), DateTime.Now, "WO", ScreenAction.Save));

                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                context.SaveChanges();
                return Json(new { Wo = workOrder.WONo/*, WoCode = workOrder.WOCode*/ }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetWorkOrderById(int id)
        {
            var workOrder = context.WorkOrder.FirstOrDefault(x => x.WONo == id);

            if (workOrder != null)
            {
                var supplierGroup = context.Supplier.FirstOrDefault(x => x.Id == workOrder.SupplierID);
               // workOrder.SupplierGroup = supplierGroup.GroupID;

                var workOrderLineItem = context.WorkOrderLineItem.Where(x => x.WONo == id).ToList().Select(x =>
                {
                    var item = context.WorkOrderLineItem.FirstOrDefault(y => y.WONo == x.WONo && y.ItemID == x.ItemID);
                    var itemE = GetItemNameById(item.ItemID);
                    x.ItemName = itemE.Name;
                    x.GroupName = GetItemNameById(item.GroupID).Name;
                    //x.PackSize = itemE.PacSize;
                    x.Unit = context.UOM.FirstOrDefault(z => z.Id == itemE.UoMID).Name;
                    x.ItemID = item.ItemID;
                    x.GroupID = item.GroupID;
                    x.ItemQty = item.ItemQty;
                    x.UnitCost = item.UnitCost;
                    x.TotalCost = item.TotalCost;
                    x.VatPerc = item.VatPerc;
                    x.Specification = item.Specification;

                    return x;
                }).ToList();
                return Json(new { workOrder, workOrderLineItem }, JsonRequestBehavior.AllowGet);
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

        public ActionResult UpdateWorkOrder(WorkOrder workOrder, List<WorkOrderLineItem> addedItems)
        {
            using (context)
            {
                //DateTime delivaryDate = workOrder.DeliveryDate;
                var IsWorkorderRev = context.GoodsReceiveNote.FirstOrDefault(x => x.WONo == workOrder.WONo);

                if (IsWorkorderRev != null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var woExist = context.WorkOrder.FirstOrDefault(x => x.WONo == workOrder.WONo);
                    if (woExist != null)
                    {
                        context.WorkOrder.Remove(woExist);

                        context.WorkOrderLineItem.Where(x => x.WONo == workOrder.WONo).ToList().ForEach(x =>
                        {
                            context.WorkOrderLineItem.Remove(x);
                        });

                        addedItems.ForEach(x =>
                        {
                            x.WONo = workOrder.WONo;
                            context.WorkOrderLineItem.Add(x);
                        });

                        workOrder.WODate = workOrder.WODate.Add(DateTime.Now.TimeOfDay);
                        workOrder.DeliveryDate = workOrder.DeliveryDate;
                        context.WorkOrder.Add(workOrder);
                        UserLog.SaveUserLog(ref context, new UserLog(workOrder.WONo.ToString(), DateTime.Now, "WorkOrder", ScreenAction.Update));
                        //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                        context.SaveChanges();
                        return Json(new { WONo = workOrder.WONo }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                }

            }
        }

        public ActionResult DeleteWorkOrder(int id)
        {
            var IsWorkorderRev = context.GoodsReceiveNote.FirstOrDefault(x => x.WONo == id);

            if (IsWorkorderRev != null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var woExist = context.WorkOrder.FirstOrDefault(x => x.WONo == id);
                if (woExist != null)
                {
                    context.WorkOrder.Remove(woExist);
                    context.WorkOrderLineItem.Where(x => x.WONo == id).ToList().ForEach(x =>
                    {
                        context.WorkOrderLineItem.Remove(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "WorkOrder", ScreenAction.Delete));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                    context.SaveChanges();
                    return Json(id, JsonRequestBehavior.AllowGet);
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult PrintWo(int WONo)
        {
            if (WONo > 0)
            {
                ReportParmPersister param = new ReportParmPersister();
                var postedBy = (from p in context.UserLog
                                join c in context.Employees on p.LogInName equals c.LogInUserName
                                where p.ScreenName == "WorkOrder" && p.TrnasId == WONo.ToString()
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

                string sql = string.Format("exec spWorkOrder " + WONo + "");
                var items = context.Database.SqlQuery<WorkOrderPreview>(sql).ToList();
                if (items.Count == 0)
                {
                    WorkOrderPreview report = new WorkOrderPreview();
                    items.Add(report);
                }
                WorkOrderPreviewR rd = ShowReport(param, items);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                return new FileStreamResult(stream, "application/pdf");
            }
            else
                return Json("Invalid Bill No", JsonRequestBehavior.AllowGet);
        }
        public WorkOrderPreviewR ShowReport(ReportParmPersister persister, List<WorkOrderPreview> data)
        {
            WorkOrderPreviewR WorkOrderpreview = new WorkOrderPreviewR();
            WorkOrderpreview.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\WorkOrderPreviewR.rpt");
            WorkOrderpreview.Load(APPPATH);
            WorkOrderpreview.SetDataSource(data);
            WorkOrderpreview.SetParameterValue("postedBy", persister.PostedBy);
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
            return WorkOrderpreview;
        }
    }
}
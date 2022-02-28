using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.Procurement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Procurement
{
    [ShowNotification]
    public class FreezerWorkOrderController : Controller
    {
        
        BEEContext context = new BEEContext();
        // GET: FrezzersWorkOrder
        public ActionResult FreezerWorkOrderEntry()
        {
            BEEContext context = new BEEContext();
            ViewBag.SGroup = LoadComboBox.LoadAllSupplierGroup();
            ViewBag.Supplier = LoadComboBox.LoadSupplier(null);
            ViewBag.Group = LoadComboBox.LoadItemGroupRM();
            ViewBag.Item = LoadComboBox.LoadRmItem(null);
            ViewBag.UoM = LoadComboBox.LoadAllUOM();
            //ViewBag.Department = LoadComboBox.LoadWorkOrderDepartment();
            ViewBag.Description = LoadComboBox.LoadFreezerModelDescription(null);
            
            return View();
        }
        public ActionResult GetSupplierByGroupId(int sGroupId)
        {
            return Json(LoadComboBox.LoadSupplier(sGroupId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUoM(int ItemID)
        {
            var item = context.FreezerModelDescription.FirstOrDefault(x => x.ID == ItemID);

            return Json(new { Item = item}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplier(int sId)
        {
            var supplier = context.Supplier.FirstOrDefault(x => x.Id == sId);
            return Json(new { Supplier = supplier }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveWorkOrder(FreezerWorkOrder workOrder, List<FreezerWorkOrderLineItem> addedItems)
        {

            using (context)
            {
                string woNo = "";
                string yearLastTwoPart = workOrder.WODate.Year.ToString().Substring(2, 2).ToString();
                var noOfInvoice = context.FreezerWorkOrder.Count();


                if (noOfInvoice > 0)
                {
                    var lastInvoice = context.FreezerWorkOrder.ToList().LastOrDefault(x => x.WONo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
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


                //if (noOfInvoice > 0)
                //{
                //    var lastInvoice = new FreezerWorkOrder();
                //    try
                //    {
                //        lastInvoice = context.FreezerWorkOrder.ToList().LastOrDefault(x => x.WONo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                //    }
                //    catch
                //    {
                //        lastInvoice = context.FreezerWorkOrder.OrderByDescending(x => x.WONo).FirstOrDefault();
                //    }


                //    woNo = yearLastTwoPart + (Convert.ToInt32(lastInvoice.WONo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                //}
                //else
                //{
                //    woNo = yearLastTwoPart + "00001";
                //}


                string yearPart = workOrder.WODate.Year.ToString().Substring(2, 2);
                string supplieGroup = workOrder.SupplierGroup.ToString().PadLeft(2, '0');
                var maxSl1 = context.FreezerWorkOrder.Where(x => x.WOCode.ToString().Substring(0, 2) == yearPart && x.WOCode.ToString().Substring(2, 2) == supplieGroup).Count();
                var maxSl = 0;
                if (maxSl1 > 0)
                {
                    maxSl = context.FreezerWorkOrder.Where(x => x.WOCode.ToString().Substring(0, 2) == yearPart && x.WOCode.ToString().Substring(2, 2) == supplieGroup).ToList().Max(x => x.WOCode);
                    maxSl += 1;
                }
                else
                {
                    maxSl = Convert.ToInt32(yearPart + supplieGroup + "00001");
                }
                workOrder.WOCode = maxSl;

                workOrder.WONo = Convert.ToInt32(woNo);
                //workOrder.WODate = workOrder.WODate.Add(DateTime.Now.TimeOfDay);
                workOrder.DeliveryDate = workOrder.DeliveryDate;

                addedItems.ForEach(x =>
                {
                    x.WONo = workOrder.WONo;
                    context.FreezerWorkOrderLineItem.Add(x);
                });

                context.FreezerWorkOrder.Add(workOrder);

                UserLog.SaveUserLog(ref context, new UserLog(workOrder.WONo.ToString(), DateTime.Now, "FWO", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { Wo = workOrder.WONo, WoCode = workOrder.WOCode }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetWorkOrderById(int id)
        {
            var workOrder = context.FreezerWorkOrder.FirstOrDefault(x => x.WONo == id);

            if (workOrder != null)
            {
                var supplierGroup = context.Supplier.FirstOrDefault(x => x.Id == workOrder.SupplierID);
                workOrder.SupplierGroup = supplierGroup.GroupID;

                var workOrderLineItem = context.FreezerWorkOrderLineItem.Where(x => x.WONo == id).ToList().Select(x =>
                {
                    var item = context.FreezerWorkOrderLineItem.FirstOrDefault(y => y.WONo == x.WONo && y.ItemID == x.ItemID);
                    var itemE = GetItemNameById(item.ItemID);
                    var itemN= LoadComboBox.LoadFreezerModelDescription(item.ItemID); 
                    //x.ItemName = itemE.Description;
                    x.ItemName = GetItemName(item.ItemID);
                    x.Unit = context.UOM.FirstOrDefault(z => z.Id == itemE.UnitID).Name;
                    x.ItemID = item.ItemID;
                    x.ItemQty = item.ItemQty;
                    x.UnitCost = item.UnitCost;
                    x.TotalCost = item.TotalCost;
                    x.VatPerc = item.VatPerc;
                    x.Description = item.Description;

                    return x;
                }).ToList();
                return Json(new { workOrder, workOrderLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public static string GetItemName(int id)
        {
            BEEContext context = new BEEContext();
            var itemID = context.FreezerModelDescription.ToList().FirstOrDefault(x => x.ID == id);
            var name1 = context.FWOTypes.ToList().FirstOrDefault(x => x.ID == itemID.TypeID).Name;
            var name2 = context.FWOBrands.ToList().FirstOrDefault(x => x.ID == itemID.BrandID).Name;
            var name3 =  context.FWOModels.ToList().FirstOrDefault(x => x.ID == itemID.ModelID).Name;
            var name4 = context.FWOCapacities.ToList().FirstOrDefault(x => x.ID == itemID.CapacityID).Name;
            var item = Convert.ToString(name1+" "+ name2+" " + name3 + " " + name4);
            return item;
        }

        public FreezerModelDescription GetItemNameById(int id)
        {
            
            var item = context.FreezerModelDescription.ToList().FirstOrDefault(x => x.ID == id);
            return item;
        }

        public ActionResult UpdateWorkOrder(FreezerWorkOrder workOrder, List<FreezerWorkOrderLineItem> addedItems)
        {
            using (context)
            {
                //DateTime delivaryDate = workOrder.DeliveryDate;
                var IsWorkorderRev = context.GoodsReceiveNote.FirstOrDefault(x => x.WONo == workOrder.WONo&&x.Type== "FWO");

                if (IsWorkorderRev != null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var woExist = context.FreezerWorkOrder.FirstOrDefault(x => x.WONo == workOrder.WONo);
                    if (woExist != null)
                    {
                        context.FreezerWorkOrder.Remove(woExist);

                        context.FreezerWorkOrderLineItem.Where(x => x.WONo == workOrder.WONo).ToList().ForEach(x =>
                        {
                            context.FreezerWorkOrderLineItem.Remove(x);
                        });

                        addedItems.ForEach(x =>
                        {
                            x.WONo = workOrder.WONo;
                            context.FreezerWorkOrderLineItem.Add(x);
                        });

                        workOrder.WODate = workOrder.WODate.Add(DateTime.Now.TimeOfDay);
                        workOrder.DeliveryDate = workOrder.DeliveryDate;
                        context.FreezerWorkOrder.Add(workOrder);
                        UserLog.SaveUserLog(ref context, new UserLog(workOrder.WONo.ToString(), DateTime.Now, "FreezerWorkOrder", ScreenAction.Update));
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
            var IsWorkorderRev = context.GoodsReceiveNote.FirstOrDefault(x => x.WONo == id&& x.Type=="FWO");

            if (IsWorkorderRev != null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var woExist = context.FreezerWorkOrder.FirstOrDefault(x => x.WONo == id);
                if (woExist != null)
                {
                    context.FreezerWorkOrder.Remove(woExist);
                    context.FreezerWorkOrderLineItem.Where(x => x.WONo == id).ToList().ForEach(x =>
                    {
                        context.FreezerWorkOrderLineItem.Remove(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "FreezerWorkOrder", ScreenAction.Delete));
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
                                where p.ScreenName == "FreezerWorkOrder" && p.TrnasId == WONo.ToString()
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

                string sql = string.Format("exec spFWOpreview " + WONo + "");
                var items = context.Database.SqlQuery<FreezerWorkOrderPreview>(sql).ToList();
                if (items.Count == 0)
                {
                    FreezerWorkOrderPreview report = new FreezerWorkOrderPreview();
                    items.Add(report);
                }
                FreezerWorkOrderPreviewR rd = ShowReport(param, items);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                return new FileStreamResult(stream, "application/pdf");
            }
            else
                return Json("Invalid Bill No", JsonRequestBehavior.AllowGet);
        }
        public FreezerWorkOrderPreviewR ShowReport(ReportParmPersister persister, List<FreezerWorkOrderPreview> data)
        {
            FreezerWorkOrderPreviewR FreezerWorkOrderpreview = new FreezerWorkOrderPreviewR();
            FreezerWorkOrderpreview.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\FreezerWorkOrderPreviewR.rpt");
            FreezerWorkOrderpreview.Load(APPPATH);
            FreezerWorkOrderpreview.SetDataSource(data);
            FreezerWorkOrderpreview.SetParameterValue("postedBy", persister.PostedBy);
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
            return FreezerWorkOrderpreview;
        }


    }
}
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
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
    public class SIIDController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: SIID
        public ActionResult SIIDEntry()
        {
            var DepotID = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = DepotID;
            //ViewBag.Depot = LoadComboBox.LoadDepotByID(DepotID);
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadComboBox.LoadFGStoreByDepot(DepotID);
            ViewBag.NewStore = LoadComboBox.LoadAllFGStore();
            ViewBag.GroupId = LoadComboBox.LoadItemGroup();
            ViewBag.Item = LoadComboBox.LoadItem(null);
            ViewBag.Vehicle = LoadComboBox.LoadVehicle();
            //ViewBag.Driver = LoadComboBox.LoadDriver();
            ViewBag.Group = LoadItemGroup();
            ViewBag.Item = LoadItem(null);
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

        public ActionResult GetDataByDepot(int Depot)
        {
            if (Depot != 0 && Depot.ToString() != null)
            {
                return Json(new { store = LoadComboBox.LoadFGStoreByDepot(Depot), vehicle = LoadComboBox.LoadVehicleByDepot(Depot)/*, driver = LoadComboBox.LoadDriver(Depot) */}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult SaveSIID(SIFD SIID, List<SIFDLineItem> addedItems)
        {
            if (SIID != null && addedItems.Count > 0)
            {
                string sifd = "";
                string yearLastTwoPart = SIID.Date.Year.ToString().Substring(2, 2).ToString();
                var totalsitv = context.SIFD.Count();
                if (totalsitv > 0)
                {
                    var lastInvoice = context.SIFD.ToList().LastOrDefault(x => x.SIFDNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastInvoice == null)
                    {
                        sifd = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        sifd = yearLastTwoPart + (Convert.ToInt32(lastInvoice.SIFDNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    sifd = yearLastTwoPart + "00001";
                }
                SIID.SIFDNo = Convert.ToInt32(sifd);
                var challan = context.SIFD.Max(x => x.ChallanNo);
                if(challan.ToString() != null && challan != 0)
                {
                    SIID.ChallanNo = challan + 1;
                }
                else
                {
                    SIID.ChallanNo = 1001;
                }
                var grq = context.SIFD.Max(x => x.GRQNo);
                if (grq.ToString() != null && grq != 0)
                {
                    SIID.GRQNo = grq + 1;
                }
                else
                {
                    SIID.GRQNo = 1;
                }
                int qty = 0;
                decimal cost = 0;

                addedItems.ForEach(x =>
                {
                    qty += x.ShiftQty;
                    cost += x.CostVal;
                    x.SIFDNo = SIID.SIFDNo;
                    context.SIFDLineItem.Add(x);
                });

                SIID.TtlQTY = qty;
                SIID.TotalCost = cost;
                context.SIFD.Add(SIID);
                UserLog.SaveUserLog(ref context, new UserLog(SIID.SIFDNo.ToString(), SIID.Date, "SIFD", ScreenAction.Save));
                InventoryTransactionBridge.InsertFromSIID(ref context, SIID, addedItems);
                context.SaveChanges();
                return Json(SIID.SIFDNo, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSIIDByID(int id)
        {
            if (id != 0 && id.ToString() != "")
            {
                var siidExist = context.SIFD.FirstOrDefault(x => x.SIFDNo == id);
                if (siidExist != null)
                {
                    var line = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList();
                    line.ForEach(x => {
                        x.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(i => i.Id == x.ItemID).Name;
                        x.GroupName = LoadComboBox.GetItemInfo().FirstOrDefault(i => i.Id == x.ItemGrpID).Name;
                    });
                    return Json(new { siid = siidExist, line = line }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSIID(SIFD SIID, List<SIFDLineItem> addedItems)
        {
            if (SIID != null && addedItems.Count > 0)
            {

                var isExist = context.SIFD.FirstOrDefault(x => x.SIFDNo == SIID.SIFDNo);
                if (isExist != null)
                {
                    var lineExist = context.SIFDLineItem.Where(x => x.SIFDNo == SIID.SIFDNo).ToList();
                    if(lineExist.Count> 0)
                    {
                        foreach(var x in lineExist)
                        {
                            context.SIFDLineItem.Remove(x);
                        }
                    }
                    context.SIFD.Remove(isExist);
                }
                int qty = 0;
                decimal cost = 0;

                addedItems.ForEach(x =>
                {
                    qty += x.ShiftQty;
                    cost += x.CostVal;
                    x.SIFDNo = SIID.SIFDNo;
                    context.SIFDLineItem.Add(x);
                });

                SIID.TtlQTY = qty;
                SIID.TotalCost = cost;
                context.SIFD.Add(SIID);
                UserLog.SaveUserLog(ref context, new UserLog(SIID.SIFDNo.ToString(), SIID.Date, "SIFD", ScreenAction.Update));
                InventoryTransactionBridge.InsertFromSIID(ref context, SIID, addedItems);
                context.SaveChanges();
                return Json(SIID.SIFDNo, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSIIDByid(int id)
        {

            if (id != 0 && id.ToString() != "")
            {
                var isExist = context.SIFD.FirstOrDefault(x => x.SIFDNo == id);
                if (isExist != null)
                {
                    context.SIFD.Remove(isExist);
                    var isLineExist = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList();
                    if (isLineExist.Count > 0)
                    {
                        foreach (var x in isLineExist)
                        {
                            context.SIFDLineItem.Remove(x);
                        }
                    }

                    UserLog.SaveUserLog(ref context, new UserLog(isExist.SIFDNo.ToString(), isExist.Date, "SIFD", ScreenAction.Delete));
                    InventoryTransactionBridge.DeleteFromSIID(ref context, isExist);
                    context.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public FileResult GetSIIDPreview(int SIFDNo)
        {


            ReportParmPersister param = new ReportParmPersister();
            var posted = (from e in context.Employees.AsEnumerable()
                          join u in context.UserLog.AsEnumerable() on e.LogInUserName equals u.LogInName
                          where u.ScreenName == "SIFD" && u.Action == "Save" && u.TrnasId == SIFDNo.ToString()
                          select e.Name).FirstOrDefault();
            if (posted != null)
            {
                param.PostedBy = posted;
            }
            else
            {
                param.PostedBy = "";
            }


            string sql = string.Format("exec PreviewSIFD " + SIFDNo + "");
            var items = context.Database.SqlQuery<PreviewSIFD>(sql).ToList();
            if (items.Count == 0)
            {
                PreviewSIFD report = new PreviewSIFD();
                items.Add(report);
            }

            PreveiwSIFDR rd = ShowReport(param, items);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            return new FileStreamResult(stream, "application/pdf");

        }

        public PreveiwSIFDR ShowReport(ReportParmPersister persister, List<PreviewSIFD> data)
        {
            PreveiwSIFDR sifd = new PreveiwSIFDR();

            sifd.SetDatabaseLogon(ServerInformation.UserId, ServerInformation.Password, ServerInformation.ServerName, ServerInformation.DataBaseName, false);
            String APPPATH = Server.MapPath(@"\CrystalReport\PreviewSIFDR.rpt");
            sifd.Load(APPPATH);
            sifd.SetDataSource(data);

            sifd.SetParameterValue("postedBy", persister.PostedBy);


            return sifd;
        }


    }
}
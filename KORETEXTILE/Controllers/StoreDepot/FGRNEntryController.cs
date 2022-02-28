using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.StoreDepot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreDepot
{
    [ShowNotification]
    public class FGRNEntryController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FGRNEntry
        public ActionResult FGRNEntry()
        {
            var DepotID = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = DepotID;
            //ViewBag.Depot = LoadComboBox.LoadDepotByID(DepotID);
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.Store = LoadComboBox.LoadFGStoreByDepot(DepotID);
            ViewBag.CurrentStore = LoadComboBox.LoadFGStore();
            ViewBag.Vehicle = LoadComboBox.LoadVehicle();
            return View();
        }
        public ActionResult GetDataByDepot(int Depot)
        {
            if (Depot != 0 && Depot.ToString() != null)
            {
                return Json(LoadComboBox.LoadFGStoreByDepot(Depot), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetDataByChallanNo(int challanNo, int store)
        {
            var data = (from s in context.SIFD
                       join st in context.Store on s.CurrentStoreID equals st.Id
                       join st1 in context.Store on s.VehicleID equals st1.Id
                       join st2 in context.Store on s.NewStoreID equals st2.Id
                       join b in context.BranchInformation on s.Depot equals b.Slno
                       where ( s.ChallanNo == challanNo && s.NewStoreID == store)
                       select (new { SIFDNo = s.SIFDNo,VehicleID= st1.Id, VehicleNo = st1.Name, CurrentStore = s.CurrentStoreID, Description = s.Description, ChallanDate = s.Date, GRQNo = s.GRQNo })).FirstOrDefault();
            if(data != null)
            {
                var dataLine = context.SIFDLineItem.Where(x => x.SIFDNo == data.SIFDNo).ToList();
           

                if (data != null && dataLine.Count > 0)
                {
                    dataLine.ForEach(x =>
                    {
                        x.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID).Name;
                        x.GroupName = LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemGrpID).Name;
                        x.CartonCapacity = (int)LoadComboBox.GetItemInfo().FirstOrDefault(y => y.Id == x.ItemID).clmCartonCapacity;

                    });
                    var TotalIssued = dataLine.Sum(x => x.ShiftQty);
                    return Json(new { data = data, line = dataLine, TotalIssued = TotalIssued }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetItemAvailabiltiy(int challanNo, int itemid)
        {
            var data = (from s in context.SIFD
                       join sl in context.SIFDLineItem on s.SIFDNo equals sl.SIFDNo
                       join g in context.GRF on s.ChallanNo equals g.ChallanNo
                       join gl in context.GRFLineItem on new { f1 = g.GRFNo, f2 = sl.ItemID } equals new { f1 = gl.GRFNo, f2 = gl.ItemID }
                       where (s.ChallanNo == challanNo && sl.ItemID == itemid)
                       select (new { SQty = sl.ShiftQty, GQty = gl.Qty })).FirstOrDefault();
           
            if(data != null)
            {
                var available = data.SQty - data.GQty;
                return Json(new { available=available, received=data.GQty }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
        }

        public JsonResult SaveFGRN(GRF FGRN, List<GRFLineItem> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Finish Goods Receive Note").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);
            if (FGRN.ReceiveDate > IsExpired.SELDate)
            {


                if (FGRN != null && addedItems.Count > 0)
                {
                    string grf = "";
                    string yearLastTwoPart = FGRN.ReceiveDate.Year.ToString().Substring(2, 2).ToString();
                    var totalgrf = context.GRF.Count();
                    if (totalgrf > 0)
                    {
                        var lastNo = context.GRF.ToList().LastOrDefault(x => x.GRFNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                        if (lastNo == null)
                        {
                            grf = yearLastTwoPart + "00001";
                        }
                        else
                        {
                            grf = yearLastTwoPart + (Convert.ToInt32(lastNo.GRFNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                        }
                    }
                    else
                    {
                        grf = yearLastTwoPart + "00001";
                    }
                    FGRN.GRFNo = Convert.ToInt32(grf);
                    var totalqty = 0;
                    decimal totalcost = 0;
                    addedItems.ForEach(x =>
                    {
                        x.GRFNo = FGRN.GRFNo;
                        totalqty += x.Qty;
                        totalcost += x.Cost;
                        context.GRFLineItem.Add(x);
                    });
                    FGRN.TotalQty = totalqty;
                    FGRN.TotalCost = totalcost;
                    context.GRF.Add(FGRN);
                    UserLog.SaveUserLog(ref context, new UserLog(FGRN.GRFNo.ToString(), FGRN.ReceiveDate, "GRF", ScreenAction.Save));
                    InventoryTransactionBridge.InsertUpdateFromFGRN(ref context, FGRN, addedItems);
                    context.SaveChanges();
                    return Json(FGRN.GRFNo, JsonRequestBehavior.AllowGet);

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
        public ActionResult GetFGRNByID(int id)
        {
            if (id != 0 && id.ToString() != "")
            {
                var fgrnExist = context.GRF.FirstOrDefault(x => x.GRFNo == id);
                if (fgrnExist != null)
                {
                    var line = context.GRFLineItem.Where(x => x.GRFNo == id).ToList();
                    line.ForEach(x => {
                        x.ItemName = LoadComboBox.GetItemInfo().FirstOrDefault(i => i.Id == x.ItemID).Name;
                        if(x.GroupID != 0)
                        {
                            x.GroupName = LoadComboBox.GetItemInfo().FirstOrDefault(i => i.Id == x.GroupID).Name;
                        }
                       
                    });
                    return Json(new { data = fgrnExist, line = line }, JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateFGRN(GRF FGRN, List<GRFLineItem> addedItems)
        {
            if (FGRN != null && addedItems.Count > 0)
            {
                var IsExist = context.GRF.FirstOrDefault(x => x.GRFNo == FGRN.GRFNo);
                if (IsExist != null)
                {
                    context.GRF.Remove(IsExist);
                    var IsExistLine = context.GRFLineItem.Where(x => x.GRFNo == FGRN.GRFNo).ToList();
                    if (IsExistLine.Count > 0)
                    {
                        IsExistLine.ForEach(x =>
                        {
                            context.GRFLineItem.Remove(x);
                        });
                    }

                    var totalqty = 0;
                    decimal totalcost = 0;
                    addedItems.ForEach(x =>
                    {
                        x.GRFNo = FGRN.GRFNo;
                        totalqty += x.Qty;
                        totalcost += x.Cost;
                        context.GRFLineItem.Add(x);
                    });
                    FGRN.TotalQty = totalqty;
                    FGRN.TotalCost = totalcost;
                    context.GRF.Add(FGRN);
                    UserLog.SaveUserLog(ref context, new UserLog(FGRN.GRFNo.ToString(), FGRN.ReceiveDate, "GRF", ScreenAction.Save));
                    InventoryTransactionBridge.InsertUpdateFromFGRN(ref context, FGRN, addedItems);
                    context.SaveChanges();
                    return Json(FGRN.GRFNo, JsonRequestBehavior.AllowGet);

                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteFGRNByid(int id)
        {

            if (id != 0 && id.ToString() != "")
            {
                var isExist = context.GRF.FirstOrDefault(x => x.GRFNo == id);
                if (isExist != null)
                {
                    context.GRF.Remove(isExist);
                    var isLineExist = context.GRFLineItem.Where(x => x.GRFNo == id).ToList();
                    if (isLineExist.Count > 0)
                    {
                        foreach (var x in isLineExist)
                        {
                            context.GRFLineItem.Remove(x);
                        }
                    }

                    UserLog.SaveUserLog(ref context, new UserLog(isExist.GRFNo.ToString(), isExist.ReceiveDate, "GRF", ScreenAction.Delete));
                    InventoryTransactionBridge.DeleteFromFGRN(ref context, isExist);
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
        

    }
}
using BEEERP.Models.AccountModule;
using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Account
{
    [ShowNotification]
    public class DAADController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: DAAD/DAADEntry
        public ActionResult DAADEntry()
        {
            ViewBag.Account = LoadComboBox.LoadAllAccount();
            ViewBag.Depot = LoadComboBox.LoadBranchInfo();
            ViewBag.CostGroup = LoadComboBox.LoadALLCostGroup();
            ViewBag.DealerID = LoadComboBox.LoadAllCustomerByDepot(null);

            return View();
        }
        public JsonResult GetDealerByDepot(int depot)
        {
            var dealer = LoadComboBox.LoadAllCustomerByDepot(depot);
            return Json(dealer, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDealerDetails(int dealer)
        {
          
            var customer = context.Customers.FirstOrDefault(x => x.Id == dealer);
            var due = context.CustomerBalanceCalculations.Where(x => x.CustomerID == dealer).Sum(y => y.Amount);
            var zone = context.TblZones.FirstOrDefault(x => x.ZoneId == customer.ZoneId).ZoneName;
            return Json(new { due = due, zone = zone}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveDAAD(DealerAccAdjustment DAA, List<DealerAccAdjustmentLineItem> addedItems)
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Dealer Account Adjustment").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);

            if (DAA.DAADate > IsExpired.SELDate)
            {
                if (DAA != null)
                {
                    if (addedItems.Count > 0)
                    {
                        string DAANO = "";
                        string yearLastTwoPart = DateTime.Now.Year.ToString().Substring(2, 2).ToString();
                        if (context.DealerAccAdjustment.Count() > 0)
                        {
                            var daa = context.DealerAccAdjustment.ToList().LastOrDefault(x => x.DAANo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                            if (daa == null)
                            {
                                DAANO = yearLastTwoPart + "00001";
                            }
                            else
                            {
                                DAANO = yearLastTwoPart + (Convert.ToInt32(daa.DAANo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                            }
                        }
                        else
                        {
                            DAANO = yearLastTwoPart + "00001";
                        }
                        DAA.DAANo = Convert.ToInt32(DAANO);
                        context.DealerAccAdjustment.Add(DAA);
                        addedItems.ForEach(x =>
                        {
                            x.DAANo = DAA.DAANo;
                            context.DealerAccAdjustmentLineItem.Add(x);
                        });
                        UserLog.SaveUserLog(ref context, new UserLog(DAA.DAANo.ToString(), DAA.DAADate, "DAA", ScreenAction.Save));
                        AccountModuleBridge.InsertUpdateFromDAA(ref context, DAA);
                        CustomerCalculationBridge.InsertFromDAA(ref context, DAA, addedItems);
                        context.SaveChanges();
                        return Json(DAA.DAANo, JsonRequestBehavior.AllowGet);
                    }
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
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDAA(int id)
        {
            if (id != 0 && id.ToString() != "")
            {
                var DAA = context.DealerAccAdjustment.FirstOrDefault(x => x.DAANo == id);
                if(DAA != null)
                {
                    var Line = context.DealerAccAdjustmentLineItem.Where(x => x.DAANo == id).ToList();
                    Line.ForEach(y =>
                    {
                        y.DealerName = context.Customers.FirstOrDefault(x => x.Id == y.DealerID).CustomerName;
                    });
                    return Json(new { DAA = DAA, Line = Line }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
       


        public ActionResult UpdateDAAD(DealerAccAdjustment DAA, List<DealerAccAdjustmentLineItem> addedItems)
        {
            if (DAA != null)
            {
                var isExist = context.DealerAccAdjustment.FirstOrDefault(x => x.DAANo == DAA.DAANo);
                if(isExist != null)
                {
                    context.DealerAccAdjustment.Remove(isExist);
                    var isExistLine = context.DealerAccAdjustmentLineItem.Where(x => x.DAANo == DAA.DAANo).ToList();
                    foreach(var a in isExistLine)
                    {
                        context.DealerAccAdjustmentLineItem.Remove(a);
                    }
                }
                if (addedItems.Count > 0)
                {
                   
                    context.DealerAccAdjustment.Add(DAA);
                    addedItems.ForEach(x => {
                        x.DAANo = DAA.DAANo;
                        context.DealerAccAdjustmentLineItem.Add(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(DAA.DAANo.ToString(), DAA.DAADate, "DAA", ScreenAction.Update));
                    AccountModuleBridge.InsertUpdateFromDAA(ref context, DAA);
                    CustomerCalculationBridge.InsertFromDAA(ref context, DAA, addedItems);
                    context.SaveChanges();
                    return Json(DAA.DAANo, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDAAD(DealerAccAdjustment DAA)
        {
            if (DAA != null)
            {
                var isExist = context.DealerAccAdjustment.FirstOrDefault(x => x.DAANo == DAA.DAANo);
                if (isExist != null)
                {
                    context.DealerAccAdjustment.Remove(isExist);
                    var isExistLine = context.DealerAccAdjustmentLineItem.Where(x => x.DAANo == DAA.DAANo).ToList();
                    foreach (var a in isExistLine)
                    {
                        context.DealerAccAdjustmentLineItem.Remove(a);
                    }
                
            
                    UserLog.SaveUserLog(ref context, new UserLog(DAA.DAANo.ToString(), DAA.DAADate, "DAA", ScreenAction.Delete));
                    AccountModuleBridge.DeleteFromDAA(ref context, DAA);
                    CustomerCalculationBridge.DeleteFromDAA(ref context, DAA);
                    context.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
        
    }
}
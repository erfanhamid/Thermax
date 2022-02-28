using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BEEERP.CrystalReport.ReportFormat;
using BEEERP.CrystalReport.ReportRdlc;
using System.Security.Principal;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.SalesModule.Retailer
{
    [ShowNotification]
    public class FRIController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FRI
        public ActionResult FRI()
        {
            ViewBag.Retailer = LoadComboBox.LoadAllRetailerByDealer(null);
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            return View();
        }
        //public ActionResult GetDealerByDepot(int id)
        //{
        //    return Json(LoadComboBox.LoadAllCustomerByDepot(id), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetRetailerByDealer(int id)
        {
            var customer = FindObjectById.CustomerSearch(id);
            if (customer != null)
            {
                var a = new { Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneName, Area = customer.AreaName, retailer = LoadComboBox.LoadAllRetailerByDealer(id) };
                return Json(a, JsonRequestBehavior.AllowGet);
                //return Json(LoadComboBox.LoadAllRetailerByDealer(id), JsonRequestBehavior.AllowGet);
                //return Json(LoadComboBox.LoadAllCustomerByDepot(custId),a, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            
        }

        //public ActionResult GetCustomerById(int custId)
        //{
        //    var customer = FindObjectById.CustomerSearch(custId);
        //    //var a = new { Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Credit = customer.Credit, AsOnDate = customer.AsDate.ToString("dd-MM-yyyy"), OB = customer.OB, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneId, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo, CreditDay = customer.CreditDays };
        //    if (customer != null)
        //    {
        //        var a = new { Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneName, Area = customer.AreaName };
        //        return Json(a, JsonRequestBehavior.AllowGet);
        //        //return Json(LoadComboBox.LoadAllCustomerByDepot(custId),a, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult GetRetailerById(int retId)
        {
            var retailer = FindObjectById.RetailerSearch(retId);
            if (retailer != null)
            {
                var a = new { RType = retailer.DealerYesNo };
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveFRI(FRI fri, List<FRILineItem> addedItems)
        {

            using (context)
            {
                var friNo = 0;
                var noOffri = context.FRI.Count();

                if(noOffri > 0)
                {
                    friNo = context.FRI.Max(x => x.clmFRINo);
                    friNo = friNo + 1;
                }
                else
                {
                    friNo = 1;
                }

                //friNo = noOffri + 1;
                fri.clmFRINo = Convert.ToInt32(friNo);
                var rentAccID = context.AccountConfiguration.FirstOrDefault(x => x.Name == "Freeze Rent Income").RefValue;
                fri.clmAccountID = rentAccID;
                addedItems.ForEach(x =>
                {
                    x.clmFRINo = fri.clmFRINo;
                    context.FRILineItem.Add(x);
                });
                
                context.FRI.Add(fri);

                AccountModuleBridge.InsertUpdateFromFRI(ref context, fri);
               //CustomerCalculationBridge.InsertAndUpdateFromFRI(ref context, fri, addedItems);
                RetailerBalanceCalculationBridge.InsertUpdateFromFRI(ref context, fri, addedItems);
                UserLog.SaveUserLog(ref context, new UserLog(fri.clmFRINo.ToString(), DateTime.Now, "FRI", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { clmFRINo = fri.clmFRINo }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateFRI(FRI fri, List<FRILineItem> addedItems)
        {
            using (context)
            {
                var friExist = context.FRI.FirstOrDefault(x => x.clmFRINo == fri.clmFRINo);
                if (friExist != null)
                {
                    context.FRI.Remove(friExist);

                    context.FRILineItem.Where(x => x.clmFRINo == fri.clmFRINo).ToList().ForEach(x =>
                    {
                        context.FRILineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.clmFRINo = fri.clmFRINo;
                        context.FRILineItem.Add(x);
                    });

                    context.FRI.Add(fri);
                    AccountModuleBridge.InsertUpdateFromFRI(ref context, fri);
                    //CustomerCalculationBridge.InsertAndUpdateFromFRI(ref context, fri, addedItems);
                    RetailerBalanceCalculationBridge.InsertUpdateFromFRI(ref context, fri, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(fri.clmFRINo.ToString(), DateTime.Now, "FreezerRentIncome", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { clmFRINo = fri.clmFRINo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult DeleteFRI(int id)
        {
            var friExist = context.FRI.FirstOrDefault(x => x.clmFRINo == id);
            if (friExist != null)
            {
                context.FRI.Remove(friExist);
                context.FRILineItem.Where(x => x.clmFRINo == id).ToList().ForEach(x =>
                {
                    context.FRILineItem.Remove(x);
                });
                AccountModuleBridge.DeleteFromFRI(ref context, friExist);
                //CustomerCalculationBridge.DeleteFromFRR(ref context, frrExist);
                RetailerBalanceCalculationBridge.DeleteFromFRI(ref context, friExist);
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "FRI", ScreenAction.Delete));

                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetFRIById(int id)
        {
            var fri = context.FRI.FirstOrDefault(x => x.clmFRINo == id);

            if (fri != null)
            {
                var friLineItem = context.FRILineItem.Where(x => x.clmFRINo == id).ToList().Select(x =>
                {
                    var item = context.FRILineItem.FirstOrDefault(y => y.clmFRINo == x.clmFRINo && y.clmRetailerID == x.clmRetailerID);
                    x.clmRefNo = item.clmRefNo;
                    x.clmRetailerID = item.clmRetailerID;
                    x.clmAmount = item.clmAmount;
                    x.clmDealerYN = item.clmDealerYN;
                    x.clmDescriptions = item.clmDescriptions;
                    x.RetailerName = FindObjectById.RetailerSearch(x.clmRetailerID).RetailerName;
                    x.DealerId = FindObjectById.GetRetailerInfoById(x.clmRetailerID).CustomerID;
                    var did = FindObjectById.GetRetailerInfoById(x.clmRetailerID).CustomerID;
                    x.DealerName = FindObjectById.GetCustomerInfoById(did).CustomerName;
                    return x;
                }).ToList();
                return Json(new { fri, friLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
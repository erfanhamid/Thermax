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
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using BEEERP.Models.FreezerRent;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.SalesModule.Retailer
{
    [ShowNotification]
    public class FRRController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: FRR
        public ActionResult FRR()
        {
            ViewBag.Account = LoadComboBox.LoadAllCashBankAccount();
            ViewBag.Retailer = LoadComboBox.LoadAllRetailerByDealer(null);
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            return View();
        }
        public ActionResult GetDealerByDepot(int id)
        {
            return Json(LoadComboBox.LoadAllCustomerByDepot(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRetailerByDealer(int id)
        {
            return Json(LoadComboBox.LoadAllRetailerByDealer(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerById(int custId)
        {
            var customer = FindObjectById.CustomerSearch(custId);
            //var a = new { Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Credit = customer.Credit, AsOnDate = customer.AsDate.ToString("dd-MM-yyyy"), OB = customer.OB, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneId, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo, CreditDay = customer.CreditDays };
            if (customer != null)
            {
                var a = new {  Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneName, Area = customer.AreaName};
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

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

        public ActionResult GetFRRById(int id)
        {
            var frr = context.TblFreezerRent.FirstOrDefault(x => x.clmFRRNo == id);

            if (frr != null)
            {
                var customer = FindObjectById.CustomerSearch(frr.clmDealerID);
                var a = new { Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneName, Area = customer.AreaName };
                var frrLineItem = context.TblFreezerRentLineItem.Where(x => x.clmFRRNo == id).ToList().Select(x =>
                {
                    var retailer = context.TblFreezerRentLineItem.FirstOrDefault(y => y.clmFRRNo == x.clmFRRNo && y.clmRetailerID == x.clmRetailerID);
                    //var itemE = GetItemNameById(item.ItemID);
                    //var itemN = LoadComboBox.LoadFreezerModelDescription(item.ItemID);
                    //x.ItemName = itemE.Description;
                    x.clmAmount = retailer.clmAmount;
                    x.clmDealerYN = retailer.clmDealerYN;
                    x.clmDescription = retailer.clmDescription;
                    x.clmRef = retailer.clmRef;
                    x.clmRetailerID = retailer.clmRetailerID;
                    x.RetailerName = FindObjectById.RetailerSearch(x.clmRetailerID).RetailerName;
                    return x;
                }).ToList();
                return Json(new {a, frr, frrLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveFRR(TblFreezerRent frr, List<TblFreezerRentLineItem> addedItems)
        {

            using (context)
            {
                string frrNo = "";
                string yearLastTwoPart = frr.clmRPDate.Year.ToString().Substring(2, 2).ToString();
                var noOffrr = context.TblFreezerRent.Count();


                if (noOffrr > 0)
                {
                    var lastfrr = context.TblFreezerRent.ToList().LastOrDefault(x => x.clmFRRNo.ToString().Substring(0, 2).ToString() == yearLastTwoPart);
                    if (lastfrr == null)
                    {
                        frrNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        frrNo = yearLastTwoPart + (Convert.ToInt32(lastfrr.clmFRRNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    frrNo = yearLastTwoPart + "00001";
                }
                frr.clmFRRNo = Convert.ToInt32(frrNo);
                addedItems.ForEach(x =>
                {
                    x.clmFRRNo = frr.clmFRRNo;
                    context.TblFreezerRentLineItem.Add(x);
                });

                context.TblFreezerRent.Add(frr);

                AccountModuleBridge.InsertUpdateFromFRR(ref context, frr);
                //CustomerCalculationBridge.InsertAndUpdateFromFRR(ref context, frr, addedItems);
                RetailerBalanceCalculationBridge.InsertUpdateFromFRR(ref context, frr, addedItems);
                UserLog.SaveUserLog(ref context, new UserLog(frr.clmFRRNo.ToString(), DateTime.Now, "FRR", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { a=1,clmFRRNo = frr.clmFRRNo }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateFRR(TblFreezerRent frr, List<TblFreezerRentLineItem> addedItems)
        {
            using (context)
            {
                var frrExist = context.TblFreezerRent.FirstOrDefault(x => x.clmFRRNo == frr.clmFRRNo);
                if (frrExist != null)
                {
                    context.TblFreezerRent.Remove(frrExist);

                    context.TblFreezerRentLineItem.Where(x => x.clmFRRNo == frr.clmFRRNo).ToList().ForEach(x =>
                    {
                        context.TblFreezerRentLineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.clmFRRNo = frr.clmFRRNo;
                        context.TblFreezerRentLineItem.Add(x);
                    });
                    AccountModuleBridge.InsertUpdateFromFRR(ref context, frr);
                    //CustomerCalculationBridge.InsertAndUpdateFromFRR(ref context, frr, addedItems);
                    RetailerBalanceCalculationBridge.InsertUpdateFromFRR(ref context, frr, addedItems);
                    context.TblFreezerRent.Add(frr);
                    UserLog.SaveUserLog(ref context, new UserLog(frr.clmFRRNo.ToString(), DateTime.Now, "FreezerRent", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { clmFRRNo = frr.clmFRRNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult DeleteFRR(int id)
        {
            var frrExist = context.TblFreezerRent.FirstOrDefault(x => x.clmFRRNo == id);
            if (frrExist != null)
            {
                context.TblFreezerRent.Remove(frrExist);
                context.TblFreezerRentLineItem.Where(x => x.clmFRRNo == id).ToList().ForEach(x =>
                {
                    context.TblFreezerRentLineItem.Remove(x);
                });
                AccountModuleBridge.DeleteFromFRR(ref context, frrExist);
                //CustomerCalculationBridge.DeleteFromFRR(ref context, frrExist);
                RetailerBalanceCalculationBridge.DeleteFromFRR(ref context, frrExist);
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "FRR", ScreenAction.Delete));
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
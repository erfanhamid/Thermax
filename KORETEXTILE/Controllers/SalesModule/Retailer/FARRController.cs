using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.SalesModule;
using BEEERP.Models.Log;
using BEEERP.Models.Bridge;

namespace BEEERP.Controllers.SalesModule.Retailer
{
    [ShowNotification]
    public class FARRController : Controller
    {
        BEEContext context = new BEEContext();
        public ActionResult FARR()
        {
            ViewBag.AccountType = LoadComboBox.LoadAccountType();
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
        public ActionResult GetAccountByType(string id)
        {
            
            if (id == "Cash or Bank")
            {
                var a =LoadComboBox.LoadAllCashBankAccount();
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else if(id == "Dealer Balance")
            {
                var a = LoadComboBox.LoadAllCustomerBalance();
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveFARR(FARR farr, List<FARRLineItem> addedItems)
        {

            using (context)
            {
                string farrNo = "";
                string yearLastTwoPart = farr.clmDate.Year.ToString().Substring(2, 2).ToString();
                var noOffarr = context.FARR.Count();


                if (noOffarr > 0)
                {
                    var lastfarr = context.FARR.ToList().LastOrDefault(x => x.clmFARRNo.ToString().Substring(0, 2) == yearLastTwoPart);
                    if (lastfarr == null)
                    {
                        farrNo = yearLastTwoPart + "00001";
                    }
                    else
                    {
                        farrNo = yearLastTwoPart + (Convert.ToInt32(lastfarr.clmFARRNo.ToString().Substring(2, 5)) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    farrNo = yearLastTwoPart + "00001";
                }
                farr.clmFARRNo = Convert.ToInt32(farrNo);
                addedItems.ForEach(x =>
                {
                    x.clmFARRNo = farr.clmFARRNo;
                    context.FARRLineItem.Add(x);
                });

                context.FARR.Add(farr);

                AccountModuleBridge.InsertUpdateFromFARR(ref context, farr);
                //CustomerCalculationBridge.InsertAndUpdateFromFARR(ref context, farr, addedItems);
                RetailerBalanceCalculationBridge.InsertUpdateFromFARR(ref context, farr, addedItems);
                UserLog.SaveUserLog(ref context, new UserLog(farr.clmFARRNo.ToString(), DateTime.Now, "FARR", ScreenAction.Save));
                context.SaveChanges();
                return Json(new { clmFARRNo = farr.clmFARRNo }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateFARR(FARR farr, List<FARRLineItem> addedItems)
        {
            using (context)
            {
                var farrExist = context.FARR.FirstOrDefault(x => x.clmFARRNo == farr.clmFARRNo);
                if (farrExist != null)
                {
                    context.FARR.Remove(farrExist);

                    context.FARRLineItem.Where(x => x.clmFARRNo == farr.clmFARRNo).ToList().ForEach(x =>
                    {
                        context.FARRLineItem.Remove(x);
                    });

                    addedItems.ForEach(x =>
                    {
                        x.clmFARRNo = farr.clmFARRNo;
                        context.FARRLineItem.Add(x);
                    });

                    context.FARR.Add(farr);
                    AccountModuleBridge.InsertUpdateFromFARR(ref context, farr);
                    //CustomerCalculationBridge.InsertAndUpdateFromFARR(ref context, farr, addedItems);
                    RetailerBalanceCalculationBridge.InsertUpdateFromFARR(ref context, farr, addedItems);
                    UserLog.SaveUserLog(ref context, new UserLog(farr.clmFARRNo.ToString(), DateTime.Now, "FARR", ScreenAction.Update));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, addedItems, issuFGStore);
                    context.SaveChanges();
                    return Json(new { clmFARRNo = farr.clmFARRNo }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult DeleteFARR(int id)
        {
            var farrExist = context.FARR.FirstOrDefault(x => x.clmFARRNo == id);
            if (farrExist != null)
            {
                context.FARR.Remove(farrExist);
                context.FARRLineItem.Where(x => x.clmFARRNo == id).ToList().ForEach(x =>
                {
                    context.FARRLineItem.Remove(x);
                });

                AccountModuleBridge.DeleteFromFARR(ref context, farrExist);
                //CustomerCalculationBridge.DeleteFromFRR(ref context, frrExist);
                RetailerBalanceCalculationBridge.DeleteFromFARR(ref context, farrExist);
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "FARR", ScreenAction.Delete));
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFARRById(int id)
        {
            var farr = context.FARR.FirstOrDefault(x => x.clmFARRNo == id);

            if (farr != null)
            {
                var farrLineItem = context.FARRLineItem.Where(x => x.clmFARRNo == id).ToList().Select(x =>
                {
                    var item = context.FARRLineItem.FirstOrDefault(y => y.clmFARRNo == x.clmFARRNo && y.clmRetailerID == x.clmRetailerID);
                    x.clmFARRNo = item.clmFARRNo;
                    x.clmRetailerID = item.clmRetailerID;
                    x.RetailerName = FindObjectById.RetailerSearch(x.clmRetailerID).RetailerName;
                    x.clmAmount = item.clmAmount;
                    x.clmDealerYN = item.clmDealerYN;
                    x.clmDescriptions = item.clmDescriptions;
                    return x;
                }).ToList();
                return Json(new { farr, farrLineItem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
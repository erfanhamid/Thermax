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
    public class RetailerInformationController : Controller 
    {
        BEEContext context = new BEEContext();
        // GET: RetailerInformation
        public ActionResult RetailerInformation()
        {
            //ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot();
            ViewBag.Depot = LoadComboBox.LoadAllDepot();
            ViewBag.Dealer = LoadComboBox.LoadAllCustomerByDepot(null);
            return View();
        }
        public ActionResult GetDealerByDepot(int id)
        {
            return Json(LoadComboBox.LoadAllCustomerByDepot(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerById(int custId)
        {
            var customer = FindObjectById.CustomerSearch(custId);
            //var a = new { Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Credit = customer.Credit, AsOnDate = customer.AsDate.ToString("dd-MM-yyyy"), OB = customer.OB, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneId, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo, CreditDay = customer.CreditDays };
            if (customer != null)
            {
                int mobileNo = customer.MobileNo.Length;
                if (mobileNo != 14)
                {
                    customer.MobileNo = customer.MobileNo;
                }
                else
                {
                    customer.MobileNo = customer.MobileNo.Substring(3, 11);
                }
                var a = new {  Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneName, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo };
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SearchRetailerById(int retId)
        {
            var item = context.RetailerInformation.FirstOrDefault(x => x.Id == retId);
            if (item!=null)
            {
                var customer = FindObjectById.CustomerSearch(item.CustomerID);
                //var a = new { Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Credit = customer.Credit, AsOnDate = customer.AsDate.ToString("dd-MM-yyyy"), OB = customer.OB, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneId, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo, CreditDay = customer.CreditDays };
                if (customer != null)
                {
                    int mobileNo = customer.MobileNo.Length;
                    if (mobileNo != 14)
                    {
                        customer.MobileNo = customer.MobileNo;
                    }
                    else
                    {
                        customer.MobileNo = customer.MobileNo.Substring(3, 11);
                    }
                    var a = new { item, Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneName, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo };
                    return Json(a, JsonRequestBehavior.AllowGet);
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

        public ActionResult SaveRetailer(RetailerInformation retailer)
        {
            var lastRetailer = context.RetailerInformation.ToList().LastOrDefault();
            if (lastRetailer!=null)
            {
                retailer.Id = lastRetailer.Id + 1;
                context.RetailerInformation.Add(retailer);

                UserLog.SaveUserLog(ref context, new UserLog(retailer.Id.ToString(), DateTime.Now, "Id", ScreenAction.Save));
                if (retailer.OB>0)
                {
                    AccountModuleBridge.InsertFromRetailerInformation(ref context, retailer);
                    RetailerBalanceCalculationBridge.InsertToCustomerBalanceCalculation(ref context, retailer);
                    RetailerBalanceCalculationBridge.InsertToInstallmentBalanceCalculation(ref context, retailer);
                }
                
                context.SaveChanges();
                return Json(new { Id = retailer.Id,message = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult updateRetailer(RetailerInformation retailer)
        {
            var existRetailer = context.RetailerInformation.FirstOrDefault(x => x.Id == retailer.Id);
            context.RetailerInformation.Remove(existRetailer);
            context.RetailerInformation.Add(retailer);

            UserLog.SaveUserLog(ref context, new UserLog(retailer.Id.ToString(), DateTime.Now, "Id", ScreenAction.Save));
            if (existRetailer.OB>0)
            {
                AccountModuleBridge.DeleteFromRetailerInformation(ref context, existRetailer);
                RetailerBalanceCalculationBridge.DeleteFromCustomerBalanceCalculation(ref context, existRetailer);
                RetailerBalanceCalculationBridge.DeleteFromInstallmentBalanceCalculation(ref context, existRetailer);
            }
            if (retailer.OB>0)
            {
                AccountModuleBridge.InsertFromRetailerInformation(ref context, retailer);
                RetailerBalanceCalculationBridge.InsertToCustomerBalanceCalculation(ref context, retailer);
                RetailerBalanceCalculationBridge.InsertToInstallmentBalanceCalculation(ref context, retailer);
            }
                
            context.SaveChanges();
            return Json(new { Id = retailer.Id,message = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRetailer(RetailerInformation retailer)
        {

            var isExists = context.RetailerInformation.FirstOrDefault(x=>x.Id==retailer.Id);
            context.RetailerInformation.Remove(isExists);

            UserLog.SaveUserLog(ref context, new UserLog(retailer.Id.ToString(), DateTime.Now, "Id", ScreenAction.Delete));
            if (isExists.OB > 0)
            {
                AccountModuleBridge.DeleteFromRetailerInformation(ref context, retailer);
                RetailerBalanceCalculationBridge.DeleteFromCustomerBalanceCalculation(ref context, retailer);
                RetailerBalanceCalculationBridge.DeleteFromInstallmentBalanceCalculation(ref context, retailer);
            }
            context.SaveChanges();
            return Json(new { message = 1 }, JsonRequestBehavior.AllowGet);
            

        }

        //public ActionResult SearchRetailerById(int retId)
        //{
        //    var retailer = FindObjectById.RetailerSearch(retId);
        //    //var a = new { Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Credit = customer.Credit, AsOnDate = customer.AsDate.ToString("dd-MM-yyyy"), OB = customer.OB, Zone = FindObjectById.ZoneSearch(customer.ZoneId).ZoneId, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo, CreditDay = customer.CreditDays };
        //    if (retailer != null)
        //    {
        //        int mobileNo = retailer.MobileNo.Length;
        //        if (mobileNo != 14)
        //        {
        //            retailer.MobileNo = retailer.MobileNo;
        //        }
        //        else
        //        {
        //            retailer.MobileNo = retailer.MobileNo.Substring(3, 11);
        //        }
        //        var a = new { Name = retailer.RetailerName, /*Depot = FindObjectById.BranchSearch(customer.DepotId).Slno*/ OB = retailer.OB,/* Zone = customer.ZoneId, Area = customer.AreaName,*/ ContactPerson = retailer.ConPerson, MobileNo = retailer.MobileNo, TelePhnNo = retailer.TelephoneNo, Email = retailer.EmailId, BillTo = retailer.Billto, ShipTo = retailer.Shipto };
        //        return Json(a, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
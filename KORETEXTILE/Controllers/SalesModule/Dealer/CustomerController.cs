using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.SalesModule.Dealer
{
    [ShowNotification]
    [SaleModule]
    public class CustomerController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: Customer
        [Permission]
        public ActionResult CustomerCreate()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            ViewBag.DepotId = depot;
            //if (depot == null)
            //{
            //    ViewBag.Disabled = "true";
            //    ViewBag.TSO = LoadComboBox.LoadTSO(null);
            //}
            //else
            //{
            //    ViewBag.Disabled = "false";

            //    ViewBag.TSO = LoadComboBox.LoadTSO((int)depot);
            //}
            int CustomerID = 0;
            var CustId = context.Customers.Count();
            if(CustId == 0)
            {
                CustomerID = 1001;
            }
            else
            {
                var maxcustid = context.Customers.Max(x => x.Id);

                CustomerID = maxcustid + 1;
            }


            ViewBag.Id = CustomerID;
            ViewBag.ZoneId = LoadComboBox.LoadZone();
            ViewBag.AreaName = LoadComboBox.LoadArea();
            //ViewBag.SubArea = LoadComboBox.LoadSubArea();
            ViewBag.GroupId = LoadComboBox.LoadBranchInfo();
            //ViewBag.CategoryType = LoadComboBox.LoadCategoryType();
            ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
            
            ViewBag.DepotId = ScreenSessionData.GetEmployeeDepot();
            //string user=context.UserActionPermission.FirstOrDefault(x=>x.RoleId==)
            return View();
        }
        
        public ActionResult SaveCustomer(Customer customer) 
        {
            var ScreenID = context.Screens.FirstOrDefault(x => x.Name == "Create Dealer").ScreenID;
            var IsExpired = context.ScreenEntryLocks.FirstOrDefault(x => x.ScreenID == ScreenID);


            customer.AsDate = FindObjectById.GetCompanySetup();
            if (customer.ShipTo == null)
            {
                customer.ShipTo = "";
            }
            if (customer.BillTo == null)
            {
                customer.BillTo = "";
            }
            if (customer.ConPerson == null)
            {
                customer.ConPerson = "";
            }
            if (customer.EmailId == null)
            {
                customer.EmailId = "";
            }
            if (customer.AreaName == null)
            {
                customer.AreaName = "";
            }
            if (customer.TelephoneNo == null)
            {
                customer.TelephoneNo = "";
            }

            var customerExist = FindObjectById.CustomerSearch(customer.Id);
            if (customerExist == null)
            {
                string mobile = customer.MobileNo;
                mobile = mobile.Trim(new Char[] { ' ' });

                if (mobile.Length != 11)
                {
                    return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    customer.MobileNo = "+88" + mobile;
                    context.Customers.Add(customer);
                    AccountModuleBridge.InsertFromCustomer(ref context, customer);
                    CustomerCalculationBridge.InsertFromCustomerBalanceCalculation(ref context, customer);
                    UserLog.SaveUserLog(ref context, new UserLog(customer.Id.ToString(), DateTime.Now, "Customer", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { customer.Id, Message = 1 }, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                return Json(new { Message = 4 }, JsonRequestBehavior.AllowGet);
            }


        }
        [Permission]
        public ActionResult GetCustomerById(int id)
        {
            var customer = context.Customers.FirstOrDefault(x => x.Id == id);
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
                return Json(customer, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        

        public ActionResult SearchCustomerById(int custId)
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
                var a = new { TSO = customer.TSOId , Name = customer.CustomerName, Depot = FindObjectById.BranchSearch(customer.DepotId).Slno, Credit = customer.Credit, AsOnDate = customer.AsDate.ToString("dd-MM-yyyy"), OB = customer.OB, Zone = customer.ZoneId, Area = customer.AreaName, ContactPerson = customer.ConPerson, MobileNo = customer.MobileNo, TelePhnNo = customer.TelephoneNo, Email = customer.EmailId, BillTo = customer.BillTo, ShipTo = customer.ShipTo, CreditDay = customer.CreditDays, CustomerType=customer.CustomerType };
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateCustomer(Customer customer)
        {
                if (customer.ShipTo == null)
                {
                    customer.ShipTo = "";
                }
                if (customer.BillTo == null)
                {
                    customer.BillTo = "";
                }
                if (customer.ConPerson == null)
                {
                    customer.ConPerson = "";
                }
                if (customer.EmailId == null)
                {
                    customer.EmailId = "";
                }
                if (customer.AreaName == null)
                {
                    customer.AreaName = "";
                }
                if (customer.TelephoneNo == null)
                {
                    customer.TelephoneNo = "";
                }

                var custExist = context.Customers.FirstOrDefault(x => x.Id == customer.Id);
                customer.AsDate = FindObjectById.GetCompanySetup();

                if (custExist != null)
                {
                    string mobile = customer.MobileNo;
                    mobile = mobile.Trim(new Char[] { ' ' });
                    if (mobile.Length != 11)
                    {
                        return Json(new { Message = 2 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        context.Customers.Remove(custExist);
                        customer.MobileNo = "+88" + mobile;
                        context.Customers.Add(customer);
                        AccountModuleBridge.InsertFromCustomer(ref context, customer);
                        CustomerCalculationBridge.InsertFromCustomerBalanceCalculation(ref context, customer);
                        UserLog.SaveUserLog(ref context, new UserLog(customer.Id.ToString(), DateTime.Now, "Customer", ScreenAction.Update));
                        context.SaveChanges();
                        return Json(new { customer.Id, Message = 1 }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
            
        }

        public ActionResult DeleteCustomerByid(int id)
        {
            var custExist = context.Customers.ToList().Find(x => x.Id == id);
            var duclicateCust = context.CustomerBalanceCalculations.Where(x => x.CustomerID == id).Count();

            if (custExist != null)
            {
                if (duclicateCust > 1)
                {
                    return Json(new { Id = 0, Message = 0}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var items = context.Transection.Where(x => x.DocType == "COB" && x.DocID == id).ToList();
                    var custBalExist = context.CustomerBalanceCalculations.Where(x => x.CustomerID == id).ToList();
                    context.Customers.Remove(custExist);
                    items.ForEach(x =>
                    {
                        context.Transection.Remove(x);
                    });
                    custBalExist.ForEach(x =>
                    {
                        context.CustomerBalanceCalculations.Remove(x);
                    });
                    //AccountModuleBridge.InsertFromCustomer(ref context, custExist);
                    //CustomerCalculationBridge.InsertFromCustomerBalanceCalculation(ref context, custExist);
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "Customer", ScreenAction.Delete));
                    context.SaveChanges();

                    return Json(new { Id = id, Message = 1}, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                return Json(new { Id = 0, Message = 2} , JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetTsoByDepotId(int depot)
        //{
        //    return Json(LoadComboBox.LoadTSO(depot), JsonRequestBehavior.AllowGet);
        //}

    }
}
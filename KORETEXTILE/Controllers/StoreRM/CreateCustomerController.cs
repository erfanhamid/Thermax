using BEEERP.Models.Bridge;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Log;
using BEEERP.Models.StoreRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.StoreRM
{
    [ShowNotification]
    public class CreateCustomerController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: CreateCustomer
        public ActionResult CreateCustomer()
        {
            ViewBag.Date = FindObjectById.GetCompanySetup().ToString("dd-MM-yyyy");
            return View();
        }
        public ActionResult SaveCustomer(CreateCustomer customer)
        {
            using (context)
            {
                var custNo = 0;
                var noOfcust = context.CreateCustomer.Count();
                custNo = noOfcust + 1;
                customer.clmCustomerID = Convert.ToInt32(custNo);
                context.CreateCustomer.Add(customer);

                var customerExist = FindObjectById.RMCustomerSearch(customer.clmCustomerID);
                if (customerExist == null)
                {
                    //string mobile = customer.clmMobileNo;
                    string name = customer.clmCustomerName;
                    //mobile = mobile.Trim(new Char[] { ' ' });

                    if (name == "")
                    {
                        return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //customer.clmMobileNo = "+88" + mobile;
                        context.CreateCustomer.Add(customer);
                        AccountModuleBridge.InsertFromRMCustomer(ref context, customer);
                        RMCustomerCalculationBridge.InsertFromCreateCustomer(ref context, customer);
                        UserLog.SaveUserLog(ref context, new UserLog(customer.clmCustomerID.ToString(), DateTime.Now, "CreateCustomer", ScreenAction.Save));
                        context.SaveChanges();
                        return Json(new { customer.clmCustomerID, Message = 1 }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json("0", JsonRequestBehavior.AllowGet);
                
            }
        }
        public ActionResult UpdateCustomer(CreateCustomer customer)
        {
            
            if (customer.clmBillingAddress == null)
            {
                customer.clmBillingAddress = "";
            }
            if (customer.clmContactPerson == null)
            {
                customer.clmContactPerson = "";
            }
            if (customer.clmEmailNo == null)
            {
                customer.clmEmailNo = "";
            }
            if (customer.clmPhoneNo == null)
            {
                customer.clmPhoneNo = "";
            }

            var custExist = context.CreateCustomer.FirstOrDefault(x => x.clmCustomerID == customer.clmCustomerID);
            customer.clmAccOBDate = FindObjectById.GetCompanySetup();

            if (custExist != null)
            {
                string name = customer.clmCustomerName;
                //name = name.Trim(new Char[] { ' ' });
                if (name == "")
                {
                    return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    context.CreateCustomer.Remove(custExist);
                    //customer.clmMobileNo = "+88" + mobile;
                    context.CreateCustomer.Add(customer);
                    AccountModuleBridge.InsertFromRMCustomer(ref context, customer);
                    RMCustomerCalculationBridge.InsertFromCreateCustomer(ref context, customer);
                    UserLog.SaveUserLog(ref context, new UserLog(customer.clmCustomerID.ToString(), DateTime.Now, "CreateCustomer", ScreenAction.Update));
                    context.SaveChanges();
                    return Json(new { customer.clmCustomerID, Message = 1 }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { Message = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DeleteCustomerByid(int id)
        {
            var custExist = context.CreateCustomer.ToList().Find(x => x.clmCustomerID == id);
            var duclicateCust = context.CustomerBalanceCalculations.Where(x => x.CustomerID == id).Count();

            if (custExist != null)
            {
                if (duclicateCust > 1)
                {
                    return Json(new { Id = 0, Message = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var items = context.Transection.Where(x => x.DocType == "COB" && x.DocID == id).ToList();
                    var custBalExist = context.RMCustomerBalanceCalculation.Where(x => x.RMCustomerID == id).ToList();
                    context.CreateCustomer.Remove(custExist);
                    items.ForEach(x =>
                    {
                        context.Transection.Remove(x);
                    });
                    custBalExist.ForEach(x =>
                    {
                        context.RMCustomerBalanceCalculation.Remove(x);
                    });
                    //AccountModuleBridge.InsertFromCustomer(ref context, custExist);
                    //CustomerCalculationBridge.InsertFromCustomerBalanceCalculation(ref context, custExist);
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "CreateCustomer", ScreenAction.Delete));
                    context.SaveChanges();

                    return Json(new { Id = id, Message = 1 }, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                return Json(new { Id = 0, Message = 2 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCustomerById(int id)
        {
            var customer = context.CreateCustomer.FirstOrDefault(x => x.clmCustomerID == id);

            if (customer != null)
            {
                return Json(new { customer }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
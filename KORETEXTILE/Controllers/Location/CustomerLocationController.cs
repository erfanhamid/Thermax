using BEEERP.Models.CommonInformation;
using BEEERP.Models.CustomAttribute;
using BEEERP.Models.Database;
using BEEERP.Models.Location;
using BEEERP.Models.Log;
using BEEERP.Models.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEEERP.Controllers.Location
{
    [ShowNotification]
    public class CustomerLocationController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: CustomerLocation
        public ActionResult CLEntry()
        {
            ViewBag.tso = LoadComboBox.LoadAllTSO();
            ViewBag.cust = LoadComboBox.LoadCustomerByTSO(0);
            return View();
        }

        public ActionResult GetDealerByTsoId(int TsoID)
        {
            return Json(LoadComboBox.LoadCustomerByTSO(TsoID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCL(List<CustomerLocation> addedItems)
        {
            CustomerLocation cl = new CustomerLocation();
            var ClNo = context.CustomerLocations.Count();
            if (ClNo == 0)
            {
                cl.CLNo = 1;
            }
            else
            {
                cl.CLNo = context.CustomerLocations.Max(b => b.CLNo) + 1;
            }
            

            var CLExist = context.CustomerLocations.FirstOrDefault(x => x.CLNo == cl.CLNo);
            if (CLExist == null)
            {
                try
                {
                    //context.CustomerLocations.Add(importProformaInvoice);
                    addedItems.ForEach(x => { x.CLNo = cl.CLNo; context.CustomerLocations.Add(x); });
                    UserLog.SaveUserLog(ref context, new UserLog(cl.CLNo.ToString(), DateTime.Now, "DealerLocation", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(new { Message = 1, JsonRequestBehavior.AllowGet });

                }
                catch (Exception ex)
                {
                    return Json(new { Message = 0, JsonRequestBehavior.AllowGet });
                }

            }
            else
            {

                return Json(new { Message = 2, JsonRequestBehavior.AllowGet });

            }


        }


        public ActionResult UpdateCL(int id, List<CustomerLocation> addedItems)
        {

            using (context)
            {
                var IsExist = context.CustomerLocations.FirstOrDefault(x => x.CLNo == id);
                if (IsExist != null)
                {
                    
                    context.CustomerLocations.Where(x => x.CLNo == id).ToList().ForEach(x =>
                    {
                        context.CustomerLocations.Remove(x);
                    });

                }
                try
                {
                    //context.CustomerLocations.Add(importProformaInvoice);
                    addedItems.ForEach(x => { x.CLNo = id; context.CustomerLocations.Add(x); });
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "DealerLocation", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(id, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet );
                }

            }
          


        }

        public ActionResult GetDealerbycust(int CustomerID)
        {
            var isExist = context.CustomerLocations.FirstOrDefault(x => x.CustomerID == CustomerID);
            if(isExist != null)
            {
                var cust = context.CustomerLocations.FirstOrDefault(x => x.CustomerID == CustomerID).CLNo;
                return Json(new { cust }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { cust = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult GetCLById(int id)
        {

            var CL = context.CustomerLocations.FirstOrDefault(x => x.CLNo == id);
            List<CustomerLocation> customerLocations = new List<CustomerLocation>();

            if (CL != null)
            {
                var CLItem = context.CustomerLocations.Where(x => x.CLNo == id).ToList();



                CLItem.ForEach(x =>
                {
                    CustomerLocation c = new CustomerLocation();

                    c.CLNo = x.CLNo;
                    c.CustomerID = x.CustomerID;
                    c.TsoID = x.TsoID;
                    c.Longitude = x.Longitude;
                    c.Latitude = x.Latitude;
                    c.TsoName = context.Employees.FirstOrDefault(y => y.Id == x.TsoID).Name;
                    c.CustomerName = context.Customers.FirstOrDefault(z => z.Id == x.CustomerID).CustomerName;
                    c.s1 += 1;


                    customerLocations.Add(c);
                });

                return Json(new { data = customerLocations }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

            public ActionResult DeleteCL(int id)
            {
                var CLExist = context.CustomerLocations.FirstOrDefault(x => x.CLNo == id);
                if (CLExist != null)
                {
                    
                    //var sifdFgLineItem = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList();
                    context.CustomerLocations.Where(x => x.CLNo == id).ToList().ForEach(x =>
                    {
                        context.CustomerLocations.Remove(x);
                    });
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "CL", ScreenAction.Delete));
                    //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                    context.SaveChanges();
                    return Json(id, JsonRequestBehavior.AllowGet);
                }

                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
    }

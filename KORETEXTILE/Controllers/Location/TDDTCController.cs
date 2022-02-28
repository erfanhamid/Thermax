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
    public class TDDTCController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: TDDTC
        public ActionResult TDDTCEntry()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            //if (depot == null)
            //{
                
            //    ViewBag.TSO = LoadComboBox.LoadTSO(null);
             
            //}
            //else
            //{
                
            //    ViewBag.tso = LoadComboBox.LoadTSO((int)depot);
            
            //}
            //ViewBag.tso = LoadComboBox.LoadAllTSO();
            ViewBag.zone = LoadComboBox.LoadZone();
            ViewBag.area = LoadComboBox.LoadCustomerArea();
            //ViewBag.cust = LoadComboBox.LoadCustomerByTSO(0);
            ViewBag.cust = LoadComboBox.LoadCustomerByTSOInCL(0);
            return View();
        }
        public ActionResult GetCustomerByZoneID(int id, int tsoId)
        {
            //CustomerList cl = new CustomerList();
            List<CustomerList> clList = new List<CustomerList>();
            if (id != 0)
            {
                
                //var cust = context.CustomerLocations.ToList();
                //var item = context.Customers.Where(x => x.ZoneId == id).ToList();
                var item = (from c in context.Customers
                            join cl in context.CustomerLocations on c.Id equals cl.CustomerID
                            where c.ZoneId == id && c.TSOId == tsoId
                            select c).ToList();
                item.ForEach(x =>
               {
                   CustomerList cl = new CustomerList();

                   var Cust = context.CustomerLocations.FirstOrDefault(b => b.CustomerID == x.Id);
                   if(Cust != null){
                       cl.TsoID = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).TsoID;
                       cl.TSOName = context.Employees.FirstOrDefault(c => c.Id == cl.TsoID).Name;
                       cl.CustomerID = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).CustomerID;
                       cl.CustomerName = context.Customers.FirstOrDefault(p => p.Id == cl.CustomerID).CustomerName;
                       cl.Latitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).Latitude;
                       cl.Longitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).Longitude;
                       cl.Area = context.Customers.FirstOrDefault(b => b.Id == cl.CustomerID).AreaName;
                       var isExist = context.TsoCustomerTravelCheckpoints.FirstOrDefault(a => a.ZoneID == id);
                       cl.ZoneID = x.ZoneId;
                      
                           //cl.Date = context.TsoCustomerTravelCheckpoints.FirstOrDefault(z => z.ZoneID == id).Date;
                       
                           cl.Date = System.DateTime.Now;
                       
                       cl.s1 += 1;
                       clList.Add(cl);

                   }
                   
               });
               
            }
            return Json(new { data = clList }, JsonRequestBehavior.AllowGet);
            //return Json(0, JsonRequestBehavior.AllowGet);
          }
        public ActionResult GetCustomerByCustomerID(int id)
        {
            List<CustomerList> clList = new List<CustomerList>();
            if (id != 0)
            {

                //var cust = context.CustomerLocations.ToList();
                var item = context.Customers.Where(x => x.Id == id).ToList();
                item.ForEach(x =>
                {
                    CustomerList cl = new CustomerList();

                    var Cust = context.CustomerLocations.FirstOrDefault(b => b.CustomerID == x.Id);
                    if (Cust != null)
                    {
                        cl.TsoID = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).TsoID;
                        cl.TSOName = context.Employees.FirstOrDefault(c => c.Id == cl.TsoID).Name;
                        cl.CustomerID = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).CustomerID;
                        cl.CustomerName = context.Customers.FirstOrDefault(p => p.Id == cl.CustomerID).CustomerName;
                        cl.Latitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).Latitude;
                        cl.Longitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).Longitude;
                        cl.Area = context.Customers.FirstOrDefault(b => b.Id == cl.CustomerID).AreaName;
                        var isExist = context.TsoCustomerTravelCheckpoints.FirstOrDefault(a => a.CustomerID == id);
                        if (isExist != null)
                        {
                            cl.Date = context.TsoCustomerTravelCheckpoints.FirstOrDefault(z => z.CustomerID == id).Date;
                        }
                        else
                        {
                            cl.Date = System.DateTime.Now;
                        }
                        cl.s1 += 1;
                        clList.Add(cl);

                    }

                });

            }
            return Json(new { data = clList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerByArea(string area, int tsoId)
        {
            List<CustomerList> clList = new List<CustomerList>();
            if (area != null)
            {

                //var cust = context.CustomerLocations.ToList();
                var item = context.Customers.Where(x => x.AreaName == area).ToList();
                item.ForEach(x =>
                {
                    CustomerList cl = new CustomerList();

                    var Cust = context.CustomerLocations.FirstOrDefault(b => b.CustomerID == x.Id && b.TsoID == tsoId);
                    if (Cust != null)
                    {
                        cl.TsoID = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).TsoID;
                        cl.TSOName = context.Employees.FirstOrDefault(c => c.Id == cl.TsoID).Name;
                        cl.CustomerID = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).CustomerID;
                        cl.CustomerName = context.Customers.FirstOrDefault(p => p.Id == cl.CustomerID).CustomerName;
                        cl.Latitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).Latitude;
                        cl.Longitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.Id).Longitude;
                        cl.Area = x.AreaName;
                        var isExist = context.TsoCustomerTravelCheckpoints.FirstOrDefault(a => a.Area == area);
                        if (isExist != null)
                        {
                            cl.Date = context.TsoCustomerTravelCheckpoints.FirstOrDefault(z => z.Area == area).Date;
                        }
                        else
                        {
                            cl.Date = System.DateTime.Now;
                        }
                        cl.s1 += 1;
                        clList.Add(cl);

                    }

                });

            }
            return Json(new { data = clList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadCustomerByTSO(int id)
        {

            if (id != 0)
            {


                return Json(LoadComboBox.LoadCustomerByTSOInCL(id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCustomerPrevDate(DateTime date, int tsoId)
        {
            List<CustomerList> clList = new List<CustomerList>();
            if (date != null)
            {

                //var cust = context.CustomerLocations.ToList();
                //var item = context.TsoCustomerTravelCheckpoints.Where(x => x.Date == date).ToList();
                //var t = DateTime.Now.Date;
                var item = context.TsoCustomerTravelCheckpoints.Where(x=> x.Date == date && x.TsoID == tsoId).ToList();
                if (item != null)
                {
                    item.ForEach(x =>
                    {
                        CustomerList cl = new CustomerList();
                        cl.TsoID = x.TsoID;
                        cl.TSOName = context.Employees.FirstOrDefault(c => c.Id == x.TsoID).Name;
                        cl.CustomerID = x.CustomerID;
                        cl.CustomerName = context.Customers.FirstOrDefault(p => p.Id == cl.CustomerID).CustomerName;
                        cl.Latitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.CustomerID).Latitude;
                        cl.Longitude = context.CustomerLocations.FirstOrDefault(y => y.CustomerID == x.CustomerID).Longitude;
                        cl.Area = context.Customers.FirstOrDefault(b => b.Id == cl.CustomerID).AreaName;
                        cl.Date = x.Date;
                        cl.Category = x.Category;
                        cl.TCTNo = x.TCTNo;
                        cl.s1 += 1;
                        clList.Add(cl);
                    });
                }
                

            }
            return Json(new { data = clList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTDDTC(List<TsoCustomerTravelCheckpoint> addedItems)
        {
            TsoCustomerTravelCheckpoint tc = new TsoCustomerTravelCheckpoint();
            var tcNo = context.TsoCustomerTravelCheckpoints.Count();
            if (tcNo == 0)
            {
                tc.TCTNo = 1;
            }
            else
            {
                tc.TCTNo = context.TsoCustomerTravelCheckpoints.Max(b => b.TCTNo) + 1;
            }


            var tcExist = context.TsoCustomerTravelCheckpoints.FirstOrDefault(x => x.TCTNo == tc.TCTNo);
            if (tcExist == null)
            {
                try
                {
                    var IsSuccess = false;
                    addedItems.ForEach(x => 
                    {
                        x.TCTNo = tc.TCTNo;
                        x.IsVisit = false;
                        var isExist = context.TsoCustomerTravelCheckpoints.FirstOrDefault(z => z.CustomerID == x.CustomerID && z.Date == x.Date);
                        if (isExist == null)
                        {
                            context.TsoCustomerTravelCheckpoints.Add(x);
                            IsSuccess = true;
                        }
                        else
                        {
                           
                        }
                    });
                    if(IsSuccess == true)
                    {
                        UserLog.SaveUserLog(ref context, new UserLog(tc.TCTNo.ToString(), DateTime.Now, "TDDTC", ScreenAction.Save));
                        context.SaveChanges();
                        return Json(new { id =tc.TCTNo,  Message = 1, JsonRequestBehavior.AllowGet });
                    }
                    else
                    {
                        return Json(new { Message = 2, JsonRequestBehavior.AllowGet });
                    }
                           

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

        public ActionResult UpdateTDDTC(int id, List<TsoCustomerTravelCheckpoint> addedItems)
        {

            using (context)
            {
                var IsExist = context.TsoCustomerTravelCheckpoints.Where(x => x.TCTNo == id).ToList();
                if (IsExist != null)
                {
                    IsExist.ForEach(x => {
                        context.TsoCustomerTravelCheckpoints.Remove(x);
                    });

                    //context.TsoCustomerTravelCheckpoints.Where(x => x.TCTNo == id).ToList().ForEach(x =>
                    //{
                    //    context.TsoCustomerTravelCheckpoints.Remove(x);
                    //});

                }
                try
                {
                    //context.CustomerLocations.Add(importProformaInvoice);
                    addedItems.ForEach(x => { x.TCTNo = id; x.IsVisit = false; context.TsoCustomerTravelCheckpoints.Add(x); });
                    UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "TDDTC", ScreenAction.Save));
                    context.SaveChanges();
                    return Json(id, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }

            }



        }
        public ActionResult DeleteTDDTC(int id)
        {
            var CLExist = context.TsoCustomerTravelCheckpoints.FirstOrDefault(x => x.TCTNo == id);
            if (CLExist != null)
            {

                //var sifdFgLineItem = context.SIFDLineItem.Where(x => x.SIFDNo == id).ToList();
                context.TsoCustomerTravelCheckpoints.Where(x => x.TCTNo == id).ToList().ForEach(x =>
                {
                    context.TsoCustomerTravelCheckpoints.Remove(x);
                });
                UserLog.SaveUserLog(ref context, new UserLog(id.ToString(), DateTime.Now, "TDDTC", ScreenAction.Delete));
                //InventoryTransactionBridge.InsertFromIFGSEntry(ref context, issuFgLineItem, ifgsExist);
                context.SaveChanges();
                return Json(id, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTCTById(int id)
        {

            var tc = context.TsoCustomerTravelCheckpoints.FirstOrDefault(x => x.TCTNo == id);
            List<CustomerList> cl = new List<CustomerList>();

            if (tc != null)
            {
                var tcItem = context.TsoCustomerTravelCheckpoints.Where(x => x.TCTNo == id).ToList();
                tcItem.ForEach(x =>
                {
                    CustomerList c = new CustomerList();

                    c.TCTNo = x.TCTNo;
                    c.CustomerID = x.CustomerID;
                    c.TsoID = x.TsoID;
                    c.Longitude = context.CustomerLocations.FirstOrDefault(z => z.CustomerID == x.CustomerID).Longitude;
                    c.Latitude = context.CustomerLocations.FirstOrDefault(z => z.CustomerID == x.CustomerID).Latitude;
                    c.TSOName = context.Employees.FirstOrDefault(y => y.Id == x.TsoID).Name;
                    c.CustomerName = context.Customers.FirstOrDefault(z => z.Id == x.CustomerID).CustomerName;
                    c.Date = x.Date;
                    c.PreviousDate = x.PreviousDate;
                    c.Category = x.Category;
                    c.Area = x.Area;
                    c.ZoneID = x.ZoneID;
                    c.s1 += 1;


                    cl.Add(c);
                });

                return Json(new { data = cl }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

    }

    public sealed class CustomerList
    {
        public int TCTNo { get; set; }
        public int TsoID { get; set; }
        public string TSOName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Category { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public int s1 { get; set; }
        public int ZoneID { get; set; }
        public string Area { get; set; }
        public bool IsVisit { get; set; }
    }
}
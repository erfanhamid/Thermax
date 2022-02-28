using BEEERP.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.ViewModel.Location;
using BEEERP.Models.Database;

namespace BEEERP.Controllers.Location
{
    [ShowNotification]
    public class GetLocationController : Controller
    {
        BEEContext context = new BEEContext();
        // GET: GetLocation
        [HttpPost]
        public ActionResult GetLocation(string userId, DateTime fromDate, DateTime toDate)
        {
            DateTime toDateA = toDate.AddDays(1);
            if (userId != "" && fromDate != null && toDate != null)
            {
                var location = context.Locationtrack.Where(x => x.UserId == userId && x.DateTime >= fromDate && x.DateTime <= toDateA).ToList();    

                List<LocationVModel> model = new List<LocationVModel>();
                var totkm = 0.0;
                location.ForEach(x => {
                    LocationVModel m = new LocationVModel();
                    m.Id = x.Id;
                    m.LocationName = x.LocationName;
                    m.Longitude = x.Longitude;
                    m.Latitude = x.Latitude;
                    m.DateTime = x.DateTime;
                    m.UserIp = x.UserIp;
                    m.Kilometer = x.Kilometer;
                    m.UserId = x.UserId;
                    m.FromDate = fromDate;
                    m.ToDate = toDate;
                    m.Image = x.Image;
                    totkm += x.Kilometer;
                    m.BatteryPerc = x.BatteryPerc;
                    model.Add(m);
                });

                ViewBag.Location = model;
                ViewBag.TotalKm = totkm;
                LocationVModel n = new LocationVModel();
                n.FromDateS = fromDate.ToString("dd-MM-yyyy");
                n.ToDateS = toDate.ToString("dd-MM-yyyy");
                n.UserId = userId;
                return View(n);
            }
            else
            {
                return View();
            }
            
        }

    }
}
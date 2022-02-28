using BEEERP.Models.CustomAttribute;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEEERP.Models.CommonInformation;
using BEEERP.Models.Database;
using BEEERP.Models.ViewModel.Location;
using LocationApi.Model;

namespace BEEERP.Controllers.Location
{
    [ShowNotification]
    public class LocationTrackController : Controller
    {
        // GET: LocationTrack
        BEEContext context = new BEEContext();
        public ActionResult LocationTrack()
        {

            var depot = ScreenSessionData.GetEmployeeDepot();
            if(depot == null)
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(null);
            }
            else
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(depot);
            }
           
            return View();
        }

        public static SelectList LoadUserForLocation()
        {
            BEEContext context = new BEEContext();
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("", "--- Select User ---");
            items.Add("1", "1");
            items.Add("2", "2");
            items.Add("3", "3");
            items.Add("4", "4");
            items.Add("5", "5");
            //context.Customers.Where(x => x.TSOId == tsoId).ToList().ForEach(x => { items.Add(x.Id.ToString(), x.Id + " - " + x.CustomerName); });
            return new SelectList(items, "Key", "Value");
        }
        public ActionResult ImageView()
        {
            //ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(null);
            //return View();
            var depot = ScreenSessionData.GetEmployeeDepot();
            if (depot == null)
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(null);
            }
            else
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(depot);
            }

            return View();
        }
        public ActionResult GetAllMyImage(string userId,DateTime fromDate,DateTime toDate)
        {
            List<ImageViewVModel> myImage = new List<ImageViewVModel>();
            context.Locationtrack.Where(x => x.UserId == userId && x.DateTime >= fromDate && x.DateTime <= toDate).ToList().ForEach(x => {
                if(!string.IsNullOrEmpty(x.Image))
                {
                    ImageViewVModel model = new ImageViewVModel();
                    model.CaptureDate = x.DateTime;
                    model.IamgeId = x.Image;
                    myImage.Add(model);
                }
            });
            return View(myImage);
        }
        public ActionResult Map (int userId, DateTime fromDate, DateTime toDate)
        {
            List<AllUserMap> maps = new List<AllUserMap>();
            string sql = "select d.Latitude as lat,d.Longitude as lng,'' as img,c.CustomerName as label,t.IsVisit from TsoCustomerTravelCheckpoints t inner  join CustomerLocations d on t.CustomerID=d.CustomerID inner join customer c on c.id = t.CustomerID where t.TsoID = 3 and CONVERT(date, Date) between '" + fromDate.ToString("yyyy-MM-dd")+ "' and '" + toDate.ToString("yyyy-MM-dd") + "'";
             maps = context.Database.SqlQuery<AllUserMap>(sql).ToList();
            maps.ForEach(x =>
            {
                if(x.IsVisit==true)
                {
                    x.img = "blue_MarkerA.png";
                }
                else
                {
                    x.img = "darkgreen_MarkerE.png";
                }
            });
            return Json(maps, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DealerTravelMap()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            if (depot == null)
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(null);
            }
            else
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(depot);
            }
            return View();
        }
        public ActionResult MapTrack()
        {
            var depot = ScreenSessionData.GetEmployeeDepot();
            if (depot == null)
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(null);
            }
            else
            {
                ViewBag.Emp = LoadComboBox.LoadEmployeeByDepot(depot);
            }
            return View();
        }
        public ActionResult GetAllLocation(string userId,DateTime fromDate,DateTime toDate)
        {
            string sql= "select distinct ltrim(rtrim(LocationName)) as label,Longitude as lat,DateTime as PostDate,latitude as lng from LocationTrack l where UserId = '" + userId+"' and convert(date, l.DateTime) between '"+fromDate.ToString("MM - dd - yyyy")+"' and '"+toDate.ToString("MM - dd - yyyy")+ "' order by DateTime";
            var allLocation = context.Database.SqlQuery<AllUserLocation>(sql).ToList();
            List<AllUserLocation> location = new List<AllUserLocation>();
            int count = 0;
            foreach (var item in allLocation)
            {

               
                AllUserLocation aLocation = new AllUserLocation();
                aLocation.lat = item.lng;
                aLocation.lng = item.lat;
                if(item.label=="")
                {
                    aLocation.label = "Location name not available";
                }
                if(count==0)
                {
                    aLocation.img = "brown_MarkerS.png";
                }
                else if(count+1==allLocation.Count())
                {
                    aLocation.img = "red_MarkerE.png";
                }
                else
                {
                    aLocation.img = "blue_MarkerT.png";
                }
                aLocation.label = item.label;
                
                location.Add(aLocation);
                count++;
            }
            return Json(location, JsonRequestBehavior.AllowGet); 
        }
        public class AllUserLocation
        {
            public double lat { set; get; }
            public double lng { set; get; }
            public string label { set; get; }
            public string img { set; get; }
        }
        public class AllUserMap
        {
            public double lat { set; get; }
            public double lng { set; get; }
            public string label { set; get; }
            public string img { set; get; }
            public bool IsVisit { set; get; }
        }
    }
}
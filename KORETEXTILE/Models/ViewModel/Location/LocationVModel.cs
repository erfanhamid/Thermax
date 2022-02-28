using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Location
{
    public class LocationVModel
    {
        public int Id { set; get; }
        public string LocationName { set; get; }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public DateTime DateTime { set; get; }
        public string UserIp { set; get; }
        public double Kilometer { set; get; }
        public string UserId { set; get; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FromDateS { get; set; }
        public string ToDateS { get; set; }
        public string Image { set; get; }
        public double BatteryPerc { set; get; }
    }
}
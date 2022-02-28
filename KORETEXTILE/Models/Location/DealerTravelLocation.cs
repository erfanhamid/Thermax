using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationApi.Models
{
    public class DealerTravelLocation
    {
        public int Id { set; get; }
        public string LocationName { set; get; }
        public double Longitude{ set; get; }
        public double Latitude { set; get; }
        public string UserId { set; get; }
    }
}
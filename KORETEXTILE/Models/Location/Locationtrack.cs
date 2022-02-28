using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocationApi.Model
{
    [Table("LocationTrack")]
    public class LocationTrack
    {
        public int Id { set; get; }
        public string LocationName { set; get; }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public DateTime DateTime { set; get; }
        public string UserIp { set; get; }
        public double Kilometer { set; get; }
        public string UserId { set; get; }
        [NotMapped]
        public DateTime FromDate { get; set; }
        [NotMapped]
        public DateTime ToDate { get; set; }
        public string Image { set; get; }
        public double BatteryPerc { set; get; }
    }
}

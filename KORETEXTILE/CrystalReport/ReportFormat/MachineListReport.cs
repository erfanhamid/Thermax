using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class MachineListReport
    {
        public int MachineID { get; set; }
        public string MachineSerial { get; set; }
        public string ModelNo { get; set; }
        public string MSName { get; set; }
        public string BrandName { get; set; }
        public string CountryName { get; set; }
        public string UnitName { get; set; }
    }
}
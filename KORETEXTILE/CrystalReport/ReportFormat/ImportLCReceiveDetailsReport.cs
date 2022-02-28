using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportLCReceiveDetailsReport
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int PacSize { get; set; }
        public int UoMID { get; set; }
        public string UOMName { get; set; }
        public int LCId { get; set; }
        public int LCQuantity { get; set; }
        public int ReceiveQuantity { get; set; }
        public int Balance { get; set; }
        public string ILCNO { get; set; }
    }
}
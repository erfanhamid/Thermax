using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DateWiseSPIssueReport
    {
        public string CompName { get; set; }
        public string MachineSerial { get; set; }
        public DateTime SRDate { get; set; }
        public int SRNo { get; set; }
        public string SPTypeName { get; set; }
        public DateTime ISPDate { get; set; }
        public int ISPNo { get; set; }
        public string ParentName { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }

    }
}
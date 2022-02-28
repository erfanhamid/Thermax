using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ItemListReport
    {
        public int Id { set; get; }
        public string ItemName { set; get; }
        public string GroupName { set; get; }
        public string ItemCode { set; get; }
        public string ShortDesc { set; get; }
        public string PacSize { set; get; }
        public double RetailPrice { set; get; }
        public double DisPerc { set; get; }
        public double VatPerc { set; get; }
        public int CartonCapacity { set; get; }
    }
}
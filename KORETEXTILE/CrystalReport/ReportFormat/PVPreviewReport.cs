using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PVPreviewReport
    {
        public string ReceiverName { set; get; }
        public string CostCentre { set; get; }
        public string RefNo { set; get; }
        public int PVNo { set; get; }
        public DateTime Date { set; get; }
        public int Code { set; get; }
        public string AccountHead { set; get; }
        public string CostGroup { set; get; }
        public string Description { set; get; }       
        public decimal DrAmount { set; get; }
        public decimal CrAmount { set; get; }      
    }
}
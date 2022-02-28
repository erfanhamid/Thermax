using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class FundTransferVoucherReport
    {
        public int FTVNo { set; get; }
        public DateTime FTVDate { set; get; }
        public string TransferFrom { set; get; }
        public string TransferTo { set; get; }
        public string RefNo { set; get; }
        public string Descriptions { set; get; }
        public decimal Amount { set; get; }
    }
}
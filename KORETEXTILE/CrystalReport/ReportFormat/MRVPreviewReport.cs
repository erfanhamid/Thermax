using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class MRVPreviewReport
    {
        public int SLNO { get; set; }
        public int EmpID { get; set; }
        public string Name { get; set; }
        public string Desig { get; set; }
        public string FuncDesignation { get; set; }
        public DateTime dstr { get; set; }
        public string RefNo { get; set; }
        public string AccHed { get; set; }
        public decimal Amount { set; get; }
        public string Description { get; set; }
    }
}
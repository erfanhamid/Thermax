using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class MRRPreviewReport
    {
        public int clmRMRNo { get; set; }
        public int clmEmpID { get; set; }
        public string Name { get; set; }
        public string Desig { get; set; }
        public string FuncDesignation { get; set; }
        public DateTime dstr { get; set; }
        public string AccHed { get; set; }
        public decimal clmAmount { get; set; }
        public string clmDescription { get; set; }
        public string clmRefNo { get; set; }

    }
}
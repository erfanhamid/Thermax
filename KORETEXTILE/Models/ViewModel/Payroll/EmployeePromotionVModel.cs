using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeePromotionVModel
    {
        public int PromotionID { get; set; }
        public int EmployeeID { get; set; }
        public string OGrade { get; set; }
        public string ODesignation { get; set; }
        public string OFuncDesignation { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string PRefNO { get; set; }
        public string PDescription { get; set; }
        public string NGrade { get; set; }
        public string NDesignation { get; set; }
        public string NFuncDesignation { get; set; }

    }
}
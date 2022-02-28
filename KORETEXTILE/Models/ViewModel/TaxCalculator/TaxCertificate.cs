using BEEERP.Models.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.TaxCalculator
{
    public class TaxCertificate
    {
        public string DescriptionOne { set; get; }
        public string Name { set; get; }
        public string FinanacialYear { get; set; }
        public decimal BasicSalary { set; get; }
        public decimal HouseRent { set; get; }
        public decimal ExemptedHouseRent { set; get; }
        public decimal ConveyanceAllowance { set; get; }
        public decimal ExemptedCA { set; get; }
        public decimal MedicalAllowance { set; get; }
        public decimal ExemptedMA { set; get; }
        public decimal Bonus { set; get; }
        public decimal SpecialAllowance { set; get; }
        public decimal PFContribution { set; get; }
        public string DescriptionTwo { get; set; }
        public decimal AmountOne { set; get; }
        public decimal AmouuntThirteen { set; get; }
        public string TaxType { get; set; }

        public List<ChallanInfo> Challans { get; set; }
    }

    public class ChallanInfo
    {
        public string DescriptionOrBank { get; set; }
        public string ChalanNo { set; get; }
        public string Date { set; get; }
        public decimal Amount { set; get; }

    }
}
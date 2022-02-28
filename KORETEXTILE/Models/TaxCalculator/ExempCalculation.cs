using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.TaxCalculator
{
    public class ExempCalculation
    {
        public string BreakeDown { get; set; }
        public decimal Deduction { get; set; }
        public decimal NetOfDeduction { get; set; }        
        public decimal Amount { get; set; }
        public decimal Exempted { get; set; }
        public decimal TaxableAmount { get; set; }
    }
}
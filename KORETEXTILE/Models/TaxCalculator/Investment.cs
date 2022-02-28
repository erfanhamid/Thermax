using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.TaxCalculator
{
    public class Investment
    {
        public decimal InvestmentAmount { get; set; }
        public decimal MaxLimit { get; set; }
        public float RebatePercent { get; set; }
        public decimal RebateAmount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.TaxCalculator
{
    public class Slab
    {
        public string SlabName { get; set; }
        public string Limit { get; set; }
        public decimal Amount { get; set; }
        public string Percentage { get; set; }
        public decimal TaxableAmount { get; set; }
    }
}
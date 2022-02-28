
using BEEERP.Models.ViewModel.TaxCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.TaxCalculator
{
    public class TaxRepotVModel
    {
        public List<TaxReportEmployee> TaxReportEmployee { set;get;}
    }
    public class TaxReportEmployee
    {
        public TaxDetails TaxDetails { set; get; }
        public TaxCertificate TaxCertificates { set; get; }
    }
}
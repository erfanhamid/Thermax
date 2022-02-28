using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("ERM_TRANSECTION")]
    public class ERMTransection
    {
        public DateTime xpaydate { get; set; }
        public int xemp { get; set; }
        public int xprorg { get; set; } 
        public string xdept { get; set; }  
        public string xprloc { get; set; }
        public string xsites { get; set; }  
        public string xdesig { get; set; }
        public DateTime xconfirmationDate { get; set; }
        public decimal xbasic { get; set; } 
        public int xpaycode { get; set; }   
        public string xdesc { get; set; }
        public decimal xamount { get; set; } 
    }
}
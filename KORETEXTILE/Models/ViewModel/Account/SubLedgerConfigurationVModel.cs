using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Account
{
    public class SubLedgerConfigurationVModel
    {
        [Display(Name =" Sub Ledger")]
        public int SubLedgerID { get; set; }
        [Display(Name ="General Ledger")]
        public int GLID { get; set; }
        [Display(Name = "General Ledger")]
        public int GLSearch { get; set; }
        public string Type { get; set; }
    }
}
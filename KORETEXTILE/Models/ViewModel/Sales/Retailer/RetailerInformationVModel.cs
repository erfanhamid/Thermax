using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Retailer
{
    public class RetailerInformationVModel
    {
        public int RetailerId { get; set; }
        public string RetailerName { get; set; }
        public int CustomerID { set; get; }
        [Display(Name = "Customer Name")]
        public string CustName { set; get; }
        public int DepotId { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        public string IsDealer { get; set; }
        public string Type { get; set; }
        public int OB { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BillTo { get; set; }
        public string ShipTo { get; set; }


    }
}
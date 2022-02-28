using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.StoreRM
{
    public class CreateCustomerVModel
    {
        public int CustomerNo { get; set; }
        public string Name { get; set; }
        public int COB { get; set; }
        public DateTime Date { get; set; }
        public string ConPerson { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
    }
}
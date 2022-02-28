using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.RMSales
{
    [Table("tblRMCustomer")]
    public class RMCustomer
    {
        [Key]
        [Required]
        [Column("clmCustomerID")]
        public int CustomerID { get; set; }
        [Column("clmCustomerName")]
        public string  CustomerName { get; set; }
        [Column("clmContactPerson")]
        public string ContactPerson { get; set; }
        [Column("clmMobileNo")]
        public string MobileNo { get; set; }
        [Column("clmPhoneNo")]
        public string PhoneNo { get; set; }
        [Column("clmEmailNo")]
        public string EmailId { get; set; }
        [Column("clmBillingAddress")]
        public string BillingAddress { get; set; }
        [Column("clmAccOB")]
        public decimal AccOB { get; set; }
        [Column("clmAccOBDate")]
        public DateTime OBDate { get; set; }

    }
}
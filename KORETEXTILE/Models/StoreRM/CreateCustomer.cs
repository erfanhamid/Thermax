using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreRM
{
    [Table("tblRMCustomer")]
    public class CreateCustomer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmCustomerID { get; set; }
        public string clmCustomerName { get; set; }
        public string clmContactPerson { get; set; }
        public string clmMobileNo { get; set; }
        public string clmPhoneNo { get; set; }
        public string clmEmailNo { get; set; }
        public string clmBillingAddress { get; set; }
        public decimal clmAccOB { get; set; }
        public DateTime clmAccOBDate { get; set; }
    }
}
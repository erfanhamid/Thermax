using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.PaymentReceiveInfo
{
    [Table("ReceivePaymentInfo")]
    public class ReceivePaymentInfo 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RPID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public DateTime TDate { get; set; }
        public int AccountID { get; set; }
        public decimal ReceiveAmt { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public decimal BankCharges { get; set; }
        
        public decimal NetAmount { get; set; }
        [NotMapped]
        public string PayMode { set; get; }
        [NotMapped]
        public int BranchId { set; get; }
        [Required]
        public int DepotID { get; set; }
        
    }
}
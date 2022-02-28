using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.PaymentReceiveInfo
{
    [Table("AdvanceAmountInfo")]
    public class AdvanceAmountInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int AANo { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int Depot { get; set; }
    }
}
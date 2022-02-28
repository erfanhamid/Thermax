using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("DealerSalesIncentiveAdjustment")]
    public class DealerSalesIncentiveAdjustment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int DSIANo { get; set; }
        [Required]
        public DateTime DSIADate { get; set; }
       // [Required]
        //public int AccountID { get; set; }
        [Required]
        public int DepotID { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
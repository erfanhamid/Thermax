using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SalesModule
{
    [Table("CustomerBalanceAdjustment")]
    public class CustomerBalanceAdjustment
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CAANo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public decimal TotalAAmount { get; set; }
    }
}
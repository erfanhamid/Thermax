using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("EmployeeBalanceCalculationTAX")]
    public class EmployeeBalanceCalculationTAX
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }
        [Required]
        [Column("TDate")]
        public DateTime Date { get; set; }
        [Required]
        public string DocumentType { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public string AccountID { get; set; }
        [Column("TRDescription")]
        public string Description { get; set; }
        public string RefNo { get; set; }
        public decimal Amount { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int DocNO { get; set; }
        [Required]
        [Column("clmTBID")]
        public int TBID { get; set; }
        [Required]
        [Column("clmFundType")]
        public int FundType { get; set; }
    }
}
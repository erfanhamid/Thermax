using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("TAXBalanceCalculation")]
    public class TAXBalanceCalculation
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int DocNO { get; set; }
        [Required]
        [Column("TDate")]
        public DateTime Date { get; set; }
        [Required]
        public string DocumentType { get; set; }
        public decimal Amount { get; set; }
        [Column("clmTBID")]
        public int TBID { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
    }
}
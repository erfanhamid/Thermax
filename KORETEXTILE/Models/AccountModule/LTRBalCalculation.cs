using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("LTRBalCalculation")]
    public class LTRBalCalculation
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LTRID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int PaymentAccountID { get; set; }
        public decimal Amount { get; set; }
        public string Descriptions { get; set; }
        public string Refno { get; set; }
        [Required]
        public string DocType { get; set; }
        [Required]
        public int DocNo { get; set; }
    }
}
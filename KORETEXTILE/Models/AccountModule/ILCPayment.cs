using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("ILCPayment")]
    public class ILCPayment
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmILCPNO")]
        public int ILCPNo { get; set; }
        [Required]
        [Column("clmILCID")]
        public int ILCID { get; set; }  
        [Required]
        [Column("clmDate")]
        public DateTime Date { get; set; }
        [Required]
        [Column("clmAccountID")]
        public int AccountID { get; set; }
        [Required]
        [Column("clmTotalAmount")]
        public decimal TotalAmount { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }

    }
}
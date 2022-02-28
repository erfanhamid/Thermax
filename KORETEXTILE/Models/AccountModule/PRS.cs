using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("RefundFromSupplier")]
    public class PRS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("PRID")]
        public int PRSNo { set; get; }
        [Required]
        [Column("Tdate")]
        public DateTime Date { set; get; }
        [Required]
        public int SupplierID { set; get; }
        [Required]
        public int ReceiveAccountID { set; get; }
        [Required]
        public decimal ReturnAmount { set; get; }
        public string RefNo { set; get; }
        public string Description { set; get; }
    }
}
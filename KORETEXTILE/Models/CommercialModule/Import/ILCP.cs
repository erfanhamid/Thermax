using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblILCPayment")]
    public class ILCP
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmILCPNO { get; set; }
        [Required]
        public int clmILCID { get; set; }
        [Required]
        public DateTime clmDate { get; set; }
        [Required]
        public int clmAccountID { get; set; }
        [Required]
        public decimal clmTotalAmount { get; set; }
    }
}
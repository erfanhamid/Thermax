using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblILCPaymentLineItem")]
    public class ILCPLine
    {
            [Required]
            [Key, Column(Order = 0)]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int clmILCPNo { get; set; }
            [Required]
            [Key, Column(Order = 1)]
            public int DebitAccID { get; set; }
            [Required]
            public decimal Amount { get; set; }
            public string Description { get; set; }
        [NotMapped]
        public string DebitAccName { get; set; }
    }
}
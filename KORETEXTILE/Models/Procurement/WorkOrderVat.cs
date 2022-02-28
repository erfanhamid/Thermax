using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("WorkOrderVat")]
    public class WorkOrderVat
    {
        [Required]
        [Key, Column(Order = 0)]
        public int WONo { get; set; }
        [Required]
        public float VatPerc { get; set; }
        [Required]
        public decimal VatAmount { get; set; }
        [Required]
        public decimal NetOfVat { get; set; }
    }
}
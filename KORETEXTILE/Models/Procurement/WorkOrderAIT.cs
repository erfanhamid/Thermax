using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("WorkOrderAIT")]
    public class WorkOrderAIT
    {
        [Required]
        [Key, Column(Order = 0)]
        public int WONo { get; set; }
        [Required]
        public float AitPerc { get; set; }
        [Required]
        public decimal AitAmount { get; set; }
        [Required]
        public decimal NetOfAit { get; set; }
    }
}
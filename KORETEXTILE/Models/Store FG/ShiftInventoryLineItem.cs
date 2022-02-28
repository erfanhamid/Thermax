using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Store_FG
{
    [Table("ShiftInventoryLineItem")]
    public class ShiftInventoryLineItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SINo { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public decimal Qty { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}
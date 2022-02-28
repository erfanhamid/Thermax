using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("SPAdvanceAdjustment")]
    public class SPAdvanceAdjustment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int SPAANo { get; set; }
        [Required]
        public int SupplierID { get; set; }
        [Required]
        public int SPVNo { get; set; }
        [Required]
        public DateTime TDate { get; set; }
        [Required]
        public decimal AdjustedAmount { get; set; }
        public string TDescription { get; set; }
        public string RefNo { get; set; }
        [NotMapped]
        public bool update { get; set; }
    }
}
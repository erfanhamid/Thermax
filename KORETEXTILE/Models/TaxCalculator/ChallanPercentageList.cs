using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblChallanPercentageList")]
    public class ChallanPercentageList
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [Column("clmPercentage")]
        public float Percentage { get; set; }
    }
}
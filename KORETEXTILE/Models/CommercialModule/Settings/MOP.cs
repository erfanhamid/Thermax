using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule
{
    [Table("tblMoP")]
    public class MOP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("clmMoPID")]
        public int MoPId { get; set; }
        [Required]
        [StringLength(50)]
        [Column("clmMoPName")]
        public string MoPName { get; set; }
        [Column("clmDaysCount")]
        public int DaysCount { get; set; }
    }
}
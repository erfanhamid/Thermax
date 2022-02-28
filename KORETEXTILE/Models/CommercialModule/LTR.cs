using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommercialModule
{
    [Table("tblLTR")]
    public class LTR
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmLTRID")]
        public int LTRID { get; set; }
        [Required]
        [Column("clmLCID")]
        public int LCID { get; set; }
        [Required]
        [Column("clmLTRNO")]
        public string LTRNO { get; set; }
        [Column("")]
        public string RefNo { get; set; }
        [Column("clmDescription")]
        public string Description { get; set; }
        [Required]
        [Column("clmACCID")]
        public int ACCID { get; set; }
        [Required]
        [Column("clmAmount")]
        public decimal Amount { get; set; }
        [Required]
        [Column("clmDate")]
        public DateTime Date { get; set; }
        [Required]
        public string DocType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("tblLTRA")]
    public class LTRA
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmLTRAID")]
        public int LTRANo { get; set; } 
        [Required]
        [Column("clmDate")]
        public DateTime Date { get; set; }
        [Required]
        [Column("clmLTRID")]
        public int LTRID { get; set; }
        [Required]
        [Column("clmType")]
        public string Type { get; set; }
        [Required]
        [Column("clmAccountID")]
        public int AccountID { get; set; }
        [Required]
        [Column("clmAmount")]
        public decimal Amount { get; set; }
        [Column("clmDescription")]
        public string Description { get; set; }
        [Required]
        public int ILCId { get; set; }
        public string RefNo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.StoreRM.IRM
{
    [Table("tblIRM")]
    public class IRM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("clmIRMID")]
        public int IRMID { get; set; }
        [Required]
        [Column("clmIRMDate")]
        public DateTime IRMDate { get; set; }
        [Required]
        [Column("clmIRMTotal")]
        public decimal IRMTotal { get; set; }
        [Column("clmTotalCost")]
        public decimal TotalCost { get; set; }
        [Column("clmDescription")]
        public string Description { get; set; }
        [Column("clmRefno")]
        public string Refno { get; set; }
        [Column("clmGRQNO")]
        public int GRQNO { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }

        [NotMapped]
        public int IssueFrom { get; set; }
        [NotMapped]
        public int IssueTo { get; set; }
    }
}
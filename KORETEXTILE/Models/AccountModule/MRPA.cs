using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("tblMRPA")]
    public class MRPA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("clmMRPANo")]
        public int MRPANo { get; set; }
        [Required]
        [Column("clmMRPADate")]
        public DateTime MRPADate { get; set; }
        [Required]
        [Column("clmEmployeeID")]
        public int EmployeeID { get; set; }
        [Required]
        [Column("clmSupplierID")]
        public int SupplierID { get; set; }
        [Required]
        [Column("clmAdjustmentAmnt")]
        public decimal AdjustmentAmnt { get; set; }
        [Column("clmRefNo")]
        public string RefNo { get; set; }
        [Column("clmDescriptions")]
        public string Descriptions { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
        [NotMapped]
        public decimal PreviousBal { get; set; }
    }
}
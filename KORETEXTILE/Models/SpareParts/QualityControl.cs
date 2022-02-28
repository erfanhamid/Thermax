using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.SpareParts
{
    [Table("QualityControl")]
    public class QualityControl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int QcNo { get; set; }
        [Required]
        public DateTime QcDate { get; set; }
        [Required]
        public int LCRNNo { get; set; }
        [Required]
        public int CompanyID { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public int GroupID { get; set; }
        [NotMapped]
        public int SupplierID { get; set; }
    }
}
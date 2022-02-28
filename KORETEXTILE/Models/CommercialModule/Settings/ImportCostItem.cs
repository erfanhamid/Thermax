using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.CommercialModule.Settings
{
    [Table("tblImportCostItems")]
    public class ImportCostItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("slno")]
        public int SlnNo { get; set; }
        [Required]
        [Column("costitem")]
        public string CostItem { get; set; }
        [Required]
        [Column("DebitAccID")]
        public int CostGroupId { get; set; }
        [NotMapped]
        public string AccountHeadName { get; set; }
    }
}
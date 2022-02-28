using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("InventoryTransfer")]
    public class InventoryTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ITNo { get; set; }
        [Required]
        public DateTime ITDate { get; set; }
        [Required]
        public int OldCompanyID { get; set; }
        [Required]
        public int TypeID { get; set; }
        [Required]
        public int NewCompanyID { get; set; }
        public string RefNo { get; set; }
        public string ITDescription { get; set; }
    }
}
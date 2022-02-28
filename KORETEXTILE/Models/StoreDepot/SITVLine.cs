using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreDepot
{
    [Table("tblSITVLine")]
    public class SITVLine
    {
        [Key]
        public int ID { get; set; }
        [Column("clmSIFDNo")]
        [Required]
        public int SITVNo { get; set; }
        [Column("clmGroupID")]
        [Required]
        public int GroupID { get; set; }
        //[Column("clmItemID")]
        [Required]
        [Column("clmItemID")]
        public int ItemID { get; set; }
        [Required]
        [Column("clmShiftQty")]
        public int ShiftQty { get; set; }
        [Column("clmCostRt")]
        public decimal CostRt { get; set; }
        [Column("clmCostVal")]
        public decimal CostVal { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
    }
}
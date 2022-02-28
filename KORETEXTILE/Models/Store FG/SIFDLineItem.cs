using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Store_FG
{
    [Table("tblSIFDLineItem")]
    public class SIFDLineItem
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("clmSIFDNo")]
        public int SIFDNo { get; set; }
        [Column("clmItemGrpID")]
        public int ItemGrpID { get; set; }
        [Required]
        [Column("clmItemID")]
        public int ItemID { get; set; }
        [Required]
        [Column("clmShiftWty")]
        public int ShiftQty { get; set; }  
        [Column("clmCostRt")]
        public decimal CostRt { get; set; }
        //public decimal UnitRetailPrice { get; set; }
        [Column("clmCostVal")]
        public decimal CostVal { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public int CtnQty { get; set; }
        [NotMapped]
        public decimal ReceivedQty { get; set; }
        [NotMapped]
        public string PackSize { get; set; }
        [NotMapped]
        public int CartonCapacity { get; set; }
    }
}
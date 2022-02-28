using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.StoreDepot
{
    [Table("tblGRF")]
    public class GRF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("clmGRFNo")]
        public int GRFNo { get; set; }
        [Required]
        [Column("clmDepotID")]
        public int DepotID { get; set; }
        [Required]
        [Column("clmReceiveDate")]
        public DateTime ReceiveDate { get; set; }
        [Required]
        [Column("clmReceiveStoreID")]
        public int ReceiveStoreID { get; set; }
        [Required]
        [Column("clmChallanNo")]
        public int ChallanNo { get; set; }
        [Required]
        [Column("clmChallanDate")]
        public DateTime ChallanDate { get; set; }
        [Required]
        [Column("clmOldStoreID")]
        public int OldStoreID { get; set; }
        [Required]
        [Column("clmVehicleID")]
        public int VehicleID { get; set; }
        [Column("clmDescriptions")]
        public string Descriptions { get; set; }
        [Required]
        [Column("clmTotalQty")]
        public int TotalQty { get; set; }
        [Column("clmCostTotal")]
        public decimal TotalCost { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}
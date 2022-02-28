using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Store_FG
{
    [Table("tblSIFD")]
    public class SIFD
    {
        [Key, Column("clmSIFDNo",Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SIFDNo { get; set; }
        [Column("clmDepot")]
        public int Depot { get; set; }
        [Column("clmCurrentStoreID")]
        public int CurrentStoreID { get; set; }
        [Column("clmNewStoreID")]
        public int NewStoreID { get; set; }
        [Column("clmVehicleID")]
        public int VehicleID { get; set; }
        [Column("clmDriverID")]
        public int DriverID { get; set; }
        [Column("clmSDate")]
        public DateTime Date { get; set; } 
        [Key,Column("clmGRQNo",Order =1)]
        public int GRQNo { get; set; }  
        [Required]
        [Key, Column("clmRef", Order = 2)]
        public int ChallanNo { get; set; }  
        [Column("clmDescription")]
        public string Description { get; set; }
        [Column("clmTtlQTY")]
        public int TtlQTY { get; set; }
        [Column("clmTotalCost")]
        public decimal TotalCost { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreDepot
{
    [Table("tblSITV")]
    public class SITV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("clmSITVNo")]
        public int SITVNo { get; set; }
        [Column("clmDepotID")]
        [Required]
        public int DepotID { get; set; }
        [Required]
        [Column("clmStoreID")]
        public int StoreID { get; set; }
        [Required]
        [Column("clmVehicleID")]
        public int VehicleID { get; set; }
        [Required]
        [Column("clmDriverID")]
        public int DriverID { get; set; }
        [Required]
        [Column("clmSDate")]
        public DateTime SDate { get; set; }
        [Required]
        [Column("clmTtlQTY")]
        public int TtlQTY { get; set; }
        [Column("clmCostTotal")]
        public  decimal CostTotal { get; set; }
    }
}
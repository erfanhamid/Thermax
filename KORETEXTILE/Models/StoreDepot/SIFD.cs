using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreDepot
{
    [Table("tblSIFD")]
    public class SIFD
    {
        [Column("clmSIFDNo")]
        [Required]
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
        public DateTime SDate { get; set; }
    }
}
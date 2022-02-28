using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("MachineDetails")]
    public class MachineDetails
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MachineID { get; set; }
        [Required]
        public string MachineSerial { get; set; }
        [Required]
        public int MFGYear { get; set; }
        [Required]
        public int SoftwareID { get; set; }
        [Required]
        public int MachineTypeID { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public int CountryID { get; set; }
        [Required]
        public int BrandID { get; set; }
        public string ModelNo { get; set; }
        [Required]
        public int MSection { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("SparePartsDamage")]
    public class SparePartsDamage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int SPDNo { get; set; }
        [Required]
        public DateTime SPDDate { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public int MachineID { get; set; }
        [Required]
        public int TypeId { get; set; }
        public string Description { get; set; }
    }
}
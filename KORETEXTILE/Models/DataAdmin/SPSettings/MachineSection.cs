using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("MachineSection")]
    public class MachineSection
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MSID { get; set; }
        public string MSName { get; set; }
    }
}
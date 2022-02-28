using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("MachineType")]
    public class MachineType
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MTID { get; set; }
        [Required]
        public string MtName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule
{
    [Table("tblPort")]
    public class Port
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmPortID")]
        public int PortID { get; set; }
        [Required]
        [StringLength(50)]
        [Column("clmPortName")]
        [Display(Name = "Port Name")]
        public string PortName { get; set; }
    }
}
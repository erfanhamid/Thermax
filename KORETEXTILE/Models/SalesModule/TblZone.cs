using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("tblZone")]
    public class TblZone
    {
        [Key]
        [DisplayName("Zone ID")]
        [Required]
        public int ZoneId { set; get; }
        [DisplayName("Zone Name")]
        [Required]
        public string ZoneName { set; get; }
    }
}
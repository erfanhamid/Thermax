using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule
{
    [Table("tblIncoterms")]
    public class Incoterm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmIncotermsID")]
        public int IncotermsId { get; set; }
        [Required]
        [Column("clmIncoterms")]
        [StringLength(500)]
        public string IncotermName { get; set; }
    }
}
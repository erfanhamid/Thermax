using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.Procurement
{
    [Table("SupplierGroup")]
    public class SupplierGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Group No ")]
        public int SgroupId { get; set; }
        [Display(Name ="Group Name")]
        public string SgroupName { get; set; }
        [Display(Name ="Group Code")]
        public int SgroupCode { get; set; }
    }
}
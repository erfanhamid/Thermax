using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("SparePartsItemList")]
    public class SparePartsItemList
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SPItemID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public string SPItemPartNo { get; set; }
        [Required]
        public int SPTypeID { get; set; }
        [Required]
        public int UoMID { get; set; }
        [NotMapped]
        public int DepartmentID { get; set; }
    }
}
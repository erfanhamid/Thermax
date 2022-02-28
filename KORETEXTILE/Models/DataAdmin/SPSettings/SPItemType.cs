using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("SPItemType")]
    public class SPItemType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemTypeID { get; set; }
        [Required]
        public string ItemType { get; set; }
        [Required]
        public int DepartmentID { get; set; }
    }
}
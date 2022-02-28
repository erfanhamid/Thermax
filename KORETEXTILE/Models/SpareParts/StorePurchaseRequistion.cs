using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.SpareParts
{
    [Table("StorePurchaseRequistion")]
    public class StorePurchaseRequistion
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SPRNo { get; set; }
        [Required]
        public DateTime SPRDate { get; set; }
        public string SPDescriptions { get; set; }
        public string SPReference { get; set; }
        [Required]
        public int SPDepartmentID { get; set; }
        [Required]
        public int StoreID { get; set; }

    }
}
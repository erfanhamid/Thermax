using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BEEERP.Models.SalesModule
{
    [Table("ChartOfInv")]
    public class ChartOfInventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public int parentId { set; get; }
        [Required]
        public string type { set; get; }
        [Required]
        public int UoMID { set; get; }
        [Required]
        public string rootAccountType { set; get; }
        public string ItemCode { get; set; }
        [NotMapped]
        public string NameForShow { get; set; }
        [NotMapped]
        public string TypeForShow { get; set; }






    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.Procurement
{
    [Table("tblFreezerModelDescription")]
    public class FreezerModelDescription
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int TypeID { get; set; }
        public int BrandID { get; set; }
        public int ModelID { get; set; }
        public int CapacityID { get; set; }
        public int UnitID { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string TypeName { get; set; }
        [NotMapped]
        public string BrandName { get; set; }
        [NotMapped]
        public string ModelName { get; set; }
        [NotMapped]
        public string Capacity { get; set; }
        [NotMapped]
        public string Unit { get; set; }
    }
}
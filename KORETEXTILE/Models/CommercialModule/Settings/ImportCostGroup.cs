using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.CommercialModule.Import
{
    [Table("ImportCostGroup")]
    public class ImportCostGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CGId { get; set; }
        public string CostGroupName { get; set; }
        public int Account { get; set; }
    }
}
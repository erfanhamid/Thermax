using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule.Import
{
    [Table("ImportItemList")]
    public class ImportItemList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UoMID { get; set; }
        public string Type { get; set; }
    }
}
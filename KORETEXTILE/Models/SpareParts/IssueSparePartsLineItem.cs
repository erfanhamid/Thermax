using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.SpareParts
{
    [Table("IssueSparePartsLineItem")]
    public class IssueSparePartsLineItem
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ISPNo { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int SPItemID { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        public int BoxID { get; set; }
        [Required]
        public decimal ItemQty { get; set; }
        [Required]
        public decimal ItemRate { get; set; }
        [Required]
        public decimal ItemValue { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public int BalaceQty { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string ItemTypeName { get; set; }
        [NotMapped]
        public string StoreName { get; set; }
        [NotMapped]
        public string BoxName { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.GeneralStore
{
    [Table("GSInventoryIssueLineItem")]
    public class GSInventoryIssueLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GSIIssueNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int ItemID { get; set; }
        [Required]
        public int GroupID { get; set; }
        [Required]
        public decimal ItemQty { get; set; }
        [Required]
        public decimal ItemRate { get; set; }
        [Required]
        public decimal ItemValue { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
    }
}